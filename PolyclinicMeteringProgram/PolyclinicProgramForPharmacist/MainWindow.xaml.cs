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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        public int _pharmacistId { get; set; }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void miMedicine_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<Medicines>();
            window._pharmacistId = _pharmacistId;
            window.Show();
        }

        private void miReceipt_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<Receipts>();
            window.Show();
        }

        private void miPrescription_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<Prescriptions>();
            window.Show();
        }

        private void miGetList_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<WindowPatientReport>();
            window.Show();
        }

        private void miGetReport_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<WindowReportProcedureReceipt>();
            window._pharmacistId = _pharmacistId;
            window.Show();
        }
    }
}
