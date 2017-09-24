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
using FrameWorkDB.V1;
using Microsoft.EntityFrameworkCore;

namespace AgileFileManagerv1.FloatWindows.FW_File.Controller
{
    /// <summary>
    /// Interaction logic for FW_File.xaml
    /// </summary>
    public partial class FW_File : Window
    {
        File file;
        public List<Report> reports;
        public List<Intervention> interventions;
        AgileManagerDB db;
        public FW_File(File file)
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(EV_Start);

            this.file = file;
            db = new AgileManagerDB();
            reports = db.Reports.Where(r => r.FileID == file.FileID).Include(r => r.employee).ToList();
            interventions = db.Interventions.Where(r => r.FileID == file.FileID).Include(i => i.employee).ToList();
            FrameWorkDB.V1.Application app = db.Applications.First(a => a.ApplicationID == file.license.ApplicationID);

            TX_License1.Text = app.Name;
            TX_License2.Text = $"Versión {app.Version}";
            TX_License4.Text = $"Fin mantenimiento {String.Format("{0:dd/MM/yyyy}", file.license.DateEnd)}";

            if(file.employee!= null)
                TB_Employee.Text = $"{file.employee.Code} - {file.employee.Name}";

            TB_State.Text = $"{file.state.Name}";
            TB_DateStart.Text = $"{String.Format("{0:dd/MM/yyyy}", file.DateStart)}";
            TB_DateEnd.Text = $"{String.Format("{0:dd/MM/yyyy}", file.DateEnd)}";
            TB_Issue.Text = $"{file.issue.Name}";
            TB_Priority.Text = $"{file.priority.Name}";
        }

        private void EV_Start(object sender, RoutedEventArgs e)
        {
            CreateReports();
            CreateInterventions();
        }

        private void CreateReports()
        {
            int num = reports.Count;

            for (int i = 0; i < num; i++)
            {
                StackPanel panel = new StackPanel();
                Label label = new Label();
                label.Content = $"Reporte de {reports[i].employee.Code} {reports[i].employee.Name} {reports[i].Date}";
                TextBox text = new TextBox();
                text.MinHeight = 150;
                text.TextWrapping = TextWrapping.Wrap;
                text.AcceptsReturn = true;
                text.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;
                text.IsReadOnly = true;
                text.Text = $"{reports[i].Description}";

                panel.Children.Add(label);
                panel.Children.Add(text);
                SP_Reports.Children.Add(panel);
            }
        }

        private void CreateInterventions()
        {
            int num = interventions.Count;

            for (int i = 0; i < num; i++)
            {
                StackPanel panel = new StackPanel();
                Label label = new Label();
                label.Content = $"Intervención de {interventions[i].employee.Code} {interventions[i].employee.Name} {interventions[i].Date}";
                TextBox text = new TextBox();
                text.MinHeight = 150;
                text.TextWrapping = TextWrapping.Wrap;
                text.AcceptsReturn = true;
                text.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;
                text.IsReadOnly = true;
                text.Text = $"{interventions[i].Description}";

                panel.Children.Add(label);
                panel.Children.Add(text);
                SP_Interventions.Children.Add(panel);
            }
        }
    }
}
