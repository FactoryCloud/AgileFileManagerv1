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

namespace AgileFileManagerv1.WorkingBoard.Nodes.CallIn.View
{
    /// <summary>
    /// Interaction logic for NV_WB_CallIn_Main.xaml
    /// </summary>
    public partial class NV_WB_CallIn_Main : Page
    {
        public NV_WB_CallIn_Main()
        {
            InitializeComponent();

            switch (GetController().Information["option"])
            {
                case 1:
                    BT_Client.Background = new SolidColorBrush(Colors.LightGreen);
                    break;

                case 2:
                    BT_Dealer.Background = new SolidColorBrush(Colors.LightGreen);
                    break;

                case 3:
                    BT_Licenses.Background = new SolidColorBrush(Colors.LightGreen);
                    break;

                case 4:
                    BT_Report.Background = new SolidColorBrush(Colors.LightGreen);
                    break;

                case 5:
                    BT_Files.Background = new SolidColorBrush(Colors.LightGreen);
                    break;
            }

            if (GetController().dealer != null)
                BT_Dealer.Visibility = Visibility.Visible;

            if (GetController().file.license != null)
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

        private WorkingBoard.Nodes.CallIn.Controller.WB_CallInController GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (MainWindow)mainWindow;
            return (WorkingBoard.Nodes.CallIn.Controller.WB_CallInController)a.MainFrame.Content;
        }
    }
}
