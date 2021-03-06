﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrameWorkDB.V1;
using System.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Windows;

namespace AgileFileManagerv1.Model
{
    public class VW_Files
    {
        public List<File> files;
        private DataTable dt;
        private int mode;
        AgileManagerDB db;

        Client client;

        public VW_Files(int mode)
        {
            this.mode = mode;
            db = new AgileManagerDB();

            dt = new DataTable();

            switch (mode)
            {
                case 1:
                    dt.Columns.Add("ID", typeof(int));
                    dt.Columns.Add("Código", typeof(string));
                    dt.Columns.Add("Cliente", typeof(string));
                    dt.Columns.Add("Fecha", typeof(string));
                    dt.Columns.Add("Prioridad", typeof(string));
                    dt.Columns.Add("Problema", typeof(string));
                    break;

                case 3:
                    dt.Columns.Add("ID", typeof(int));
                    dt.Columns.Add("Código", typeof(string));
                    dt.Columns.Add("Cliente", typeof(string));
                    dt.Columns.Add("Fecha", typeof(string));
                    dt.Columns.Add("Prioridad", typeof(string));
                    dt.Columns.Add("Problema", typeof(string));
                    break;

                case 5:
                    dt.Columns.Add("ID", typeof(int));
                    dt.Columns.Add("Código", typeof(string));
                    dt.Columns.Add("Cliente", typeof(string));
                    dt.Columns.Add("Fecha Inicio", typeof(string));
                    dt.Columns.Add("Fecha Finalización", typeof(string));
                    dt.Columns.Add("Prioridad", typeof(string));
                    dt.Columns.Add("Problema", typeof(string));
                    break;

                case 11:
                    Window mainWindow = System.Windows.Application.Current.MainWindow;
                    var a = (MainWindow)mainWindow;
                    WorkingBoard.Controller.FileController CT = (WorkingBoard.Controller.FileController)a.MainFrame.Content; ;
                    client = CT.file.client;
                    dt.Columns.Add("ID", typeof(int));
                    dt.Columns.Add("Código", typeof(string));
                    dt.Columns.Add("Licencia", typeof(string));
                    dt.Columns.Add("Técnico", typeof(string));
                    dt.Columns.Add("Fecha Inicio", typeof(string));
                    dt.Columns.Add("Fecha Finalización", typeof(string));
                    dt.Columns.Add("Estado", typeof(string));
                    dt.Columns.Add("Prioridad", typeof(string));
                    dt.Columns.Add("Problema", typeof(string));
                    break;

                default:
                    dt.Columns.Add("ID", typeof(int));
                    dt.Columns.Add("Código", typeof(string));
                    dt.Columns.Add("Cliente", typeof(string));
                    dt.Columns.Add("Fecha", typeof(string));
                    dt.Columns.Add("Prioridad", typeof(string));
                    dt.Columns.Add("Problema", typeof(string));
                    break;
            }
        }

        public void UpdateTable()
        {
            switch(mode)
            {
                case 1:
                    files = db.Files.Where(f=> f.StateID == 1 && (f.license.application.DepartmentID == ((MainWindow)System.Windows.Application.Current.MainWindow).employee.DepartmentID || f.EmployeeID == ((MainWindow)System.Windows.Application.Current.MainWindow).employee.EmployeeID || f.EmployeeID == null)).Include(f => f.client).Include(f=> f.priority).Include(f=> f.issue).Include(f=> f.license.application).ToList();
                    break;

                case 3:
                    files = db.Files.Where(f => (f.StateID == 3 && (f.license.application.DepartmentID == ((MainWindow)System.Windows.Application.Current.MainWindow).employee.DepartmentID || f.EmployeeID == ((MainWindow)System.Windows.Application.Current.MainWindow).employee.EmployeeID)))
                        .Include(f => f.client).Include(f => f.priority).Include(f => f.issue).ToList();
                    break;

                case 5:
                    files = db.Files.Where(f => (f.StateID == 5 && (f.license.application.DepartmentID == ((MainWindow)System.Windows.Application.Current.MainWindow).employee.DepartmentID || f.EmployeeID == ((MainWindow)System.Windows.Application.Current.MainWindow).employee.EmployeeID)))
                        .Include(f => f.client).Include(f => f.priority).Include(f => f.issue).ToList();
                    break;

                case 11:
                    files = db.Files.Where(f => (f.ClientID == client.ClientID))
                        .Include(f => f.client).Include(f => f.priority).Include(f => f.issue)
                        .Include(f=> f.license).Include(f=> f.employee).Include(f=> f.state).ToList();
                    break;

                default:
                    files = db.Files.Include(f => f.client).ToList();
                    break;
            }
            

            dt.Clear();
            foreach (File item in files)
            {
                switch(mode)
                {
                    case 1:
                        dt.Rows.Add(item.FileID, item.Code, $"{item.client.Code} {item.client.Name} ({item.license.application.Name})", 
                            $"{String.Format("{0:dd/MM/yyyy}", item.DateStart)}", item.priority.Name, item.issue.Name);
                        break;

                    case 3:
                        dt.Rows.Add(item.FileID, item.Code, $"{item.client.Code} {item.client.Name}",
                            $"{String.Format("{0:dd/MM/yyyy}", item.DateStart)}", item.priority.Name, item.issue.Name);
                        break;

                    case 5:
                        dt.Rows.Add(item.FileID, item.Code, $"{item.client.Code} {item.client.Name}",
                            $"{String.Format("{0:dd/MM/yyyy}", item.DateStart)}", $"{String.Format("{0:dd/MM/yyyy}", item.DateEnd)}", item.priority.Name, item.issue.Name);
                        break;

                    case 11:
                        FrameWorkDB.V1.Application app = db.Applications.First(a => a.ApplicationID == item.license.ApplicationID);
                        if(item.employee != null)
                            dt.Rows.Add(item.FileID, item.Code, $"{item.license.Code} {app.Name} {app.Version}", $"{item.employee.Code} {item.employee.Name}",
                                $"{String.Format("{0:dd/MM/yyyy}", item.DateStart)}", $"{String.Format("{0:dd/MM/yyyy}", item.DateEnd)}", 
                                item.state.Name ,item.priority.Name, item.issue.Name);
                        else
                            dt.Rows.Add(item.FileID, item.Code, $"{item.license.Code} {app.Name} {app.Version}", $"",
                                $"{String.Format("{0:dd/MM/yyyy}", item.DateStart)}", $"{String.Format("{0:dd/MM/yyyy}", item.DateEnd)}", item.priority.Name, item.issue.Name);
                        break;

                    default:
                        dt.Rows.Add(item.FileID, item.Code, $"{item.client.Code} {item.client.Name}",
                            $"{String.Format("{0:dd/MM/yyyy}", item.DateStart)}", item.priority.Name, item.issue.Name);
                        break;
                }
                
            }
        }

        public IEnumerable GetTable()
        {
            UpdateTable();
            return dt.DefaultView;
        }
    }
}
