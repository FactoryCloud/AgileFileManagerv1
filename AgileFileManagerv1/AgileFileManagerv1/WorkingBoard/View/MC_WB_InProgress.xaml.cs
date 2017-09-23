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
    /// Interaction logic for MC_WB_InProgress.xaml
    /// </summary>
    public partial class MC_WB_InProgress : Page
    {
        Model.VW_Lines lines;
        public MC_WB_InProgress()
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(EV_Start);
            lines = new Model.VW_Lines();
        }

        private void EV_Start(object sender, RoutedEventArgs e)
        {
            List<Grid> grids = lines.GetTable();
            foreach (Grid grid in grids)
            {
                SP_Lines.Children.Add(grid);
            }
        }
    }
}
