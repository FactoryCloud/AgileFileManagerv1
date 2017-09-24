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

namespace AgileFileManagerv1.WorkingBoard.Nodes.NewFile.Controller
{
    /// <summary>
    /// Interaction logic for MainController.xaml
    /// </summary>
    public partial class WB_NewFileController : WorkingBoard.Controller.FileController
    {

        public WB_NewFileController(Client client)
        {
            InitializeComponent();
            Information = new Dictionary<string, int>();
            Information.Add("mode", 2);
            Information.Add("oldmode", 2);
            Information.Add("controller", 0);
            Information.Add("oldcontroller", 0);
            Information.Add("option", 3);

            db = new AgileManagerDB();
            
            file = new File();
            file.client = client;
            file.ClientID = client.ClientID;            

            reports = new List<Report>();
            interventions = new List<Intervention>();

            reports.Add(new Report
            {
                Date = DateTime.Now,
                EmployeeID = ((MainWindow)System.Windows.Application.Current.MainWindow).employee.EmployeeID,
                file = file,
                Description = ""
            });

            interventions.Add(new Intervention
            {
                Date = DateTime.Now,
                EmployeeID = ((MainWindow)System.Windows.Application.Current.MainWindow).employee.EmployeeID,
                file = file,
                Description = ""
            });

            licenses = db.Licenses.Where(c => c.ClientID == client.ClientID).Include(c=> c.application).ToList();

            dealer = client.dealer;

            this.Loaded += new RoutedEventHandler(EV_Start);
        }

        override public void EV_TS_Update()
        {
            TS_Page = new WorkingBoard.Nodes.NewFile.View.TS_WB_NewFile_Main();
            LeftSide.Content = TS_Page;
        }

        private void EV_Start(object sender, RoutedEventArgs e)
        {
            UpdateComponents();
        }

        public void SaveFile()
        {
            file.Code = $"{String.Format("{0:YY}", file.DateStart)}\\{file.client.Code}\\{db.Files.Where(f => f.ClientID == file.ClientID).ToList().Count + 1}";
            file.client = null;
            if (file.EmployeeID == null)
                file.StateID = db.States.First(s => s.Name == "Pendiente").StateID;
            else
                file.StateID = db.States.First(s => s.Name == "Por Terminar").StateID;

            
            db.Files.Add(file);
            db.Reports.Add(reports.Last());
            db.Interventions.Add(interventions.Last());

            db.SaveChanges();
            CT_WB();
        }

        public void FinishFile()
        {
            file.Code = $"{String.Format("{0:YY}", file.DateStart)}\\{file.client.Code}\\{db.Files.Where(f => f.ClientID == file.ClientID).ToList().Count + 1}";
            file.client = null;
            file.StateID = db.States.First(s => s.Name == "Terminado").StateID;
            file.DateEnd = DateTime.Today;
            

            db.Files.Add(file);
            db.Reports.Add(reports.Last());
            db.Interventions.Add(interventions.Last());

            db.SaveChanges();
            CT_WB();
        }

        override public void UpdateComponents()
        {
            switch(Information["mode"])
            {
                case 0:
                    NV_Page = new WorkingBoard.Nodes.NewFile.View.NV_WB_NewFile_Main();
                    TS_Page = new WorkingBoard.Nodes.NewFile.View.TS_WB_NewFile_Main();
                    MC_Page = new WorkingBoard.Nodes.Common.MC_WB_Common_Client();
                    ChangeComponents();
                    break;

                case 1:
                    NV_Page = new WorkingBoard.Nodes.NewFile.View.NV_WB_NewFile_Main();
                    TS_Page = new WorkingBoard.Nodes.NewFile.View.TS_WB_NewFile_Main();
                    MC_Page = new WorkingBoard.Nodes.Common.MC_WB_Common_Dealer();
                    ChangeComponents();
                    break;

                case 2:
                    NV_Page = new WorkingBoard.Nodes.NewFile.View.NV_WB_NewFile_Main();
                    TS_Page = new WorkingBoard.Nodes.NewFile.View.TS_WB_NewFile_Main();
                    MC_Page = new WorkingBoard.Nodes.Common.MC_WB_Common_Licenses();
                    ChangeComponents();
                    break;

                case 3:
                    NV_Page = new WorkingBoard.Nodes.NewFile.View.NV_WB_NewFile_Main();
                    TS_Page = new WorkingBoard.Nodes.NewFile.View.TS_WB_NewFile_Main();
                    MC_Page = new WorkingBoard.Nodes.NewFile.View.MC_WB_NewFile_Report();
                    ChangeComponents();
                    break;

                case 4:
                    NV_Page = new WorkingBoard.Nodes.NewFile.View.NV_WB_NewFile_Main();
                    TS_Page = new WorkingBoard.Nodes.NewFile.View.TS_WB_NewFile_Main();
                    MC_Page = new WorkingBoard.Nodes.Common.MC_WB_Common_Files();
                    ChangeComponents();
                    break;
            }
        }
    }
}
