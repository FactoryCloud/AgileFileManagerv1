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
    /// Interaction logic for TS_WB_ToDo_Main.xaml
    /// </summary>
    public partial class TS_WB_ToDo_Main : Page
    {
        public TS_WB_ToDo_Main()
        {
            InitializeComponent();

            MessageBox.Show("recarga");

            if (GetController().file.priority != null && GetController().file.issue != null && GetController().reports.Count > 0)
                BT_SaveFile.IsEnabled = true;
        }

        private void EV_SaveFile(object sender, RoutedEventArgs e)
        {
            GetController().SaveFile();
        }

        private WorkingBoard.Nodes.WB_ToDo.Controller.WB_ToDoController GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (MainWindow)mainWindow;
            return (WorkingBoard.Nodes.WB_ToDo.Controller.WB_ToDoController)a.MainFrame.Content;
        }
    }
}
