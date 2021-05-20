using PolyclinicBusinessLogic.BindingModels;
using PolyclinicBusinessLogic.BusinessLogics;
using PolyclinicBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
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
using Unity;

namespace PolyclinicProgramForPharmacist
{
    /// <summary>
    /// Логика взаимодействия для Receipt.xaml
    /// </summary>
    public partial class Receipt : Window
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        ReceiptLogic _logic;
        MedicineLogic medicineLogic;
        public int Id { set { id = value; } }
        private int? id;
        private Dictionary<int,(string, int)> receiptMedicine;

        public Receipt(ReceiptLogic logic, MedicineLogic medicineLogic)
        {
            InitializeComponent();
            _logic = logic;
            this.medicineLogic = medicineLogic;
        }

        private void Window_loaded(object sender, RoutedEventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    ReceiptViewModel view = _logic.Read(new ReceiptBindingModel
                    {
                        Id = id.Value
                    })?[0];
                    if (view != null)
                    {
                        tbDeliverymanName.Text = view.DeliverymanName;
                        tbDate.SelectedDate = view.Date;
                        receiptMedicine = view.ReceiptMedicines;
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
                   MessageBoxImage.Error);
                }
            }
            else
            {
                receiptMedicine = new Dictionary<int, (string, int)>();
            }
        }

        private void LoadData()
        {
            try
            {
                if (receiptMedicine != null)
                {
                    List<ReceiptMedicineViewModel> list = new List<ReceiptMedicineViewModel>();
                    foreach (var medicine in receiptMedicine)
                    {
                        list.Add(new ReceiptMedicineViewModel { Id = medicine.Key, MedicineName = medicine.Value.Item1, MedicineCount = medicine.Value.Item2 });
                    }
                    dgReceiptMedicine.ItemsSource = list;
                    dgReceiptMedicine.Columns[0].Visibility = Visibility.Hidden;
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
            if (tbDate.SelectedDate == null || tbDate.SelectedDate < DateTime.Now)
            {
                MessageBox.Show("Выберите дату", "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
                return;
            }
            try
            {
                _logic.CreateOrUpdate(new ReceiptBindingModel
                {
                    Id = id,
                    Date = tbDate.SelectedDate.Value,
                    DeliverymanName = tbDeliverymanName.Text,
                    ReceiptMedecines = receiptMedicine
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение",
               MessageBoxButton.OK, MessageBoxImage.Information);
                this.DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AddMedicine_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<AddMedicine>();
            window.ShowDialog();
            if (window.DialogResult == true)
            {
                if (!receiptMedicine.ContainsKey(window.Id))
                {
                    receiptMedicine.Add(window.Id, (window.MedicineName, window.Count));
                }

            }
            LoadData();
        }


        private void btnDeleteMedicine_Click(object sender, RoutedEventArgs e)
        {
            if (dgReceiptMedicine.SelectedIndex != -1)
            {
                MessageBoxResult result = MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButton.YesNo,
               MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    ReceiptMedicineViewModel medicine = (ReceiptMedicineViewModel)dgReceiptMedicine.SelectedCells[0].Item;
                    try
                    {
                        receiptMedicine.Remove(medicine.Id);
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
