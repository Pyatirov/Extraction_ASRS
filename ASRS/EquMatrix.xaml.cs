using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ASRS.Database;

namespace ASRS
{
    /// <summary>
    /// Логика взаимодействия для EquMatrix.xaml
    /// </summary>
    public partial class EquMatrix : Window
    {
        private readonly ApplicationContext _dbContext;

        public EquMatrix()
        {
            InitializeComponent();
            _dbContext = new ApplicationContext(); // Инициализация контекста БД
        }

        private void GenerateMatrix_Click(object sender, RoutedEventArgs e)
        {
            // Чтение и проверка пользовательского ввода
            if (!int.TryParse(RowsInput.Text, out int rows) || rows <= 0 ||
                !int.TryParse(ColsInput.Text, out int cols) || cols <= 0)
            {
                MessageBox.Show("Введите корректные положительные числа для строк и столбцов.");
                return;
            }

            // Добавляем дополнительные строки и столбцы
            int totalRows = rows + 1; // +1 строка для заголовков
            int totalCols = cols + 1 + 5; // +1 колонка для заголовков строк, +5 автостолбцов

            double cellWidth = 90;
            double cellHeight = 26;
            double extraColWidth = 100;

            string[] extraHeaders = { "lgK", "Параметр", "1/0 - Водная/Органическая", "Δz²", "Модель" };

            // Очищаем старую матрицу
            MatrixGrid.Children.Clear();
            MatrixGrid.RowDefinitions.Clear();
            MatrixGrid.ColumnDefinitions.Clear();

            // Создаем строки и столбцы
            for (int i = 0; i < totalRows; i++)
                MatrixGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(cellHeight) });

            for (int j = 0; j < totalCols; j++)
            {
                bool isExtraColumn = j >= (cols + 1);
                double width = isExtraColumn ? extraColWidth : cellWidth;
                MatrixGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(width) });
            }

            // Устанавливаем ширину Grid для корректной прокрутки
            MatrixGrid.Width = cols * cellWidth + 5 * extraColWidth + cellWidth;
            MatrixGrid.Height = totalRows * cellHeight;

            // Заполняем ячейки
            for (int row = 0; row < totalRows; row++)
            {
                for (int col = 0; col < totalCols; col++)
                {
                    UIElement element;

                    bool isExtraCol = col >= cols + 1;
                    bool isExtraRow = row >= rows + 1;

                    // Заголовок (первая строка) — не для ввода данных
                    if (row == 0 && col == 0)
                    {
                        // Угловая ячейка — пустая
                        element = new TextBlock();
                    }
                    else if (row == 0 && col <= cols)
                    {
                        // Заголовки столбцов от пользователя
                        element = new TextBox
                        {
                            Width = (col >= cols + 1) ? extraColWidth : cellWidth,
                            Height = cellHeight,
                            Margin = new Thickness(0),
                            Padding = new Thickness(0),
                            FontWeight = FontWeights.Bold,
                            Background = Brushes.LightYellow,
                            TextAlignment = TextAlignment.Center,
                            BorderThickness = new Thickness(0.5)
                        };
                    }
                    else if (row == 0 && isExtraCol)
                    {
                        // Заголовки 5 автостолбцов
                        int idx = col - (cols + 1);
                        element = new TextBlock
                        {
                            Text = extraHeaders[idx],
                            FontWeight = FontWeights.Bold,
                            FontSize = 11,
                            TextAlignment = TextAlignment.Center,
                            VerticalAlignment = VerticalAlignment.Center,
                            HorizontalAlignment = HorizontalAlignment.Center,
                            TextWrapping = TextWrapping.Wrap
                        };
                    }
                    else if (col == 0 && row <= rows)
                    {
                        // Заголовки строк от пользователя
                        element = new TextBox
                        {
                            Width = cellWidth,
                            Height = cellHeight,
                            Margin = new Thickness(0),
                            Padding = new Thickness(0),
                            FontWeight = FontWeights.Bold,
                            Background = Brushes.LightYellow,
                            TextAlignment = TextAlignment.Center,
                            BorderThickness = new Thickness(0.5)
                        };
                    }
                    else
                    {
                        // Обычные ячейки (начиная с row 1 и col 1)
                        double textboxWidth = (col >= totalCols - 5) ? extraColWidth : cellWidth;

                        element = new TextBox
                        {
                            Width = textboxWidth,
                            Height = cellHeight,
                            Margin = new Thickness(0),
                            Padding = new Thickness(0),
                            FontSize = 12,
                            FontFamily = new FontFamily("Consolas"),
                            TextAlignment = TextAlignment.Center,
                            BorderThickness = new Thickness(0.5)
                        };
                    }

                    Grid.SetRow(element, row);
                    Grid.SetColumn(element, col);
                    MatrixGrid.Children.Add(element);
                }
            }
        }

        private void AddBaseFormsToDatabase(int cols)
        {
            // Получаем список названий базовых форм из первой строки матрицы
            for (int col = 1; col <= cols; col++)
            {
                // Извлекаем название базовой формы из соответствующей ячейки в первой строке
                var baseFormTextBox = (TextBox)MatrixGrid.Children
                    .Cast<UIElement>()
                    .FirstOrDefault(e => Grid.GetRow(e) == 0 && Grid.GetColumn(e) == col);

                if (baseFormTextBox != null)
                {
                    string baseFormName = baseFormTextBox.Text;

                    if (!string.IsNullOrEmpty(baseFormName))
                    {
                        // Проверяем наличие этой базовой формы в базе данных
                        var existingBaseForm = _dbContext.BaseForms
                            .FirstOrDefault(b => b.Name == baseFormName);

                        if (existingBaseForm == null)
                        {
                            // Если форма не найдена, добавляем её в базу
                            var newBaseForm = new BaseForm
                            {
                                Name = baseFormName,
                                Phase = 1 // Например, фаза 1 по умолчанию, можно настроить
                            };

                            _dbContext.BaseForms.Add(newBaseForm);
                        }
                    }
                }
            }

            // Сохраняем изменения в базе данных
            _dbContext.SaveChanges();
        }

        // Метод для сохранения данных в БД
        private void SaveDataButton_Click(object sender, RoutedEventArgs e)
        {
            var matrixData = GetMatrixDataFromUI();
            var matrixDataService = new MatrixDataService(_dbContext);
            matrixDataService.SaveMatrixData(matrixData);
        }

        // Метод для извлечения данных из UI
        private List<List<string>> GetMatrixDataFromUI()
        {
            var matrixData = new List<List<string>>();

            for (int row = 0; row < MatrixGrid.RowDefinitions.Count; row++)
            {
                var rowData = new List<string>();

                for (int col = 0; col < MatrixGrid.ColumnDefinitions.Count; col++)
                {
                    var textBox = GetTextBoxAt(row, col);
                    rowData.Add(textBox?.Text ?? string.Empty);
                }

                matrixData.Add(rowData);
            }

            return matrixData;
        }

        // Метод для получения TextBox по индексу
        private TextBox GetTextBoxAt(int row, int col)
        {
            // Примерный метод для получения TextBox. Реализуйте его в зависимости от вашей разметки.
            var element = MatrixGrid.Children
                .OfType<TextBox>()
                .FirstOrDefault(e => Grid.GetRow(e) == row && Grid.GetColumn(e) == col);
            return element;
        }

        public class MatrixDataService
        {
            private readonly ApplicationContext _dbContext;

            public MatrixDataService(ApplicationContext dbContext)
            {
                _dbContext = dbContext;
            }

            // Метод для сохранения данных в базу данных
            public void SaveMatrixData(List<List<string>> matrixData)
            {
                int colCount = matrixData[0].Count - 6; // -1 заголовок строк, -5 автостолбцов
                int rowCount = matrixData.Count - 1;   // -1 строка заголовков

                var reactions = new List<Reaction>();

                // Кэш для форм, чтобы не выполнять лишние запросы к БД
                var baseFormsCache = _dbContext.BaseForms.ToDictionary(b => b.Name, b => b);
                var formingFormsCache = _dbContext.FormingForms.ToDictionary(f => f.Name, f => f);

                for (int row = 1; row <= rowCount; row++) // строки с данными
                {
                    string formingFormName = matrixData[row][0];

                    if (string.IsNullOrWhiteSpace(formingFormName)) continue;

                    // Добавляем в БД, если образующаяся форма отсутствует
                    if (!formingFormsCache.TryGetValue(formingFormName, out var formingForm))
                    {
                        formingForm = new FormingForm { Name = formingFormName };
                        _dbContext.FormingForms.Add(formingForm);
                        formingFormsCache[formingFormName] = formingForm;
                    }

                    // Сохраняем входные формы и коэффициенты
                    var inputs = new List<(string name, int coeff)>();

                    for (int col = 1; col <= colCount; col++)
                    {
                        string baseFormName = matrixData[0][col];
                        string cellValue = matrixData[row][col];

                        if (string.IsNullOrWhiteSpace(baseFormName) || string.IsNullOrWhiteSpace(cellValue)) continue;

                        if (!int.TryParse(cellValue, out int coeff)) continue;

                        // Добавляем в БД, если базовая форма отсутствует
                        if (!baseFormsCache.TryGetValue(baseFormName, out var baseForm))
                        {
                            baseForm = new BaseForm { Name = baseFormName, Phase = 1 };
                            _dbContext.BaseForms.Add(baseForm);
                            baseFormsCache[baseFormName] = baseForm;
                        }

                        inputs.Add((baseFormName, coeff));
                    }

                    // Заполняем Reaction с поддержкой до 3-х базовых форм
                    var reaction = new Reaction
                    {
                        Phase = 1, // При необходимости можно брать из UI
                        Prod = formingFormName
                    };

                    if (inputs.Count > 0)
                    {
                        reaction.Inp1 = inputs[0].name;
                        reaction.KInp1 = inputs[0].coeff;
                    }
                    if (inputs.Count > 1)
                    {
                        reaction.Inp2 = inputs[1].name;
                        reaction.KInp2 = inputs[1].coeff;
                    }
                    if (inputs.Count > 2)
                    {
                        reaction.Inp3 = inputs[2].name;
                        reaction.KInp3 = inputs[2].coeff;
                    }

                    reactions.Add(reaction);
                }

                // Сохраняем формы и реакции
                _dbContext.SaveChanges(); // сначала формы
                _dbContext.Reactions.AddRange(reactions);
                _dbContext.SaveChanges();
            }

        }
    }
}
