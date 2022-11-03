using Space_shooter.Logic;
using Space_shooter.Services;
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
    /// Interaction logic for GamePauseWindow.xaml
    /// </summary>
    public partial class GamePauseWindow : Window
    {
        SpaceShooterLogic model;
        bool restart;

        public bool Restart { get => restart; set => restart = value; }

        public GamePauseWindow(SpaceShooterLogic model)
        {
            this.model = model;
            InitializeComponent();
        }

        private void Resume_Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void Quit_Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            Save_LoadGameService sgs = new Save_LoadGameService();
            sgs.SaveGame(model);
            lb_gamesaved.Content = "Game saved";
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape) this.DialogResult = true;
        }

        private void Restart_Button_Click(object sender, RoutedEventArgs e)
        {
            restart = true;
            this.DialogResult |= true;
        }
    }
}
