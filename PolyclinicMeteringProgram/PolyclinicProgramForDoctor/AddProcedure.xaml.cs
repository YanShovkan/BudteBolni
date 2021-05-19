using System;
using System.Windows;
using PolyclinicBusinessLogic.BusinessLogics;
using PolyclinicBusinessLogic.ViewModels;
using Unity;

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
        private ProcedureViewModel procedureViewModel;
        public int Id { 
            get 
            {
                return procedureViewModel.Id;
            }
            set
            {
                cbProcedureName.SelectedItem = value;
            }
        }

        public string ProcedureName { get { return cbProcedureName.Text; } }
       
        public AddProcedure(ProcedureLogic logic)
        {
            InitializeComponent();
            _logic = logic;
        }

        private void Window_loaded(object sender, RoutedEventArgs e)
        {
            var list = _logic.Read(null);
            if (list.Count > 0)
            {
                try
                {
                    cbProcedureName.DisplayMemberPath = "Name";
                    cbProcedureName.ItemsSource = list;
                    cbProcedureName.SelectedItem = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
                   MessageBoxImage.Error);
                }
            }
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
                procedureViewModel = (ProcedureViewModel)cbProcedureName.SelectionBoxItem;
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
