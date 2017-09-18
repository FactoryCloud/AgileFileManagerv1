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

        public WorkingBoardController()
        {
            InitializeComponent();
            Information = new Dictionary<string, int>();
            Information.Add("mode", 0);
            Information.Add("oldmode", 0);
            Information.Add("controller", 0);
            Information.Add("oldcontroller", 0);

            this.Loaded += new RoutedEventHandler(EV_Start);
        }

        private void EV_Start (object sender, RoutedEventArgs e)
        {
            UpdateComponents();
        }

        public void MD_Change(int i)
        {
            Information["oldmode"] = Information["mode"];
            Information["mode"] = i;
        }

        public void CT_Main()
        {
            Information["controller"] = 1;
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
                    TS_Page = null;
                    MC_Page = new WorkingBoard.View.MC_WB_InProgress();
                    ChangeComponents();
                    break;

                case 2:
                    NV_Page = new WorkingBoard.View.NV_WB_Main();
                    TS_Page = null;
                    MC_Page = new WorkingBoard.View.MC_WB_ToTest();
                    ChangeComponents();
                    break;

                case 3:
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
                case 1:
                    MainWindow b = (MainWindow)Application.Current.MainWindow;
                    b.MainFrame.Content = new Main.Controller.MainController();
                    break;
            }
        }
    }
}
