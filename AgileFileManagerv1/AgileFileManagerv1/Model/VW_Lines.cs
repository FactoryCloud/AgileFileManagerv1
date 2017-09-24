using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FrameWorkDB.V1;
using System.Data;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;

namespace AgileFileManagerv1.Model
{
    public class VW_Lines
    {
        public List<Employee> employees;
        AgileManagerDB db;
        public File file;

        public VW_Lines()
        {
            db = new AgileManagerDB();
            employees = db.Employees.Where(e => e.DepartmentID == ((MainWindow)System.Windows.Application.Current.MainWindow).employee.EmployeeID).ToList();
        }

        public List<Grid> GetTable()
        {
            List<Grid> grids = new List<Grid>();
            int i = 0;
            foreach(Employee employee in employees)
            {
                i++;
                Grid grid = new Grid();

                ColumnDefinition column1 = new ColumnDefinition();
                ColumnDefinition column2 = new ColumnDefinition();
                ColumnDefinition column3 = new ColumnDefinition();
                column1.Width = new GridLength(0.5, GridUnitType.Star);
                column2.Width = new GridLength(1, GridUnitType.Star);
                column3.Width = new GridLength(1, GridUnitType.Star);
                grid.ColumnDefinitions.Add(column1);
                grid.ColumnDefinitions.Add(column2);
                grid.ColumnDefinitions.Add(column3);

                grid.Margin = new Thickness(15);
                grid.MinHeight = 50;

                Label label1 = new Label();
                label1.Content = $"Linea {i}";
                label1.VerticalContentAlignment = VerticalAlignment.Center;
                label1.HorizontalAlignment = HorizontalAlignment.Center;
                Grid.SetColumn(label1, 0);

                Label label2 = new Label();
                label2.Content = $"{employee.Code} - {employee.Name}";
                label2.VerticalContentAlignment = VerticalAlignment.Center;
                label2.HorizontalAlignment = HorizontalAlignment.Center;
                Grid.SetColumn(label2, 1);

                Label label3 = new Label();
                if (db.Files.Where(f => f.EmployeeID == employee.EmployeeID && f.state.Name == "En Progreso").ToList().Count > 0)
                {
                    file = db.Files.Where(f => f.EmployeeID == employee.EmployeeID && f.state.Name == "En Progreso").Include(f => f.client).Include(f => f.priority).Include(f=> f.issue).Last();
                    label3.Content = $"{file.Code} - {file.client.Name} - {file.issue.Name} ({file.priority.Name})";
                }
                else
                    label3.Content = $"Libre";
                label3.VerticalContentAlignment = VerticalAlignment.Center;
                label3.HorizontalAlignment = HorizontalAlignment.Center;
                Grid.SetColumn(label3, 2);

                Border border1 = new Border
                {
                    BorderThickness = new Thickness()
                    {
                        Bottom = 1,
                        Left = 1,
                        Right = 1,
                        Top = 1
                    },
                    BorderBrush = new SolidColorBrush(Colors.Black)
                };

                Grid.SetColumn(border1, 0);
                grid.Children.Add(border1);

                Border border2 = new Border
                {
                    BorderThickness = new Thickness()
                    {
                        Bottom = 1,
                        Right = 1,
                        Top = 1
                    },
                    BorderBrush = new SolidColorBrush(Colors.Black)
                };

                Grid.SetColumn(border2, 1);
                grid.Children.Add(border2);

                Border border3 = new Border
                {
                    BorderThickness = new Thickness()
                    {
                        Bottom = 1,
                        Right = 1,
                        Top = 1
                    },
                    BorderBrush = new SolidColorBrush(Colors.Black)
                };

                Grid.SetColumn(border3, 2);
                grid.Children.Add(border3);

                grid.Children.Add(label1);
                grid.Children.Add(label2);
                grid.Children.Add(label3);

                grids.Add(grid);
            }
            return grids;
        }
    }
}
