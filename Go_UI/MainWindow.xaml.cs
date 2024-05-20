using System.Windows;
using System.Windows.Input;

namespace Go_UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// page constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// a play button
        /// sends the player to the game page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Play_Click(object sender, RoutedEventArgs e)
        {
            SelectKomi gp = new SelectKomi();
            this.Close();
            gp.ShowDialog();
        }

        /// <summary>
        /// exit button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Exit_OnMouseEnter(object sender, MouseEventArgs e)
        {
            exit.Opacity = 0.575;
        }

        private void Exit_OnMouseLeave(object sender, MouseEventArgs e)
        {
            exit.Opacity = 1;
        }
        
        private void Play_OnMouseEnter(object sender, MouseEventArgs e)
        {
            play.Opacity = 0.575;
        }

        private void Play_OnMouseLeave(object sender, MouseEventArgs e)
        {
            play.Opacity = 1;
        }
    }
}