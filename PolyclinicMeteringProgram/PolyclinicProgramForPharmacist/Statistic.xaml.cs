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
        ReceiptLogic _logic;

        private List<StatisticByReceiptViewModel> _receipts = new List<StatisticByReceiptViewModel>();

        public Statistic(ReceiptLogic logic)
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
            _receipts.Clear();
            List<ReceiptViewModel> receipts = _logic.Read(new ReceiptBindingModel { DateFrom = dpFrom.SelectedDate, DateTo = dpTo.SelectedDate});
            foreach (var receipt in receipts)
            {
                int medicineCount = 0;
                foreach(var medicine in receipt.ReceiptMedicines)
                {
                    medicineCount += medicine.Value.Item2;
                }
                _receipts.Add(new StatisticByReceiptViewModel { DeliverymanName = receipt.DeliverymanName, MedicineCount = medicineCount });
            }
            DataGridView.ItemsSource = _receipts;
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
            Build(_receipts);
        }

        private void Build(List<StatisticByReceiptViewModel> statistic)
        {
            SeriesCollection series = new SeriesCollection();
            List<string> deliverymanName = new List<string>();
            ChartValues<int> medicineCount = new ChartValues<int>();

            foreach (var item in statistic)
            {
                deliverymanName.Add(item.DeliverymanName);
                medicineCount.Add(item.MedicineCount);
            }

            Graph.AxisX.Clear();
            Graph.AxisX.Add(new Axis()

            {
                Title = "\nФамилии доставщиков",
                Labels = deliverymanName
            });

            LineSeries prescriptionLine = new LineSeries
            {
                Title = "Кол-во поступивших лекарств: ",
                Values = medicineCount
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
