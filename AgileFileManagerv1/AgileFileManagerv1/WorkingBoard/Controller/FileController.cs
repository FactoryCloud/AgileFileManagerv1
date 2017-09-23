using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrameWorkDB.V1;
using Microsoft.EntityFrameworkCore;
using System.Windows.Controls;

namespace AgileFileManagerv1.WorkingBoard.Controller
{
    public class FileController : Original.Controller.AgileManagerController
    {
        internal Page NV_Page;
        internal Page TS_Page;
        internal Page MC_Page;

        public File file;
        public File fileSelected;
        public Dealer dealer;
        public List<License> licenses;
        public List<Report> reports;
        public List<Intervention> interventions;
        internal AgileManagerDB db;

        public void SetFileSelected(int num)
        {
            fileSelected = db.Files.Where(f => f.FileID == num).Include(f => f.employee).Include(f => f.state).Include(f => f.issue).Include(f => f.priority).First();
        }

        public void DiscardFile()
        {
            CT_WB();
        }

        public void MD_Change(int i)
        {
            Information["oldmode"] = Information["mode"];
            Information["mode"] = i;
            Information["option"] = i + 1;
            UpdateComponents();
        }

        public void CT_WB()
        {
            Information["controller"] = 1;
            ChangeController();
        }

        virtual public void UpdateComponents()
        { }

        internal void ChangeComponents()
        {
            TopSide.Content = NV_Page;
            LeftSide.Content = TS_Page;
            MainContent.Content = MC_Page;
        }

        internal void ChangeController()
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
