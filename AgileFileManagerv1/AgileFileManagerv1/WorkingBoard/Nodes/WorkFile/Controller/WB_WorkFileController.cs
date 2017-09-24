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

namespace AgileFileManagerv1.WorkingBoard.Nodes.WorkFile.Controller
{
    /// <summary>
    /// Interaction logic for MainController.xaml
    /// </summary>
    public partial class WB_WorkFileController : WorkingBoard.Controller.FileController
    {
        public WB_WorkFileController(File file)
        {
            InitializeComponent();
            Information = new Dictionary<string, int>();
            Information.Add("mode", 3);
            Information.Add("oldmode", 3);
            Information.Add("controller", 0);
            Information.Add("oldcontroller", 0);
            Information.Add("option", 4);

            db = new AgileManagerDB();
           
            this.file = new File();
            this.file = db.Files.Where(f => f.FileID == file.FileID)
                        .Include(f => f.client).Include(f=> f.client.dealer).Include(f => f.priority).Include(f => f.issue).First();
            reports = db.Reports.Where(r => r.FileID == file.FileID).ToList();
            interventions = db.Interventions.Where(r => r.FileID == file.FileID).ToList();
            licenses = db.Licenses.Where(c => c.ClientID == this.file.client.ClientID).Include(c=> c.application).ToList();

            dealer = this.file.client.dealer;

            this.Loaded += new RoutedEventHandler(EV_Start);
        }

        override public void EV_TS_Update()
        {
            TS_Page = new WorkingBoard.Nodes.WorkFile.View.TS_WB_WorkFile_Main();
            LeftSide.Content = TS_Page;
        }

        private void EV_Start(object sender, RoutedEventArgs e)
        {
            UpdateComponents();
        }

        public void SaveFile()
        {
            file.client = null;
            if (file.EmployeeID == null)
                file.StateID = db.States.First(s => s.Name == "Pendiente").StateID;
            else
                file.StateID = db.States.First(s => s.Name == "En Progreso").StateID;

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
                    NV_Page = new WorkingBoard.Nodes.WorkFile.View.NV_WB_WorkFile_Main();
                    TS_Page = new WorkingBoard.Nodes.WorkFile.View.TS_WB_WorkFile_Main();
                    MC_Page = new WorkingBoard.Nodes.Common.MC_WB_Common_Client();
                    ChangeComponents();
                    break;

                case 1:
                    NV_Page = new WorkingBoard.Nodes.WorkFile.View.NV_WB_WorkFile_Main();
                    TS_Page = new WorkingBoard.Nodes.WorkFile.View.TS_WB_WorkFile_Main();
                    MC_Page = new WorkingBoard.Nodes.Common.MC_WB_Common_Dealer();
                    ChangeComponents();
                    break;

                case 2:
                    NV_Page = new WorkingBoard.Nodes.WorkFile.View.NV_WB_WorkFile_Main();
                    TS_Page = new WorkingBoard.Nodes.WorkFile.View.TS_WB_WorkFile_Main();
                    MC_Page = new WorkingBoard.Nodes.Common.MC_WB_Common_Licenses();
                    ChangeComponents();
                    break;

                case 3:
                    NV_Page = new WorkingBoard.Nodes.WorkFile.View.NV_WB_WorkFile_Main();
                    TS_Page = new WorkingBoard.Nodes.WorkFile.View.TS_WB_WorkFile_Main();
                    MC_Page = new WorkingBoard.Nodes.WorkFile.View.MC_WB_WorkFile_Report();
                    ChangeComponents();
                    break;

                case 4:
                    NV_Page = new WorkingBoard.Nodes.WorkFile.View.NV_WB_WorkFile_Main();
                    TS_Page = new WorkingBoard.Nodes.WorkFile.View.TS_WB_WorkFile_Main();
                    MC_Page = new WorkingBoard.Nodes.Common.MC_WB_Common_Files();
                    ChangeComponents();
                    break;
            }
        }
    }
}
