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

namespace AgileFileManagerv1.WorkingBoard.Controller
{
    /// <summary>
    /// Interaction logic for MainController.xaml
    /// </summary>
    public partial class WorkingBoardController : Original.Controller.AgileManagerController
    {
        private Page NV_Page;
        private Page TS_Page;
        private Page MC_Page;

        AgileManagerDB db;
        Client client;
        File fileToDo;
        File fileInProgress;
        File fileToFinish;
        File fileFinished;
        int Option;

        public WorkingBoardController()
        {
            InitializeComponent();
            db = new AgileManagerDB();
            Information = new Dictionary<string, int>();
            Information.Add("mode", 1);
            Information.Add("oldmode", 1);
            Information.Add("controller", 0);
            Information.Add("oldcontroller", 0);
            Information.Add("option", 2);

            this.Loaded += new RoutedEventHandler(EV_Start);
        }

        private void EV_Start (object sender, RoutedEventArgs e)
        {
            UpdateComponents();
        }

        public void SetFileToDo(int num)
        {
            fileToDo = db.Files.First(f => f.FileID == num);
            TS_Page = new WorkingBoard.View.TS_WB_ToDo();
            LeftSide.Content = TS_Page;
        }

        public void SetFileToFinish(int num)
        {
            fileToFinish = db.Files.First(f => f.FileID == num);
            TS_Page = new WorkingBoard.View.TS_WB_ToFinish();
            LeftSide.Content = TS_Page;
        }

        public void SetFileFinished(int num)
        {
            fileFinished = db.Files.First(f => f.FileID == num);
            TS_Page = new WorkingBoard.View.TS_WB_ToFinish();
            LeftSide.Content = TS_Page;
        }

        public void MD_Change(int i)
        {
            Information["oldmode"] = Information["mode"];
            Information["mode"] = i;
            Information["option"] = i + 1;

            UpdateComponents();
        }

        public void CT_Main()
        {
            Information["controller"] = 1;
            ChangeController();
        }

        public void CT_StartFile(Client client)
        {
            this.client = client;
            Information["controller"] = 2;
            ChangeController();
        }

        private void UpdateComponents()
        {
            switch(Information["mode"])
            {
                case 0:
                    NV_Page = new WorkingBoard.View.NV_WB_Main(Information["option"]);
                    TS_Page = new WorkingBoard.View.TS_WB_ToDo();
                    MC_Page = new WorkingBoard.View.MC_WB_ToDo();
                    ChangeComponents();
                    break;

                case 1:
                    NV_Page = new WorkingBoard.View.NV_WB_Main(Information["option"]);
                    TS_Page = new WorkingBoard.View.TS_WB_InProgress();
                    MC_Page = new WorkingBoard.View.MC_WB_InProgress();
                    ChangeComponents();
                    break;

                case 2:
                    NV_Page = new WorkingBoard.View.NV_WB_Main(Information["option"]);
                    TS_Page = new WorkingBoard.View.TS_WB_ToFinish();
                    MC_Page = new WorkingBoard.View.MC_WB_ToFinish();
                    ChangeComponents();
                    break;

                case 3:
                    NV_Page = new WorkingBoard.View.NV_WB_Main(Information["option"]);
                    TS_Page = null;
                    MC_Page = new WorkingBoard.View.MC_WB_ToTest();
                    ChangeComponents();
                    break;

                case 4:
                    NV_Page = new WorkingBoard.View.NV_WB_Main(Information["option"]);
                    TS_Page = null;
                    MC_Page = new WorkingBoard.View.MC_WB_Finished();
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
                    b.MainFrame.Content = new Main.Controller.MainController();
                    break;

                case 2:
                    MainWindow c = (MainWindow)System.Windows.Application.Current.MainWindow;
                    c.MainFrame.Content = new WorkingBoard.Nodes.WB_ToDo.Controller.WB_ToDoController(client);
                    break;
            }
        }
    }
}
