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
    /// Interaction logic for NV_WB_Main.xaml
    /// </summary>
    public partial class NV_WB_Main : Page
    {
        public NV_WB_Main(int option)
        {
            InitializeComponent();

            switch(option)
            {
                case 1:
                    BT_ToDo.Background = new SolidColorBrush(Colors.LightGreen);
                    break;

                case 2:
                    BT_InProgress.Background = new SolidColorBrush(Colors.LightGreen);
                    break;

                case 3:
                    BT_ToFinish.Background = new SolidColorBrush(Colors.LightGreen);
                    break;

                case 4:
                    BT_ToTest.Background = new SolidColorBrush(Colors.LightGreen);
                    break;

                case 5:
                    BT_Finished.Background = new SolidColorBrush(Colors.LightGreen);
                    break;
            }
        }

        private void MD_ToDo(object sender, RoutedEventArgs e)
        {
            GetController().MD_Change(0);
        }

        private void MD_InProgress(object sender, RoutedEventArgs e)
        {
            GetController().MD_Change(1);
        }

        private void MD_ToFinish(object sender, RoutedEventArgs e)
        {
            GetController().MD_Change(2);
        }

        private void MD_ToTest(object sender, RoutedEventArgs e)
        {
            GetController().MD_Change(3);
        }

        private void MD_Finished(object sender, RoutedEventArgs e)
        {
            GetController().MD_Change(4);
        }

        private void MD_Back(object sender, RoutedEventArgs e)
        {
            GetController().CT_Main();
        }

        private WorkingBoard.Controller.WorkingBoardController GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (MainWindow)mainWindow;
            return (WorkingBoard.Controller.WorkingBoardController)a.MainFrame.Content;
        }
    }
}
