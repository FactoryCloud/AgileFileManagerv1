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
using System.Windows.Navigation;
using System.Windows.Shapes;
using FrameWorkDB.V1;
using Microsoft.EntityFrameworkCore;

namespace AgileFileManagerv1.WorkingBoard.Nodes.CallIn.Controller
{
    /// <summary>
    /// Interaction logic for MainController.xaml
    /// </summary>
    public partial class WB_CallInController : WorkingBoard.Controller.FileController
    {
        public WB_CallInController(Client client)
        {
            InitializeComponent();
            Information = new Dictionary<string, int>();
            Information.Add("mode", 2);
            Information.Add("oldmode", 2);
            Information.Add("controller", 0);
            Information.Add("oldcontroller", 0);
            Information.Add("option", 3);

            db = new AgileManagerDB();
            reports = new List<Report>();
            file = new File();
            file.client = client;
            file.ClientID = client.ClientID;
            interventions = new List<Intervention>();
            licenses = db.Licenses.Where(c => c.ClientID == client.ClientID).Include(c=> c.application).ToList();

            dealer = client.dealer;

            this.Loaded += new RoutedEventHandler(EV_Start);
        }

        public void EV_TS_Update()
        {
            TS_Page = new WorkingBoard.Nodes.CallIn.View.TS_WB_CallIn_Main();
            LeftSide.Content = TS_Page;
        }

        private void EV_Start (object sender, RoutedEventArgs e)
        {
            UpdateComponents();
        }

        public void SetLicense(int number)
        {
            file.license = db.Licenses.First(l => l.LicenseID == number);
            file.LicenseID = file.license.LicenseID;
        }

        public void SetIssue(int number)
        {
            file.issue = db.Issues.First(i => i.IssueID == number);
            file.IssueID = file.issue.IssueID;
            EV_TS_Update();
        }

        public void SetPriority(int number)
        {
            file.priority = db.Priorities.First(i => i.PriorityID == number);
            file.PriorityID = file.priority.PriorityID;
            EV_TS_Update();
        }

        public List<Issue> GetIssues ()
        {
            return db.Issues.Include(i => i.department).ToList();
        }

        public List<Priority> GetPriorities()
        {
            return db.Priorities.ToList();
        }

        public void SaveFile()
        {
            file.client = null;
            if (interventions.Count == 0)
                file.StateID = db.States.First(s => s.Name == "Pendiente").StateID;
            else
            {
                file.StateID = db.States.First(s => s.Name == "En Progreso").StateID;
                file.EmployeeID = ((MainWindow)System.Windows.Application.Current.MainWindow).employee.EmployeeID;
            }
                
            db.Files.Add(file);
            db.Reports.AddRange(reports);
            db.Interventions.AddRange(interventions);

            db.SaveChanges();
            CT_WB();
        }

        public void FinishFile()
        {
            file.client = null;
            file.StateID = db.States.First(s => s.Name == "Terminado").StateID;
            file.EmployeeID = ((MainWindow)System.Windows.Application.Current.MainWindow).employee.EmployeeID;
            file.DateEnd = DateTime.Today;

            db.Files.Add(file);
            db.Reports.AddRange(reports);
            db.Interventions.AddRange(interventions);

            db.SaveChanges();
            CT_WB();
        }

        override public void UpdateComponents()
        {
            switch(Information["mode"])
            {
                case 0:
                    NV_Page = new WorkingBoard.Nodes.CallIn.View.NV_WB_CallIn_Main();
                    TS_Page = new WorkingBoard.Nodes.CallIn.View.TS_WB_CallIn_Main();
                    MC_Page = new WorkingBoard.Nodes.Common.MC_WB_Common_Client();
                    ChangeComponents();
                    break;

                case 1:
                    NV_Page = new WorkingBoard.Nodes.CallIn.View.NV_WB_CallIn_Main();
                    TS_Page = new WorkingBoard.Nodes.CallIn.View.TS_WB_CallIn_Main();
                    MC_Page = new WorkingBoard.Nodes.Common.MC_WB_Common_Dealer();
                    ChangeComponents();
                    break;

                case 2:
                    NV_Page = new WorkingBoard.Nodes.CallIn.View.NV_WB_CallIn_Main();
                    TS_Page = new WorkingBoard.Nodes.CallIn.View.TS_WB_CallIn_Main();
                    MC_Page = new WorkingBoard.Nodes.CallIn.View.MC_WB_CallIn_Licenses();
                    ChangeComponents();
                    break;

                case 3:
                    NV_Page = new WorkingBoard.Nodes.CallIn.View.NV_WB_CallIn_Main();
                    TS_Page = new WorkingBoard.Nodes.CallIn.View.TS_WB_CallIn_Main();
                    MC_Page = new WorkingBoard.Nodes.CallIn.View.MC_WB_CallIn_Report();
                    ChangeComponents();
                    break;

                case 4:
                    NV_Page = new WorkingBoard.Nodes.CallIn.View.NV_WB_CallIn_Main();
                    TS_Page = new WorkingBoard.Nodes.CallIn.View.TS_WB_CallIn_Main();
                    MC_Page = new WorkingBoard.Nodes.Common.MC_WB_Common_Files();
                    ChangeComponents();
                    break;
            }
        }
    }
}
