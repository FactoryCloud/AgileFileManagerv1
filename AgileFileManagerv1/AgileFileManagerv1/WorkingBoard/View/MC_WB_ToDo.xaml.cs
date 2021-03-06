﻿using System;
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

namespace AgileFileManagerv1.WorkingBoard.View
{
    /// <summary>
    /// Interaction logic for MC_WB_ToDo.xaml
    /// </summary>
    public partial class MC_WB_ToDo : Page
    {
        Model.VW_Files viewFiles;
        public MC_WB_ToDo()
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(EV_Start);

            viewFiles = new Model.VW_Files(1);

            DG_FilesPending.MouseLeftButtonUp += new MouseButtonEventHandler(FileSelected_Event);
            DG_FilesPending.MouseDoubleClick += new MouseButtonEventHandler(EV_FileOpen);
        }

        private void EV_Start(object sender, RoutedEventArgs e)
        {
            UpdateData();
        }

        private void FileSelected_Event(object sender, MouseButtonEventArgs e)
        {
            int num = DG_FilesPending.SelectedIndex;
            if (num >= 0)
            {
                DataGridRow row = (DataGridRow)DG_FilesPending.ItemContainerGenerator.ContainerFromIndex(num);
                DataRowView dr = row.Item as DataRowView;
                GetController().SetFileToDo(Int32.Parse(dr.Row.ItemArray[0].ToString()));
            }
        }

        private void EV_FileOpen(object sender, MouseButtonEventArgs e)
        {
            if (GetController().fileToDo != null)
            {
                FloatWindows.FW_File.Controller.FW_File floatWindow = new FloatWindows.FW_File.Controller.FW_File(GetController().fileToDo);
                floatWindow.Show();
            }
        }

        private void UpdateData()
        {
            DG_FilesPending.ItemsSource = null;
            DG_FilesPending.ItemsSource = viewFiles.GetTable();
        }

        private WorkingBoard.Controller.WorkingBoardController GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (MainWindow)mainWindow;
            return (WorkingBoard.Controller.WorkingBoardController)a.MainFrame.Content;
        }
    }
}
