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
    /// Interaction logic for AIGamePage.xaml
    /// </summary>
    public partial class AIGamePage : Window
    {
        private double komi;
        private Player player;
        public AIGamePage(double komi, Player player)
        {
            InitializeComponent();
            this.komi = komi;
            this.player = player;
        }
    }
}
