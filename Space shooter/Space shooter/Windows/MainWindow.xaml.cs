using Space_shooter.Logic;
using Space_shooter.Logic.Interfaces;
using Space_shooter.Windows;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Space_shooter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SpaceShooterLogic logic;
        public int score;
        public int health = 100;
        private int wait = 0;
        MediaPlayer _backgroundMusic_Settings;
        MainMenuWindow gameMenu;
        DispatcherTimer gameTimer;
        public MainWindow()
        {
            InitializeComponent();
        }
        public MainWindow(MainMenuWindow menu, MediaPlayer _backgroundMusic, ISettings settings)
        {
            InitializeComponent();
            logic = settings as SpaceShooterLogic;
            gameMenu = menu;
            _backgroundMusic_Settings = _backgroundMusic;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            display.SetupModel(logic);
            display.SetupSizes(new Size(MyGrid.ActualWidth, MyGrid.ActualHeight));
            logic.SetupSizes(new System.Windows.Size(MyGrid.ActualWidth, MyGrid.ActualHeight));
            logic.GameOver += Logic_GameOver;

            gameTimer = new DispatcherTimer();
            gameTimer.Interval = TimeSpan.FromMilliseconds(20);
            gameTimer.Tick += (sender, eventargs) =>
            {
                logic.TimeStep();
                display.InvalidateVisual();
            };
            gameTimer.Start();

        }

        private void Logic_GameOver(object? sender, EventArgs e)
        {
            gameTimer.Stop();
        }


        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (logic != null)
            {
                logic.SetupSizes(new System.Windows.Size(MyGrid.ActualWidth, MyGrid.ActualHeight));
                display.SetupModel(logic);
                display.SetupSizes(new Size(MyGrid.ActualWidth, MyGrid.ActualHeight));
                display.InvalidateVisual();
            }

        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                logic.Controldown(SpaceShooterLogic.Controls.Left);
            }
            else if (e.Key == Key.Right)
            {
                logic.Controldown(SpaceShooterLogic.Controls.Right);
            }
            else if (e.Key == Key.Space)
            {
                logic.Controldown(SpaceShooterLogic.Controls.Shoot);
            }
            else if (e.Key == Key.Escape)
            {
                logic.Controldown(SpaceShooterLogic.Controls.Escape);
            }
            else if (e.Key == Key.G)
            {
                logic.Controldown(SpaceShooterLogic.Controls.G);
            }
            else if (e.Key == Key.O)
            {
                logic.Controldown(SpaceShooterLogic.Controls.O);
            }
            else if (e.Key == Key.D)
            {
                logic.Controldown(SpaceShooterLogic.Controls.D);
            }
        }
        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                logic.Controlup(SpaceShooterLogic.Controls.Left);
            }
            else if (e.Key == Key.Right)
            {
                logic.Controlup(SpaceShooterLogic.Controls.Right);
            }
            else if (e.Key == Key.Space)
            {
                logic.Controlup(SpaceShooterLogic.Controls.Shoot);
            }
        }
        //private void Pause_Button_Click(object sender, RoutedEventArgs e)
        //{
        //    InGamePausePopUp pausePopUp = new InGamePausePopUp(this, gameTimer, gameMenu, _backgroundMusic_Settings);
        //    pausePopUp.Show();

        //}
    }
}
