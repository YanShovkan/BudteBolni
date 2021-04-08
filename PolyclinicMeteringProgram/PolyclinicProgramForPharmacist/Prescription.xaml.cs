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
    /// Логика взаимодействия для Prescription.xaml
    /// </summary>
    public partial class Prescription : Window
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        public Prescription()
        {
            InitializeComponent();
        }

        private void AddMedicine_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<AddMedicine>();
            window.Show();
        }

        private void AddTreatment_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<AddTreatment>();
            window.Show();
        }
    }
}
