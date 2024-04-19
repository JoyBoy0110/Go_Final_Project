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
using System.Windows.Shapes;

namespace Go_UI
{
    /// <summary>
    /// Interaction logic for AIChoose.xaml
    /// </summary>
    public partial class AIChoose : Window
    {
        private double komi;
        public AIChoose(double komi)
        {
            InitializeComponent();
            this.komi = komi;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            SelectKomi page = new SelectKomi();
            this.Close();
            page.ShowDialog();
        }

        private void AIvP_Click(object sender, RoutedEventArgs e)
        {
            SelectColor page = new SelectColor(komi);
            this.Close();
            page.ShowDialog();
        }

        private void PvP_Click(object sender, RoutedEventArgs e)
        {
            GamePage page = new GamePage(komi);
            this.Close();
            page.ShowDialog();
        }
    }
}
