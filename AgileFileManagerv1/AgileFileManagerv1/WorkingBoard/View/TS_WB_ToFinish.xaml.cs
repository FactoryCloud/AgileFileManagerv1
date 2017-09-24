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

namespace AgileFileManagerv1.WorkingBoard.View
{
    /// <summary>
    /// Interaction logic for TS_WB_ToFinish.xaml
    /// </summary>
    public partial class TS_WB_ToFinish : Page
    {
        public TS_WB_ToFinish()
        {
            InitializeComponent();

            if(GetController().fileToFinish != null)
            {
                BT_OpenFile.IsEnabled = true;
                BT_StartFile.IsEnabled = true;
                BT_FreeFile.IsEnabled = true;
            }
            if(GetController().TF_GetInterventions() > 0)
            {
                BT_FinishFile.IsEnabled = true;
            }
        }

        private void EV_StartCallIn(object sender, RoutedEventArgs e)
        {
            FloatWindows.FW_Clients.Controller.FW_Clients floatWindow = new FloatWindows.FW_Clients.Controller.FW_Clients(1);
            floatWindow.Show();
        }

        private void EV_StartNewFile(object sender, RoutedEventArgs e)
        {
            FloatWindows.FW_Clients.Controller.FW_Clients floatWindow = new FloatWindows.FW_Clients.Controller.FW_Clients(2);
            floatWindow.Show();
        }

        private void EV_OpenFile(object sender, RoutedEventArgs e)
        {
            FloatWindows.FW_File.Controller.FW_File floatWindow = new FloatWindows.FW_File.Controller.FW_File(GetController().fileToFinish);
            floatWindow.Show();
        }

        private void EV_StartFile(object sender, RoutedEventArgs e)
        {
            GetController().CT_GetFileToFinish();
        }

        private void EV_FinishFile(object sender, RoutedEventArgs e)
        {
            GetController().MD_FinishFileToFinish();
        }

        private void EV_FreeFile(object sender, RoutedEventArgs e)
        {
            GetController().MD_FreeFileToFinish();
        }

        private WorkingBoard.Controller.WorkingBoardController GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (MainWindow)mainWindow;
            return (WorkingBoard.Controller.WorkingBoardController)a.MainFrame.Content;
        }
    }
}
