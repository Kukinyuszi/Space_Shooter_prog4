using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for IntroWindow.xaml
    /// </summary>
    public partial class IntroWindow : Window
    {
        private string videoPath = Directory.GetCurrentDirectory();

        public IntroWindow()
        {
            InitializeComponent();
        }

        private void MediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            MainMenuWindow mmw = new MainMenuWindow();
            mmw.Show();
            this.Close();
        }
    }
}
