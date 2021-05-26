using PolyclinicBusinessLogic.BindingModels;
using PolyclinicBusinessLogic.BusinessLogics;
using PolyclinicBusinessLogic.ViewModels;
using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using Unity;
using LiveCharts;
using LiveCharts.Wpf;
using System.Collections.Generic;

namespace PolyclinicMeteringProgram
{
    /// <summary>
    /// Логика взаимодействия для Statistic.xaml
    /// </summary>
    public partial class Statistic : Window
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        PatientLogic _logic;

        private List<StatisticByPatientViewModel> _patients = new List<StatisticByPatientViewModel>();

        public Statistic(PatientLogic logic)
        {
            InitializeComponent();
            _logic = logic;
        }
        private void LoadData()
        {
            _patients.Clear();
            List<PatientViewModel> patients = _logic.Read(new PatientBindingModel { DateTo = dpTo.SelectedDate , DateFrom = dpFrom.SelectedDate });
            foreach(var patient in patients)
            {
                _patients.Add(new StatisticByPatientViewModel { PatientName = patient.FullName, ProcedureCount = patient.PatientProcedures.Count });
            }
            DataGridView.ItemsSource = _patients;
            DataGridView.Items.Refresh();
        }

        private void BuildGraph_Click(object sender, RoutedEventArgs e)
        {
            if (dpFrom.SelectedDate == null)
            {
                MessageBox.Show("Выберите дату начала",
               "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (dpTo.SelectedDate == null)
            {
                MessageBox.Show("Выберите дату окончания",
               "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (dpFrom.SelectedDate >= dpTo.SelectedDate)
            {
                MessageBox.Show("Дата начала должна быть меньше даты окончания",
               "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            LoadData();
            Build(_patients);
        }

        private void Build(List<StatisticByPatientViewModel> statistic)
        {
            SeriesCollection series = new SeriesCollection();
            List<string> patientName = new List<string>();
            ChartValues<int> procedureCount = new ChartValues<int>();

            foreach (var item in statistic)
            {
                patientName.Add(item.PatientName);
                procedureCount.Add(item.ProcedureCount);
            }

            Graph.AxisX.Clear();
            Graph.AxisX.Add(new Axis()

            {
                Title = "\nПациенты",
                Labels = patientName
            });

            LineSeries procedureLine = new LineSeries
            {
                Title = "Кол-во процедур: ",
                Values = procedureCount
            };

            series.Add(procedureLine);
            Graph.Series = series;
        }

        private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string displayName = GetPropertyDisplayName(e.PropertyDescriptor);
            if (!string.IsNullOrEmpty(displayName))
            {
                e.Column.Header = displayName;
            }
        }

        public static string GetPropertyDisplayName(object descriptor)
        {
            PropertyDescriptor pd = descriptor as PropertyDescriptor;
            if (pd != null)
            {
                DisplayNameAttribute displayName = pd.Attributes[typeof(DisplayNameAttribute)] as DisplayNameAttribute;
                if (displayName != null && displayName != DisplayNameAttribute.Default)
                {
                    return displayName.DisplayName;
                }
            }
            else
            {
                PropertyInfo pi = descriptor as PropertyInfo;
                if (pi != null)
                {
                    Object[] attributes = pi.GetCustomAttributes(typeof(DisplayNameAttribute), true);
                    for (int i = 0; i < attributes.Length; ++i)
                    {
                        DisplayNameAttribute displayName = attributes[i] as DisplayNameAttribute;
                        if (displayName != null && displayName != DisplayNameAttribute.Default)
                        {
                            return displayName.DisplayName;
                        }
                    }
                }
            }
            return null;
        }
    }
}
