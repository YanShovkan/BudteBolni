using PolyclinicBusinessLogic.BusinessLogics;
using System;
using Unity;
using System.Windows;

namespace PolyclinicMeteringProgram
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Enter : Window
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        private readonly DoctorLogic logic;
        public Enter(DoctorLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
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
            try
            {
                int doctorId = logic.CheckPassword(tbUserName.Text, tbPassword.Text);
                var window = Container.Resolve<MainWindow>();
                window._doctorId = doctorId;
                window.Show();
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
            var window = Container.Resolve<WelcomeWindow>();
            window.Show();
            Close();
        }
    }
}
