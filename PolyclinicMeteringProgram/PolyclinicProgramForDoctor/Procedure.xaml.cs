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
        public int _doctorId { get; set; }
        public int Id { set { id = value; } }
        private int? id;
        private Dictionary<int,(string, int)> procedureMedicines;
        private Dictionary<int, string> procedureTreatments;
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
                    foreach (var proc in procedureMedicines)
                    {
                        DataGridMedicines.ItemsSource = procedureMedicines;
                    }
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
                        procedureMedicines = view.MedicineProcedures;
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
                procedureMedicines =  new Dictionary<int, (string, int)>();
                procedureTreatments = new Dictionary<int, string>();
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
                    MedicineProcedures = procedureMedicines
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
    }
}
