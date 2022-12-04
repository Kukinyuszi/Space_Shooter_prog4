using Space_shooter.Logic;
using Space_shooter.Logic.Interfaces;
using Space_shooter.Renderer;
using Space_shooter.Renderer.Interfaces;
using Space_shooter.Services;
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
using System.Windows.Threading;

using static Space_shooter.Services.Save_LoadGameService;
using static Space_shooter.Services.SettingsSaveService;

namespace Space_shooter.Windows
{
    /// <summary>
    /// Interaction logic for MainMenuWindow.xaml
    /// </summary>
    public partial class MainMenuWindow : Window
    {
        //DispatcherTimer videoTimer = new DispatcherTimer();
        SoundPlayerService sps = new SoundPlayerService();
        ISettings settings;
        IDisplaySettings displaySettings;
        public IDisplaySettings DisplaySettings { get => displaySettings; set => displaySettings = value; }
        public SoundPlayerService Sps { get => sps; set => sps = value; }

        public MainMenuWindow()
        {
            settings = new SpaceShooterLogic();
            SetupSettings();
            InitializeComponent();
            Sps.StartBackgroundMusic();
        }

        private void SetupSettings()
        {
            this.displaySettings = new Display();
            SettingsSaveService sss = new SettingsSaveService();
            Settings_Convert sc = sss.LoadSettings();
            if(sc != null)
            {
                this.displaySettings.FullScreen = sc.displaySettings.FullScreen;
                this.displaySettings.Animation = sc.displaySettings.Animation;
                this.displaySettings.Hitboxes = sc.displaySettings.Hitboxes;
                this.sps = sc.soundPlayerService;
            }
        }

        public MainMenuWindow(IDisplaySettings displaySettings, SoundPlayerService sps)
        {
            settings = new SpaceShooterLogic();
            this.displaySettings = displaySettings;
            this.sps = sps;
            InitializeComponent();
        }


        private void Settings_Button_Click(object sender, RoutedEventArgs e)
        {
            SettingsMenuWindow smw = new SettingsMenuWindow(DisplaySettings, sps);
            smw.Template = this.Template;
            smw.Owner = this;
            if(smw.ShowDialog() == true)
            {
                SettingsSaveService sss = new SettingsSaveService();
                sss.SaveSettings(DisplaySettings, sps);
            }
        }

        private void Start_Button_Click(object sender, RoutedEventArgs e)
        {
            PlayerSettingsWindow psw = new PlayerSettingsWindow(this, settings, DisplaySettings, Sps);
            psw.Template = this.Template;
            psw.Show();
            this.Visibility = Visibility.Hidden;

        }

        private void HowToPlay_Button_Click(object sender, RoutedEventArgs e)
        {
            HowToPlayWindow howToPlay = new HowToPlayWindow();
            howToPlay.Template = this.Template;
            howToPlay.Owner = this;
            howToPlay.Show();
        }

        private void ScoreBoard_Click(object sender, RoutedEventArgs e)
        {
            HighScoresWindow hsw = new HighScoresWindow();
            hsw.Template = this.Template;
            hsw.Owner = this;
            hsw.Show();
        }


        private void Quit_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Credits_Button_Click(object sender, RoutedEventArgs e)
        {
            CreditsWindow cw = new CreditsWindow();
            cw.Template = this.Template;
            cw.Owner = this;
            cw.Show();
        }

        private void Load_Button_Click(object sender, RoutedEventArgs e)
        {
            Save_LoadGameService gls = new Save_LoadGameService();
            
            IGameModel model = gls.LoadGame();
            if(model != null)
            {
                MainWindow StartingTheGame = new MainWindow(this, model, DisplaySettings, sps);
                StartingTheGame.Show();
                this.Close();
            }
        }
    }
}
