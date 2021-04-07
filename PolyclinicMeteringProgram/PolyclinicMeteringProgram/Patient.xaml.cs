using System;
using System.Windows;
using PolyclinicBusinessLogic.BusinessLogics;
using PolyclinicBusinessLogic.BindingModels;
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
        public int Id { set { id = value; } }
        private int? id;

        public Patient(PatientLogic logic)
        {
            InitializeComponent();
            _logic = logic;
        }

        private void btnAddProcedure_Click(object sender, RoutedEventArgs e)
        {

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
                    PhoneNumber = Convert.ToInt32(tbTelephoneNumber.Text)
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
