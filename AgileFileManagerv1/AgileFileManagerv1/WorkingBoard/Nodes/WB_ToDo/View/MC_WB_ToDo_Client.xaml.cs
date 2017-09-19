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
    /// Interaction logic for MC_WB_ToDo_Client.xaml
    /// </summary>
    public partial class MC_WB_ToDo_Client : Page
    {
        public MC_WB_ToDo_Client()
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(EV_Start);
        }

        private void EV_Start(object sender, RoutedEventArgs e)
        {
            TX_ClientName.Text = GetController().client.Name;
            TX_ClientSubName.Text = GetController().client.SubName;
            TX_ClientAddress.Text = GetController().client.Address;
            TX_ClientPhone1.Text = GetController().client.Phone1;
            TX_ClientPhone2.Text = GetController().client.Phone2;
            TX_ClientMail1.Text = GetController().client.Mail1;
        }

        private WorkingBoard.Nodes.WB_ToDo.Controller.WB_ToDoController GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (MainWindow)mainWindow;
            return (WorkingBoard.Nodes.WB_ToDo.Controller.WB_ToDoController)a.MainFrame.Content;
        }
    }
}
