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

namespace AgileFileManagerv1.Main.Controller
{
    /// <summary>
    /// Interaction logic for MainController.xaml
    /// </summary>
    public partial class MainController : Original.Controller.AgileManagerController
    {
        private Page NV_Page;
        private Page TS_Page;
        private Page MC_Page;

        public MainController()
        {
            InitializeComponent();
            Information = new Dictionary<string, int>();
            Information.Add("mode", 0);
            Information.Add("controller", 0);

            this.Loaded += new RoutedEventHandler(EV_Start);
        }

        private void EV_Start (object sender, RoutedEventArgs e)
        {
            UpdateComponents();
        }

        private void UpdateComponents()
        {
            switch(Information["mode"])
            {
                case 0:
                    NV_Page = new Main.View.NV_Main();
                    TS_Page = new Main.View.TS_Main();
                    MC_Page = new Main.View.MC_Main();
                    ChangeComponents();
                    break;
            }
        }

        private void ChangeComponents()
        {
            TopSide.Content = NV_Page;
            LeftSide.Content = TS_Page;
            MainContent.Content = MC_Page;
        }
    }
}
