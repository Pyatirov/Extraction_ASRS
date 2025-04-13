﻿using ASRS.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ASRS
{
    /// <summary>
    /// Логика взаимодействия для ComponentMatrix.xaml
    /// </summary>
    public partial class ComponentMatrix : Window
    {
        public ObservableCollection<MatrixRow> Rows { get; } = new ObservableCollection<MatrixRow>();
        private readonly List<BaseForm> _baseForms;
        private readonly List<FormingForm> _formingForms;
        private readonly List<List<int>> _componentMatrix;

        public ComponentMatrix(List<BaseForm> baseForms,
            List<FormingForm> formingForms,
            List<List<int>> componentMatrix)
        {
            InitializeComponent();

            _baseForms = baseForms ?? throw new System.ArgumentNullException(nameof(baseForms));
            _formingForms = formingForms ?? throw new System.ArgumentNullException(nameof(formingForms));
            _componentMatrix = componentMatrix ?? throw new System.ArgumentNullException(nameof(componentMatrix));

            ValidateData();
            InitializeGridColumns();
            PopulateRows();
            DataContext = this;
        }

        private void ValidateData()
        {
            if (_componentMatrix.Count != _formingForms.Count)
                throw new ArgumentException("Количество строк в матрице не совпадает с количеством образующих форм");

            if (_componentMatrix.Any(row => row.Count != _baseForms.Count))
                throw new ArgumentException("Количество столбцов в матрице не совпадает с количеством базовых форм");
        }

        private void InitializeGridColumns()
        {
            // Очищаем существующие колонки
            dg_Component_Matrix.Columns.Clear();

            // Первая колонка - названия образующих форм
            dg_Component_Matrix.Columns.Add(new DataGridTextColumn
            {
                Header = "Образующиеся формы",
                Binding = new Binding("FormingFormName"),
                Width = new DataGridLength(2, DataGridLengthUnitType.Star)
            });

            // Колонки для базовых форм
            foreach (var baseForm in _baseForms)
            {
                dg_Component_Matrix.Columns.Add(new DataGridTextColumn
                {
                    Header = baseForm.Name,
                    Binding = new Binding($"Coefficients[{baseForm.Name}]"),
                    Width = new DataGridLength(1, DataGridLengthUnitType.Star)
                });
            }
        }

        private void PopulateRows()
        {
            for (int i = 0; i < _formingForms.Count; i++)
            {
                var rowData = new MatrixRow
                {
                    FormingFormName = _formingForms[i].Name,
                    Coefficients = new Dictionary<string, int>()
                };

                for (int j = 0; j < _baseForms.Count; j++)
                {
                    rowData.Coefficients[_baseForms[j].Name!] = _componentMatrix[i][j];
                }

                Rows.Add(rowData);
            }
        }
    }
}
