using System.Windows;
namespace Go_UI
{
    /// <summary>
    /// Interaction logic for SelectSize.xaml
    /// </summary>
    public partial class SelectSize : Window
    {
        private double komi;//stores the komi value

        /// <summary>
        /// a constructor that initializes the komi value
        /// </summary>
        /// <param name="komi"></param>
        public SelectSize(double komi)
        {
            InitializeComponent();
            this.komi = komi;
        }

        private void Board9_Click(object sender, RoutedEventArgs e)
        {
            AIChoose page = new AIChoose(komi, 9);
            this.Close();
            page.ShowDialog();
        }

        private void Board13_Click(object sender, RoutedEventArgs e)
        {
            AIChoose page = new AIChoose(komi, 13);
            this.Close();
            page.ShowDialog();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            SelectKomi page = new SelectKomi();
            this.Close();
            page.ShowDialog();
        }
    }
}
