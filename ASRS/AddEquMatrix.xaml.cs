using ASRS.Database;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace ASRS
{
    /// <summary>
    /// Логика взаимодействия для AddEquMatrix.xaml
    /// </summary>
    public partial class AddEquMatrix : Window
    {
        private DataTable? dataTable;
        private const int AdditionalColumnsCount = 5;
        private ApplicationContext _context;
        private List<DataGridColumn> dataGridColumns = new List<DataGridColumn>();
        private List<TextBox> headerTextBoxes = new List<TextBox>();
        public AddEquMatrix()
        {
            InitializeComponent();
            _context = new ApplicationContext();
        }

        private void BtnCreateMatrix_Click(object sender, RoutedEventArgs e)
        {
            // Сбор имен базовых и образующихся форм
            var baseNames = GetNames(baseNamesPanel);
            var formingNames = GetNames(formingNamesPanel);

            // Проверка на пустые поля
            if (baseNames.Any(string.IsNullOrEmpty) || formingNames.Any(string.IsNullOrEmpty))
            {
                MessageBox.Show("Все поля должны быть заполнены!");
                return;
            }

            // Создание заголовков колонок
            var columns = baseNames.Concat(formingNames).ToList();

            // Настройка DataGrid для ввода чисел
            dataGrid.Columns.Clear();
            foreach (var name in columns)
            {
                var column = new DataGridTextColumn
                {
                    Header = name,
                    Binding = new Binding($"[\"{name}\"]") { UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged }
                };
                dataGrid.Columns.Add(column);
            }

            // Инициализация данных
            dataGrid.ItemsSource = new List<ReactionRow>();
        }

        // Вспомогательный метод для получения имен из StackPanel
        private List<string> GetNames(StackPanel panel)
        {
            return panel.Children.OfType<TextBox>()
                .Select(tb => tb.Text.Trim())
                .ToList();
        }

        private void BtnApply_Click(object sender, RoutedEventArgs e)
        {
            // Проверка на пустые поля
            foreach (var textBox in headerTextBoxes)
            {
                if (string.IsNullOrEmpty(textBox.Text))
                {
                    MessageBox.Show("Все поля заголовков должны быть заполнены!");
                    return;
                }
            }

            // Обновление заголовков колонок
            for (int i = 0; i < headerTextBoxes.Count; i++)
            {
                if (dataGridColumns[i] is DataGridTextColumn column)
                {
                    column.Header = headerTextBoxes[i].Text;
                }
            }
        }

        private void InitializeDataTable(int baseCount, int resultingCount)
        {
            dataTable = new DataTable();
            dataTable.Columns.Add("Образующиеся формы", typeof(string));

            // Base forms columns
            for (int i = 0; i < baseCount; i++)
                dataTable.Columns.Add($"Базовая форма {i + 1}", typeof(double));

            // Additional columns
            dataTable.Columns.Add("lgK", typeof(double));
            dataTable.Columns.Add("Параметр", typeof(double));
            dataTable.Columns.Add("Фаза", typeof(int));
            dataTable.Columns.Add("deltaZ^2", typeof(double));
            dataTable.Columns.Add("MODEL#", typeof(int));

            // Add rows
            for (int i = 0; i < resultingCount; i++)
                dataTable.Rows.Add($"Форма {i + 1}");

            // Add phase row
            var phaseRow = dataTable.NewRow();
            phaseRow[0] = "Фаза (0-водная/1-органическая)";
            dataTable.Rows.Add(phaseRow);
        }

        private void CreateColumnHeaders(int baseCount)
        {
            panelColumnHeaders.Children.Clear();

            // Base form headers
            for (int i = 0; i < baseCount; i++)
            {
                var txtHeader = new TextBox
                {
                    Text = $"Форма {i + 1}",
                    Width = 100,
                    Margin = new Thickness(5, 0, 5, 5)
                };

                int columnIndex = i + 1;
                txtHeader.TextChanged += (s, args) =>
                {
                    dataTable.Columns[columnIndex].ColumnName = txtHeader.Text;
                    dataGrid.Columns[columnIndex].Header = txtHeader.Text;
                };

                panelColumnHeaders.Children.Add(txtHeader);
            }

            // Fixed additional headers
            string[] additionalHeaders = { "lgK", "Parameter", "Phase", "deltaZ²", "MODEL#" };
            foreach (var header in additionalHeaders)
            {
                panelColumnHeaders.Children.Add(new TextBlock
                {
                    Text = header,
                    Width = 100,
                    Margin = new Thickness(5, 0, 5, 5),
                    VerticalAlignment = VerticalAlignment.Center
                });
            }
        }

        private void CreateBaseNames_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtBaseCount.Text, out int count) && count > 0)
            {
                baseNamesPanel.Children.Clear();
                for (int i = 0; i < count; i++)
                {
                    var textBox = new TextBox { Width = 100, Margin = new Thickness(5) };
                    textBox.Text = $"Форма {i + 1}";
                    baseNamesPanel.Children.Add(textBox);
                }
            }
        }

        private void CreateFormingNames_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(resultCount.Text, out int count) && count > 0)
            {
                formingNamesPanel.Children.Clear();
                for (int i = 0; i < count; i++)
                {
                    var textBox = new TextBox { Width = 100, Margin = new Thickness(5) };
                    textBox.Text = $"Форма {i + 1}";
                    formingNamesPanel.Children.Add(textBox);
                }
            }
        }

        private void SetupDataGrid(int baseCount)
        {
            dataGrid.Columns.Clear();
            dataGrid.ItemsSource = dataTable.DefaultView;

            // Resulting forms column
            var formColumn = new DataGridTextColumn
            {
                Header = "Образующиеся формы",
                Binding = new Binding("[0]")
            };
            dataGrid.Columns.Add(formColumn);

            // Base form columns
            for (int i = 0; i < baseCount; i++)
            {
                var column = new DataGridTemplateColumn
                {
                    Header = dataTable.Columns[i + 1].ColumnName,
                    CellTemplate = CreateCellTemplate(i + 1)
                };
                dataGrid.Columns.Add(column);
            }

            // Additional columns
            for (int i = 0; i < AdditionalColumnsCount; i++)
            {
                var column = new DataGridTemplateColumn
                {
                    Header = dataTable.Columns[baseCount + 1 + i].ColumnName,
                    CellTemplate = CreateCellTemplate(baseCount + 1 + i)
                };
                dataGrid.Columns.Add(column);
            }
        }

        private DataTemplate CreateCellTemplate(int columnIndex)
        {
            var factory = new FrameworkElementFactory(typeof(TextBox));
            factory.SetBinding(TextBox.TextProperty, new Binding($"[{columnIndex}]"));

            // Правильный способ добавления обработчика для маршрутизируемого события
            factory.AddHandler(
                UIElement.PreviewTextInputEvent,
                new TextCompositionEventHandler(NumberValidation)
            );

            // Для обработки вставки через Ctrl+V
            factory.AddHandler(
                DataObject.PastingEvent,
                new DataObjectPastingEventHandler(OnPasting)
            );

            return new DataTemplate { VisualTree = factory };
        }

        // Добавьте этот метод для обработки вставки
        private void OnPasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                var text = (string)e.DataObject.GetData(typeof(string));
                if (!double.TryParse(text, out _))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }

        private void NumberValidation(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;
            var newText = textBox.Text.Insert(textBox.CaretIndex, e.Text);
            e.Handled = !double.TryParse(newText, out _);
        }

        //private async void BtnSaveToDb_Click(object sender, RoutedEventArgs e)
        //{
        //    using (var context = new ApplicationContext())
        //    {
        //        // Сохранение базовых форм
        //        foreach (var name in baseNames)
        //        {
        //            await GetOrCreateBaseForm(context, name);
        //        }

        //        // Сохранение образующихся форм
        //        foreach (var name in formingNames)
        //        {
        //            await GetOrCreateFormingForm(context, name);
        //        }

        //        // Сохранение реакций
        //        var reactions = (dataGrid.ItemsSource as IEnumerable<ReactionRow>)?
        //            .Where(row => row.Coefficients.Any());

        //        if (reactions != null)
        //        {
        //            foreach (var row in reactions)
        //            {
        //                var reaction = new Reaction
        //                {
        //                    // Предполагается, что первые три базовые формы — входные
        //                    Inp1 = baseNames[0],
        //                    Inp2 = baseNames[1],
        //                    Inp3 = baseNames[2],
        //                    // Первая образующаяся форма — выходная
        //                    Prod = formingNames[0],
        //                    KInp1 = row.Coefficients.GetValueOrDefault(baseNames[0]),
        //                    KInp2 = row.Coefficients.GetValueOrDefault(baseNames[1]),
        //                    KInp3 = row.Coefficients.GetValueOrDefault(baseNames[2]),
        //                    KProd = row.Coefficients.GetValueOrDefault(formingNames[0])
        //                };
        //                context.Reactions.Add(reaction);
        //            }
        //            await context.SaveChangesAsync();
        //        }
        //    }
        //}

        private async Task<BaseForm> GetOrCreateBaseForm(ApplicationContext context, string name)
        {
            var form = await context.BaseForms
                .FirstOrDefaultAsync(f => f.Name == name);
            if (form == null)
            {
                form = new BaseForm { Name = name, Phase = 0 }; // Значение Phase можно задавать через UI
                await context.BaseForms.AddAsync(form);
                await context.SaveChangesAsync();
            }
            return form;
        }

        private async Task<FormingForm> GetOrCreateFormingForm(ApplicationContext context, string name)
        {
            var form = await context.FormingForms
                .FirstOrDefaultAsync(f => f.Name == name);
            if (form == null)
            {
                form = new FormingForm { Name = name };
                await context.FormingForms.AddAsync(form);
                await context.SaveChangesAsync();
            }
            return form;
        }

        private void DataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.Column.DisplayIndex == 0) // If editing form name
            {
                var textBox = e.EditingElement as TextBox;
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    e.Cancel = true;
                    txtStatus.Text = "Form name cannot be empty!";
                }
            }
        }

        public class ReactionRow
        {
            public Dictionary<string, int> Coefficients { get; } = new Dictionary<string, int>();
        }
    }
}
