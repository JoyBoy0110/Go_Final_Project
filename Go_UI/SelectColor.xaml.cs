using Go_Logic;
using System.Windows;

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
    }
}
