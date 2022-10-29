using Space_shooter.Logic.Interfaces;
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
using static Space_shooter.Logic.Interfaces.ISettings;

namespace Space_shooter.Windows
{
    /// <summary>
    /// Interaction logic for PlayerSettingsWindow.xaml
    /// </summary>
    public partial class PlayerSettingsWindow : Window
    {
        ISettings settings;
        MediaPlayer _backgroundMusic;
        MainMenuWindow _mainMenu;
        bool hasBeenClicked = false;
        public PlayerSettingsWindow(MainMenuWindow menu, MediaPlayer _backgroundMusic, ISettings settings)
        {
            _mainMenu = menu;
            this._backgroundMusic = _backgroundMusic;
            this.settings = settings;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (tb_playername.Text != "")
            {
                settings.PlayerName = tb_playername.Text;
                settings.Difficultyness = DifficultyChecker();
                MainWindow StartingTheGame = new MainWindow(_mainMenu, _backgroundMusic, settings);
                this.Visibility = Visibility.Hidden;
                StartingTheGame.Show();
            }
        }

        private Difficulty DifficultyChecker()
        {

            int i = 0;
            while (sp_difficulty.Children[i] != null || ((sp_difficulty.Children[i] as RadioButton).IsChecked).HasValue ? !(sp_difficulty.Children[i] as RadioButton).IsChecked.Value : true)
            {
                i++;
            }
            if (sp_difficulty.Children[i] != null)
            {
                return (Difficulty)i;
            }
            else throw new Exception("No difficulty checked");
        }


        private void tb_playername_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!hasBeenClicked)
            {
                TextBox box = sender as TextBox;
                box.Text = String.Empty;
                hasBeenClicked = true;
            }
        }

        private void rb_custom_Click(object sender, RoutedEventArgs e)
        {
            CustomDifficultySettings sw = new CustomDifficultySettings(settings);
            sw.ShowDialog();
            sw.Close();
        }
    }
}
