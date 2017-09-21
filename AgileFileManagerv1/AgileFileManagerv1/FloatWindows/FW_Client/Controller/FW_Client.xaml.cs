﻿using System;
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
using System.Windows.Shapes;
using FrameWorkDB.V1;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Windows.Threading;
using System.Windows.Controls.Primitives;

namespace AgileFileManagerv1.FloatWindows.FW_Client.Controller
{
    /// <summary>
    /// Interaction logic for FW_Client.xaml
    /// </summary>
    public partial class FW_Client : Window
    {
        FloatWindows.FW_Client.Model.VW_Clients viewClients;
        Client client;
        AgileManagerDB db;

        public FW_Client()
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(EV_Start);

            db = new AgileManagerDB();
            viewClients = new Model.VW_Clients();

            DG_Clients.MouseLeftButtonUp += new MouseButtonEventHandler(ClientSelected_Event);
        }

        private void EV_Start(object sender, RoutedEventArgs e)
        {
            UpdateData();
        }

        private void ClientSelected_Event(object sender, RoutedEventArgs e)
        {
            int num = DG_Clients.SelectedIndex;
            if (num >= 0)
            {
                DataGridRow row = (DataGridRow)DG_Clients.ItemContainerGenerator.ContainerFromIndex(num);
                DataRowView dr = row.Item as DataRowView;
                client = db.Clients.First(p => p.ClientID == Int32.Parse(dr.Row.ItemArray[0].ToString()));
            }
        }

        private void UpdateData()
        {
            DG_Clients.ItemsSource = null;
            DG_Clients.ItemsSource = viewClients.GetTable(db);
        }

        private void EV_ClientSelect(object sender, RoutedEventArgs e)
        {
            GetController().CT_StartFile(client);
            this.Close();
        }

        private WorkingBoard.Controller.WorkingBoardController GetController()
        {
            Window mainWindow = System.Windows.Application.Current.MainWindow;
            var a = (MainWindow)mainWindow;
            return (WorkingBoard.Controller.WorkingBoardController)a.MainFrame.Content;
        }
    }
}
