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

namespace AgileFileManagerv1.WorkingBoard.Nodes.WorkFile.View
{
    /// <summary>
    /// Interaction logic for TS_WB_WorkFile_Main.xaml
    /// </summary>
    public partial class TS_WB_WorkFile_Main : Page
    {
        public TS_WB_WorkFile_Main()
        {
            InitializeComponent();

            if (GetController().file.priority != null && GetController().file.issue != null && GetController().reports.Count > 0)
                BT_SaveFile.IsEnabled = true;

            if (GetController().file.priority != null && GetController().file.issue != null && GetController().reports.Count > 0 && GetController().interventions.Count > 0)
                BT_FinishFile.IsEnabled = true;
        }

        private void EV_SaveFile(object sender, RoutedEventArgs e)
        {
            GetController().SaveFile();
        }

        private void EV_DiscardFile(object sender, RoutedEventArgs e)
        {
            GetController().DiscardFile();
        }

        private void EV_FinishFile(object sender, RoutedEventArgs e)
        {
            GetController().FinishFile();
        }

        private WorkingBoard.Nodes.WorkFile.Controller.WB_WorkFileController GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (MainWindow)mainWindow;
            return (WorkingBoard.Nodes.WorkFile.Controller.WB_WorkFileController)a.MainFrame.Content;
        }
    }
}
