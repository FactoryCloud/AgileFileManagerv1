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

namespace AgileFileManagerv1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Employee employee;
        public MainWindow(Employee employee)
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized;
            System.Windows.Application.Current.MainWindow = this;
            this.Loaded += new RoutedEventHandler(EV_Start);
            this.employee = employee;
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("¿Esta seguro que desea salir?", "Salir", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                System.Windows.Application.Current.Shutdown();
            }
        }

        private void EV_Start(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new Main.Controller.MainController();
        }
    }
}
