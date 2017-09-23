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
    /// Interaction logic for TS_WB_ToDo.xaml
    /// </summary>
    public partial class TS_WB_ToDo : Page
    {
        public TS_WB_ToDo()
        {
            InitializeComponent();

            if (GetController().fileToDo != null)
            {
                BT_GetFile.IsEnabled = true;
                BT_OpenFile.IsEnabled = true;
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
            FloatWindows.FW_File.Controller.FW_File floatWindow = new FloatWindows.FW_File.Controller.FW_File(GetController().fileToDo);
            floatWindow.Show();
        }

        private void EV_GetFile(object sender, RoutedEventArgs e)
        {
            GetController().CT_GetFileToDo();
        }

        private WorkingBoard.Controller.WorkingBoardController GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (MainWindow)mainWindow;
            return (WorkingBoard.Controller.WorkingBoardController)a.MainFrame.Content;
        }
    }
}
