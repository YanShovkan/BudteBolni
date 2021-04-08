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
using PolyclinicBusinessLogic.BusinessLogics;

namespace PolyclinicProgramForPharmacist
{
    /// <summary>
    /// Логика взаимодействия для WindowReportProcedureReceipt.xaml
    /// </summary>
    public partial class WindowReportProcedureReceipt : Window
    {
        public new IUnityContainer Container { get; set; }
        ReportProcedureReceiptLogic _logic;
        public int _doctorId { get; set; }

        public WindowReportProcedureReceipt(ReportProcedureReceiptLogic logic)
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
