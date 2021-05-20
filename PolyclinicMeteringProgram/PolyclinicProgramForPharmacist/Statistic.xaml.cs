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

namespace PolyclinicProgramForPharmacist
{
    /// <summary>
    /// Логика взаимодействия для Statistic.xaml
    /// </summary>
    public partial class Statistic : Window
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        PrescriptionLogic _logic;

        private List<StatisticByPrescriptionViewModel> _prescription = new List<StatisticByPrescriptionViewModel>();

        public Statistic(PrescriptionLogic logic)
        {
            InitializeComponent();
            _logic = logic;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            List<PrescriptionViewModel> prescriptions = _logic.Read(null);
            foreach (var prescription in prescriptions)
            {
                _prescription.Add(new StatisticByPrescriptionViewModel { Price = prescription.Price, PharmacyAddress = prescription.PharmacyAddress });
            }
            DataGridView.ItemsSource = _prescription;
        }

        private void BuildGraph_Click(object sender, RoutedEventArgs e)
        {
            List<StatisticByPrescriptionViewModel> selection = new List<StatisticByPrescriptionViewModel>();
            if (DataGridView.SelectedItems.Count != 0)
            {
                foreach (var prescription in DataGridView.SelectedItems)
                {
                    selection.Add((StatisticByPrescriptionViewModel)prescription);
                }
            }
            else
            {
                selection = _prescription;
            }
            Build(selection);
        }

        private void Build(List<StatisticByPrescriptionViewModel> statistic)
        {
            SeriesCollection series = new SeriesCollection();
            List<string> pharmacyAddress = new List<string>();
            ChartValues<int> price = new ChartValues<int>();

            foreach (var item in statistic)
            {
                pharmacyAddress.Add(item.PharmacyAddress);
                price.Add(item.Price);
            }

            Graph.AxisX.Clear();
            Graph.AxisX.Add(new Axis()

            {
                Title = "\nРецепты",
                Labels = pharmacyAddress
            });

            LineSeries prescriptionLine = new LineSeries
            {
                Title = "Стоимость: ",
                Values = price
            };

            series.Add(prescriptionLine);
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
