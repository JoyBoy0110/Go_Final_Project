using Go_Logic;
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
    /// Interaction logic for SelectColor.xaml
    /// </summary>
    public partial class SelectColor : Window
    {
        private double komi;
        public SelectColor(double komi)
        {
            InitializeComponent();
            this.komi = komi;
        }

        private void BlackChose_Click(object sender, RoutedEventArgs e)
        {
            AIGamePage page = new AIGamePage(komi, Player.Black);
            this.Close();
            page.ShowDialog();
        }

        private void WhiteChose_Click(object sender, RoutedEventArgs e)
        {
            AIGamePage page = new AIGamePage(komi, Player.White);
            this.Close();
            page.ShowDialog();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            AIChoose page = new AIChoose(komi);
            this.Close();
            page.ShowDialog();
        }
    }
}
