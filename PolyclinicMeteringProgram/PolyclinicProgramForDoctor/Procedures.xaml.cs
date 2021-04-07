using System;
using System.Windows;
using PolyclinicBusinessLogic.BusinessLogics;
using PolyclinicBusinessLogic.BindingModels;
using PolyclinicBusinessLogic.ViewModels;
using Unity;

namespace PolyclinicMeteringProgram
{
    /// <summary>
    /// Логика взаимодействия для Procedures.xaml
    /// </summary>
    public partial class Procedures : Window
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        ProcedureLogic _logic;
        public Procedures(ProcedureLogic logic)
        {
            InitializeComponent();
            _logic = logic;
            LoadData();
        }
        private void LoadData()
        {
            try
            {
                var list = _logic.Read(null);

                if (list != null)
                {
                    DataGridView.ItemsSource = list;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<Procedure>();
            window.ShowDialog();
            if (window.DialogResult == true)
            {
                LoadData();
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridView.SelectedIndex != -1)
            {
                MessageBoxResult result = MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButton.YesNo,
               MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    ProcedureViewModel treatment = (ProcedureViewModel)DataGridView.SelectedCells[0].Item;
                    int id = Convert.ToInt32(treatment.Id);
                    try
                    {
                        _logic.Delete(new ProcedureBindingModel { Id = id });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
                       MessageBoxImage.Error);
                    }
                    LoadData();
                }
            }
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridView.SelectedIndex != -1)
            {
                var window = Container.Resolve<Procedure>();
                ProcedureViewModel procedure = (ProcedureViewModel)DataGridView.SelectedCells[0].Item;
                window.Id = Convert.ToInt32(procedure.Id);
                window.ShowDialog();
                if (window.DialogResult == true)
                {
                    LoadData();
                }
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
