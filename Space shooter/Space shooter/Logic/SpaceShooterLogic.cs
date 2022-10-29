using Space_shooter.Logic.Interfaces;
using Space_shooter.Models;
using Space_shooter.Models.Powerups;
using Space_shooter.Models.Powerups.Weapons;
using Space_shooter.Services;
using Space_shooter.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using static Space_shooter.Logic.Interfaces.ISettings;
using static Space_shooter.Services.Save_LoadGameService;

namespace Space_shooter.Logic
{
    public class SpaceShooterLogic : IGameModel
    {
        public enum Controls
        {
            Left, Right, Shoot, G, O, D
        }

        //Basic varibles --> like hud counters, game speed and difficulty settings, firerates, enemy firerates

        public event EventHandler Changed, GameOver, PowerUpPickedUp,GamePaused;
        private int asteroidspeed = 5, firerate = 30, poweruprate = 40, enemyfirerate = 60, bosshealth = 400, bossfirerate = 40,
        enemyshottimer = 0, bossshottimer = 0, playershottimer = 0, enemiescount = 2, score = 0, highscore, health = 100, rapidfireTime, strongTime, weaponTime;
        private bool godmode, sound, shield, rapid, strong, weaponon, left, right, shoot, g, o, d;
        private string playername;
        private Difficulty difficulty;
        System.Windows.Size area;

        public List<Laser> Lasers { get; set; }
        public List<Asteroid> Asteroids { get; set; }
        public List<EnemyShip> EnemyShips { get; set; }
        public List<Powerup> Powerups { get; set; }
        public Player Player { get; set; }
        public Boss Boss { get; set; }
        public int Health { get => health; set => health = value; }
        public int Score { get => score; set => score = value; }
        public int HighScore { get => highscore; set => highscore = value; }
        public string PlayerName { get => playername; set => playername = value; }
        public int Asteroidspeed { get => asteroidspeed; set => asteroidspeed = value; }
        public int Firerate { get => firerate; set => firerate = value; }
        public int Poweruprate { get => poweruprate; set => poweruprate = value; }
        public int Enemyshottimechange { get => enemyfirerate; set => enemyfirerate = value; }
        public int Bossshottimechange { get => bossfirerate; set => bossfirerate = value; }
        public int EnemySpawnCount { get => enemiescount; set => enemiescount = value; }
        public int BossHealth { get => bosshealth; set => bosshealth = value; }
        public bool Godmode { get => godmode; set => godmode = value; }
        public bool Sound { get => sound; set => sound = value; }
        public Difficulty Difficultyness { get { return difficulty; } set { difficulty = value; } }
        public int RapidfireTime { get => rapidfireTime; set => rapidfireTime = value; }
        public int StrongTime { get => strongTime; set => strongTime = value; }
        public int WeaponTime { get => weaponTime; set => weaponTime = value; }
        public bool Shield { get => shield; set => shield = value; }
        public bool Rapid { get => rapid; set => rapid = value; }
        public bool Strong { get => strong; set => strong = value; }
        public bool Weaponon { get => weaponon; set => weaponon = value; }
        public Size Area { get => area; set => area = value; }

        static Random random = new Random();

        // set up the properties and the basic game area
        public void SetupSizes(System.Windows.Size area)
        {
            this.Area = area;
            SetupDifficulty();
            highscore = new ScoreBoardService().GetHighScore();
            if (Lasers == null) Lasers = new List<Laser>();
            if (Asteroids == null) Asteroids = new List<Asteroid>();
            if (EnemyShips == null) EnemyShips = new List<EnemyShip>();
            if (Powerups == null) Powerups = new List<Powerup>();
            if (Player == null) Player = new Player(new System.Windows.Point((int)area.Width / 2, (int)area.Height - 50));
            if (Asteroids.Count == 0) Asteroids.Add(new Asteroid(area, Asteroidspeed));
            if (EnemyShips.Count == 0) SetupEnemyes(area);

        }



        public SpaceShooterLogic()
        {

        }

        // controls left-right move shooting --> these are bools so it watches if you keep the key down --> if you relese it, than it becomes false and stops the action
        // if you type god, than godmode activates --> if you press it in wrong order or you push down different key than it resets --> if states

        public void Controldown(Controls control)
        {
            switch (control)
            {
                case Controls.Left:
                    left = true;
                    if (g || o || d) g = o = d = false;
                    break;
                case Controls.Right:
                    right = true;
                    if (g || o || d) g = o = d = false;
                    break;
                case Controls.Shoot:
                    shoot = true;
                    if (g || o || d) g = o = d = false;
                    break;
                case Controls.G:
                    g = true;
                    break;
                case Controls.O:
                    if (g) o = true;
                    else { d = false; g = false; }
                    break;
                case Controls.D:
                    if (g && o)
                    {
                        d = true;
                        Godmode = !Godmode;
                        if (!Godmode) health = 100;
                    }
                    else g = o = false;
                    break;
                default:
                    break;
            }
            Changed?.Invoke(this, null);
        }

        // if you relese the key than control bools became false --> stops the action

        public void Controlup(Controls control)
        {
            switch (control)
            {
                case Controls.Left:
                    left = false;
                    break;
                case Controls.Right:
                    right = false;
                    break;
                case Controls.Shoot:
                    shoot = false;
                    break;
                default:
                    break;
            }
            Changed?.Invoke(this, null);
        }

        //Logic timestep method

        public void TimeStep()
        {
            
                System.Windows.Size size = new System.Windows.Size((int)Area.Width, (int)Area.Height);          // Screen size variable, it is for the objects to know if it leaves the screen
                Rect playerrect = new Rect(Player.Position.X - 15, Player.Position.Y - 12, 30, 25);             // Generates the player hitbox
                if (EnemyShips.Count > 0) EnemyShipsMovement(size);
                if (Boss != null)                                                                               // If it is not a boss round, than the enemies move one time
                {
                    Boss.Move(size);                                                                            // If it is a boss round, than the boss moves, and checks if it is dead ( boss health less than 1) --> sets boss to null
                    IsBossDead(size);                                                                           // It moves first bc if it checks first, than the boss would be null, it cant move
                }

                if (DifficultyByScore() && Boss == null) SetupNewBoss(size);                                    // If the score is 1000-1050(if its 990 and you kill an enemy than it is 1020, so it skips 1000, also it is 1050 bc if you kill the boss you get 50 so at least you have 1050 points)
                                                                                                                // and there is no boss round, than sets up a new boss, and removes all the enemy ships
                LasersMovement(size);
                AsteroidsMovement(size);                                                                        // All the asteroids and enemies move one time
                HitCheck(size);                                                                                 // Goes through all the lasers, asteroids, enemies, if it got hit in the last timestep, than it deletes it
                                                                                                                // it is for the animations --> if you just remove the object, than it dont goes to display class, and cant start the animation
                for (int i = 0; i < Lasers.Count; i++)
                {                                                                                               // Goes through the lasers...
                    var laser = Lasers[i];
                    Rect laserrect = LaserFromWhom(laser);                                                      // ...checks if it is from a player, and creats it hitbox (if it doesnt from a player, than the asteroids dont blow up)

                    AsteroidsCollison(size, playerrect, laser, i, laserrect);                                   // ...checks if the laser hitbox intersected with one of the asteroids hitbox, than it sets them to get hited
                                                                                                                // also checks if the asteroid hitbox intersected with the player hitbox
                    if (Boss == null) EnemyShipsCollisions(size, laser, laserrect, i);                          // ...checks if it is a boss round and if laser hitbox intersected with one of the enemies hitbox, than it sets them to get hited

                    else if (Boss != null) BossCollisions(size, laserrect, laser, i);                           // ...if boss round, checks if it intersected with the boss hitbox, and removes bosses hp

                    if (Collide(size, laserrect, playerrect) && !laser.Fromplayer)                              // ...checks if laser hitbox intersected with the player hitbox 
                    {                                                                                           // If it did than sets the laser to get hit
                        Lasers[i].IsHit = true;                                                                 // If player has shield than it removes it and dont lose health
                        if (shield) shield = false;
                        else health -= 10;
                    }
                }
                if (EnemyShips.Count > 0) EnemiesShoot();                                                       // If there are enemies, than they shoot once

                else if (Boss != null && (bossshottimer == 0 || bossshottimer == Bossshottimechange / 2)) NewEnemyShoot(Boss as EnemyShip);  // If the bosses firerate counter is 0 (or its 2. shot counter is 0) --> 
                                                                                                                                             // --> so it can shoot again, than it shoots with the boss
                PowerupPickup(size, playerrect);                                                                // If the player hitbox intersects with a powerup hitbox, than it picks up
                PlayerInteractions(size);                                                                       // Moves and shoots with the player
                CountersTimeEllapses();                                                                         // All the counters step, if it is 0, than it resets
                SeeIfGameEnds();                                                                                // Check if player health below 1, than the game ends

                Changed?.Invoke(this, null);

                if (godmode)
                {
                    health = 99999;
                }
            
        }
        private void SetupDifficulty()
        {
            switch (Difficultyness)
            {
                case Difficulty.Easy:
                    enemiescount = 1;
                    asteroidspeed = 5;
                    firerate = 25;
                    poweruprate = 50;
                    enemyfirerate = 70;
                    bossfirerate = 60;
                    bosshealth = 300;
                    break;
                case Difficulty.Medium:
                    enemiescount = 2;
                    asteroidspeed = 5;
                    firerate = 30;
                    poweruprate = 40;
                    enemyfirerate = 60;
                    bossfirerate = 40;
                    bosshealth = 400;
                    break;
                case Difficulty.Hard:
                    enemiescount = 3;
                    asteroidspeed = 7;
                    firerate = 30;
                    poweruprate = 30;
                    enemyfirerate = 50;
                    bossfirerate = 40;
                    bosshealth = 500;
                    break;
                default:
                    break;
            }
        }

        private void HitCheck(System.Windows.Size size)
        {
            for (int i = 0; i < Asteroids.Count; i++)
            {
                if (Asteroids[i].IsHit)
                {
                    Asteroids.RemoveAt(i);
                    Asteroids.Add(new Asteroid(size, Asteroidspeed));
                }
            }

            for (int i = 0; i < EnemyShips.Count; i++)
            {
                if (EnemyShips[i].IsHit)
                {
                    EnemyShips.RemoveAt(i);
                    EnemyShips.Add(new EnemyShip(size));
                }
            }

            for (int i = 0; i < Lasers.Count; i++)
            {
                if (Lasers[i].IsHit) { Lasers.RemoveAt(i); }
            }
        }

        private void SetupNewBoss(System.Windows.Size size)
        {
            Boss = new Boss(size, bosshealth);
            EnemyShips.Clear();
        }

        private void IsBossDead(System.Windows.Size size)
        {
            if (Boss.Health <= 0)
            {
                score += 50;
                Boss = null;
                SetupEnemyes(size);
            }
        }

        private Rect LaserFromWhom(Laser laser)
        {
            if (laser.Fromplayer)
            {
                if (laser.Big) return new Rect(laser.Position.X - (laser.Ammosize / 2), laser.Position.Y - (laser.Ammosize / 2), laser.Ammosize, laser.Ammosize);
                return new Rect(laser.Position.X - (laser.Ammosize / 2), laser.Position.Y - (laser.Ammosize / 2), laser.Ammosize, laser.Ammosize);
            }
            else return new Rect(laser.Position.X - (laser.Ammosize / 2), laser.Position.Y - (laser.Ammosize / 2), laser.Ammosize, laser.Ammosize);
        }

        private void BossCollisions(System.Windows.Size size, Rect laserrect, Laser laser, int i)
        {

            Rect bossrect = new Rect(Boss.Position.X - 50, Boss.Position.Y - 80, 100, 160);
            if (Collide(size, laserrect, bossrect) && laser.Fromplayer)
            {
                Lasers[i].IsHit = true;
                if (Strong || Lasers[i].Big) Boss.Health -= 30;
                else Boss.Health -= 10;
            }
        }

        private void EnemyShipsCollisions(System.Windows.Size size, Laser laser, Rect laserrect, int i)
        {
            for (int j = 0; j < EnemyShips.Count; j++)
            {
                var enemy = EnemyShips[j];

                Rect enemyrect = new Rect(enemy.Position.X - 25, enemy.Position.Y - 20, 50, 40);
                if (laser.Fromplayer && !laser.IsHit && Collide(size, laserrect, enemyrect))
                {
                    score += 30;
                    EnemyShips[j].IsHit = true;
                    if (!Strong) Lasers[i].IsHit = true;
                }
            }
        }

        private void AsteroidsCollison(System.Windows.Size size, Rect playerrect, Laser laser, int j, Rect laserrect)
        {
            for (int i = 0; i < Asteroids.Count; i++)
            {
                var asteroid = Asteroids[i];
                Rect asteroidrect = new Rect(asteroid.Position.X - 25, asteroid.Position.Y - 20, 50, 40);
                if (Collide(size, laserrect, asteroidrect) && laser.Fromplayer)
                {
                    PowerupDrop(size, asteroid);
                    score += 10;
                    Asteroids[i].IsHit = true;
                    if (!Strong) Lasers[j].IsHit = true;
                }
                else if (!asteroid.IsHit && Collide(size, playerrect, asteroidrect))
                {
                    Asteroids[i].IsHit = true;
                    if (shield) shield = false;
                    else health -= 10;
                }
            }
        }

        private void EnemiesShoot()
        {
            for (int i = 0; i < EnemyShips.Count; i++)
            {
                if (enemyshottimer == 0)
                {
                    NewEnemyShoot(EnemyShips[i]);
                }
            }
        }

        private void AsteroidsMovement(System.Windows.Size size)
        {
            for (int i = 0; i < Asteroids.Count; i++)
            {
                bool inside = Asteroids[i].Move(size);
                if (!inside)
                {
                    Asteroids.RemoveAt(i);
                    Asteroids.Add(new Asteroid(size, Asteroidspeed));
                }
            }
        }

        private void SetupEnemyes(System.Windows.Size area)
        {
            for (int i = 0; i < enemiescount; i++)
            {
                EnemyShips.Add(new EnemyShip(area));
            }
        }

        private void NewPlayerShoot()
        {
            Point playerpositiontemp = new System.Windows.Point(Player.Position.X, Player.Position.Y - 25);
            switch (Player.Weapon)
            {
                case WeaponPowerup.WeaponType.Doubleshooter:
                    Lasers.Add(new Laser(playerpositiontemp, new Vector(-1, -5), true, false));
                    Lasers.Add(new Laser(playerpositiontemp, new Vector(1, -5), true, false));
                    break;
                case WeaponPowerup.WeaponType.Tripplehooter:
                    Lasers.Add(new Laser(new Point(playerpositiontemp.X - 20, playerpositiontemp.Y), new Vector(0, -5), true, false));
                    Lasers.Add(new Laser(playerpositiontemp, new Vector(0, -5), true, false));
                    Lasers.Add(new Laser(new Point(playerpositiontemp.X + 20, playerpositiontemp.Y), new Vector(0, -5), true, false));
                    break;
                case WeaponPowerup.WeaponType.Biggerammo:
                    Lasers.Add(new Laser(playerpositiontemp, new Vector(0, -5), true, true));
                    break;
                case WeaponPowerup.WeaponType.None:
                    Lasers.Add(new Laser(playerpositiontemp, new Vector(0, -5), true, false));
                    break;
                default:
                    break;
            }
        }

        private void NewEnemyShoot(EnemyShip enemyship)
        {
            Point enemyshippositiontemp = new System.Windows.Point(enemyship.Position.X, enemyship.Position.Y + 23);
            Point bosspositiontemp = new System.Windows.Point(enemyship.Position.X, enemyship.Position.Y + 60);
            double x = ((Player.Position.X) - enemyship.Position.X) / 40;
            double y = ((Player.Position.Y - 40) - enemyship.Position.Y + 23) / 40;

            int x1;
            if (enemyship.Position.X < Area.Width / 2) x1 = random.Next(4);
            else x1 = random.Next(-4, 0);

            switch (enemyship.Name)
            {
                case EnemyShip.EnemyEnum.one:
                    Lasers.Add(new Laser(enemyshippositiontemp, new Vector(0, 5), false, false));
                    break;
                case EnemyShip.EnemyEnum.two:
                    Lasers.Add(new Laser(enemyshippositiontemp, new Vector(1, 5), false, false));
                    Lasers.Add(new Laser(enemyshippositiontemp, new Vector(-1, 5), false, false));
                    break;
                case EnemyShip.EnemyEnum.three:
                    Lasers.Add(new Laser(enemyshippositiontemp, new Vector(x, y), false, false));
                    break;
                case EnemyShip.EnemyEnum.four:
                    Lasers.Add(new Laser(enemyshippositiontemp, new Vector(x1, 10), false, false));
                    break;
                case EnemyShip.EnemyEnum.boss:
                    if (bossshottimer == Bossshottimechange / 2) Lasers.Add(new Laser(bosspositiontemp, new Vector(Math.Round(x) * 1.5, Math.Round(y) * 1.5), false, false));
                    else
                    {
                        Lasers.Add(new Laser(bosspositiontemp, new Vector((x * 2) - 2, y * 1.5), false, false));
                        Lasers.Add(new Laser(bosspositiontemp, new Vector(x * 2 + random.Next(-1, 2), y * 1.5), false, false));
                        Lasers.Add(new Laser(bosspositiontemp, new Vector((x * 2) + 2, y * 1.5), false, false));
                    }
                    break;
                default:
                    break;
            }
        }

        private void CountersTimeEllapses()
        {
            if (enemyshottimer == 0) enemyshottimer = Enemyshottimechange;
            if (enemyshottimer > 0) enemyshottimer--;
            if (bossshottimer == 0) bossshottimer = Bossshottimechange;
            if (bossshottimer > 0) bossshottimer--;

            if (playershottimer > 0) playershottimer--;
        }

        private void SeeIfGameEnds()
        {
            if (health <= 0)
            {
                health = 0;
                new ScoreBoardService().SaveNewScore(Score, PlayerName);
                GameOver?.Invoke(this, null);

            }
        }

        private void PlayerInteractions(System.Windows.Size size)
        {
            if (left)
            {
                Player.left = true;
                Player.Move(size);
            }

            if (right)
            {
                Player.left = false;
                Player.Move(size);
            }

            if (shoot && playershottimer <= 0)
            {
                NewPlayerShoot();
                if (Rapid)
                {
                    playershottimer = 10;
                }
                else
                {
                    playershottimer = Firerate;
                }
            }
        }

        private void PowerupPickup(System.Windows.Size size, Rect playerrect)
        {
            for (int i = 0; i < Powerups.Count; i++)
            {
                bool inside = Powerups[i].Move(size);
                if (!inside)
                {
                    Powerups.RemoveAt(i);
                }
                else
                {
                    Rect poweruprect = new Rect(Powerups[i].Position.X - 25, Powerups[i].Position.Y - 20, 50, 40);
                    if (poweruprect.IntersectsWith(playerrect))
                    {

                        object obj = Powerups[i] as object;
                        switch (Powerups[i].PowerupType)
                        {
                            case Powerup.Type.ExtraScore:
                                score += 30;
                                PowerUpPickedUp?.Invoke(obj, null);
                                break;
                            case Powerup.Type.MoreHealth:
                                health += 20;
                                PowerUpPickedUp?.Invoke(obj, null);
                                break;
                            case Powerup.Type.RapidFire:
                                Rapid = true;
                                rapidfireTime = 9;
                                PowerUpPickedUp?.Invoke(obj, null);
                                break;
                            case Powerup.Type.Shield:
                                shield = true;
                                PowerUpPickedUp?.Invoke(obj, null);
                                break;
                            case Powerup.Type.Stronger:
                                Strong = true;
                                strongTime = 9;
                                PowerUpPickedUp?.Invoke(obj, null);
                                break;
                            case Powerup.Type.Weapon:
                                Weaponon = true;
                                weaponTime = 9;
                                PowerUpPickedUp?.Invoke(obj, null);
                                switch ((Powerups[i] as WeaponPowerup).TypeofWeapon)
                                {
                                    case WeaponPowerup.WeaponType.Doubleshooter:
                                        Player.Weapon = WeaponPowerup.WeaponType.Doubleshooter;
                                        break;
                                    case WeaponPowerup.WeaponType.Tripplehooter:
                                        Player.Weapon = WeaponPowerup.WeaponType.Tripplehooter;
                                        break;
                                    case WeaponPowerup.WeaponType.Biggerammo:
                                        Player.Weapon = WeaponPowerup.WeaponType.Biggerammo;
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            default:
                                break;
                        }
                        Powerups.RemoveAt(i);
                    }
                }
            }
        }

        private bool Collide(System.Windows.Size size, Rect rect1, Rect rect2)
        {
            if (rect1.IntersectsWith(rect2))
            {
                return true;
            }
            else return false;
        }

        private void PowerupDrop(System.Windows.Size size, Asteroid asteroid)
        {
            if (random.Next(100) < Poweruprate)
            {
                int temprand = random.Next(101);
                Point position = asteroid.Position;
                switch (temprand)
                {
                    case < 25:
                        Powerups.Add(new ExtraScorePowerup(size, Asteroidspeed, position));
                        break;
                    case < 50:
                        Powerups.Add(new MoreHealthPowerup(size, Asteroidspeed, position));
                        break;
                    case < 65:
                        Powerups.Add(new RapidFirePowerup(size, Asteroidspeed, position));
                        break;
                    case < 80:
                        Powerups.Add(new ShieldPowerup(size, Asteroidspeed, position));
                        break;
                    case < 90:
                        Powerups.Add(new StrongerPowerup(size, Asteroidspeed, position));
                        break;
                    default:
                        int tempweaponrand = random.Next(3);
                        switch (tempweaponrand)
                        {
                            case 0:
                                Powerups.Add(new BiggerAmmo(size, Asteroidspeed, position));
                                break;
                            case 1:
                                Powerups.Add(new Shotgun(size, Asteroidspeed, position));
                                break;
                            case 2:
                                Powerups.Add(new TrippleShooter(size, Asteroidspeed, position));
                                break;
                            default:
                                break;
                        }
                        break;
                }
            }
        }

        private void EnemyShipsMovement(System.Windows.Size size)
        {
            foreach (var item in EnemyShips)
            {
                item.Move(size);
            }
        }

        private bool DifficultyByScore()
        {

            if (score > 1000 && score % 1000 <= 50)
            {
                return true;
            }
            return false;
        }

        private void LasersMovement(System.Windows.Size size)
        {
            for (int i = 0; i < Lasers.Count; i++)
            {
                Lasers[i].Counter++;
                bool inside = Lasers[i].Move(size);
                if (!inside)
                {
                    Lasers.RemoveAt(i);
                }
            }
        }
        public void Powerup_Timer_Step()
        {
            if (RapidfireTime > 0) rapidfireTime--;
            else Rapid = false;
            if (strongTime > 0) strongTime--;
            else Strong = false;
            if (weaponTime > 0) weaponTime--;
            else { Weaponon = false; Player.Weapon = WeaponPowerup.WeaponType.None; }
        }
    }
}
