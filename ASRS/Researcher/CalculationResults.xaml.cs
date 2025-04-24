using ASRS.Database;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using static alglib;
using static ASRS.Database.ConnectToDB;
using static ASRS.Researcher;

namespace ASRS
{
    public partial class CalculationResults : Window
    {
        public ObservableCollection<MatrixRow> Rows { get; } = new ObservableCollection<MatrixRow>();
        private List<List<double>> solutions;

        public CalculationResults(List<List<double>> Solutions)
        {
            InitializeComponent();
            solutions = Solutions ?? throw new System.ArgumentNullException(nameof(solutions));
            DataContext = this;
            ShowResults();
        }

        private void ShowResults()
        {
            var sb = new StringBuilder();

            for (int pointIndex = 0; pointIndex < solutions.Count; pointIndex++)
            {
                sb.AppendLine($"Результаты для точки {pointIndex + 1}:");
                sb.AppendLine("----------------------------------");

                var solution = solutions[pointIndex];
                for (int i = 0; i < solution.Count; i++)
                {
                    sb.AppendLine($"x{i + 1} = {solution[i]:F6}"); // 6 знаков после запятой
                }

                sb.AppendLine();
                sb.AppendLine();
            }

            tb_Results.Text = sb.ToString();
        }

        private double CalculateTermValue(Term term, double[] solution, double[] K)
        {
            double value = term.Coefficient;
            value *= K[term.KIndex]; // Умножаем на константу равновесия

            foreach (var varEntry in term.Variables!)
            {
                int varIndex = varEntry.Key;
                int exponent = varEntry.Value;
                value *= Math.Pow(solution[varIndex], exponent); // Учитываем степень переменной
            }

            return value;
        }

        //private string GetPhase(string formName)
        //{
        //    // Предполагается, что baseForms содержит формы с свойством Phase
        //    var form = baseForms.FirstOrDefault(f => f.Name == formName);
        //    return form?.Phase ?? "Неизвестно"; // Если фаза не определена, возвращаем "Неизвестно"
        //}

        private void AddToTotal(Dictionary<string, Dictionary<string, double>> totals,
    string phase, string formName, double value)
        {
            if (!totals.ContainsKey(phase))
                totals[phase] = new Dictionary<string, double>();

            if (!totals[phase].ContainsKey(formName))
                totals[phase][formName] = 0;

            totals[phase][formName] += value;
        }

    }



    //public static void Residuals(double[] x, double[] fi, object obj)
    //{
    //    var data = (ResidualData)obj;
    //    for (int i = 0; i < fi.Length; i++)
    //    {
    //        double sum = x[i]; // Учет текущей переменной x_i
    //        foreach (var term in data.TermsPerEquation![i])
    //        {
    //            double termValue = data.K![term.KIndex] * term.Coefficient;
    //            foreach (var var in term.Variables!)
    //                termValue *= Math.Pow(x[var.Key], var.Value);
    //            sum += termValue;
    //        }
    //        fi[i] = data.B![i] - sum;
    //    }
    //}
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
    public class SystemBuilder
    {
        private List<ConcentrationSummary> concentrationsSum;
        private List<ConcentrationConstant> concentrationConstants;
        private List<Reaction> reactions;
        private List<BaseForm> baseForms;
        private List<FormingForm> formingForms;

        public SystemBuilder(
            List<ConcentrationSummary> concentrationsSum,
            List<ConcentrationConstant> concentrationConstants,
            List<Reaction> reactions,
            List<BaseForm> baseForms,
            List<FormingForm> formingForms
        )
        {
            this.concentrationsSum = concentrationsSum;
            this.concentrationConstants = concentrationConstants;
            this.reactions = reactions;
            this.baseForms = baseForms;
            this.formingForms = formingForms;
        }

        public Dictionary<int, Dictionary<string, EquationData>> BuildEquations()
        {
            var results = new Dictionary<int, Dictionary<string, EquationData>>();
            var reactionList = reactions.ToList(); // Для определения индексов реакций

            foreach (var point in concentrationsSum.Select(c => c.PointId).Distinct())
            {
                var pointData = concentrationsSum
                    .Where(c => c.PointId == point)
                    .ToDictionary(c => c.FormName!, c => c.TotalConcentration);

                var equations = new Dictionary<string, EquationData>();

                foreach (var form in baseForms)
                {
                    var formName = form.Name!;
                    if (!pointData.TryGetValue(formName, out var b))
                        throw new InvalidOperationException($"Концентрация для {formName} не найдена.");

                    var terms = new List<Term>();
                    foreach (var reaction in reactionList)
                    {
                        var coeff = GetFormCoefficient(reaction, formName);
                        if (coeff == null || coeff == 0) continue;

                        var term = new Term
                        {
                            KIndex = reactionList.IndexOf(reaction),
                            Coefficient = coeff.Value,
                            Variables = new Dictionary<int, int>()
                        };

                        foreach (var component in GetReactionComponents(reaction))
                        {
                            if (component.Coeff <= 0 || string.IsNullOrEmpty(component.Name)) continue;
                            var varIndex = baseForms.FindIndex(f => f.Name == component.Name);
                            if (varIndex == -1) continue;
                            term.Variables[varIndex] = component.Coeff;
                        }

                        terms.Add(term);
                    }

                    equations.Add(formName, new EquationData { B = b, Terms = terms });
                }

                results.Add(point, equations);
            }

            return results;
        }

        // Вспомогательные методы
        private int? GetFormCoefficient(Reaction reaction, string formName)
        {
            if (reaction.Inp1 == formName) return reaction.KInp1;
            if (reaction.Inp2 == formName) return reaction.KInp2;
            if (reaction.Inp3 == formName) return reaction.KInp3;
            return null;
        }

        private IEnumerable<(string Name, int Coeff)> GetReactionComponents(Reaction reaction)
        {
            yield return (reaction.Inp1!, reaction.KInp1 ?? 0);
            yield return (reaction.Inp2!, reaction.KInp2 ?? 0);
            yield return (reaction.Inp3!, reaction.KInp3 ?? 0);
        }
    }

    public class MatrixRow
    {
        public string? FormingFormName { get; set; }
        public Dictionary<string, int>? Coefficients { get; set; }
    }

    public class Term
    {
        public int KIndex { get; set; }
        public double Coefficient { get; set; }
        public Dictionary<int, int>? Variables { get; set; } // Индекс переменной -> степень
    }

    public class EquationData
    {
        public double B { get; set; }
        public List<Term>? Terms { get; set; }
    }

    public class ResidualData
    {
        public double[]? K { get; set; }
        public double[]? B { get; set; }
        public List<Term>[]? TermsPerEquation { get; set; }
    }

    public class ConcentrationSummary
    {
        public int PointId { get; set; }
        public string? FormName { get; set; }
        public double TotalConcentration { get; set; }
    }

    public class CalculationResult
    {
        public int PointId { get; set; }
        public string? FormName { get; set; }
        public double CalculatedValue { get; set; }
    }
}
