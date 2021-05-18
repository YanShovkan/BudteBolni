using System;
using System.Windows;
using PolyclinicBusinessLogic.BusinessLogics;
using PolyclinicBusinessLogic.ViewModels;
using Unity;

namespace PolyclinicMeteringProgram
{
    /// <summary>
    /// Логика взаимодействия для AddMedicine.xaml
    /// </summary>
    public partial class AddMedicine : Window
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        MedicineLogic _logic;
        private MedicineViewModel medicineViewModel;
        public int Id
        {
            get
            {
                return medicineViewModel.Id;
            }
            set
            {
                cbMrdicineName.SelectedItem = value;
            }
        }
        public string MediceineName { get { return cbMrdicineName.Text; } }

        public int MediceineCount
        {
            get { return Convert.ToInt32(tbCount.Text); }
            set
            {
                tbCount.Text = value.ToString();
            }
        }

        public AddMedicine(MedicineLogic logic)
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
                    cbMrdicineName.DisplayMemberPath = "Name";
                    cbMrdicineName.ItemsSource = list;
                    cbMrdicineName.SelectedItem = null;
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
                if (cbMrdicineName.SelectedValue == null)
                {
                    MessageBox.Show("Выберите процедуру", "Ошибка", MessageBoxButton.OK,
                   MessageBoxImage.Error);
                    return;
                }
                if (tbCount.Text == null)
                {
                    MessageBox.Show("Введите количество лекарств", "Ошибка", MessageBoxButton.OK,
                   MessageBoxImage.Error);
                    return;
                }
                medicineViewModel = (MedicineViewModel)cbMrdicineName.SelectionBoxItem;
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
