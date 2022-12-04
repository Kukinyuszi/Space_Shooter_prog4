using Space_shooter.Logic.Interfaces;
using Space_shooter.Renderer.Interfaces;
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
using static Space_shooter.Logic.Interfaces.ISettings;
using static Space_shooter.Services.Save_LoadGameService;

namespace Space_shooter.Windows
{
    /// <summary>
    /// Interaction logic for PlayerSettingsWindow.xaml
    /// </summary>
    public partial class PlayerSettingsWindow : Window
    {
        IGameModel settings;
        SoundPlayerService sps;
        MainMenuWindow _mainMenu;
        IDisplaySettings displaySettings;
        bool hasBeenClicked = false;
        public PlayerSettingsWindow(MainMenuWindow menu, ISettings settings, IDisplaySettings displaySettings, SoundPlayerService sps)
        {
            _mainMenu = menu;
            this.settings = settings as IGameModel;
            this.displaySettings = displaySettings;
            this.sps = sps;
            InitializeComponent();

        }

        private void Start_Button_Click(object sender, RoutedEventArgs e)
        {
            if (tb_playername.Text != "" && tb_playername.Text != "Type here your name")
            {
                settings.PlayerName = tb_playername.Text;
                settings.Difficultyness = DifficultyChecker();
                MainWindow StartingTheGame = new MainWindow(_mainMenu, settings, displaySettings, sps);
                StartingTheGame.Show();
                _mainMenu.Close();
                this.Close();
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
            sw.Template = this.Template;
            sw.ShowDialog();
            sw.Close();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                if (tb_playername.Text != "" && tb_playername.Text != "Type here your name")
                {
                    settings.PlayerName = tb_playername.Text;
                    settings.Difficultyness = DifficultyChecker();
                    MainWindow StartingTheGame = new MainWindow(_mainMenu, settings, displaySettings, sps);

                }
            }
            else if(e.Key == Key.Escape)
            {
                this.Visibility = Visibility.Hidden;
                this.DialogResult = true;
            }

        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            _mainMenu.Visibility = Visibility.Visible;
            this.Close();
        }

        private void tb_playername_LostFocus(object sender, RoutedEventArgs e)
        {
            if (tb_playername.Text == "")
            {
                tb_playername.Text = "Type here your name";
                hasBeenClicked = false;
            }
        }
    }
}
