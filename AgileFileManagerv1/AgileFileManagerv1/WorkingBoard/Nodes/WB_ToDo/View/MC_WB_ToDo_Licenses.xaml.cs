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

namespace AgileFileManagerv1.WorkingBoard.Nodes.WB_ToDo.View
{
    /// <summary>
    /// Interaction logic for MC_WB_ToDo_Licenses.xaml
    /// </summary>
    public partial class MC_WB_ToDo_Licenses : Page
    {
        public MC_WB_ToDo_Licenses()
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(EV_Start);

            foreach(var item in GetController().licenses)
            {
                ListBoxItem temp = new ListBoxItem();
                Grid grid = new Grid();
                if (item.DateEnd < DateTime.Now)
                    grid.Background = Brushes.Red;
                else
                    grid.Background = Brushes.Green;

                ColumnDefinition column1 = new ColumnDefinition();
                ColumnDefinition column2 = new ColumnDefinition();
                column1.Width = new GridLength(1, GridUnitType.Star);
                column2.Width = new GridLength(1, GridUnitType.Star);
                grid.ColumnDefinitions.Add(column1);
                grid.ColumnDefinitions.Add(column2);

                grid.Margin = new Thickness(15);
                grid.MinHeight = 100;

                Label label = new Label();
                label.Content = $"{item.application.Name} Version {item.application.Version}" +
                    $"\nFecha Inicio: {String.Format("{0:dd/MM/yyyy}", item.DateStart)}\nHasta: {String.Format("{0:dd/MM/yyyy}", item.DateEnd)}";
                label.VerticalContentAlignment = VerticalAlignment.Center;
                label.HorizontalAlignment = HorizontalAlignment.Center;
                Grid.SetColumn(label, 0);

                Button button = new Button();
                button.Content = "Seleccionar licencia";
                button.Margin = new Thickness(10);
                button.MaxHeight = 40;
                button.MaxWidth = 120;
                button.Tag = item.LicenseID;
                if (item.DateEnd < DateTime.Now)
                    button.IsEnabled = false;
                button.Click += new RoutedEventHandler(EV_License);
                Grid.SetColumn(button, 1);

                grid.Children.Add(label);
                grid.Children.Add(button);

                //temp.Content = grid;
                temp.HorizontalContentAlignment = HorizontalAlignment.Stretch;
                temp.Margin = new Thickness(15);
                temp.Padding = new Thickness(15);
                temp.IsHitTestVisible = false;
                if (item.DateEnd < DateTime.Now)
                    temp.Background = Brushes.Red;
                else
                    temp.Background = Brushes.Green;
                SP_Licenses.Children.Add(grid);
            }
            
        }

        private void EV_License(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show($"{(sender as Button).Tag}");
            GetController().SetLicense(Convert.ToInt32((sender as Button).Tag));
            GetController().MD_Change(3);
        }

        private void EV_Start(object sender, RoutedEventArgs e)
        {
            
        }

        private WorkingBoard.Nodes.WB_ToDo.Controller.WB_ToDoController GetController()
        {
            Window mainWindow = System.Windows.Application.Current.MainWindow;
            var a = (MainWindow)mainWindow;
            return (WorkingBoard.Nodes.WB_ToDo.Controller.WB_ToDoController)a.MainFrame.Content;
        }
    }
}
