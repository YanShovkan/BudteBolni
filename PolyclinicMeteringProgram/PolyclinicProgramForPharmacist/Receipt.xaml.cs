using PolyclinicBusinessLogic.BindingModels;
using PolyclinicBusinessLogic.BusinessLogics;
using PolyclinicBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
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
                    List<MedicineViewModel> list = new List<MedicineViewModel>();
                    foreach (var medicine in receiptMedicine)
                    {
                        list.Add(medicineLogic.Read(new MedicineBindingModel { Id = medicine.Key })?[0]);

                    }
                    dgReceiptMedicine.ItemsSource = list;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (tbDate.SelectedDate == null || tbDate.SelectedDate > DateTime.Now)
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
                    ReceiptMedecines = receiptMedicine,
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

        private void btnAddMedicine_Click(object sender, RoutedEventArgs e)
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
                    MedicineViewModel medicine = (MedicineViewModel)dgReceiptMedicine.SelectedCells[0].Item;
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

    }
}
