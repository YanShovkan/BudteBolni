using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Forms;
using PolyclinicBusinessLogic.BusinessLogics;
using PolyclinicBusinessLogic.ViewModels;
using PolyclinicBusinessLogic.BindingModels;
using Unity;

namespace PolyclinicMeteringProgram
{
    public partial class WindowReceiptReport : Window
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        ProcedureLogic logicP;
        ReceiptReportLogic logicR;
        List<ProcedureViewModel> list = new List<ProcedureViewModel>();
        public WindowReceiptReport(ReceiptReportLogic _logicR, ProcedureLogic _logicP)
        {
            InitializeComponent();
            logicR = _logicR;
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
            var window = Container.Resolve<AddProcedure>();
            window.ShowDialog();
            if (window.DialogResult == true)
            {
                if (!list.Contains(logicP.Read(new ProcedureBindingModel { Id = window.Id })[0]))
                {
                    list.Add(logicP.Read(new ProcedureBindingModel { Id = window.Id })[0]);
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
                        logicR.SaveToWordFile(dialog.FileName, list);
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
                        logicR.SaveToExcelFile(dialog.FileName, list);
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
