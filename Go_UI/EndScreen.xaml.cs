using System.Windows;
using System.Windows.Input;
using Go_Logic;

namespace Go_UI
{
    /// <summary>
    /// Interaction logic for EndScreen.xaml
    /// </summary>
    public partial class EndScreen : Window
    {
        /// <summary>
        /// a constructor for the end screen which takes in the end type, the winner and the scores and displays the appropriate message
        /// </summary>
        /// <param name="end"></param>
        /// <param name="winner"></param>
        /// <param name="scores"></param>
        public EndScreen(EndType end, Player winner, (double, double) scores)
        {
            InitializeComponent();
            string endMessage = "", winnerMessage = "";
            switch (end)
            {
                case EndType.Resign:
                    endMessage = PlayerExtensions.Opponnent(winner).ToString() + " Resigned";
                    break;
                case EndType.Material:
                    endMessage = "Win By Material";
                    break;
                case EndType.Pass:
                    endMessage = "Win By Two Consecutive Passes";
                    break;
            }
            switch (winner)
            {
                case Player.Black:
                    winnerMessage = "Black Has Won";
                    break;
                case Player.White:
                    winnerMessage = "White Has Won";
                    break;
                case Player.None:
                    winnerMessage = "It's A Tie";
                    break;
            }
            EndMessage.Text = endMessage;
            WinnerMessage.Text = winnerMessage;
            EndScore.Text = " " + scores.Item1 + " - " + scores.Item2 + " ";
        }

        /// <summary>
        /// a button click event that takes the user back to the select komi page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlayAgain_Click(object sender, RoutedEventArgs e)
        {
            SelectKomi page = new SelectKomi();
            this.Close();
            page.ShowDialog();
        }

        /// <summary>
        /// a button click event that closes the application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Application.Current.Shutdown();
        }

        private void PlayAgain_OnMouseEnter(object sender, MouseEventArgs e)
        {
            PlayAgain.Opacity= 0.575;
        }

        private void PlayAgain_OnMouseLeave(object sender, MouseEventArgs e)
        {
            PlayAgain.Opacity = 1;
        }

        private void Exit_OnMouseEnter(object sender, MouseEventArgs e)
        {
            Exit.Opacity = 0.575;
        }

        private void Exit_OnMouseLeave(object sender, MouseEventArgs e)
        {
            Exit.Opacity = 1;
        }
    }
}
