using System;
using System.Windows;
using PolyclinicBusinessLogic.BusinessLogics;
using PolyclinicBusinessLogic.BindingModels;
using Unity;

namespace PolyclinicMeteringProgram
{
    /// <summary>
    /// Логика взаимодействия для Treatment.xaml
    /// </summary>
    public partial class Treatment : Window
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        TreatmentLogic _logic;
        public int Id { set { id = value; } }
        private int? id;
        public Treatment(TreatmentLogic logic)
        {
            InitializeComponent();
            _logic = logic;
        }

        private void Window_loaded(object sender, RoutedEventArgs e)
        {
            if (id.HasValue)

            {
                try
                {
                    var view = _logic.Read(new TreatmentBindingModel { Id = id })?[0];
                    if (view != null)
                    {
                        tbName.Text = view.Name;
                        tbAreaOfAction.Text = view.AreaOfAction;
                        tbUrgency.Text = view.Urgency;
                    }
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
            if (string.IsNullOrEmpty(tbName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrEmpty(tbAreaOfAction.Text))
            {
                MessageBox.Show("Заполните область действия", "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrEmpty(tbUrgency.Text))
            {
                MessageBox.Show("Заполните срочность", "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
                return;
            }
            try
            {
                _logic.CreateOrUpdate(new TreatmentBindingModel
                {
                    Id = id,
                    Name = tbName.Text,
                    Urgency = tbUrgency.Text,
                    AreaOfAction = tbAreaOfAction.Text
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение",
               MessageBoxButton.OK, MessageBoxImage.Information);
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
            this.DialogResult = true;
            Close();
        }
    }
}
