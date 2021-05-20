using PolyclinicBusinessLogic.BindingModels;
using PolyclinicBusinessLogic.BusinessLogics;
using PolyclinicBusinessLogic.ViewModels;
using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using Unity;

namespace PolyclinicProgramForPharmacist
{
    /// <summary>
    /// Логика взаимодействия для Medicines.xaml
    /// </summary>
    public partial class Medicines : Window
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        MedicineLogic _logic;
        public int _pharmacistId { get; set; }

        public Medicines(MedicineLogic logic)
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
            try
            {
                var list = _logic.Read(null);
                if (list != null)
                {
                    DataGridView.ItemsSource = list;
                    DataGridView.Columns[0].Visibility = Visibility.Hidden;
                    DataGridView.Columns[4].Visibility = Visibility.Hidden;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
            }
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<Medicine>();
            window._pharmacistId = _pharmacistId;
            window.ShowDialog();
            if (window.DialogResult == true)
            {
                LoadData();
            }

        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridView.SelectedIndex != -1)
            {
                MessageBoxResult result = MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButton.YesNo,
               MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    MedicineViewModel treatment = (MedicineViewModel)DataGridView.SelectedCells[0].Item;
                    int id = Convert.ToInt32(treatment.Id);
                    try
                    {
                        _logic.Delete(new MedicineBindingModel { Id = id });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
                       MessageBoxImage.Error);
                    }
                    LoadData();
                }
            }
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridView.SelectedIndex != -1)
            {
                var window = Container.Resolve<Medicine>();
                MedicineViewModel medicine = (MedicineViewModel)DataGridView.SelectedCells[0].Item;
                window.Id = Convert.ToInt32(medicine.Id);
                window._pharmacistId = _pharmacistId;
                window.ShowDialog();
                if (window.DialogResult == true)
                {
                    LoadData();
                }
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
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
