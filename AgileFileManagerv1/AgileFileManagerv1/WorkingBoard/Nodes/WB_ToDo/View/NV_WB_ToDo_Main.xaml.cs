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

namespace AgileFileManagerv1.WorkingBoard.Nodes.WB_ToDo.View
{
    /// <summary>
    /// Interaction logic for NV_WB_ToDo_Main.xaml
    /// </summary>
    public partial class NV_WB_ToDo_Main : Page
    {
        public NV_WB_ToDo_Main()
        {
            InitializeComponent();

            if (GetController().dealer != null)
                BT_Dealer.Visibility = Visibility.Visible;

            if (GetController().license != null)
                BT_Report.IsEnabled = true;
        }

        private void EV_Client(object sender, RoutedEventArgs e)
        {
            GetController().MD_Change(0);
        }

        private void EV_Dealer(object sender, RoutedEventArgs e)
        {
            GetController().MD_Change(1);
        }

        private void EV_Licenses(object sender, RoutedEventArgs e)
        {
            GetController().MD_Change(2);
        }

        private void EV_Report(object sender, RoutedEventArgs e)
        {
            GetController().MD_Change(3);
        }

        private void EV_Files(object sender, RoutedEventArgs e)
        {
            GetController().MD_Change(4);
        }

        private void EV_BacKWB(object sender, RoutedEventArgs e)
        {
            GetController().CT_WB();
        }

        private WorkingBoard.Nodes.WB_ToDo.Controller.WB_ToDoController GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (MainWindow)mainWindow;
            return (WorkingBoard.Nodes.WB_ToDo.Controller.WB_ToDoController)a.MainFrame.Content;
        }

        
    }
}
