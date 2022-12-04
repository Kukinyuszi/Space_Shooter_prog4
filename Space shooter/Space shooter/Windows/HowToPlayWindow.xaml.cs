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

namespace Space_shooter.Windows
{
    /// <summary>
    /// Interaction logic for HowToPlayWindow.xaml
    /// </summary>
    public partial class HowToPlayWindow : Window
    {
        public HowToPlayWindow()
        {
            InitializeComponent();

        }

        private void Next_Button_Click(object sender, RoutedEventArgs e)
        {
            HowToPlayPage2Window howToPlay = new HowToPlayPage2Window();
            howToPlay.Template = this.Template;
            howToPlay.Owner = this;
            if (howToPlay.ShowDialog() == false)
            {
                this.Owner.Show();
                this.Close();
            }
        }
        private void Close_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Owner.Show();
            this.Close();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Owner.Hide();
        }
    }
}
