using System;
using System.Windows;
using PolyclinicBusinessLogic.BusinessLogics;
using PolyclinicBusinessLogic.BindingModels;
using PolyclinicBusinessLogic.ViewModels;
using Unity;

namespace PolyclinicMeteringProgram
{
    /// <summary>
    /// Логика взаимодействия для WindowReportPatientReceipt.xaml
    /// </summary>
    public partial class WindowReportPatientReceipt : Window
    {
        public new IUnityContainer Container { get; set; }
        ReportPatientReceiptLogic _logic;
        public int _doctorId { get; set; }

        public WindowReportPatientReceipt(ReportPatientReceiptLogic logic)
        {
            InitializeComponent();
            _logic = logic;
        }

        private void btnMail_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnShow_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
