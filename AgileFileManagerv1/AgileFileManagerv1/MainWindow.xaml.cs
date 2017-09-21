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
        public MainWindow()
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized;
            this.Loaded += new RoutedEventHandler(EV_Start);
        }

        private void EV_Start(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new Main.Controller.MainController();
            AgileManagerDB db = new AgileManagerDB();
            employee = db.Employees.First(em => em.EmployeeID == 2);
        }
    }
}
