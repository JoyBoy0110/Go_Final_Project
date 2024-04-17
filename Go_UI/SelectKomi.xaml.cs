using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Go_UI
{
    /// <summary>
    /// Interaction logic for SelectKomi.xaml
    /// </summary>
    public partial class SelectKomi : Window
    {
        private double komi;
        public SelectKomi()
        {
            InitializeComponent();
            komi = 0.0;
        }

        private void PlusHalf_Click(object sender, RoutedEventArgs e)
        {
            komi += 0.5;
            UpdateKomi();
        }

        private void PlusOne_Click(object sender, RoutedEventArgs e)
        {
            komi += 1;
            UpdateKomi();
        }

        private void MinusHalf_Click(object sender, RoutedEventArgs e)
        {
            komi -= 0.5;
            UpdateKomi();
        }

        private void MinusOne_Click(object sender, RoutedEventArgs e)
        {
            komi -= 1;
            UpdateKomi();
        }

        private void UpdateKomi()
        {
            KomiText.Text = komi.ToString();
        }

        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            AIChoose page = new AIChoose(komi);
            this.Close();
            page.ShowDialog();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow page = new MainWindow();
            this.Close();
            page.ShowDialog();
        }
    }
}
