using ASRS.Database;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using static alglib;

namespace ASRS
{
    public partial class CalculationResults : Window
    {
        public ObservableCollection<MatrixRow> Rows { get; } = new ObservableCollection<MatrixRow>();
        //private readonly List<BaseForm> _baseForms;
        //private readonly List<FormingForm> _formingForms;
        //private readonly List<List<int>> _componentMatrix;
        private List<List<double>> initial_b;
        private List<List<double>> XStart;
        private List<double> inputK;
        private int pointsCount;
        private List<List<double>> expPointsValues;

        public CalculationResults(
            List<List<double>> _b,
            List<List<double>> _XStart,
            List<double> _inputK,
            int _pointsCount,
            List<List<double>> _expPointsValues)
        {
            InitializeComponent();

            //_baseForms = baseForms ?? throw new System.ArgumentNullException(nameof(baseForms));
            //_formingForms = formingForms ?? throw new System.ArgumentNullException(nameof(formingForms));
            //_componentMatrix = componentMatrix ?? throw new System.ArgumentNullException(nameof(componentMatrix));
            initial_b = _b ?? throw new System.ArgumentNullException(nameof(_b));
            XStart = _XStart ?? throw new System.ArgumentNullException(nameof(_XStart));
            inputK = _inputK ?? throw new System.ArgumentNullException(nameof(_inputK));
            pointsCount = _pointsCount;
            expPointsValues = _expPointsValues;
            DataContext = this;
            List<double> xInitial = new List<double>();
            List<double> b = new List<double>();
            List<List<double>> Solutions = new List<List<double>>();
            //List<List<double>> optimalSolutions = new List<List<double>>();
            for (int i = 0; i < pointsCount; i++)
            {
                xInitial = XStart[i];
                double[] x_Initial = new double[5];
                double[] K = new double[15];
                double[] initial_b = new double[5];
                b = xInitial;
                for (int j = 0; j < x_Initial.Length; j++)
                {
                    x_Initial[j] = xInitial[j];
                    initial_b[j] = b[j];
                }
                for (int j = 0; j < inputK.Count; j++)
                {
                    K[j] = inputK[j];
                }
                //    //var solver = new PythonSolver();
                //    //var solution = solver.Solve(xInitial, inputK, b);
                //    //Solutions.Add(solution);
                //    //List<double> optimalInputK = new List<double>(inputK);
                //    //optimalInputK[12] = 28.973;
                //    //optimalInputK[13] = 0;
                //    //var newsolver = new PythonSolver();
                //    //var optimalsolution = solver.Solve(xInitial, optimalInputK, b);
                //    //optimalSolutions.Add(optimalsolution);
                // Настройка оптимизации
                minlmstate state;
                minlmreport report;
                alglib.minlmcreatev(
                            5,             // Количество переменных
                            5,             // Количество функций остатков
                            x_Initial,
                            1e-14,
                            out state
                        );

                // 2. Исправление для minlmsetcond:
                alglib.minlmsetcond(
                    state,
                    1e-14,          // Точность остатков (ftol)         // Точность параметров (xtol)
                    0              // Максимальное число итераций (0 = по умолчанию)
                );

                var data = Tuple.Create(K, initial_b);
                alglib.minlmoptimize(state, Residuals, null, data);

                double[] optimizedParams;
                alglib.minlmresults(state, out optimizedParams, out report);

                tb_Results.Text += "Оптимальные параметры: \n";
                tb_Results.Text += $" {string.Join(", ", optimizedParams.Select(v => v.ToString("F6")))}\n";

                double[] residuals = new double[5];
                Residuals(optimizedParams, residuals, data);
                //tb_Results.Text += "\nОстатки:";
                //tb_Results.Text += $"{string.Join(", ", residuals.Select(r => r.ToString("F6")))}\n\n";

                var solver = new PythonSolver();
                var solution = solver.Solve(xInitial, inputK, b);

                Solutions.Add(solution);

            }

        }

        public static void Residuals(double[] x, double[] fi, object obj)
        {
            var data = (Tuple<double[], double[]>)obj;
            double[] K = data.Item1;
            double[] b = data.Item2;

            double x1 = x[0], x2 = x[1], x3 = x[2], x4 = x[3], x5 = x[4];

            //Уравнение 1
            fi[0] = b[0] - (x1
                + K[7] * x1 * x2
                + K[12] * x1 * x2 * x2 * x2 * x4 * x4 * x4
                + K[13] * x1 * x2 * x2 * x2 * x4 * x4 * x4 * x4);

            // Уравнение 2
            fi[1] = b[1] - (x2
                + K[5] * x2 * x3
                + K[6] * x2 * x5
                + K[7] * x1 * x2
                + K[8] * x2 * x4 * x4 * x5
                + 3 * K[9] * x2 * x2 * x2 * x3 * x3 * x3 * x4
                + K[10] * x2 * x3 * x4
                + 2 * K[11] * x2 * x2 * x3 * x3 * x4
                + 3 * K[12] * x1 * x2 * x2 * x2 * x4 * x4 * x4
                + 3 * K[13] * x1 * x2 * x2 * x2 * x4 * x4 * x4 * x4);

            // Уравнение 3
            fi[2] = b[2] - (x3
                + K[5] * x2 * x3
                + 3 * K[9] * x2 * x2 * x2 * x3 * x3 * x3 * x4
                + K[10] * x2 * x3 * x4
                + 2 * K[11] * x2 * x2 * x3 * x3 * x4);

            // Уравнение 4
            fi[3] = b[3] - (x4
                + 2 * K[8] * x2 * x4 * x4 * x5
                + K[9] * x2 * x2 * x2 * x3 * x3 * x3 * x4
                + K[10] * x2 * x3 * x4
                + K[11] * x2 * x2 * x3 * x3 * x4
                + 3 * K[12] * x1 * x2 * x2 * x2 * x4 * x4 * x4
                + 4 * K[13] * x1 * x2 * x2 * x2 * x4 * x4 * x4 * x4
                + 2 * K[14] * x4 * x4);

            // Уравнение 5
            fi[4] = b[4] - (x5
                + K[6] * x2 * x5
                + K[8] * x2 * x4 * x4 * x5);

            ////Уравнение 1
            //fi[0] = b[0] - (1
            //    + K[7] * x2
            //    + K[12] * x2 * x2 * x2 * x4 * x4 * x4
            //    + K[13] * x2 * x2 * x2 * x4 * x4 * x4 * x4);

            //// Уравнение 2
            //fi[1] = b[1] - (1
            //    + K[5] * x3
            //    + K[7] * x1
            //    + K[8] * x4 * x4 * x5
            //    + 3 * K[9] * x2 * x2 * x3 * x3 * x3 * x4
            //    + K[10] * x3 * x4
            //    + 2 * K[11] * x2 * x3 * x3 * x4
            //    + 3 * K[12] * x1 * x2 * x2 * x4 * x4 * x4
            //    + 3 * K[13] * x1 * x2 * x2 * x4 * x4 * x4 * x4);

            //// Уравнение 3
            //fi[2] = b[2] - (1
            //    + K[5] * x2
            //    + K[9] * x2 * x2 * x2 * x3 * x3 * x4
            //    + K[10] * x2 * x4
            //    + 2 * K[11] * x2 * x2 * x3 * x4);

            //// Уравнение 4
            //fi[3] = b[3] - (1
            //    + 2 * K[8] * x2 * x4 * x5
            //    + K[9] * x2 * x2 * x2 * x3 * x3 * x3
            //    + K[10] * x2 * x3
            //    + K[11] * x2 * x2 * x3 * x3
            //    + 3 * K[12] * x1 * x2 * x2 * x2 * x4 * x4
            //    + 4 * K[13] * x1 * x2 * x2 * x2 * x4 * x4 * x4
            //    + 2 * K[14] * x4);

            //// Уравнение 5
            //fi[4] = b[4] - (1
            //    + K[8] * x2 * x4 * x4);
        }

        //private void ValidateData()
        //{
        //    if (_componentMatrix.Count != _formingForms.Count)
        //        throw new ArgumentException("Количество строк в матрице не совпадает с количеством образующих форм");

        //    if (_componentMatrix.Any(row => row.Count != _baseForms.Count))
        //        throw new ArgumentException("Количество столбцов в матрице не совпадает с количеством базовых форм");
        //}

        //private void InitializeGridColumns()
        //{
        //    // Очищаем существующие колонки
        //    dg_Component_Matrix.Columns.Clear();

        //    // Первая колонка - названия образующих форм
        //    dg_Component_Matrix.Columns.Add(new DataGridTextColumn
        //    {
        //        Header = "Образующиеся формы",
        //        Binding = new Binding("FormingFormName"),
        //        Width = new DataGridLength(2, DataGridLengthUnitType.Star)
        //    });

        //    // Колонки для базовых форм
        //    foreach (var baseForm in _baseForms)
        //    {
        //        dg_Component_Matrix.Columns.Add(new DataGridTextColumn
        //        {
        //            Header = baseForm.Name,
        //            Binding = new Binding($"Coefficients[{baseForm.Name}]"),
        //            Width = new DataGridLength(1, DataGridLengthUnitType.Star)
        //        });
        //    }
        //}

        //private void PopulateRows()
        //{
        //    for (int i = 0; i < _formingForms.Count; i++)
        //    {
        //        var rowData = new MatrixRow
        //        {
        //            FormingFormName = _formingForms[i].Name,
        //            Coefficients = new Dictionary<string, int>()
        //        };

        //        for (int j = 0; j < _baseForms.Count; j++)
        //        {
        //            rowData.Coefficients[_baseForms[j].Name!] = _componentMatrix[i][j];
        //        }

        //        Rows.Add(rowData);
        //    }
        //}


    }
    public class PythonSolver
    {
        public List<double> Solve(
            List<double> xInitial,
            List<double> K,
            List<double> b)
        {
            // 1. Подготовка входных данных
            var input = new
            {
                x = xInitial,
                K = K,
                b = b
            };
            string jsonInput = JsonConvert.SerializeObject(input);

            // 2. Настройка процесса Python
            var processInfo = new ProcessStartInfo
            {
                FileName = "python",
                Arguments = "main.py",
                UseShellExecute = false,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                StandardOutputEncoding = System.Text.Encoding.UTF8,
                StandardErrorEncoding = System.Text.Encoding.UTF8,
                CreateNoWindow = true
            };

            // 3. Запуск скрипта
            using (var process = new Process())
            {
                process.StartInfo = processInfo;
                process.Start();

                // 4. Передача данных в Python
                using (var sw = process.StandardInput)
                {
                    sw.WriteLine(jsonInput);
                }

                // 5. Чтение результатов
                string jsonOutput = process.StandardOutput.ReadToEnd();
                string errors = process.StandardError.ReadToEnd();
                process.WaitForExit(5000);

                Console.WriteLine("Логи Python:");
                Console.WriteLine(errors);

                if (process.ExitCode != 0)
                    throw new Exception($"Python error: {errors}");

                // 6. Десериализация результатов
                dynamic output = JsonConvert.DeserializeObject(jsonOutput)!;
                return (
                    output.x.ToObject<List<double>>()
                );
            }
        }
    }

    public class MatrixRow
    {
        public string? FormingFormName { get; set; }
        public Dictionary<string, int>? Coefficients { get; set; }
    }
}
