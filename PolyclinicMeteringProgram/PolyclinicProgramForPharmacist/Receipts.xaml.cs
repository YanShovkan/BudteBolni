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
    /// Логика взаимодействия для Receipts.xaml
    /// </summary>
    public partial class Receipts : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }
        public Receipts()
        {
            InitializeComponent();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<Receipt>();
            window.Show();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
