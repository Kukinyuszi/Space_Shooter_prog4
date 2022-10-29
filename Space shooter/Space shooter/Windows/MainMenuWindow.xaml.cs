using Space_shooter.Logic;
using Space_shooter.Logic.Interfaces;
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

        public MainMenuWindow()
        {
            settings = new SpaceShooterLogic();
            InitializeComponent();

            StartBackgroundMusic();
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
            HighScoresWindow hsw = new HighScoresWindow();
            hsw.Show();
        }

        private void Start_Button_Click(object sender, RoutedEventArgs e)
        {
            PlayerSettingsWindow psw = new PlayerSettingsWindow(this, _backgroundMusic, settings);
            this.Visibility = Visibility.Hidden;
            psw.Show();

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

        private void HowToPlay_Button_Click(object sender, RoutedEventArgs e)
        {
            HowToPlayWindow howToPlay = new HowToPlayWindow(this);
            this.Visibility = Visibility.Hidden;
            howToPlay.Show();
        }

        private void ScoreBoard_Click(object sender, RoutedEventArgs e)
        {
            HighScoresWindow hsw = new HighScoresWindow();
            hsw.Show();
        }
    }
}
