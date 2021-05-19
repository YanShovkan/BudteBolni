using System;
using System.Windows;
using PolyclinicBusinessLogic.BusinessLogics;
using PolyclinicBusinessLogic.BindingModels;
using PolyclinicBusinessLogic.ViewModels;
using Unity;
using Microsoft.Reporting.WinForms;

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
        private void ReportViewer_Load(object sender, EventArgs e)
        {
            reportViewer.LocalReport.ReportPath = "../../ReportByDate.rdlc";
        }
        private void btnMail_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnShow_Click(object sender, RoutedEventArgs e)
        {
            if (dpFrom.SelectedDate == null)
            {
                MessageBox.Show("Выберите дату начала",
               "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (dpTo.SelectedDate == null)
            {
                MessageBox.Show("Выберите дату окончания",
               "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (dpFrom.SelectedDate >= dpTo.SelectedDate)
            {
                MessageBox.Show("Дата начала должна быть меньше даты окончания",
               "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                var dataSource = _logic.GetPatientReceipt(new ReportPatientReceiptBindingModel
                {
                    DoctorId = _doctorId,
                    DateFrom = dpFrom.SelectedDate,
                    DateTo = dpTo.SelectedDate
                });
                ReportDataSource source = new ReportDataSource("DataSetReportByDate", dataSource);
                reportViewer.LocalReport.DataSources.Add(source);
               
                reportViewer.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
            }
        }
    }
}
