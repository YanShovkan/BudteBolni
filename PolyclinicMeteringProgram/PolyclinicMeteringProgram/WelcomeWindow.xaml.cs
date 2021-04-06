using System.Windows;
using Unity;

namespace PolyclinicMeteringProgram
{
    /// <summary>
    /// Логика взаимодействия для WelcomeWindow.xaml
    /// </summary>
    public partial class WelcomeWindow : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        public WelcomeWindow()
        {
            InitializeComponent();
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<Enter>();
            window.Show();
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<Register>();
            window.Show();
        }
    }
}
