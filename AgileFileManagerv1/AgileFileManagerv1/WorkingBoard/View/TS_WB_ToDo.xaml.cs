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
        }

        private void EV_StartCallIn(object sender, RoutedEventArgs e)
        {
            FloatWindows.FW_Clients.Controller.FW_Clients floatWindow = new FloatWindows.FW_Clients.Controller.FW_Clients();
            floatWindow.Show();
        }
    }
}
