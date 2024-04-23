using System.Windows;
using Go_Logic;

namespace Go_UI
{
    /// <summary>
    /// Interaction logic for EndScreen.xaml
    /// </summary>
    public partial class EndScreen : Window
    {
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

        private void PlayAgain_Click(object sender, RoutedEventArgs e)
        {
            SelectKomi page = new SelectKomi();
            this.Close();
            page.ShowDialog();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
