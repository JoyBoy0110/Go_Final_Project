using System.Windows;
using System.Windows.Input;

namespace Go_UI
{
    /// <summary>
    /// Interaction logic for SelectKomi.xaml
    /// </summary>
    public partial class SelectKomi : Window
    {
        private double komi;// The komi value that the user has selected
        
        /// <summary>
        /// Constructor for the SelectKomi
        /// </summary>
        public SelectKomi()
        {
            InitializeComponent();
            komi = 0.0;
        }

        /// <summary>
        /// a button click event that increases the komi by 0.5
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlusHalf_Click(object sender, RoutedEventArgs e)
        {
            if (komi + 0.5 <= 15)
                komi += 0.5;
            UpdateKomi();
        }

        /// <summary>
        /// a button click event that increases the komi by 1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlusOne_Click(object sender, RoutedEventArgs e)
        {
            if (komi + 1 <= 15)
                komi += 1.0;
            UpdateKomi();
        }

        /// <summary>
        /// a button click event that decreases the komi by 0.5
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MinusHalf_Click(object sender, RoutedEventArgs e)
        {
            if (komi - 0.5 >= 0)
                komi -= 0.5;
            UpdateKomi();
        }

        /// <summary>
        /// a button click event that decreases the komi by 1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MinusOne_Click(object sender, RoutedEventArgs e)
        {
            if (komi - 1 >= 0)
                komi -= 1.0;
            UpdateKomi();
        }

        /// <summary>
        /// Updates the Komi text box with the current komi value
        /// </summary>
        private void UpdateKomi()
        {
            KomiText.Text = komi.ToString();
        }

        /// <summary>
        /// a button click event that takes the user to the AIChoose page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            SelectSize page = new SelectSize(komi);
            this.Close();
            page.ShowDialog();
        }

        /// <summary>
        /// a button click event that takes the user back to the main menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow page = new MainWindow();
            this.Close();
            page.ShowDialog();
        }

        private void PlusHalf_OnMouseEnter(object sender, MouseEventArgs e)
        {
            PlusHalf.Opacity = 0.675;
        }

        private void PlusHalf_OnMouseLeave(object sender, MouseEventArgs e)
        {
            PlusHalf.Opacity = 1;
        }

        private void PlusOne_OnMouseEnter(object sender, MouseEventArgs e)
        {
            PlusOne.Opacity = 0.675;
        }

        private void PlusOne_OnMouseLeave(object sender, MouseEventArgs e)
        {
            PlusOne.Opacity = 1;
        }

        private void MinusHalf_OnMouseEnter(object sender, MouseEventArgs e)
        {
            MinusHalf.Opacity = 0.675;
        }

        private void MinusHalf_OnMouseLeave(object sender, MouseEventArgs e)
        {
            MinusHalf.Opacity = 1;
        }

        private void MinusOne_OnMouseEnter(object sender, MouseEventArgs e)
        {
            MinusOne.Opacity = 0.675;
        }

        private void MinusOne_OnMouseLeave(object sender, MouseEventArgs e)
        {
            MinusOne.Opacity = 1;
        }

        private void BackButton_OnMouseEnter(object sender, MouseEventArgs e)
        {
            BackButton.Opacity = 0.6;
        }

        private void BackButton_OnMouseLeave(object sender, MouseEventArgs e)
        {
            BackButton.Opacity = 1;
        }

        private void Continue_OnMouseEnter(object sender, MouseEventArgs e)
        {
            Continue.Opacity = 0.6;
        }

        private void Continue_OnMouseLeave(object sender, MouseEventArgs e)
        {
            Continue.Opacity = 1;
        }
    }
}
