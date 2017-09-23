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

namespace AgileFileManagerv1.WorkingBoard.Nodes.CallIn.View
{
    /// <summary>
    /// Interaction logic for MC_WB_CallIn_Licenses.xaml
    /// </summary>
    public partial class MC_WB_CallIn_Licenses : Page
    {
        public MC_WB_CallIn_Licenses()
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
                ColumnDefinition column3 = new ColumnDefinition();
                column1.Width = new GridLength(2, GridUnitType.Star);
                column2.Width = new GridLength(1, GridUnitType.Star);
                column3.Width = new GridLength(1, GridUnitType.Star);
                grid.ColumnDefinitions.Add(column1);
                grid.ColumnDefinitions.Add(column2);
                grid.ColumnDefinitions.Add(column3);

                grid.Margin = new Thickness(15);
                grid.MinHeight = 100;

                Label label = new Label();
                label.Content = $"{item.application.Name} Version {item.application.Version}" +
                    $"\nFecha Inicio: {String.Format("{0:dd/MM/yyyy}", item.DateStart)}\nHasta: {String.Format("{0:dd/MM/yyyy}", item.DateEnd)}";
                label.VerticalContentAlignment = VerticalAlignment.Center;
                label.HorizontalAlignment = HorizontalAlignment.Center;
                Grid.SetColumn(label, 0);

                Button button1 = new Button();
                button1.Content = "Ver Documentación";
                button1.Margin = new Thickness(10);
                button1.MaxHeight = 40;
                button1.MaxWidth = 120;
                button1.IsEnabled = false;
                Grid.SetColumn(button1, 1);

                Button button2 = new Button();
                button2.Content = "Seleccionar Licencia";
                button2.Margin = new Thickness(10);
                button2.MaxHeight = 40;
                button2.MaxWidth = 120;
                button2.Tag = item.LicenseID;
                if (item.DateEnd < DateTime.Now)
                    button2.IsEnabled = false;
                button2.Click += new RoutedEventHandler(EV_License);
                Grid.SetColumn(button2, 2);

                grid.Children.Add(label);
                grid.Children.Add(button1);
                grid.Children.Add(button2);

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

        private WorkingBoard.Nodes.CallIn.Controller.WB_CallInController GetController()
        {
            Window mainWindow = System.Windows.Application.Current.MainWindow;
            var a = (MainWindow)mainWindow;
            return (WorkingBoard.Nodes.CallIn.Controller.WB_CallInController)a.MainFrame.Content;
        }
    }
}
