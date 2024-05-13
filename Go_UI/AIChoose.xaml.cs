using System.Windows;

namespace Go_UI
{
    /// <summary>
    /// Interaction logic for AIChoose.xaml
    /// </summary>
    public partial class AIChoose : Window
    {
        private double komi;//stores the komi value
        private int size;//stores the komi value

        /// <summary>
        /// a constructor that initializes the komi value
        /// </summary>
        /// <param name="komi"></param>
        public AIChoose(double komi, int size)
        {
            InitializeComponent();
            this.komi = komi;
            this.size = size;
        }

        /// <summary>
        /// a button click event that takes the user back to the SelectKomi page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            SelectSize page = new SelectSize(komi);
            this.Close();
            page.ShowDialog();
        }

        /// <summary>
        /// a button click event that takes the user to the SelectColor page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AIvP_Click(object sender, RoutedEventArgs e)
        {
            SelectColor page = new SelectColor(komi, size, 0);
            this.Close();
            page.ShowDialog();
        }

        /// <summary>
        /// a button click event that takes the user to the GamePage page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PvP_Click(object sender, RoutedEventArgs e)
        {
            GamePage page = new GamePage(komi, size);
            this.Close();
            page.ShowDialog();
        }

        /// <summary>
        /// a button click event that takes the user to the SelectColor page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FSMvP_Click(object sender, RoutedEventArgs e)
        {
            SelectColor page = new SelectColor(komi, size, 1);
            this.Close();
            page.ShowDialog();
        }
    }
}
