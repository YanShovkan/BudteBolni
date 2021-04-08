using PolyclinicBusinessLogic.BusinessLogics;
using System;
using Unity;
using System.Windows;

namespace PolyclinicMeteringProgram
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        public int _doctorId { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void miTreatments_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<Treatments>();
            window.Show();
        }

        private void miProcedures_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<Procedures>();
            window.Show();
        }

        private void miPatients_Click(object sender, RoutedEventArgs e)
        {

            var window = Container.Resolve<Patients>();
            window._doctorId = _doctorId;
            window.Show();
        }

        private void miGetList_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<WindowReceiptReport>();
            window.Show();   
        }

        private void miGetReport_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<WindowReportPatientReceipt>();
            window._doctorId = _doctorId;
            window.Show();
        }
    }
}
