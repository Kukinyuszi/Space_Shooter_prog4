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

namespace Space_shooter.Windows
{
    /// <summary>
    /// Interaction logic for MainMenuWindow.xaml
    /// </summary>
    public partial class MainMenuWindow : Window
    {
        DispatcherTimer videoTimer = new DispatcherTimer();
        static MediaPlayer _backgroundMusic = new MediaPlayer();
        ISettings settings;
        IDisplaySettings displaySettings;

        public MainMenuWindow()
        {
            settings = new SpaceShooterLogic();
            displaySettings = new Display();
            InitializeComponent();

            //StartBackgroundMusic();
        }

        public static void StartBackgroundMusic()
        {
            var cd = Directory.GetCurrentDirectory();
            _backgroundMusic.Open(new Uri(cd + @"\Menu\Musics\menu_music_cut_2_WAV.wav"));
            _backgroundMusic.MediaEnded += new EventHandler(BackgroundMusic_Ended);
            _backgroundMusic.Volume = 0.3;
            _backgroundMusic.Play();
        }

        private static void BackgroundMusic_Ended(object sender, EventArgs e)
        {
            _backgroundMusic.Position = TimeSpan.Zero;
            _backgroundMusic.Play();
        }

        private void Settings_Button_Click(object sender, RoutedEventArgs e)
        {
            SettingsMenuWindow smw = new SettingsMenuWindow(displaySettings);
            smw.ShowDialog();
        }

        private void Start_Button_Click(object sender, RoutedEventArgs e)
        {
            PlayerSettingsWindow psw = new PlayerSettingsWindow(this, _backgroundMusic, settings, displaySettings);
            this.Visibility = Visibility.Hidden;
            if(psw.ShowDialog() == true) this.Visibility = Visibility.Visible;

        }

        //private void Window_Loaded(object sender, RoutedEventArgs e)
        //{
        //    backgroundvideo.Play();
        //    videoTimer.Interval = TimeSpan.FromSeconds(22);

        //    videoTimer.Tick += (sender, eventargs) =>
        //    {
        //        backgroundvideo.Stop();
        //        backgroundvideo.Play();

        //    };
        //    videoTimer.Start();

        //    if (File.Exists("highscore.txt"))
        //    {
        //        string[] tmp = File.ReadAllLines("highscore.txt");
        //        lb_score.Content = tmp[0];
        //        lb_money.Content = tmp[1];
        //    }
        //    else
        //    {
        //        lb_score.Content = 0;
        //    }
        //}

        //private void HowToPlay_Button_Click(object sender, RoutedEventArgs e)
        //{
        //    HowToPlayWindow howToPlay = new HowToPlayWindow(this);
        //    this.Visibility = Visibility.Hidden;
        //    howToPlay.Show();
        //}

        private void ScoreBoard_Click(object sender, RoutedEventArgs e)
        {
            HighScoresWindow hsw = new HighScoresWindow();
            hsw.Show();
        }

        private void Quit_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Load_Button_Click(object sender, RoutedEventArgs e)
        {
            Save_LoadGameService gls = new Save_LoadGameService();
            
            IGameModel model = gls.LoadGame();
            if(settings != null)
            {
                MainWindow StartingTheGame = new MainWindow(this, _backgroundMusic, model, displaySettings);
                this.Close();
                StartingTheGame.Show();
            }
        }
    }
}
