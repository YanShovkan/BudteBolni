using PolyclinicBusinessLogic.BindingModels;
using PolyclinicBusinessLogic.BusinessLogics;
using PolyclinicBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Unity;

namespace PolyclinicProgramForPharmacist
{
    /// <summary>
    /// Логика взаимодействия для WindowPatientReport.xaml
    /// </summary>
    public partial class WindowPatientReport : Window
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        MedicineLogic logicM;
        PatientReportLogic logicP;
        List<MedicineViewModel> list = new List<MedicineViewModel>();
        public WindowPatientReport(PatientReportLogic _logicP, MedicineLogic _logicM)
        {
            InitializeComponent();
            logicM = _logicM;
            logicP = _logicP;
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                if (list != null)
                {
                    DataGridView.ItemsSource = list;
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<AddMedicine>();
            window.ShowDialog();
            if (window.DialogResult == true)
            {
                if (!list.Contains(logicM.Read(new MedicineBindingModel { Id = window.Id })[0]))
                {
                    list.Add(logicM.Read(new MedicineBindingModel { Id = window.Id })[0]);
                }
            }
            LoadData();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SaveToWord_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new SaveFileDialog { Filter = "docx|*.docx" })
            {
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    try
                    {
                        logicP.SaveToWordFile(dialog.FileName, list);
                        System.Windows.MessageBox.Show("Выполнено", "Успех", MessageBoxButton.OK,
                            MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        System.Windows.MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
                       MessageBoxImage.Error);
                    }
                }
            }
        }

        private void SaveToExcel_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new SaveFileDialog { Filter = "xlsx|*.xlsx" })
            {
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    try
                    {
                        logicP.SaveToExcelFile(dialog.FileName, list);
                        System.Windows.MessageBox.Show("Выполнено", "Успех", MessageBoxButton.OK,
                        MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        System.Windows.MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
                       MessageBoxImage.Error);
                    }
                }
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
