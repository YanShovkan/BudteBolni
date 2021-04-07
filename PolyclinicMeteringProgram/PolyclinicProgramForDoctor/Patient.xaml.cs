﻿using System;
using System.Windows;
using PolyclinicBusinessLogic.BusinessLogics;
using PolyclinicBusinessLogic.BindingModels;
using PolyclinicBusinessLogic.ViewModels;
using Unity;
using System.Collections.Generic;

namespace PolyclinicMeteringProgram
{
    /// <summary>
    /// Логика взаимодействия для Patient.xaml
    /// </summary>
    public partial class Patient : Window
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        PatientLogic _logic;
        public int _doctorId { get; set; }
        public int Id { set { id = value; } }
        private int? id;
        private Dictionary<int, string> patientProsedures;
        public Patient(PatientLogic logic)
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
                    PatientViewModel view = _logic.Read(new PatientBindingModel
                    {
                        Id = id.Value
                    })?[0];
                    if (view != null)
                    {
                        tbFIO.Text = view.FullName;
                        tbTelephoneNumber.Text = view.PhoneNumber.ToString();
                        tbBirthday.SelectedDate = view.DateOfBirth;
                        patientProsedures = view.ProcedurePatients;
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
                   MessageBoxImage.Error);
                }
            }
            else
            {
                patientProsedures = new Dictionary<int, string>();
            }
        }

        private void LoadData()
        {
            try
            {
                if (patientProsedures != null)
                {
                    foreach (var proc in patientProsedures)
                    {
                        DataGridView.ItemsSource = patientProsedures;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbFIO.Text))
            {
                MessageBox.Show("Заполните ФИО", "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
                return;
            }
            if (tbBirthday.SelectedDate == null || tbBirthday.SelectedDate > DateTime.Now)
            {
                MessageBox.Show("Выберите дату рождения", "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
                return;
            }
            if (patientProsedures == null || patientProsedures.Count == 0)
            {
                MessageBox.Show("Заполните процедуры", "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
                return;
            }
            try
            {
                _logic.CreateOrUpdate(new PatientBindingModel
                {
                    Id = id,
                    FullName = tbFIO.Text,
                    DateOfBirth = tbBirthday.SelectedDate.Value,
                    PhoneNumber = tbTelephoneNumber.Text,
                    ProcedurePatients = patientProsedures,
                    DoctorId = _doctorId
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
            Close();
        }

        private void btnAddProcedure_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<AddProcedure>();
            window.ShowDialog();
            if (window.DialogResult == true)
            {
                if (!patientProsedures.ContainsKey(window.Id))

                    patientProsedures.Add(window.Id, window.Name);
            }
            LoadData();
        }


        private void btnDeleteProcedure_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnEditProcedure_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}