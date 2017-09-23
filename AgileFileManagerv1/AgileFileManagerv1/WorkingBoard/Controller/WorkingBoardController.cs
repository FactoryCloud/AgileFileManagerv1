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
        public File fileToDo;
        public File fileInProgress;
        public File fileToFinish;
        public File fileFinished;

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

            if(db.Files.Where(f => f.state.Name == "En Progreso" && f.EmployeeID == ((MainWindow)System.Windows.Application.Current.MainWindow).employee.EmployeeID).ToList().Count > 0)
            {
                fileInProgress = db.Files.Last(f => f.state.Name == "En Progreso" && f.EmployeeID == ((MainWindow)System.Windows.Application.Current.MainWindow).employee.EmployeeID);
            }
            

            this.Loaded += new RoutedEventHandler(EV_Start);
        }

        private void EV_Start (object sender, RoutedEventArgs e)
        {
            UpdateComponents();
        }

        public void SetFileToDo(int num)
        {
            fileToDo = db.Files.Where(f => f.FileID == num).Include(f => f.employee).Include(f => f.state).Include(f => f.issue).Include(f => f.priority).Include(f=> f.license).First();
            TS_Page = new WorkingBoard.View.TS_WB_ToDo();
            LeftSide.Content = TS_Page;
        }

        public void SetFileToFinish(int num)
        {
            fileToFinish = db.Files.Where(f => f.FileID == num).Include(f => f.employee).Include(f => f.state).Include(f => f.issue).Include(f => f.priority).Include(f => f.license).First();
            TS_Page = new WorkingBoard.View.TS_WB_ToFinish();
            LeftSide.Content = TS_Page;
        }

        public void SetFileFinished(int num)
        {
            fileFinished = db.Files.Where(f => f.FileID == num).Include(f => f.employee).Include(f => f.state).Include(f => f.issue).Include(f => f.priority).Include(f => f.license).First();
            TS_Page = null;
            LeftSide.Content = TS_Page;
        }

        public void MD_Change(int i)
        {
            Information["oldmode"] = Information["mode"];
            Information["mode"] = i;
            Information["option"] = i + 1;

            UpdateComponents();
        }

        public void MD_FinishFileToFinish()
        {
            fileToFinish.StateID = db.States.First(s => s.Name == "Terminado").StateID;
            db.Files.Update(fileToFinish);
            db.SaveChanges();
            MC_Page = new WorkingBoard.View.MC_WB_ToFinish();
            MainContent.Content = MC_Page;
        }

        public void MD_FreeFileToFinish()
        {
            fileToFinish.EmployeeID = null;
            fileToFinish.StateID = db.States.First(s => s.Name == "Pendiente").StateID;
            db.Files.Update(fileToFinish);
            db.SaveChanges();
            MC_Page = new WorkingBoard.View.MC_WB_ToFinish();
            MainContent.Content = MC_Page;
        }

        public void CT_Main()
        {
            Information["controller"] = 1;
            ChangeController();
        }

        public void CT_CallInFile(Client client)
        {
            this.client = client;
            Information["controller"] = 2;
            ChangeController();
        }

        public void CT_NewFile(Client client)
        {
            this.client = client;
            Information["controller"] = 3;
            ChangeController();
        }

        public void CT_GetFileToDo()
        {
            fileToDo.EmployeeID = ((MainWindow)System.Windows.Application.Current.MainWindow).employee.EmployeeID;
            fileToDo.StateID = db.States.First(s => s.Name == "En Progreso").StateID;
            if(fileInProgress != null)
            {
                fileInProgress.StateID= db.States.First(s => s.Name == "Por Terminar").StateID;
                db.Files.Update(fileInProgress);
            }
            db.Files.Update(fileToDo);
            db.SaveChanges();
            Information["controller"] = 0;
            ChangeController();
        }

        public void CT_GetFileToFinish()
        {
            fileToFinish.EmployeeID = ((MainWindow)System.Windows.Application.Current.MainWindow).employee.EmployeeID;
            fileToFinish.StateID = db.States.First(s => s.Name == "En Progreso").StateID;
            if (fileInProgress != null)
            {
                fileInProgress.StateID = db.States.First(s => s.Name == "Por Terminar").StateID;
                db.Files.Update(fileInProgress);
            }
            db.Files.Update(fileToFinish);
            db.SaveChanges();
            Information["controller"] = 0;
            ChangeController();
        }

        private void UpdateComponents()
        {
            switch(Information["mode"])
            {
                case 0:
                    NV_Page = new WorkingBoard.View.NV_WB_Main();
                    TS_Page = new WorkingBoard.View.TS_WB_ToDo();
                    MC_Page = new WorkingBoard.View.MC_WB_ToDo();
                    ChangeComponents();
                    break;

                case 1:
                    NV_Page = new WorkingBoard.View.NV_WB_Main();
                    TS_Page = new WorkingBoard.View.TS_WB_InProgress();
                    MC_Page = new WorkingBoard.View.MC_WB_InProgress();
                    ChangeComponents();
                    break;

                case 2:
                    NV_Page = new WorkingBoard.View.NV_WB_Main();
                    TS_Page = new WorkingBoard.View.TS_WB_ToFinish();
                    MC_Page = new WorkingBoard.View.MC_WB_ToFinish();
                    ChangeComponents();
                    break;

                case 3:
                    NV_Page = new WorkingBoard.View.NV_WB_Main();
                    TS_Page = null;
                    MC_Page = new WorkingBoard.View.MC_WB_ToTest();
                    ChangeComponents();
                    break;

                case 4:
                    NV_Page = new WorkingBoard.View.NV_WB_Main();
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
                case 0:
                    MainWindow a = (MainWindow)System.Windows.Application.Current.MainWindow;
                    a.MainFrame.Content = new WorkingBoard.Controller.WorkingBoardController();
                    break;

                case 1:
                    MainWindow b = (MainWindow)System.Windows.Application.Current.MainWindow;
                    b.MainFrame.Content = new Main.Controller.MainController();
                    break;

                case 2:
                    MainWindow c = (MainWindow)System.Windows.Application.Current.MainWindow;
                    c.MainFrame.Content = new WorkingBoard.Nodes.CallIn.Controller.WB_CallInController(client);
                    break;
                case 3:
                    MainWindow d = (MainWindow)System.Windows.Application.Current.MainWindow;
                    d.MainFrame.Content = new WorkingBoard.Nodes.NewFile.Controller.WB_NewFileController(client);
                    break;
            }
        }
    }
}
