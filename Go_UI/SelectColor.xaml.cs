using Go_Logic;
using System.Windows;
using System.Windows.Input;

namespace Go_UI
{
    /// <summary>
    /// Interaction logic for SelectColor.xaml
    /// </summary>
    public partial class SelectColor : Window
    {
        private double komi;// stores the komi value
        private int size;// stores the size value
        private int ai;// stores which ai is used by value

        /// <summary>
        /// a constructor that initializes the komi value
        /// </summary>
        /// <param name="komi"></param>
        public SelectColor(double komi, int size, int ai)
        {
            InitializeComponent();
            this.komi = komi;
            this.size = size;
            this.ai = ai;
        }

        /// <summary>
        /// a button click event that opens the AIGamePage with the black player
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BlackChoose_Click(object sender, RoutedEventArgs e)
        {
            Window page = ai switch
            {
                0 => new AIGamePage(komi, Player.Black, size),
                1 => new FSMGamePage(komi, Player.Black, size),
                _ => new AIGamePage(komi, Player.Black, size)
            };
            this.Close();
            page.ShowDialog();
        }

        /// <summary>
        /// a button click event that opens the AIGamePage with the white player
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WhiteChoose_Click(object sender, RoutedEventArgs e)
        {
            Window page = ai switch
            {
                0 => new AIGamePage(komi, Player.White, size),
                1 => new FSMGamePage(komi, Player.White, size),
                _ => new AIGamePage(komi, Player.White, size)
            };
            this.Close();
            page.ShowDialog();
        }

        /// <summary>
        /// a button click event that goes back to the AIChoose page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            AIChoose page = new AIChoose(komi, size);
            this.Close();
            page.ShowDialog();
        }
        private void BlackChoose_OnMouseEnter(object sender, MouseEventArgs e)
        {
            BlackChoose.Content = "Black";
            BlackChoose.Opacity = 0.7;
        }

        private void BlackChoose_OnMouseLeave(object sender, MouseEventArgs e)
        {
            BlackChoose.Content = "";
            BlackChoose.Opacity = 1;
        }
        private void WhiteChoose_OnMouseEnter(object sender, MouseEventArgs e)
        {
            WhiteChoose.Content = "White";
            WhiteChoose.Opacity = 0.575;
        }

        private void WhiteChoose_OnMouseLeave(object sender, MouseEventArgs e)
        {
            WhiteChoose.Content = "";
            WhiteChoose.Opacity = 1;
        }
        
        private void BackButton_OnMouseEnter(object sender, MouseEventArgs e)
        {
            BackButton.Opacity = 0.575;
        }

        private void BackButton_OnMouseLeave(object sender, MouseEventArgs e)
        {
            BackButton.Opacity = 1;
        }
    }
}
