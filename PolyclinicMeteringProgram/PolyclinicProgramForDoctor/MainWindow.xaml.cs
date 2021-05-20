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

        private void btnTreatments_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<Treatments>();
            window.Show();
        }

        private void btnProcedures_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<Procedures>();
            window.Show();
        }

        private void btnPatients_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<Patients>();
            window._doctorId = _doctorId;
            window.Show();
        }

        private void btnReceiptReport_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<WindowReceiptReport>();
            window.Show();
        }

        private void btnPatientReport_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<WindowReportPatientReceipt>();
            window._doctorId = _doctorId;
            window.Show();
        }

        private void btnGraph_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<Statistic>();
            window.Show();
        }

        private void btnAccount_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<Doctor>();
            window.Id = _doctorId;
            window.Show();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<WelcomeWindow>();
            MessageBoxResult result = MessageBox.Show("Выйти из учетной записи?", "Вопрос", MessageBoxButton.YesNo,
              MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                window.Show();
                Close();
            }
        }
    }
}
