using System.Windows;
using System.Windows.Input;

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

        private void BackButton_OnMouseEnter(object sender, MouseEventArgs e)
        {
            BackButton.Opacity = 0.6;
        }

        private void BackButton_OnMouseLeave(object sender, MouseEventArgs e)
        {
            BackButton.Opacity = 1;
        }

        private void Board13_OnMouseEnter(object sender, MouseEventArgs e)
        {
            TextBlock13.Text = "13 x 13";
            Board13.Opacity = 0.8;
        }

        private void Board13_OnMouseLeave(object sender, MouseEventArgs e)
        {
            TextBlock13.Text = "";
            Board13.Opacity = 1;
        }

        private void Board9_OnMouseEnter(object sender, MouseEventArgs e)
        {
            TextBlock9.Text = "9 x 9";
            Board9.Opacity = 0.8;
        }

        private void Board9_OnMouseLeave(object sender, MouseEventArgs e)
        {
            TextBlock9.Text = "";
            Board9.Opacity = 1;
        }
    }
}
