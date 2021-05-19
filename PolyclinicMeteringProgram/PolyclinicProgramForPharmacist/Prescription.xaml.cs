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
    /// Логика взаимодействия для Prescription.xaml
    /// </summary>
    public partial class Prescription : Window
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        PrescriptionLogic _logic;
        public int _doctorId { get; set; }
        public int Id { set { id = value; } }
        private int? id;
        private Dictionary<int, (string, int)> prescriptionMedicines;
        private Dictionary<int, (string, int)> prescriptionTreatments;

        public Prescription(PrescriptionLogic logic)
        {
            InitializeComponent();
            _logic = logic;
        }

        private void LoadData()
        {
            try
            {
                if (prescriptionMedicines != null)
                {
                    List<PrescriptionMedicineViewModel> list = new List<PrescriptionMedicineViewModel>();
                    foreach (var medicine in prescriptionMedicines)
                    {
                        list.Add(new PrescriptionMedicineViewModel { Id = medicine.Key, MedicineName = medicine.Value.Item1, MedicineCount = medicine.Value.Item2 });
                    }
                    dgMedicines.ItemsSource = list;
                    dgMedicines.Columns[0].Visibility = Visibility.Hidden;
                }
                if (prescriptionTreatments != null)
                {
                    List<PrescriptionTreatmentViewModel> list = new List<PrescriptionTreatmentViewModel>();
                    foreach (var treatment in prescriptionTreatments)
                    {
                        list.Add(new PrescriptionTreatmentViewModel { Id = treatment.Key, TreatmentName = treatment.Value.Item1, TreatmentCount = treatment.Value.Item2 });
                    }
                    dgTreatments.ItemsSource = list;
                    dgTreatments.Columns[0].Visibility = Visibility.Hidden;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
            }
        }

        private void Window_loaded(object sender, RoutedEventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    PrescriptionViewModel view = _logic.Read(new PrescriptionBindingModel
                    {
                        Id = id.Value
                    })?[0];
                    if (view != null)
                    {
                        tbFullNameDoctor.Text = view.FullNameDoctor;
                        tbPharmacyAddress.Text = view.PharmacyAddress;
                        prescriptionMedicines = view.PrescriptionMedicines;
                        prescriptionTreatments = view.PrescriptionTreatment;
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
                prescriptionMedicines = new Dictionary<int, (string, int)>();
                prescriptionTreatments = new Dictionary<int, (string, int)>();
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbFullNameDoctor.Text))
            {
                MessageBox.Show("Заполните имя", "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(tbPharmacyAddress.Text))
            {
                MessageBox.Show("Заполните адрес", "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
                return;
            }
            try
            {
                _logic.CreateOrUpdate(new PrescriptionBindingModel
                {
                    Id = id,
                    FullNameDoctor = tbFullNameDoctor.Text,
                    PharmacyAddress = tbPharmacyAddress.Text,
                    PrescriptionTreatment = prescriptionTreatments,
                    PrescriptionMedicines = prescriptionMedicines
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

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AddMedicine_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<AddMedicine>();
            window.ShowDialog();
            if (window.DialogResult == true)
            {
                if (!prescriptionMedicines.ContainsKey(window.Id))
                {
                    prescriptionMedicines.Add(window.Id, (window.MedicineName, window.Count));
                }

            }
            LoadData();
        }

        private void DeleteMedicine_Click(object sender, RoutedEventArgs e)
        {
            if (dgMedicines.SelectedIndex != -1)
            {
                MessageBoxResult result = MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButton.YesNo,
               MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    PrescriptionMedicineViewModel medicine = (PrescriptionMedicineViewModel)dgMedicines.SelectedCells[0].Item;
                    try
                    {
                        prescriptionMedicines.Remove(medicine.Id);
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

        private void AddTreatment_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<AddTreatment>();
            window.ShowDialog();
            if (window.DialogResult == true)
            {
                if (!prescriptionTreatments.ContainsKey(window.Id))
                {
                    prescriptionTreatments.Add(window.Id, (window.TreatmentName, window.TreatmentCount));
                }

            }
            LoadData();
        }

        private void DeleteTreatment_Click(object sender, RoutedEventArgs e)
        {
            if (dgTreatments.SelectedIndex != -1)
            {
                MessageBoxResult result = MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButton.YesNo,
               MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    PrescriptionTreatmentViewModel treatment = (PrescriptionTreatmentViewModel)dgTreatments.SelectedCells[0].Item;
                    try
                    {
                        prescriptionTreatments.Remove(treatment.Id);
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
