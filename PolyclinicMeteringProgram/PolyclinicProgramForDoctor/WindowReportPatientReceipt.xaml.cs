using System;
using System.Windows;
using PolyclinicBusinessLogic.BusinessLogics;
using PolyclinicBusinessLogic.BindingModels;
using PolyclinicBusinessLogic.ViewModels;
using Unity;
using Microsoft.Reporting.WinForms;
using System.Windows.Forms;
using MessageBox = System.Windows.MessageBox;
using System.Net.Mail;

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
            MailMessage msg = new MailMessage();
            SmtpClient client = new SmtpClient();
            try
            {
                string basis = "Отчет по пациентам";
                msg.Subject = basis;
                msg.Body = basis + " c " + dpFrom.SelectedDate.Value.ToShortDateString() +
                " по " + dpTo.SelectedDate.Value.ToShortDateString();

                msg.From = new MailAddress("shovkanyanforlab@gmail.com");
                msg.To.Add(TextBoxEmail.Text);
                msg.IsBodyHtml = true;

                _logic.SaveToPdfFile(new ReportPatientReceiptBindingModel
                {
                    DoctorId = _doctorId,
                    FileName = "D:\\Otchet.pdf",
                    DateFrom = dpFrom.SelectedDate,
                    DateTo = dpTo.SelectedDate
                });

                Attachment attach = new Attachment("D:\\Otchet.pdf", MediaTypeNames.Application.Octet);
                ContentDisposition disposition = attach.ContentDisposition;

                //meta inf for mail
                disposition.CreationDate = System.IO.File.GetCreationTime("D:\\Otchet.pdf");
                disposition.ModificationDate = System.IO.File.GetLastWriteTime("D:\\Otchet.pdf");
                disposition.ReadDate = System.IO.File.GetLastAccessTime("D:\\Otchet.pdf");

                //conn
                msg.Attachments.Add(attach);
                client.Host = "smtp.gmail.com";
                NetworkCredential basicauthenticationinfo = new NetworkCredential("iamthewisdom8@gmail.com", "password");
                client.Port = int.Parse("587");
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = basicauthenticationinfo;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Send(msg);

                //success
                MessageBox.Show("Сообщение отправлено", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
                MessageBoxImage.Error);
            }

            using (var dialog = new SaveFileDialog { Filter = "pdf|*.pdf" })
            {
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    try
                    {
                        
                        MessageBox.Show("Выполнено", "Успех", MessageBoxButton.OK,
                        MessageBoxImage.Information);
                    }
                    
                }
            }
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
