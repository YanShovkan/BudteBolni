using System;
using System.Windows;
using PolyclinicBusinessLogic.BusinessLogics;
using PolyclinicBusinessLogic.BindingModels;
using Unity;

namespace PolyclinicProgramForPharmacist
{
    /// <summary>
    /// Логика взаимодействия для Medicine.xaml
    /// </summary>
    public partial class Medicine : Window
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        MedicineLogic _logic;
        public int _pharmacistId { get; set; }
        public int Id { set { id = value; } }
        private int? id;

        public Medicine(MedicineLogic logic)
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
                    var view = _logic.Read(new MedicineBindingModel { Id = id })?[0];
                    if (view != null)
                    {
                        tbName.Text = view.Name;
                        tbActiveSubstance.Text = view.ActiveSubstance;
                        tbClassification.Text = view.Classification;
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

            if (string.IsNullOrEmpty(tbActiveSubstance.Text))
            {
                MessageBox.Show("Заполните активное вещество", "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrEmpty(tbClassification.Text))
            {
                MessageBox.Show("Заполните классификацию", "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
                return;
            }
            try
            {
                _logic.CreateOrUpdate(new MedicineBindingModel
                {
                    Id = id,
                    Name = tbName.Text,
                    ActiveSubstance = tbActiveSubstance.Text,
                    Classification = tbClassification.Text,
                    PharmacistId = _pharmacistId
                    
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
