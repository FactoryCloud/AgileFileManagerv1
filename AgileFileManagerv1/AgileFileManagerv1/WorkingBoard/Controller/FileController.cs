using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrameWorkDB.V1;
using Microsoft.EntityFrameworkCore;
using System.Windows.Controls;
using System.Windows;

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

        public bool FS_Selectable()
        {
            if (fileSelected != null)
            {
                return (fileSelected.StateID == 1 || fileSelected.StateID == 3 || fileSelected.StateID == 4);
            }    

            else
                return false;
        }

        public void SetFileSelected(int num)
        {
            fileSelected = db.Files.Where(f => f.FileID == num).Include(f => f.employee).Include(f => f.state).Include(f => f.issue).Include(f => f.priority).First();
            EV_TS_Update();
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

        public void SetEmployee(int number)
        {
            if (number == 0)
            {
                file.employee = null;
                file.EmployeeID = null;
            }

            else
            {
                file.employee = db.Employees.First(i => i.EmployeeID == number);
                file.EmployeeID = file.employee.EmployeeID;
            }

            EV_TS_Update();
        }

        public List<Issue> GetIssues()
        {
            return db.Issues.Include(i => i.department).ToList();
        }

        public List<Priority> GetPriorities()
        {
            return db.Priorities.ToList();
        }

        public List<Employee> GetEmployees()
        {
            return db.Employees.ToList();
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

        public void CT_WorkFile()
        {
            Information["controller"] = 4;
            ChangeController();
        }

        virtual public void UpdateComponents()
        { }

        virtual public void EV_TS_Update()
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
                case 4:
                    MainWindow e = (MainWindow)System.Windows.Application.Current.MainWindow;
                    e.MainFrame.Content = new WorkingBoard.Nodes.WorkFile.Controller.WB_WorkFileController(fileSelected);
                    break;
            }
        }
    }
}
