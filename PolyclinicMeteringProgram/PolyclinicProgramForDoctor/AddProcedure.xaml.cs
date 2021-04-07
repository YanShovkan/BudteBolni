using System;
using System.Windows;
using PolyclinicBusinessLogic.BusinessLogics;
using PolyclinicBusinessLogic.BindingModels;
using PolyclinicBusinessLogic.ViewModels;
using Unity;
using System.Collections.Generic;


namespace PolyclinicMeteringProgram
{
    /// <summary>
    /// Логика взаимодействия для AddProcedure.xaml
    /// </summary>
    public partial class AddProcedure : Window
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        ProcedureLogic _logic;

        public int Id { 
            get 
            {
                return cbProcedureName.SelectedIndex;
            }
            set
            {
                cbProcedureName.SelectedItem = value;
            }
        }
        public string ProcedureName { get { return cbProcedureName.Text; } }

        private void Window_loaded(object sender, RoutedEventArgs e)
        {
            var list = _logic.Read(null);
            if (list.Count > 0)
            {
                try
                {

                    cbProcedureName.ItemsSource = list;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
                   MessageBoxImage.Error);
                }
            }
        }
        public AddProcedure(ProcedureLogic logic)
        {
            InitializeComponent();
            _logic = logic;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbProcedureName.SelectedValue == null)
                {
                    MessageBox.Show("Выберите процедуру", "Ошибка", MessageBoxButton.OK,
                   MessageBoxImage.Error);
                    return;
                }
                this.DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
