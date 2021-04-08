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
    /// Логика взаимодействия для Prescriptions.xaml
    /// </summary>
    public partial class Prescriptions : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }
        public Prescriptions()
        {
            InitializeComponent();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<Prescription>();
            window.Show();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
