using System;
using System.Collections.Generic;
using System.Data;
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

namespace AgileFileManagerv1.WorkingBoard.Nodes.Common
{
    /// <summary>
    /// Interaction logic for MC_WB_CallIn_Files.xaml
    /// </summary>
    public partial class MC_WB_Common_Files : Page
    {
        Model.VW_Files viewFiles;
        public MC_WB_Common_Files()
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(EV_Start);

            viewFiles = new Model.VW_Files(11);

            DG_FilesClient.MouseLeftButtonUp += new MouseButtonEventHandler(EV_FileSelected);
            DG_FilesClient.MouseDoubleClick += new MouseButtonEventHandler(EV_FileOpen);
        }

        private void EV_Start(object sender, RoutedEventArgs e)
        {
            UpdateData();
        }

        private void EV_FileSelected(object sender, MouseButtonEventArgs e)
        {
            int num = DG_FilesClient.SelectedIndex;
            if (num >= 0)
            {
                DataGridRow row = (DataGridRow)DG_FilesClient.ItemContainerGenerator.ContainerFromIndex(num);
                DataRowView dr = row.Item as DataRowView;
                GetController().SetFileSelected(Int32.Parse(dr.Row.ItemArray[0].ToString()));
            }
        }

        private void EV_FileOpen(object sender, MouseButtonEventArgs e)
        {
            if (GetController().fileSelected != null)
            {
                FloatWindows.FW_File.Controller.FW_File floatWindow = new FloatWindows.FW_File.Controller.FW_File(GetController().fileSelected);
                floatWindow.Show();
            }
        }

        private void UpdateData()
        {
            DG_FilesClient.ItemsSource = null;
            DG_FilesClient.ItemsSource = viewFiles.GetTable();
        }

        private WorkingBoard.Controller.FileController GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (MainWindow)mainWindow;
            return (WorkingBoard.Controller.FileController)a.MainFrame.Content;
        }
    }
}
