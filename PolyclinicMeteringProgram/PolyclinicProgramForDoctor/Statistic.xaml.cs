﻿using PolyclinicBusinessLogic.BindingModels;
using PolyclinicBusinessLogic.BusinessLogics;
using PolyclinicBusinessLogic.ViewModels;
using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using Unity;
using LiveCharts;
using LiveCharts.Wpf;
using System.Collections.Generic;

namespace PolyclinicMeteringProgram
{
    /// <summary>
    /// Логика взаимодействия для Statistic.xaml
    /// </summary>
    public partial class Statistic : Window
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        ProcedureLogic _logic;

        private List<StatisticByProcedureViewModel> _procedures = new List<StatisticByProcedureViewModel>();

        public Statistic(ProcedureLogic logic)
        {
            InitializeComponent();
            _logic = logic;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            List<ProcedureViewModel> procedures = _logic.Read(null);
            foreach(var procedure in procedures)
            {
                _procedures.Add(new StatisticByProcedureViewModel { ProcedureName = procedure.Name, ProcedureCost = procedure.Cost });
            }
            DataGridView.ItemsSource = _procedures;
        }

        private void BuildGraph_Click(object sender, RoutedEventArgs e)
        {
            List<StatisticByProcedureViewModel> selection = new List<StatisticByProcedureViewModel>();
            if (DataGridView.SelectedItems.Count != 0)
            {
                foreach(var procedure in DataGridView.SelectedItems)
                {
                    selection.Add((StatisticByProcedureViewModel)procedure);
                }
            }
            else
            {
                selection = _procedures;
            }
            Build(selection);
        }

        private void Build(List<StatisticByProcedureViewModel> statistic)
        {
            SeriesCollection series = new SeriesCollection();
            List<string> proceduresName = new List<string>();
            ChartValues<int> procedureCost = new ChartValues<int>();

            foreach (var item in statistic)
            {
                proceduresName.Add(item.ProcedureName);
                procedureCost.Add(item.ProcedureCost);
            }

            Graph.AxisX.Clear();
            Graph.AxisX.Add(new Axis()

            {
                Title = "\nПроцедуры",
                Labels = proceduresName
            });

            LineSeries procedureLine = new LineSeries
            {
                Title = "Стоимость: ",
                Values = procedureCost
            };

            series.Add(procedureLine);
            Graph.Series = series;
        }

        private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string displayName = GetPropertyDisplayName(e.PropertyDescriptor);
            if (!string.IsNullOrEmpty(displayName))
            {
                e.Column.Header = displayName;
            }
        }

        public static string GetPropertyDisplayName(object descriptor)
        {
            PropertyDescriptor pd = descriptor as PropertyDescriptor;
            if (pd != null)
            {
                DisplayNameAttribute displayName = pd.Attributes[typeof(DisplayNameAttribute)] as DisplayNameAttribute;
                if (displayName != null && displayName != DisplayNameAttribute.Default)
                {
                    return displayName.DisplayName;
                }
            }
            else
            {
                PropertyInfo pi = descriptor as PropertyInfo;
                if (pi != null)
                {
                    Object[] attributes = pi.GetCustomAttributes(typeof(DisplayNameAttribute), true);
                    for (int i = 0; i < attributes.Length; ++i)
                    {
                        DisplayNameAttribute displayName = attributes[i] as DisplayNameAttribute;
                        if (displayName != null && displayName != DisplayNameAttribute.Default)
                        {
                            return displayName.DisplayName;
                        }
                    }
                }
            }
            return null;
        }
    }
}
