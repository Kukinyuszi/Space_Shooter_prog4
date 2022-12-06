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

namespace Space_shooter.Windows
{
    /// <summary>
    /// Interaction logic for StoryWindow.xaml
    /// </summary>
    public partial class StoryWindow : Window
    {
        IGameModel settings;
        SoundPlayerService sps;
        MainMenuWindow _mainMenu;
        IDisplaySettings displaySettings;

        public StoryWindow(MainMenuWindow menu, IGameModel settings, IDisplaySettings displaySettings, SoundPlayerService sps)
        {
            this._mainMenu = menu;
            this.sps = sps;
            this.settings = settings;
            this.displaySettings = displaySettings;
            InitializeComponent();
            mediaElement.Volume = sps.SoundVolume + 0.3;
        }

        private void mediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            MainWindow StartingTheGame = new MainWindow(_mainMenu, settings, displaySettings, sps);
            StartingTheGame.Show();
            this.Close();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            MainWindow StartingTheGame = new MainWindow(_mainMenu, settings, displaySettings, sps);
            StartingTheGame.Show();
            this.Close();
        }
    }
}
