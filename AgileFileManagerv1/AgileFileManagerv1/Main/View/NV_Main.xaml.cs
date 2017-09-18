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

namespace AgileFileManagerv1.Main.View
{
    /// <summary>
    /// Interaction logic for NV_Main.xaml
    /// </summary>
    public partial class NV_Main : Page
    {
        public NV_Main()
        {
            InitializeComponent();
        }

        private void EV_WorkingBoard(object sender, RoutedEventArgs e)
        {
            GetController().CT_WorkingBoard();
        }

        private Main.Controller.MainController GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (MainWindow)mainWindow;
            return (Main.Controller.MainController)a.MainFrame.Content;
        }
    }
}
