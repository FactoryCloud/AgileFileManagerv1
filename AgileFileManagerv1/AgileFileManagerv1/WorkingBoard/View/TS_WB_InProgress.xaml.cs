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
    /// Interaction logic for TS_WB_InProgress.xaml
    /// </summary>
    public partial class TS_WB_InProgress : Page
    {
        public TS_WB_InProgress()
        {
            InitializeComponent();

            if(GetController().fileInProgress != null)
            {
                BT_OpenFile.IsEnabled = true;
                BT_SaveFile.IsEnabled = true;
                BT_FreeFile.IsEnabled = true;
                if (GetController().IP_GetInterventions() > 0)
                {
                    BT_FinishFile.IsEnabled = true;
                }
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
            GetController().CT_WorkFile();
        }

        private void EV_SaveFile(object sender, RoutedEventArgs e)
        {
            GetController().MD_SaveFileInProgress();
        }

        private void EV_FinishFile(object sender, RoutedEventArgs e)
        {
            GetController().MD_FinishFileInProgress();
        }

        private void EV_FreeFile(object sender, RoutedEventArgs e)
        {
            GetController().MD_FreeFileInProgress();
        }

        private WorkingBoard.Controller.WorkingBoardController GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (MainWindow)mainWindow;
            return (WorkingBoard.Controller.WorkingBoardController)a.MainFrame.Content;
        }
    }
}
