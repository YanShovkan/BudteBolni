using System;
using System.Windows;
using PolyclinicBusinessLogic.BusinessLogics;
using PolyclinicBusinessLogic.BindingModels;
using PolyclinicBusinessLogic.ViewModels;
using Unity;
using System.Collections.Generic;

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
        MedicineLogic medicineLogic;
        TreatmentLogic treatmentLogic;
        public int _doctorId { get; set; }
        public int Id { set { id = value; } }
        private int? id;
        private Dictionary<int, (string, int)> procedureMedicines;
        private Dictionary<int, (string, int)> procedureTreatments;
        public Procedure(ProcedureLogic logic, MedicineLogic medicineLogic, TreatmentLogic treatmentLogic)
        {
            InitializeComponent();
            _logic = logic;
            this.medicineLogic = medicineLogic;
            this.treatmentLogic = treatmentLogic;
        }

        private void LoadData()
        {
            try
            {
                if (procedureMedicines != null)
                {
                    List<MedicineViewModel> list = new List<MedicineViewModel>();
                    foreach (var medicine in procedureMedicines)
                    {
                        list.Add(medicineLogic.Read(new MedicineBindingModel { Id = medicine.Key })?[0]);

                    }
                    DataGridMedicines.ItemsSource = list;
                }
                if (procedureTreatments != null)
                {
                    List<TreatmentViewModel> list = new List<TreatmentViewModel>();
                    foreach (var treatment in procedureTreatments)
                    {
                        list.Add(treatmentLogic.Read(new TreatmentBindingModel { Id = treatment.Key })?[0]);

                    }
                    DataGridTreatments.ItemsSource = list;
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
                    MedicineViewModel medicine = (MedicineViewModel)DataGridMedicines.SelectedCells[0].Item;
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
                    TreatmentViewModel treatment = (TreatmentViewModel)DataGridTreatments.SelectedCells[0].Item;
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
    }
}
