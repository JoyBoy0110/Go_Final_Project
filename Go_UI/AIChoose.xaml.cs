using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Go_UI
{
    /// <summary>
    /// Interaction logic for AIChoose.xaml
    /// </summary>
    public partial class AIChoose : Window
    {
        private double komi; //stores the komi value
        private int size; //stores the komi value

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

        private void FSMvP_OnMouseLeave(object sender, MouseEventArgs e)
        {
            PVMT1.Text = "";
            PVMT2.Text = "";
            PVMT3.Text = "";
            PVMBorder.Opacity = 1;
        }

        private void FSMvP_OnMouseEnter(object sender, MouseEventArgs e)
        {
            PVMT1.Text = "Player";
            PVMT2.Text = "VS";
            PVMT3.Text = "FSM";
            PVMBorder.Opacity = 0.65;
        }

        private void AIvP_OnMouseLeave(object sender, MouseEventArgs e)
        {
            PVAIT1.Text = "";
            PVAIT2.Text = "";
            PVAIT3.Text = "";
            PVAIBorder.Opacity = 1;
        }

        private void AIvP_OnMouseEnter(object sender, MouseEventArgs e)
        {
            PVAIT1.Text = "Player";
            PVAIT2.Text = "VS";
            PVAIT3.Text = "AI";
            PVAIBorder.Opacity = 0.65;
        }

        private void PvP_OnMouseLeave(object sender, MouseEventArgs e)
        {
            PVPT1.Text = "";
            PVPT2.Text = "";
            PVPT3.Text = "";
            PVPBorder.Opacity = 1;
        }

        private void PvP_OnMouseEnter(object sender, MouseEventArgs e)
        {
            PVPT1.Text = "Player";
            PVPT2.Text = "VS";   
            PVPT3.Text = "Player";
            PVPBorder.Opacity = 0.65;
        }
        private void BackButton_OnMouseLeave(object sender, MouseEventArgs e)
        {
            BackButton.Opacity = 1;
        }

        private void BackButton_OnMouseEnter(object sender, MouseEventArgs e)
        {
            BackButton.Opacity = 0.65;
        }
    }
}