using PolyclinicBusinessLogic.BindingModels;
using PolyclinicBusinessLogic.BusinessLogics;
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
    /// Логика взаимодействия для Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        public int Id { set { id = value; } }
        private int? id;
        private readonly PharmacistLogic logic;
        public Register(PharmacistLogic logic)
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
            if (string.IsNullOrEmpty(tbPhoneNumber.Text))
            {
                MessageBox.Show("Введите номер телефона", "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
                return;
            }
            if (!cbTermsOfUse.IsChecked.Value)
            {
                MessageBox.Show("Примите пользовательсое соглашение", "Ошибка", MessageBoxButton.OK,
              MessageBoxImage.Error);
                return;
            }
            try
            {
                logic.CreateOrUpdate(new PharmacistBindingModel
                {
                    Id = id,
                    FullName = tbUserName.Text,
                    Password = tbPassword.Text,
                    PhoneNumber = tbPhoneNumber.Text
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение",
               MessageBoxButton.OK, MessageBoxImage.Information);

                var window = Container.Resolve<WelcomeWindow>();
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
