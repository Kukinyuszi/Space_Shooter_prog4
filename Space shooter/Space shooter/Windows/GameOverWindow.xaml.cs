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
    /// Interaction logic for GameOverWindow.xaml
    /// </summary>
    public partial class GameOverWindow : Window
    {
        bool restart;

        public bool Restart { get => restart; set => restart = value; }

        public GameOverWindow()
        {
            InitializeComponent();
        }

        private void Restart_Button_Click(object sender, RoutedEventArgs e)
        {
            restart = true;
            this.DialogResult = true;
        }

        private void Reload_Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void Menu_Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
