using PolyclinicBusinessLogic.BindingModels;
using PolyclinicBusinessLogic.BusinessLogics;
using PolyclinicBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using Unity;

namespace PolyclinicMeteringProgram
{
    /// <summary>
    /// Логика взаимодействия для Procedure.xaml
    /// </summary>
    public partial class Procedure : Window
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        ProcedureLogic _logic;
        public int _doctorId { get; set; }
        public int Id { set { id = value; } }
        private int? id;
        private Dictionary<int, (string, int)> procedureMedicines;
        private Dictionary<int, (string, int)> procedureTreatments;

        public Procedure(ProcedureLogic logic)
        {
            InitializeComponent();
            _logic = logic;
        }

        private void LoadData()
        {
            try
            {
                if (procedureMedicines != null)
                {
                    List<ProcedureMedicineViewModel> list = new List<ProcedureMedicineViewModel>();
                    foreach (var medicine in procedureMedicines)
                    {
                        list.Add(new ProcedureMedicineViewModel { Id = medicine.Key, MedicineName = medicine.Value.Item1, MedicineCount = medicine.Value.Item2 });
                    }
                    DataGridMedicines.ItemsSource = list;
                    DataGridMedicines.Columns[0].Visibility = Visibility.Hidden;
                }
                if (procedureTreatments != null)
                {
                    List<ProcedureTreatmentViewModel> list = new List<ProcedureTreatmentViewModel>();
                    foreach (var treatment in procedureTreatments)
                    {
                        list.Add(new ProcedureTreatmentViewModel { Id = treatment.Key, TreatmentName = treatment.Value.Item1, TreatmentCount = treatment.Value.Item2 });
                    }
                    DataGridTreatments.ItemsSource = list;
                    DataGridTreatments.Columns[0].Visibility = Visibility.Hidden;
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
                    ProcedureViewModel view = _logic.Read(new ProcedureBindingModel
                    {
                        Id = id.Value
                    })?[0];
                    if (view != null)
                    {
                        tbName.Text = view.Name;
                        tbCost.Text = view.Cost.ToString();
                        procedureMedicines = view.ProcedureMedicines;
                        procedureTreatments = view.ProcedureTreatments;
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
                procedureMedicines = new Dictionary<int, (string, int)>();
                procedureTreatments = new Dictionary<int, (string, int)>();
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(tbCost.Text))
            {
                MessageBox.Show("Заполните стоимость", "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
                return;
            }
            try
            {
                _logic.CreateOrUpdate(new ProcedureBindingModel
                {
                    Id = id,
                    Name = tbName.Text,
                    Cost = Convert.ToInt32(tbCost.Text),
                    ProcedureTreatments = procedureTreatments,
                    ProcedureMedicines = procedureMedicines
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
                if (!procedureMedicines.ContainsKey(window.Id))
                {
                    procedureMedicines.Add(window.Id, (window.MediceineName, window.MediceineCount));
                }
                else
                {
                    procedureMedicines[window.Id] = (window.MediceineName, window.MediceineCount);
                }

            }
            LoadData();
        }

        private void btnDeleteMedicine_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridMedicines.SelectedIndex != -1)
            {
                MessageBoxResult result = MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButton.YesNo,
               MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    ProcedureMedicineViewModel medicine = (ProcedureMedicineViewModel)DataGridMedicines.SelectedCells[0].Item;
                    try
                    {
                        procedureMedicines.Remove(medicine.Id);
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

        private void btnAddTreatment_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<AddTreatment>();
            window.ShowDialog();
            if (window.DialogResult == true)
            {
                if (!procedureTreatments.ContainsKey(window.Id))
                {
                    procedureTreatments.Add(window.Id, (window.TreatmentName, window.TreatmentCount));
                }
                else
                {
                    procedureTreatments[window.Id] = (window.TreatmentName, window.TreatmentCount);
                }

            }
            LoadData();
        }

        private void btnDeleteTreatment_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridTreatments.SelectedIndex != -1)
            {
                MessageBoxResult result = MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButton.YesNo,
               MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    ProcedureTreatmentViewModel treatment = (ProcedureTreatmentViewModel)DataGridTreatments.SelectedCells[0].Item;
                    try
                    {
                        procedureTreatments.Remove(treatment.Id);
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
