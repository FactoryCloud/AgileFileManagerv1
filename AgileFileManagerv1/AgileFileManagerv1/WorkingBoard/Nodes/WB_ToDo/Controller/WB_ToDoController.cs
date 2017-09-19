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

namespace AgileFileManagerv1.WorkingBoard.Nodes.WB_ToDo.Controller
{
    /// <summary>
    /// Interaction logic for MainController.xaml
    /// </summary>
    public partial class WB_ToDoController : Original.Controller.AgileManagerController
    {
        private Page NV_Page;
        private Page TS_Page;
        private Page MC_Page;

        public Client client;
        public Dealer dealer;
        public License license;
        public List<License> licenses;
        AgileManagerDB db;

        public WB_ToDoController(Client client)
        {
            InitializeComponent();
            Information = new Dictionary<string, int>();
            Information.Add("mode", 2);
            Information.Add("oldmode", 2);
            Information.Add("controller", 0);
            Information.Add("oldcontroller", 0);

            db = new AgileManagerDB();
            licenses = db.Licenses.Where(c => c.ClientID == client.ClientID).Include(c=> c.application).ToList();

            this.client = client;
            dealer = client.dealer;

            this.Loaded += new RoutedEventHandler(EV_Start);
        }

        private void EV_Start (object sender, RoutedEventArgs e)
        {
            UpdateComponents();
        }

        public void SetLicense(int number)
        {
            license = db.Licenses.First(l => l.LicenseID == number);
        }

        public void MD_Change(int i)
        {
            Information["oldmode"] = Information["mode"];
            Information["mode"] = i;
            UpdateComponents();
        }

        public void CT_WB()
        {
            Information["controller"] = 1;
            ChangeController();
        }

        private void UpdateComponents()
        {
            switch(Information["mode"])
            {
                case 0:
                    NV_Page = new WorkingBoard.Nodes.WB_ToDo.View.NV_WB_ToDo_Main();
                    TS_Page = new WorkingBoard.Nodes.WB_ToDo.View.TS_WB_ToDo_Main();
                    MC_Page = new WorkingBoard.Nodes.WB_ToDo.View.MC_WB_ToDo_Client();
                    ChangeComponents();
                    break;

                case 1:
                    NV_Page = new WorkingBoard.Nodes.WB_ToDo.View.NV_WB_ToDo_Main();
                    TS_Page = new WorkingBoard.Nodes.WB_ToDo.View.TS_WB_ToDo_Main();
                    MC_Page = new WorkingBoard.Nodes.WB_ToDo.View.MC_WB_ToDo_Dealer();
                    ChangeComponents();
                    break;

                case 2:
                    NV_Page = new WorkingBoard.Nodes.WB_ToDo.View.NV_WB_ToDo_Main();
                    TS_Page = new WorkingBoard.Nodes.WB_ToDo.View.TS_WB_ToDo_Main();
                    MC_Page = new WorkingBoard.Nodes.WB_ToDo.View.MC_WB_ToDo_Licenses();
                    ChangeComponents();
                    break;

                case 3:
                    NV_Page = new WorkingBoard.Nodes.WB_ToDo.View.NV_WB_ToDo_Main();
                    TS_Page = new WorkingBoard.Nodes.WB_ToDo.View.TS_WB_ToDo_Main();
                    MC_Page = new WorkingBoard.Nodes.WB_ToDo.View.MC_WB_ToDo_Report();
                    ChangeComponents();
                    break;

                case 4:
                    NV_Page = new WorkingBoard.Nodes.WB_ToDo.View.NV_WB_ToDo_Main();
                    TS_Page = new WorkingBoard.Nodes.WB_ToDo.View.TS_WB_ToDo_Main();
                    MC_Page = new WorkingBoard.Nodes.WB_ToDo.View.MC_WB_ToDo_Files();
                    ChangeComponents();
                    break;
            }
        }

        private void ChangeComponents()
        {
            TopSide.Content = NV_Page;
            LeftSide.Content = TS_Page;
            MainContent.Content = MC_Page;
        }

        private void ChangeController()
        {
            switch (Information["controller"])
            {
                case 1:
                    MainWindow b = (MainWindow)System.Windows.Application.Current.MainWindow;
                    b.MainFrame.Content = new WorkingBoard.Controller.WorkingBoardController();
                    break;
            }
        }
    }
}
