using Space_shooter.Logic;
using Space_shooter.Logic.Animation;
using Space_shooter.Models;
using Space_shooter.Models.Powerups;
using Space_shooter.Renderer.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using static Space_shooter.Models.Powerups.WeaponPowerup;
using static Space_shooter.Renderer.Interfaces.IDisplaySettings;

namespace Space_shooter.Renderer
{
    internal class Display : FrameworkElement, IDisplaySettings
    {
        private Size area;
        private SpaceShooterLogic model;
        private int backgroundcounter = 0;
        private string chatpopup;
        private DispatcherTimer ChatPopupTimer;
        private DispatcherTimer AnimationTimer;
        private List<Explosion> Explodings = new List<Explosion>();
        private Resolution resolution;
        private bool animation = true;
        private bool hitboxes;
        private bool fullScreen = true;

        private int exhaustCounter;



        public Resolution WindowResolution { get => resolution; set => resolution = value; }
        public bool Animation { get => animation; set => animation = value; }
        public bool Hitboxes { get => hitboxes; set => hitboxes = value; }
        public bool FullScreen { get => fullScreen; set => fullScreen = value; }


        public void SetupSizes(Size area)
        {
            this.area = area;
            this.InvalidateVisual();
            model.PowerUpPickedUp += Model_PowerUpPickedUp;
            ChatPopupTimer = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(3) };
            ChatPopupTimer.Tick += ChatPopup;
            AnimationTimer = new DispatcherTimer() { Interval = TimeSpan.FromMilliseconds(40) };
            AnimationTimer.Tick +=  Animation_Time_Step;
            AnimationTimer.Start();
        }



        public void SetupModel(SpaceShooterLogic model)
        {
            this.model = model;
            this.model.Changed += (sender, eventargs) => this.InvalidateVisual();
        }
        public void SetupSettings(IDisplaySettings displaySettings)
        {
            animation = displaySettings.Animation;
            resolution = displaySettings.WindowResolution;
            hitboxes = displaySettings.Hitboxes;
        }

        public Brush SpaceBrush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "space1080.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Enemy1Brush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Ship1/Ship1.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush ChatboxBrush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "Chatbox.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush HealthBar
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "HealthBar.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush HealthDot
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "HealthDot.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush ExtraHealthDot
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "ExtraDot.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush BossHpLeft
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "BossHP1.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush BossHpMiddle
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "BossHP2.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush BossHpRight
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "BossHP3.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush AsteroidBrush1
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "asteroids/medium/a10000.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush AsteroidBrush2
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "asteroids/small/a10001.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush AsteroidBrush3
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "asteroids/small/a10002.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush AsteroidBrush4
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "asteroids/small/a10003.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush AsteroidBrush5
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "asteroids/small/a10004.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush AsteroidBrush6
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "asteroids/small/a10005.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush AsteroidBrush7
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "asteroids/small/a10006.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush AsteroidBrush8
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "asteroids/small/a10007.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush AsteroidBrush9
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "asteroids/small/a10008.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush AsteroidBrush10
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "asteroids/small/a10009.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush AsteroidBrush11
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "asteroids/small/a10010.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush AsteroidBrush12
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "asteroids/small/a10011.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush AsteroidBrush13
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "asteroids/small/a10012.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush AsteroidBrush14
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "asteroids/small/a10013.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush AsteroidBrush15
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "asteroids/small/a10014.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush AsteroidBrush16
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "asteroids/small/a10015.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Exaust1Brush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Ship5/Exhaust/Normal_flight/Exhaust1/exhaust1.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Exaust2Brush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Ship5/Exhaust/Normal_flight/Exhaust1/exhaust2.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Exaust3Brush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Ship5/Exhaust/Normal_flight/Exhaust1/exhaust3.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Exaust4Brush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Ship5/Exhaust/Normal_flight/Exhaust1/exhaust04.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush TurboExaust1Brush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Ship5/Exhaust/Turbo_flight/Exhaust1/exhaust1.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush TurboExaust2Brush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Ship5/Exhaust/Turbo_flight/Exhaust1/exhaust2.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush TurboExaust3Brush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Ship5/Exhaust/Turbo_flight/Exhaust1/exhaust3.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush TurboExaust4Brush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Ship5/Exhaust/Turbo_flight/Exhaust1/exhaust4.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Ship6Brush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Ship6/Ship6.png"), UriKind.RelativeOrAbsolute)));
            }
        }

        public Brush ShipBrush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Ship5/Ship5.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush enemy2Brush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Ship2/Ship2.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush enemy3Brush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Ship3/Ship3.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush enemy4Brush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Ship4/Ship4.png"), UriKind.RelativeOrAbsolute)));
            }
        }


        public Brush DoubleShooterBrush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "double.jpg"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush TrippleShooterBrush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "tripple.jpg"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush BiggerAmmoBrush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "bfg.jpg"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush HealthBrush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "health.jpg"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush CoinBrush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "coin.jpg"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush FastBrush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "fast.jpg"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush ShieldBrush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "shield.jpg"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush RapidBrush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Shots/Shot4/shot4_5.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush StrongBrush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "strong.jpg"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Shot1Brush1
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Shots/Shot1/shot1_1.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Shot1Brush2
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Shots/Shot1/shot1_2.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Shot1Brush3
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Shots/Shot1/shot1_3.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Shot1Brush4
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Shots/Shot1/shot1_4.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Shot1Brush5
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Shots/Shot1/shot1_asset.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Exploding1
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Explosions/Explosion2/Explosion2_1.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Exploding2
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Explosions/Explosion2/Explosion2_2.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Exploding3
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Explosions/Explosion2/Explosion2_3.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Exploding4
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Explosions/Explosion2/Explosion2_4.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Exploding5
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Explosions/Explosion2/Explosion2_5.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Exploding6
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Explosions/Explosion2/Explosion2_6.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Exploding7
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Explosions/Explosion2/Explosion2_7.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Exploding8
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Explosions/Explosion2/Explosion2_8.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Exploding9
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Explosions/Explosion2/Explosion2_9.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Exploding10
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Explosions/Explosion2/Explosion2_10.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Exploding11
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Explosions/Explosion2/Explosion2_11.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Shot1Brushexp1
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Shots/Shot1/shot1_exp0.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Shot1Brushexp2
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Shots/Shot1/shot1_exp1.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Shot1Brushexp3
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Shots/Shot1/shot1_exp2.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Shot1Brushexp4
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Shots/Shot1/shot1_exp3.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Shot1Brushexp5
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Shots/Shot1/shot1_exp4.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Shot2Brushexp1
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Shots/Shot4/shot4_exp1.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Shot2Brushexp2
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Shots/Shot4/shot4_exp2.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Shot2Brushexp3
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Shots/Shot4/shot4_exp3.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Shot2Brushexp4
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Shots/Shot4/shot4_exp4.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Shot2Brushexp5
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Shots/Shot4/shot4_exp5.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Shot2Brushexp6
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Shots/Shot4/shot4_exp6.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Shot2Brushexp7
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Shots/Shot4/shot4_exp7.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Shot2Brushexp8
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Shots/Shot4/shot4_exp8.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Shot6Brush1
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Shots/Shot6/shot6_1.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Shot6Brush2
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Shots/Shot6/shot6_2.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Shot6Brush3
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Shots/Shot6/shot6_3.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Shot6Brush4
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Shots/Shot6/shot6_4.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Shot6Brush5
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Shots/Shot6/shot6_5.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Shot6Brushexp1
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Shots/Shot6/shot6_exp1.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Shot6Brushexp2
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Shots/Shot6/shot6_exp2.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Shot6Brushexp3
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Shots/Shot6/shot6_exp3.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Shot6Brushexp4
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Shots/Shot6/shot6_exp4.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Shot6Brushexp5
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Shots/Shot6/shot6_exp5.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Shot6Brushexp6
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Shots/Shot6/shot6_exp6.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Shot6Brushexp7
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Shots/Shot6/shot6_exp7.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Shot6Brushexp8
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Shots/Shot6/shot6_exp8.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Shot6Brushexp9
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Shots/Shot6/shot6_exp9.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Shot6Brushexp10
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Shots/Shot6/shot6_exp10.png"), UriKind.RelativeOrAbsolute)));
            }
        }

        public Brush Shot2Brush1
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Shots/Shot4/shot4_1.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Shot2Brush2
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Shots/Shot4/shot4_2.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Shot2Brush3
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Shots/Shot4/shot4_3.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Shot2Brush4
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Shots/Shot4/shot4_4.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Shot2Brush5
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Shots/Shot4/shot4_asset.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Shot3Brush1
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Shots/Shot6/shot6_1.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Shot3Brush2
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Shots/Shot6/shot6_2.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Shot3Brush3
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Shots/Shot6/shot6_3.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Shot3Brush4
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Shots/Shot6/shot6_4.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Shot3Brush5
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Shots/Shot6/shot6_asset.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Shot7Brush1
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Shots/Shot7/shot7_1.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Shot7Brush2
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Shots/Shot7/shot7_2.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Shot7Brush3
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Shots/Shot7/shot7_3.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Shot7Brush4
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Shots/Shot7/shot7_4.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Shot7Brush5
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Shots/Shot7/shot7_asset.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Shot5Brush1
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Shots/Shot5/shot5_1.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Shot5Brush2
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Shots/Shot5/shot5_2.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Shot5Brush3
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Shots/Shot5/shot5_3.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Shot5Brush4
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Shots/Shot5/shot5_4.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Shot5Brush5
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Shots/Shot5/shot5_asset.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Shot5Brushexp1
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Shots/Shot5/shot5_exp1.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Shot5Brushexp2
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Shots/Shot5/shot5_exp2.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Shot5Brushexp3
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Shots/Shot5/shot5_exp3.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Shot5Brushexp4
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Shots/Shot5/shot5_exp4.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Shot5Brushexp5
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Shots/Shot5/shot5_exp5.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Shot5Brushexp6
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Shots/Shot5/shot5_exp6.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Shot5Brushexp7
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Shots/Shot5/shot5_exp7.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Shot5Brushexp8
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Shots/Shot5/shot5_exp8.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush WeaponbarBrush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "Weapon_bar.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush StrongbarBrush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "Strong_bar.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush RapidbarBrush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "Rapid_bar.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush ShieldAuraBrush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "Shield_aura.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush ScoreBrush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "Score.png"), UriKind.RelativeOrAbsolute)));
            }
        }




        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            if (area.Width > 0 && area.Height > 0 && model != null)
            {
                Rect background0 = new Rect(0, backgroundcounter, area.Width, area.Height);
                Rect background1 = new Rect(0, background0.Y - area.Height, area.Width, area.Height);

                drawingContext.DrawRectangle(SpaceBrush, null, background0);
                drawingContext.DrawRectangle(SpaceBrush, null, background1);

                backgroundcounter++;
                if (backgroundcounter > area.Height) backgroundcounter = 0;

                foreach (var item in model.Asteroids)
                {
                    if (Animation && item.IsHit)
                    {
                        Explodings.Add(new Explosion(item.Position, 11, true, false));
                    }
                    else
                    {
                        //if (Animation)
                        //{
                        //    switch (exhaustCounter)
                        //    {
                        //        case < 2:
                        //            drawingContext.DrawEllipse(AsteroidBrush1, null, item.Position, 50, 50);
                        //            break;
                        //        case < 4:
                        //            drawingContext.DrawEllipse(AsteroidBrush2, null, item.Position, 50, 50);
                        //            break;
                        //        case < 6:
                        //            drawingContext.DrawEllipse(AsteroidBrush3, null, item.Position, 50, 50);
                        //            break;
                        //        case < 8:
                        //            drawingContext.DrawEllipse(AsteroidBrush4, null, item.Position, 50, 50);
                        //            break;
                        //        case < 10:
                        //            drawingContext.DrawEllipse(AsteroidBrush5, null, item.Position, 50, 50);
                        //            break;
                        //        case < 12:
                        //            drawingContext.DrawEllipse(AsteroidBrush6, null, item.Position, 50, 50);
                        //            break;
                        //        case < 14:
                        //            drawingContext.DrawEllipse(AsteroidBrush7, null, item.Position, 50, 50);
                        //            break;
                        //        case < 16:
                        //            drawingContext.DrawEllipse(AsteroidBrush8, null, item.Position, 50, 50);
                        //            break;
                        //        case < 18:
                        //            drawingContext.DrawEllipse(AsteroidBrush9, null, item.Position, 50, 50);
                        //            break;
                        //        case < 20:
                        //            drawingContext.DrawEllipse(AsteroidBrush10, null, item.Position, 50, 50);
                        //            break;
                        //        case < 22:
                        //            drawingContext.DrawEllipse(AsteroidBrush11, null, item.Position, 50, 50);
                        //            break;
                        //        case < 24:
                        //            drawingContext.DrawEllipse(AsteroidBrush12, null, item.Position, 50, 50);
                        //            break;
                        //        case < 26:
                        //            drawingContext.DrawEllipse(AsteroidBrush13, null, item.Position, 50, 50);
                        //            break;
                        //        case < 28:
                        //            drawingContext.DrawEllipse(AsteroidBrush14, null, item.Position, 50, 50);
                        //            break;
                        //        case < 30:
                        //            drawingContext.DrawEllipse(AsteroidBrush15, null, item.Position, 50, 50);
                        //            break;
                        //        case < 32:
                        //            drawingContext.DrawEllipse(AsteroidBrush16, null, item.Position, 50, 50);
                        //            break;
                        //        default:
                        //            break;
                        //    }
                        //}
                        /*else*/ drawingContext.DrawEllipse(AsteroidBrush1, null, item.Position, 50, 50);

                        }
                        if (hitboxes)drawingContext.DrawRectangle(null, new Pen(Brushes.Blue, 2), item.Hitbox);
                    }
                    foreach (var item in model.Powerups)
                {
                    switch (item.PowerupType)
                    {
                        case Models.Powerups.Powerup.Type.ExtraScore:
                            drawingContext.DrawEllipse(CoinBrush, null, new Point(item.Position.X, item.Position.Y), 25, 25);
                            break;
                        case Models.Powerups.Powerup.Type.MoreHealth:
                            drawingContext.DrawEllipse(HealthBrush, null, new Point(item.Position.X, item.Position.Y), 25, 25);
                            break;
                        case Models.Powerups.Powerup.Type.RapidFire:
                            drawingContext.DrawEllipse(FastBrush, null, new Point(item.Position.X, item.Position.Y), 25, 25);
                            break;
                        case Models.Powerups.Powerup.Type.Shield:
                            drawingContext.DrawEllipse(ShieldBrush, null, new Point(item.Position.X, item.Position.Y), 25, 25);
                            break;
                        case Models.Powerups.Powerup.Type.Stronger:
                            drawingContext.DrawEllipse(StrongBrush, null, new Point(item.Position.X, item.Position.Y), 25, 25);
                            break;
                        case Models.Powerups.Powerup.Type.Weapon:
                            switch ((item as WeaponPowerup).TypeofWeapon)
                            {
                                case WeaponPowerup.WeaponType.Doubleshooter:
                                    drawingContext.DrawEllipse(DoubleShooterBrush, null, new Point(item.Position.X, item.Position.Y), 25, 25);
                                    break;
                                case WeaponPowerup.WeaponType.Tripplehooter:
                                    drawingContext.DrawEllipse(TrippleShooterBrush, null, new Point(item.Position.X, item.Position.Y), 25, 25);
                                    break;
                                case WeaponPowerup.WeaponType.Biggerammo:
                                    drawingContext.DrawEllipse(BiggerAmmoBrush, null, new Point(item.Position.X, item.Position.Y), 25, 25);
                                    break;
                                case WeaponPowerup.WeaponType.None:
                                    drawingContext.DrawEllipse(Brushes.AntiqueWhite, null, new Point(item.Position.X, item.Position.Y), 25, 25);
                                    break;
                            }

                            break;
                        case Powerup.Type.None:
                            drawingContext.DrawEllipse(Brushes.AntiqueWhite, null, new Point(item.Position.X, item.Position.Y), 25, 25);
                            break;
                        default:
                            break;
                    }
                    if (hitboxes) drawingContext.DrawRectangle(null, new Pen(Brushes.Yellow, 2), item.Hitbox);
                }
                foreach (var item in model.Lasers)
                {
                    if (Animation && item.IsHit)
                    {
                        if (item.Fromplayer) Explodings.Add(new Explosion(item.Position, 8, true, true, item.Big));
                        else Explodings.Add(new Explosion(item.Position, 5, item.Fromplayer, true, false, item.FromBoss));
                    }
                    else
                    {
                        drawingContext.PushTransform(new RotateTransform(item.Angle, item.Position.X, item.Position.Y));
                        if (item.Fromplayer)
                        {
                            if(Animation)
                            {
                                switch (item.Counter)
                                {
                                    case < 2:
                                        if (item.Big) drawingContext.DrawEllipse(Shot5Brush1, null, new Point(item.Position.X + 50, item.Position.Y), item.Ammosize + 70, item.Ammosize + 70);
                                        else drawingContext.DrawEllipse(Shot2Brush1, null, new Point(item.Position.X + 45, item.Position.Y), item.Ammosize + 50, item.Ammosize + 50);
                                        break;
                                    case < 4:
                                        if (item.Big) drawingContext.DrawEllipse(Shot5Brush2, null, new Point(item.Position.X + 40, item.Position.Y), item.Ammosize + 70, item.Ammosize + 70);
                                        else drawingContext.DrawEllipse(Shot2Brush2, null, new Point(item.Position.X + 35, item.Position.Y), item.Ammosize + 50, item.Ammosize + 50);
                                        break;
                                    case < 6:
                                        if (item.Big) drawingContext.DrawEllipse(Shot5Brush3, null, new Point(item.Position.X + 30, item.Position.Y), item.Ammosize + 70, item.Ammosize + 70);
                                        else drawingContext.DrawEllipse(Shot2Brush3, null, new Point(item.Position.X + 25, item.Position.Y), item.Ammosize + 50, item.Ammosize + 50);
                                        break;
                                    case < 8:
                                        if (item.Big)
                                        {
                                            drawingContext.DrawEllipse(Shot5Brush4, null, new Point(item.Position.X + 15, item.Position.Y), item.Ammosize + 70, item.Ammosize + 70);
                                            drawingContext.DrawEllipse(Shot5Brush5, null, new Point(item.Position.X + 10, item.Position.Y), item.Ammosize + 70, item.Ammosize + 70);
                                        }
                                        else
                                        {
                                            drawingContext.DrawEllipse(Shot2Brush4, null, new Point(item.Position.X + 10, item.Position.Y), item.Ammosize + 50, item.Ammosize + 50);
                                            drawingContext.DrawEllipse(Shot2Brush5, null, new Point(item.Position.X + 5, item.Position.Y), item.Ammosize + 50, item.Ammosize + 50);
                                        }
                                        break;
                                    case >= 8:
                                        if (item.Big) drawingContext.DrawEllipse(Shot5Brush5, null, new Point(item.Position.X, item.Position.Y), item.Ammosize + 70, item.Ammosize + 70);
                                        else drawingContext.DrawEllipse(Shot2Brush5, null, new Point(item.Position.X, item.Position.Y), item.Ammosize + 50, item.Ammosize + 50);
                                        break;
                                    default:
                                        break;
                                }
                            }
                            else
                            {
                                if (item.Big) drawingContext.DrawEllipse(Shot5Brush5, null, new Point(item.Position.X, item.Position.Y), item.Ammosize + 70, item.Ammosize + 70);
                                else drawingContext.DrawEllipse(Shot2Brush5, null, new Point(item.Position.X, item.Position.Y), item.Ammosize + 50, item.Ammosize + 50);
                            }



                        }
                        else if(item.FromBoss)
                        {
                            if (Animation)
                            {
                                switch (item.Counter)
                                {
                                    case < 2:
                                        drawingContext.DrawEllipse(Shot7Brush1, null, new Point(item.Position.X + 35, item.Position.Y), item.Ammosize + 25, item.Ammosize + 25);
                                        break;
                                    case < 4:
                                        drawingContext.DrawEllipse(Shot7Brush2, null, new Point(item.Position.X + 25, item.Position.Y), item.Ammosize + 25, item.Ammosize + 25);
                                        break;
                                    case < 6:
                                        drawingContext.DrawEllipse(Shot7Brush3, null, new Point(item.Position.X + 15, item.Position.Y), item.Ammosize + 25, item.Ammosize + 25);
                                        break;
                                    case < 8:
                                        drawingContext.DrawEllipse(Shot7Brush4, null, new Point(item.Position.X + 5, item.Position.Y), item.Ammosize + 25, item.Ammosize + 25);
                                        drawingContext.DrawEllipse(Shot7Brush5, null, new Point(item.Position.X, item.Position.Y), item.Ammosize + 25, item.Ammosize + 25);
                                        break;
                                    case >= 8:
                                        drawingContext.DrawEllipse(Shot7Brush5, null, new Point(item.Position.X, item.Position.Y), item.Ammosize + 25, item.Ammosize + 25);
                                        break;
                                    default:
                                        break;
                                }
                            }
                            else drawingContext.DrawEllipse(Shot7Brush5, null, new Point(item.Position.X, item.Position.Y), item.Ammosize + 25, item.Ammosize + 25);

                        }

                        else
                        {
                            if(Animation)
                            {
                                switch (item.Counter)
                                {
                                    case < 2:
                                        drawingContext.DrawEllipse(Shot1Brush1, null, new Point(item.Position.X + 35, item.Position.Y), item.Ammosize + 30, item.Ammosize + 30);
                                        break;
                                    case < 4:
                                        drawingContext.DrawEllipse(Shot1Brush2, null, new Point(item.Position.X + 25, item.Position.Y), item.Ammosize + 30, item.Ammosize + 30);
                                        break;
                                    case < 6:
                                        drawingContext.DrawEllipse(Shot1Brush3, null, new Point(item.Position.X + 15, item.Position.Y), item.Ammosize + 30, item.Ammosize + 30);
                                        break;
                                    case < 8:
                                        drawingContext.DrawEllipse(Shot1Brush4, null, new Point(item.Position.X + 5, item.Position.Y), item.Ammosize + 30, item.Ammosize + 30);
                                        drawingContext.DrawEllipse(Shot1Brush5, null, new Point(item.Position.X, item.Position.Y), item.Ammosize + 30, item.Ammosize + 30);
                                        break;
                                    case >= 8:
                                        drawingContext.DrawEllipse(Shot1Brush5, null, new Point(item.Position.X, item.Position.Y), item.Ammosize + 30, item.Ammosize + 30);
                                        break;
                                    default:
                                        break;
                                }
                            }
                            else drawingContext.DrawEllipse(Shot1Brush5, null, new Point(item.Position.X, item.Position.Y), item.Ammosize + 30, item.Ammosize + 30);


                        }
                        drawingContext.Pop();
                    }

                    if (hitboxes) drawingContext.DrawRectangle(null, new Pen(Brushes.Orange, 2), item.Hitbox);
                }

                foreach (var item in model.EnemyShips)
                {
                    if (Animation && item.IsHit)
                    {
                        Explodings.Add(new Explosion(item.Position, 11, true, false));
                    }
                    else
                    {
                        if (Animation && item.IsMoving)
                        {
                            switch (exhaustCounter)
                            {
                                case 0:
                                    drawingContext.DrawEllipse(Exaust1Brush, null, new Point(item.Position.X, item.Position.Y - 50), 20, 20);
                                    break;
                                case 1:
                                    drawingContext.DrawEllipse(Exaust2Brush, null, new Point(item.Position.X, item.Position.Y - 50), 20, 20);
                                    break;
                                case 2:
                                    drawingContext.DrawEllipse(Exaust3Brush, null, new Point(item.Position.X, item.Position.Y - 50), 20, 20);
                                    break;
                                case 3:
                                    drawingContext.DrawEllipse(Exaust4Brush, null, new Point(item.Position.X, item.Position.Y - 50), 20, 20);
                                    break;
                                default:
                                    break;
                            }
                        }
                        else if(Animation)
                        {
                            switch (exhaustCounter)
                            {
                                case 0:
                                    drawingContext.DrawEllipse(TurboExaust1Brush, null, new Point(item.Position.X, item.Position.Y - 50), 20, 20);
                                    break;
                                case 1:
                                    drawingContext.DrawEllipse(TurboExaust2Brush, null, new Point(item.Position.X, item.Position.Y - 50), 20, 20);
                                    break;
                                case 2:
                                    drawingContext.DrawEllipse(TurboExaust3Brush, null, new Point(item.Position.X, item.Position.Y - 50), 20, 20);
                                    break;
                                case 3:
                                    drawingContext.DrawEllipse(TurboExaust4Brush, null, new Point(item.Position.X, item.Position.Y - 50), 20, 20);
                                    break;
                                default:
                                    break;
                            }
                        }
                        switch (item.Name)
                        {
                            case EnemyShip.EnemyEnum.one:
                                drawingContext.DrawEllipse(Enemy1Brush, null, new Point(item.Position.X, item.Position.Y), 40, 40);
                                break;
                            case EnemyShip.EnemyEnum.two:
                                drawingContext.DrawEllipse(enemy4Brush, null, new Point(item.Position.X, item.Position.Y), 40, 40);
                                break;
                            case EnemyShip.EnemyEnum.three:
                                drawingContext.DrawEllipse(enemy2Brush, null, new Point(item.Position.X, item.Position.Y), 40, 40);
                                break;
                            case EnemyShip.EnemyEnum.four:
                                drawingContext.DrawEllipse(enemy3Brush, null, new Point(item.Position.X, item.Position.Y), 40, 40);
                                break;
                            default:
                                break;
                        }
                    }
                    if (hitboxes) drawingContext.DrawRectangle(null, new Pen(Brushes.Red, 2), item.Hitbox);
                }

                if (model.Player != null) drawingContext.DrawEllipse(ShipBrush, null, new Point(model.Player.Position.X, model.Player.Position.Y), 40, 40);
                if (model.Player != null && hitboxes) drawingContext.DrawRectangle(null, new Pen(Brushes.Green, 2), model.Player.Hitbox);

                if (model.Boss != null) drawingContext.DrawEllipse(Ship6Brush, null, new Point(model.Boss.Position.X, model.Boss.Position.Y), 100, 100);
                if (model.Boss != null && hitboxes) drawingContext.DrawRectangle(null, new Pen(Brushes.Red, 2), model.Boss.Hitbox);
                if(model.Boss != null && model.Boss.Health > 0)
                {
                    //drawingContext.DrawRectangle(BossHpLeft, null, new Rect(area.Width / 2 - model.Boss.Health / 4 - 10, 35, 10, 10));
                    drawingContext.DrawRectangle(BossHpMiddle, null, new Rect(model.Boss.Position.X - model.Boss.Health / 4, model.Boss.Position.Y - 100, model.Boss.Health / 2, 10));
                    //drawingContext.DrawRectangle(BossHpRight, null, new Rect(area.Width / 2 + model.Boss.Health / 4 + 10, 35, 10, 10));
                }
                if(model.Boss != null && model.Boss.Health <= 0)
                {
                    Explodings.Add(new Explosion(model.Boss.Position, 11, true, false));
                    Explodings.Add(new Explosion(new Point(model.Boss.Position.X - 20, model.Boss.Position.Y - 20), 13, true, false));
                    Explodings.Add(new Explosion(new Point(model.Boss.Position.X + 20, model.Boss.Position.Y + 20), 17, true, false));
                    Explodings.Add(new Explosion(new Point(model.Boss.Position.X - 25, model.Boss.Position.Y - 30), 23, true, false));
                    Explodings.Add(new Explosion(new Point(model.Boss.Position.X + 30, model.Boss.Position.Y + 35), 30, true, false));

                }


                if (Animation)
                {
                    for (int i = 0; i < Explodings.Count; i++)
                    {
                        var item = Explodings[i];
                        if (item.IsLaser)
                        {
                            if (item.FromPlayer)
                            {
                                    switch (item.Counter)
                                    {
                                        case 1:
                                            if(!item.Big) drawingContext.DrawEllipse(Shot2Brushexp8, null, new Point(item.Position.X, item.Position.Y), 30, 30);
                                            else drawingContext.DrawEllipse(Shot5Brushexp8, null, new Point(item.Position.X, item.Position.Y), 50, 50);

                                        break;
                                        case 2:
                                            if(!item.Big) drawingContext.DrawEllipse(Shot2Brushexp7, null, new Point(item.Position.X, item.Position.Y), 30, 30);
                                            else drawingContext.DrawEllipse(Shot5Brushexp7, null, new Point(item.Position.X, item.Position.Y), 50, 50);
                                        break;
                                        case 3:
                                            if (!item.Big) drawingContext.DrawEllipse(Shot2Brushexp6, null, new Point(item.Position.X, item.Position.Y), 30, 30);
                                            else drawingContext.DrawEllipse(Shot5Brushexp6, null, new Point(item.Position.X, item.Position.Y), 50, 50);
                                            break;
                                        case 4:
                                            if (!item.Big) drawingContext.DrawEllipse(Shot2Brushexp5, null, new Point(item.Position.X, item.Position.Y), 30, 30);
                                            else drawingContext.DrawEllipse(Shot5Brushexp5, null, new Point(item.Position.X, item.Position.Y), 50, 50);
                                            break;
                                        case 5:
                                            if (!item.Big) drawingContext.DrawEllipse(Shot2Brushexp4, null, new Point(item.Position.X, item.Position.Y), 30, 30);
                                            else drawingContext.DrawEllipse(Shot5Brushexp4, null, new Point(item.Position.X, item.Position.Y), 50, 50);
                                            break;
                                        case 6:
                                            if (!item.Big) drawingContext.DrawEllipse(Shot2Brushexp3, null, new Point(item.Position.X, item.Position.Y), 30, 30);
                                            else drawingContext.DrawEllipse(Shot5Brushexp3, null, new Point(item.Position.X, item.Position.Y), 50, 50);
                                            break;
                                        case 7:
                                            if (!item.Big) drawingContext.DrawEllipse(Shot2Brushexp2, null, new Point(item.Position.X, item.Position.Y), 30, 30);
                                            else drawingContext.DrawEllipse(Shot5Brushexp2, null, new Point(item.Position.X, item.Position.Y), 50, 50);
                                            break;
                                        default:
                                            if (!item.Big) drawingContext.DrawEllipse(Shot2Brushexp1, null, new Point(item.Position.X, item.Position.Y), 30, 30);
                                            else drawingContext.DrawEllipse(Shot5Brushexp1, null, new Point(item.Position.X, item.Position.Y), 50, 50);
                                            break;
                                    }
                                
                            }
                            else if (item.FromBoss)
                            {
                                switch (item.Counter)
                                {
                                    case 1:
                                        drawingContext.DrawEllipse(Shot6Brushexp10, null, new Point(item.Position.X, item.Position.Y), 30, 30);
                                        break;
                                    case 2:
                                        drawingContext.DrawEllipse(Shot6Brushexp9, null, new Point(item.Position.X, item.Position.Y), 30, 30);
                                        break;
                                    case 3:
                                        drawingContext.DrawEllipse(Shot6Brushexp8, null, new Point(item.Position.X, item.Position.Y), 30, 30);
                                        break;
                                    case 4:
                                        drawingContext.DrawEllipse(Shot6Brushexp7, null, new Point(item.Position.X, item.Position.Y), 30, 30);
                                        break;
                                    case 5:
                                        drawingContext.DrawEllipse(Shot6Brushexp6, null, new Point(item.Position.X, item.Position.Y), 30, 30);
                                        break;
                                    case 6:
                                        drawingContext.DrawEllipse(Shot6Brushexp5, null, new Point(item.Position.X, item.Position.Y), 30, 30);
                                        break;
                                    case 7:
                                        drawingContext.DrawEllipse(Shot6Brushexp4, null, new Point(item.Position.X, item.Position.Y), 30, 30);
                                        break;
                                    case 8:
                                        drawingContext.DrawEllipse(Shot6Brushexp3, null, new Point(item.Position.X, item.Position.Y), 30, 30);
                                        break;
                                    case 9:
                                        drawingContext.DrawEllipse(Shot6Brushexp2, null, new Point(item.Position.X, item.Position.Y), 30, 30);
                                        break;
                                    default:
                                        drawingContext.DrawEllipse(Shot6Brushexp1, null, new Point(item.Position.X, item.Position.Y), 30, 30);
                                        break;
                                }
                            }
                            else
                            {
                                switch (item.Counter)
                                {
                                    case 1:
                                        drawingContext.DrawEllipse(Shot1Brushexp5, null, new Point(item.Position.X, item.Position.Y), 25, 25);
                                        break;
                                    case 2:
                                        drawingContext.DrawEllipse(Shot1Brushexp4, null, new Point(item.Position.X, item.Position.Y), 25, 25);
                                        break;
                                    case 3:
                                        drawingContext.DrawEllipse(Shot1Brushexp3, null, new Point(item.Position.X, item.Position.Y), 25, 25);
                                        break;
                                    case 4:
                                        drawingContext.DrawEllipse(Shot1Brushexp2, null, new Point(item.Position.X, item.Position.Y), 25, 25);
                                        break;
                                    default:
                                        drawingContext.DrawEllipse(Shot1Brushexp1, null, new Point(item.Position.X, item.Position.Y), 25, 25);
                                        break;
                                }
                            }
                        }
                        else
                        {
                            switch (item.Counter)
                            {
                                case 1:
                                    drawingContext.DrawEllipse(Exploding11, null, new Point(item.Position.X, item.Position.Y), 50, 50);
                                    break;
                                case 2:
                                    drawingContext.DrawEllipse(Exploding10, null, new Point(item.Position.X, item.Position.Y), 50, 50);
                                    break;
                                case 3:
                                    drawingContext.DrawEllipse(Exploding9, null, new Point(item.Position.X, item.Position.Y), 50, 50);
                                    break;
                                case 4:
                                    drawingContext.DrawEllipse(Exploding8, null, new Point(item.Position.X, item.Position.Y), 50, 50);
                                    break;
                                case 5:
                                    drawingContext.DrawEllipse(Exploding7, null, new Point(item.Position.X, item.Position.Y), 50, 50);
                                    break;
                                case 6:
                                    drawingContext.DrawEllipse(Exploding6, null, new Point(item.Position.X, item.Position.Y), 50, 50);
                                    break;
                                case 7:
                                    drawingContext.DrawEllipse(Exploding5, null, new Point(item.Position.X, item.Position.Y), 50, 50);
                                    break;
                                case 8:
                                    drawingContext.DrawEllipse(Exploding4, null, new Point(item.Position.X, item.Position.Y), 50, 50);
                                    break;
                                case 9:
                                    drawingContext.DrawEllipse(Exploding3, null, new Point(item.Position.X, item.Position.Y), 50, 50);
                                    break;
                                case 10:
                                    drawingContext.DrawEllipse(Exploding2, null, new Point(item.Position.X, item.Position.Y), 50, 50);
                                    break;
                                case 11:
                                    drawingContext.DrawEllipse(Exploding1, null, new Point(item.Position.X, item.Position.Y), 50, 50);
                                    break;
                                default:
                                    break;
                            }
                        }

                        item.Counter--;
                        if (item.Counter == 0) Explodings.RemoveAt(i);
                    }
                }

                drawingContext.DrawRectangle(HealthBar, null, new Rect(10, 5, 194, 25));
                if (model.Player.Health == 99999) drawingContext.DrawText(FormatText($"∞"), new Point(150, 5));
                else
                {
                    int i = 0;
                    while (i < 10 && i < model.Player.Health / 10)
                    {
                        drawingContext.DrawRectangle(HealthDot, null, new Rect(13 + i * 16, 8, 15, 19));
                        i++;
                    }
                    if(model.Player.Health > 100)
                    {
                        i = 0;
                        while (i < 10 && i < (model.Player.Health - 100) / 10)
                        {
                            drawingContext.DrawRectangle(ExtraHealthDot, null, new Rect(13 + i * 16, 8, 15, 19));
                            i++;
                        }
                    }

                }
                drawingContext.DrawRectangle(ScoreBrush, null, new Rect(area.Width - 140, 10, 70, 15));
                drawingContext.DrawText(FormatText($"{model.Score}"), new Point(area.Width - 65, 5));

                //drawingContext.DrawText(FormatText($"HighScore:{model.HighScore}"), new Point(5, 30));
                //drawingContext.DrawRectangle(ChatboxBrush, null, new Rect(4, 56, 200, 26));
                if (chatpopup != null) drawingContext.DrawText(FormatText(chatpopup), new Point(7, 55));

                if (model.Strong) drawingContext.DrawEllipse(StrongBrush, null, new Point(area.Width -40 , 145), 25, 25);
                if (model.Rapid) drawingContext.DrawEllipse(FastBrush, null, new Point((area.Width - 40), 70), 25, 25);
                if (model.Shield) drawingContext.DrawEllipse(ShieldBrush, null, new Point((area.Width -40), 295), 25, 25);
                if (model.Weaponon)
                {
                    switch (model.Player.Weapon)
                    {
                        case WeaponType.Doubleshooter:
                            drawingContext.DrawEllipse(DoubleShooterBrush, null, new Point(area.Width - 40, 220), 25, 25);
                            break;
                        case WeaponType.Tripplehooter:
                            drawingContext.DrawEllipse(TrippleShooterBrush, null, new Point(area.Width - 40, 220), 25, 25);
                            break;
                        case WeaponType.Biggerammo:
                            drawingContext.DrawEllipse(BiggerAmmoBrush, null, new Point(area.Width - 40, 220), 25, 25);
                            break;
                        default:
                            break;
                    }
                }
                if(model.RapidfireTime > 0) 
                {
                    drawingContext.DrawRectangle(RapidbarBrush, null, new Rect(area.Width - 65, 100, model.RapidfireTime * 5.5, 10));
                }
                if(model.StrongTime > 0)
                {
                    drawingContext.DrawRectangle(StrongbarBrush, null, new Rect(area.Width - 65, 175, model.StrongTime * 5.5, 10));
                }
                if(model.WeaponTime > 0)
                {
                    drawingContext.DrawRectangle(WeaponbarBrush, null, new Rect(area.Width - 65, 250, model.WeaponTime * 5.5, 10));
                }
                if (model.Shield)
                {
                    drawingContext.PushOpacity(0.5);
                    drawingContext.DrawEllipse(ShieldAuraBrush, null, model.Player.Position, 50, 50);
                    drawingContext.Pop();
                }


            }

        }
            private FormattedText FormatText(string text)
            {
            return new FormattedText(text, CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, new Typeface("magneto"), 20, Brushes.White);
            }

        //Bauhaus 93  Cooper Black  magneto
        private void Model_PowerUpPickedUp(object? sender, EventArgs e)
        {
            if (sender != null)
            {
                var powerup = (sender as Powerup);
                switch (powerup.PowerupType)
                {
                    case Powerup.Type.ExtraScore:
                        chatpopup = "Score picked up";
                        break;
                    case Powerup.Type.MoreHealth:
                        chatpopup = "Health picked up";
                        break;
                    case Powerup.Type.RapidFire:
                        chatpopup = "Rapid fire picked up";
                        break;
                    case Powerup.Type.Shield:
                        chatpopup = "Shield picked up";
                        break;
                    case Powerup.Type.Stronger:
                        chatpopup = "Strength picked up";
                        break;
                    case Powerup.Type.Weapon:
                        chatpopup = "Weapon picked up";
                        break;
                    default:
                        break;
                }
                ChatPopupTimer.Start();
            }
        }
        private void Animation_Time_Step(object sender, EventArgs e)
        {
            if (exhaustCounter == 3) exhaustCounter = 0;
                exhaustCounter++;
        }
        private void ChatPopup(object? sender, EventArgs e)
        {
            chatpopup = null;
            ChatPopupTimer.Stop();
        }




    }
}