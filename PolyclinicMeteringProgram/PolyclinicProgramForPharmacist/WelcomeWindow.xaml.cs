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

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<Register>();
            window.Show();
            Close();
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<Enter>();
            window.Show();
            Close();
        }
    }
}
