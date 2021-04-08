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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Unity;

namespace PolyclinicProgramForPharmacist
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
                cbMedicineName.SelectedItem = value;
            }
        }

        public string MedicineName { get { return cbMedicineName.Text; } }

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
                    cbMedicineName.DisplayMemberPath = "Name";
                    cbMedicineName.ItemsSource = list;
                    cbMedicineName.SelectedItem = null;
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
                if (cbMedicineName.SelectedValue == null)
                {
                    MessageBox.Show("Выберите лекарство", "Ошибка", MessageBoxButton.OK,
                   MessageBoxImage.Error);
                    return;
                }
                medicineViewModel = (MedicineViewModel)cbMedicineName.SelectionBoxItem;
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
