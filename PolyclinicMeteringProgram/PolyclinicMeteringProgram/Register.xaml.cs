using PolyclinicBusinessLogic.BindingModels;
using PolyclinicBusinessLogic.BusinessLogics;
using System;
using Unity;
using System.Windows;


namespace PolyclinicMeteringProgram
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class Register : Window
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        private readonly DoctorLogic logic;
        public Register(DoctorLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbUserName.Text))
            {
                MessageBox.Show("Введите имя пользователя", "Ошибка",
               MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(tbPassword.Text))
            {
                MessageBox.Show("Выберите пароль", "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(tbPosition.Text))
            {
                MessageBox.Show("Выберите должность", "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
                return;
            }
            if (cbTermsOfUse.IsChecked.Value)
            {
                MessageBox.Show("Примите пользовательсое соглашение", "Ошибка", MessageBoxButton.OK,
              MessageBoxImage.Error);
                return;
            }
            try
            {
                logic.CreateOrUpdate(new DoctorBindingModel
                {
                    FullName = tbUserName.Text,
                    Password = tbPassword.Text,
                    Position = tbPosition.Text
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение",
               MessageBoxButton.OK, MessageBoxImage.Information);
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

        }
    }
}
