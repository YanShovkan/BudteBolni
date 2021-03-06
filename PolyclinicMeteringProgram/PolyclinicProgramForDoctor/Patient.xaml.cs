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
    /// Логика взаимодействия для Patient.xaml
    /// </summary>
    public partial class Patient : Window
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        PatientLogic _logic;
        public int _doctorId { get; set; }
        public int Id { set { id = value; } }
        private int? id;
        private Dictionary<int, string> patientProsedures;

        public Patient(PatientLogic logic)
        {
            InitializeComponent();
            _logic = logic;
        }

        private void Window_loaded(object sender, RoutedEventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    PatientViewModel view = _logic.Read(new PatientBindingModel
                    {
                        Id = id.Value
                    })?[0];
                    if (view != null)
                    {
                        tbFIO.Text = view.FullName;
                        tbTelephoneNumber.Text = view.PhoneNumber;
                        tbBirthday.SelectedDate = view.DateOfBirth;
                        patientProsedures = view.PatientProcedures;
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
                patientProsedures = new Dictionary<int, string>();
            }
        }

        private void LoadData()
        {
            try
            {
                if (patientProsedures != null)
                {
                    List<PatientProcedureViewModel> list = new List<PatientProcedureViewModel>();
                    foreach (var procedure in patientProsedures)
                    {
                        list.Add(new PatientProcedureViewModel { Id = procedure.Key, ProcedureName = procedure.Value });
                    }
                    DataGridView.ItemsSource = list;
                    DataGridView.Columns[0].Visibility = Visibility.Hidden;
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
            if (string.IsNullOrEmpty(tbFIO.Text))
            {
                MessageBox.Show("Заполните ФИО", "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
                return;
            }
            if (tbBirthday.SelectedDate == null || tbBirthday.SelectedDate > DateTime.Now)
            {
                MessageBox.Show("Выберите дату рождения", "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
                return;
            }
            try
            {
                _logic.CreateOrUpdate(new PatientBindingModel
                {
                    Id = id,
                    FullName = tbFIO.Text,
                    DateOfBirth = tbBirthday.SelectedDate.Value,
                    PhoneNumber = tbTelephoneNumber.Text,
                    PatientProcedures = patientProsedures,
                    DoctorId = _doctorId
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

        private void btnAddProcedure_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<AddProcedure>();
            window.ShowDialog();
            if (window.DialogResult == true)
            {
                if (!patientProsedures.ContainsKey(window.Id))
                {
                    patientProsedures.Add(window.Id, window.ProcedureName);
                }

            }
            LoadData();
        }


        private void btnDeleteProcedure_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridView.SelectedIndex != -1)
            {
                MessageBoxResult result = MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButton.YesNo,
               MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    PatientProcedureViewModel procedure = (PatientProcedureViewModel)DataGridView.SelectedCells[0].Item;
                    try
                    {
                        patientProsedures.Remove(procedure.Id);
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