﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using FrameWorkDB.V1;

namespace AgileFileManagerv1.WorkingBoard.Nodes.CallIn.View
{
    /// <summary>
    /// Interaction logic for MC_WB_CallIn_Report.xaml
    /// </summary>
    public partial class MC_WB_CallIn_Report : Page
    {
        public MC_WB_CallIn_Report()
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(EV_Start);

            TX_License1.Text = GetController().file.license.application.Name;
            TX_License2.Text = $"Versión {GetController().file.license.application.Version}";
            TX_License4.Text = $"Fin mantenimiento {String.Format("{0:dd/MM/yyyy}", GetController().file.license.DateEnd)}";
            DP_DateStart.SelectedDate = DateTime.Today;

            CB_Issues.SelectionChanged += new SelectionChangedEventHandler(EV_Issue);
            CB_Priorities.SelectionChanged += new SelectionChangedEventHandler(EV_Priority);
            CB_Employees.SelectionChanged += new SelectionChangedEventHandler(EV_Employee);
        }

        private void EV_Start(object sender, RoutedEventArgs e)
        {
            List<Issue> issues = GetController().GetIssues().OrderBy(i=>i.Code.ToString()).ToList();

            foreach (Issue issue in issues)
            {
                ComboBoxItem temp = new ComboBoxItem();
                temp.Content = $"{issue.Code} - {issue.Name} ({issue.department.Name})";
                temp.Name = $"issue{issue.IssueID}";
                CB_Issues.Items.Add(temp);
            }

            List<Priority> priorities = GetController().GetPriorities();

            foreach (Priority priority in priorities.Take(4))
            {
                ComboBoxItem temp = new ComboBoxItem();
                temp.Content = $"{priority.Name}";
                temp.Name = $"priority{priority.PriorityID}";
                CB_Priorities.Items.Add(temp);
            }

            List<Employee> employees = GetController().GetEmployees();

            ComboBoxItem temp1 = new ComboBoxItem();
            temp1.Content = "";
            temp1.Name = $"employee0";
            CB_Employees.Items.Add(temp1);

            int count = 1;
            foreach (Employee employee in employees)
            {
                ComboBoxItem temp = new ComboBoxItem();
                temp.Content = $"{employee.Code} - {employee.Name}";
                temp.Name = $"employee{employee.EmployeeID}";
                CB_Employees.Items.Add(temp);
                if (employee.EmployeeID == ((MainWindow)System.Windows.Application.Current.MainWindow).employee.EmployeeID)
                {
                    CB_Employees.SelectedIndex = count;
                }
                count++;
            }

            CreateReports();
            CreateInterventions();
        }

        private void EV_Priority(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem temp1 = (ComboBoxItem)CB_Priorities.SelectedItem;
            if (CB_Priorities.SelectedIndex >= 0)
            {
                GetController().SetPriority(Convert.ToInt32(temp1.Name.Replace("priority", "")));
            }
        }

        private void EV_Issue(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem temp1 = (ComboBoxItem)CB_Issues.SelectedItem;
            if (CB_Issues.SelectedIndex >= 0)
            {
                GetController().SetIssue(Convert.ToInt32(temp1.Name.Replace("issue", "")));
            }
        }

        private void EV_Employee(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem temp1 = (ComboBoxItem)CB_Employees.SelectedItem;
            if (CB_Employees.SelectedIndex >= 0)
            {
                GetController().SetEmployee(Convert.ToInt32(temp1.Name.Replace("employee", "")));
            }
        }

        public void DateStart_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var picker = sender as DatePicker;

            DateTime? date = picker.SelectedDate;
            if (date == null)
            {
                this.Title = "Sin fecha";
                GetController().file.DateStart = null;
            }
            else
            {
                this.Title = date.Value.ToShortDateString();
                GetController().file.DateStart = date.Value;
            }
        }

        private void CreateReports()
        {
            int num = GetController().reports.Count;

            for(int i=0; i<num; i++)
            {
                StackPanel panel = new StackPanel();
                Label label = new Label();
                label.Content = $"Reporte de {((MainWindow)System.Windows.Application.Current.MainWindow).employee.Code} - {((MainWindow)System.Windows.Application.Current.MainWindow).employee.Name} " +
                    $"{ GetController().reports[i].Date}";
                TextBox text = new TextBox();
                text.MinHeight = 150;
                text.TextWrapping = TextWrapping.Wrap;
                text.AcceptsReturn = true;
                text.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;
                text.Tag = i;
                text.KeyUp += new KeyEventHandler(EV_Reports);
                text.Text = $"{GetController().reports[i].Description}";

                panel.Children.Add(label);
                panel.Children.Add(text);
                SP_Reports.Children.Add(panel);
            }
        }

        private void EV_Reports(object sender, KeyEventArgs e)
        {
            GetController().reports[Convert.ToInt32((sender as TextBox).Tag)].Description = (sender as TextBox).Text;
            GetController().EV_TS_Update();
        }

        private void CreateInterventions()
        {
            int num = GetController().interventions.Count;

            for (int i = 0; i < num; i++)
            {
                StackPanel panel = new StackPanel();
                Label label = new Label();
                label.Content = $"Intervención de {((MainWindow)System.Windows.Application.Current.MainWindow).employee.Code} - {((MainWindow)System.Windows.Application.Current.MainWindow).employee.Name} " +
                        $"{ GetController().interventions[i].Date}";
                TextBox text = new TextBox();
                text.MinHeight = 150;
                text.TextWrapping = TextWrapping.Wrap;
                text.AcceptsReturn = true;
                text.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;
                text.Tag = i;
                text.KeyUp += new KeyEventHandler(EV_Interventions);
                text.Text = $"{GetController().interventions[i].Description}";

                panel.Children.Add(label);
                panel.Children.Add(text);
                SP_Interventions.Children.Add(panel);
            }
        }

        private void EV_Interventions(object sender, KeyEventArgs e)
        {
            GetController().interventions[Convert.ToInt32((sender as TextBox).Tag)].Description = (sender as TextBox).Text;
            GetController().EV_TS_Update();
        }

        private WorkingBoard.Nodes.CallIn.Controller.WB_CallInController GetController()
        {
            Window mainWindow = System.Windows.Application.Current.MainWindow;
            var a = (MainWindow)mainWindow;
            return (WorkingBoard.Nodes.CallIn.Controller.WB_CallInController)a.MainFrame.Content;
        }
    }
}
