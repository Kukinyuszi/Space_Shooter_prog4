using Space_shooter.Logic.Interfaces;
using Space_shooter.Models;
using Space_shooter.Models.Enemies;
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
using static Space_shooter.Models.Boss;
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

        public event EventHandler Changed, GameOver, PowerUpPickedUp, PlayerShoot, EnemyShoot, Coin_Pickup, Health_Pickup, Powerup_Pickup, Shield_Pickup, Explosion, Godmode_activated;
        private int asteroidSpeed = 5, fireRate = 30, powerupRate = 40, enemyFireRate = 60, bossSpawnHealth = 300, bossFireRate = 40, angleCounter = -5,enemySpawnCountTemp,enemyFireRateTemp,
        enemyShotTimer = 0, bossShotTimer = 0, playerShotTimer = 0, enemiesSpawnCount = 2, score = 0, highScore, rapidFireTime, strongTime, weaponTime;
        private bool godmode, shield, rapid, strong, weaponon, left, right, shoot, g, o, d;
        private string playername;
        private Difficulty difficulty;
        System.Windows.Size area;

        public List<Laser> Lasers { get; set; }
        public List<Asteroid> Asteroids { get; set; }
        public List<EnemyShip> EnemyShips { get; set; }
        public List<Powerup> Powerups { get; set; }
        public Player Player { get; set; }
        public Boss Boss { get; set; }
        public int Score { get => score; set => score = value; }
        public int HighScore { get => highScore; set => highScore = value; }
        public string PlayerName { get => playername; set => playername = value; }
        public int Asteroidspeed { get => asteroidSpeed; set => asteroidSpeed = value; }
        public int Firerate { get => fireRate; set => fireRate = value; }
        public int Poweruprate { get => powerupRate; set => powerupRate = value; }
        public int Enemyshottimechange { get => enemyFireRate; set => enemyFireRate = value; }
        public int Bossshottimechange { get => bossFireRate; set => bossFireRate = value; }
        public int EnemySpawnCount { get => enemiesSpawnCount; set => enemiesSpawnCount = value; }
        public int BossHealth { get => bossSpawnHealth; set => bossSpawnHealth = value; }
        public bool Godmode { get => godmode; set => godmode = value; }
        public Difficulty Difficultyness { get { return difficulty; } set { difficulty = value; } }
        public int RapidfireTime { get => rapidFireTime; set => rapidFireTime = value; }
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
            highScore = new ScoreBoardService().GetHighScore();
            if (Lasers == null) Lasers = new List<Laser>();
            if (Asteroids == null) Asteroids = new List<Asteroid>();
            if (EnemyShips == null) EnemyShips = new List<EnemyShip>();
            if (Powerups == null) Powerups = new List<Powerup>();
            if (Player == null) Player = new Player(area);
            if (Asteroids.Count == 0) Asteroids.Add(new Asteroid(area, Asteroidspeed));
            if (EnemyShips.Count == 0 && Boss == null) SetupEnemyes(area);
            Godmode_activated?.Invoke(this, null);

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
                        Godmode_activated?.Invoke(this, null);
                        if (!Godmode) Player.Health = 100;
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
                                                                                
                if (EnemyShips.Count > 0)
                {
                     EnemyShipsMovement(size);
                     if(EnemyShips.Count > 1) EnemyShipsClearance();
                }
                if (Boss != null)                                                                               // If it is not a boss round, than the enemies move one time
                {
                    Boss.MoveSideWays(size);                                                                            // If it is a boss round, than the boss moves, and checks if it is dead ( boss health less than 1) --> sets boss to null
                    IsBossDead(size);                                                                           // It moves first bc if it checks first, than the boss would be null, it cant move
                }

                if (DifficultyByScore() && Boss == null && bossSpawnHealth != 0) SetupNewBoss(size);            // If the score is 1000-1050(if its 990 and you kill an enemy than it is 1020, so it skips 1000, also it is 1050 bc if you kill the boss you get 50 so at least you have 1050 points)
                                                                                                                // and there is no boss round, than sets up a new boss, and removes all the enemy ships
                LasersMovement(size);
                AsteroidsMovement(size);
                                                                                                                // All the asteroids and enemies move one time
                HitCheck(size);                                                                                 // Goes through all the lasers, asteroids, enemies, if it got hit in the last timestep, than it deletes it
                                                                                                                // it is for the animations --> if you just remove the object, than it dont goes to display class, and cant start the animation
                for (int i = 0; i < Lasers.Count; i++)
                {                                                                                               // Goes through the lasers...
                    var laser = Lasers[i];
                                                             

                    AsteroidsCollison(i);                                                                       // ...checks if the laser hitbox intersected with one of the asteroids hitbox, than it sets them to get hited
                                                                                                                // also checks if the asteroid hitbox intersected with the player hitbox
                    if (Boss == null) EnemyShipsCollisions(i);                                                  // ...checks if it is a boss round and if laser hitbox intersected with one of the enemies hitbox, than it sets them to get hited

                    else if (Boss != null) BossCollisions(i);                                                   // ...if boss round, checks if it intersected with the boss hitbox, and removes bosses hp

                    if (Collide(laser.Hitbox, Player.Hitbox) && !laser.Fromplayer)                              // ...checks if laser hitbox intersected with the player hitbox 
                    {                                                                                           // If it did than sets the laser to get hit
                        Lasers[i].IsHit = true;                                                                 // If player has shield than it removes it and dont lose health
                        if (shield) shield = false;
                        else Player.Health -= 10;
                    }
                }
                if (EnemyShips.Count > 0) EnemiesShoot();                                                       // If there are enemies, than they shoot once

                else if (Boss != null) NewEnemyShoot(Boss);                                                     // If the bosses firerate counter is 0 (or its 2. shot counter is 0) --> 
                                                                                                                // --> so it can shoot again, than it shoots with the boss
                PowerupPickup(size);                                                                            // If the player hitbox intersects with a powerup hitbox, than it picks up
                PlayerInteractions(size);                                                                       // Moves and shoots with the player
                CountersTimeEllapses();                                                                         // All the counters step, if it is 0, than it resets
                SeeIfGameEnds();                                                                                // Check if player health below 1, than the game ends

                Changed?.Invoke(this, null);

                if (godmode)
                {
                    Player.Health = 99999;
                }
            
        }

        private void EnemyShipsClearance()
        {
            for (int i = 1; i < EnemyShips.Count; i++)
            {
                if(Collide(EnemyShips[0].Hitbox,EnemyShips[i].Hitbox) && EnemyShips[0].left == EnemyShips[i].left) EnemyShips[0].left = !EnemyShips[0].left;
            }
        }

        private void SetupDifficulty()
        {
            switch (Difficultyness)
            {
                case Difficulty.Easy:
                    enemiesSpawnCount = 1;
                    asteroidSpeed = 5;
                    fireRate = 25;
                    powerupRate = 50;
                    enemyFireRate = 70;
                    bossFireRate = 60;
                    bossSpawnHealth = 250;
                    break;
                case Difficulty.Medium:
                    enemiesSpawnCount = 2;
                    asteroidSpeed = 5;
                    fireRate = 30;
                    powerupRate = 40;
                    enemyFireRate = 60;
                    bossFireRate = 40;
                    bossSpawnHealth = 300;
                    break;
                case Difficulty.Hard:
                    enemiesSpawnCount = 3;
                    asteroidSpeed = 7;
                    fireRate = 30;
                    powerupRate = 30;
                    enemyFireRate = 50;
                    bossFireRate = 40;
                    bossSpawnHealth = 400;
                    break;
                default:
                    break;
            }
            enemyFireRateTemp = enemyFireRate;
            enemySpawnCountTemp = enemiesSpawnCount;
        }

        public void RelocateObjects(Size area)
        {
            if(Player.Position.X >= area.Width - 25 )Player.Position = new Point(area.Width - 25, Player.Position.Y);
            if (Player.Position.X <= 0) Player.Position = new Point(0, Player.Position.Y);
            foreach (var enemy in EnemyShips)
            {
                if (enemy.Position.X >= area.Width - 25) enemy.Position = new Point(random.Next(25, (int)area.Width - 25), enemy.Position.Y);
            }
        }

        private void HitCheck(System.Windows.Size size)
        {
            for (int i = 0; i < Asteroids.Count; i++)
            {
                if (Asteroids[i].IsHit)
                {
                    Asteroids.RemoveAt(i);
                    Explosion?.Invoke(this, null);
                    Asteroids.Add(new Asteroid(size, Asteroidspeed));
                }
            }

            for (int i = 0; i < EnemyShips.Count; i++)
            {
                if (EnemyShips[i].IsHit)
                {
                    EnemyShips.RemoveAt(i);
                    Explosion?.Invoke(this, null);
                    SetupEnemyes(area);
                }
            }

            for (int i = 0; i < Lasers.Count; i++)
            {
                if (Lasers[i].IsHit) { Lasers.RemoveAt(i); }
            }
        }

        private void SetupNewBoss(System.Windows.Size size)
        {
            if((score % 2000) < 90)
            {
                Boss = new Boss2(area, bossSpawnHealth);
            }
            else Boss = new Boss1(area, bossSpawnHealth);

            EnemyShips.Clear();
        }

        private void IsBossDead(System.Windows.Size size)
        {
            if (Boss.Health <= 0)
            {
                score += 100;
                Boss = null;
                SetupEnemyes(size);
            }
        }

        private void BossCollisions(int i)
        {

            if (Collide(Lasers[i].Hitbox, Boss.Hitbox) && Lasers[i].Fromplayer)
            {
                Lasers[i].IsHit = true;
                if (Strong || Lasers[i].Big) Boss.Health -= 20;
                else Boss.Health -= 10;
            }
        }

        private void EnemyShipsCollisions(int i)
        {
            for (int j = 0; j < EnemyShips.Count; j++)
            {
                var enemy = EnemyShips[j];

                if (Lasers[i].Fromplayer && !Lasers[i].IsHit && Collide(Lasers[i].Hitbox, enemy.Hitbox))
                {
                    score += 30;
                    EnemyShips[j].IsHit = true;
                    if (!Strong) Lasers[i].IsHit = true;
                }
            }
        }

        private void AsteroidsCollison(int j)
        {
            for (int i = 0; i < Asteroids.Count; i++)
            {
                var asteroid = Asteroids[i];
                if (!asteroid.IsHit && Collide(Lasers[j].Hitbox, asteroid.Hitbox) && Lasers[j].Fromplayer)
                {
                    PowerupDrop(asteroid);
                    score += 10;
                    Asteroids[i].IsHit = true;
                    if (!Strong) Lasers[j].IsHit = true;
                }
                else if (!asteroid.IsHit && Collide(Player.Hitbox, asteroid.Hitbox))
                {
                    Asteroids[i].IsHit = true;
                    if (shield) shield = false;
                    else Player.Health -= 10;
                }
            }
        }

        private void EnemiesShoot()
        {
            for (int i = 0; i < EnemyShips.Count; i++)
            {
                if (enemyShotTimer == 0 && !EnemyShips[i].IsMoving)
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

            while(EnemyShips.Count != enemiesSpawnCount)
            {
                int temp = random.Next(4);

                switch (temp)
                {
                    case 0:
                        EnemyShips.Add(new Enemy1(area));
                        break;
                    case 1:
                        EnemyShips.Add(new Enemy2(area));
                        break;
                    case 2:
                        EnemyShips.Add(new Enemy3(area));
                        break;
                    case 3:
                        EnemyShips.Add(new Enemy4(area));
                        break;
                    default:
                        throw new Exception("Enemy setup error");
                }
            }
        }

        private void NewPlayerShoot()
        {
            Point playerpositiontemp = new System.Windows.Point(Player.Position.X, Player.Position.Y - 25);
            switch (Player.Weapon)
            {
                case WeaponPowerup.WeaponType.Doubleshooter:
                    Lasers.Add(new Laser(playerpositiontemp, new Vector(-1, -5), true));
                    Lasers.Add(new Laser(playerpositiontemp, new Vector(1, -5), true));
                    break;
                case WeaponPowerup.WeaponType.Tripplehooter:
                    Lasers.Add(new Laser(new Point(playerpositiontemp.X - 20, playerpositiontemp.Y), new Vector(0, -5), true));
                    Lasers.Add(new Laser(playerpositiontemp, new Vector(0, -5), true, false));
                    Lasers.Add(new Laser(new Point(playerpositiontemp.X + 20, playerpositiontemp.Y), new Vector(0, -5), true));
                    break;
                case WeaponPowerup.WeaponType.Biggerammo:
                    Lasers.Add(new Laser(playerpositiontemp, new Vector(0, -5), true, true));
                    break;
                case WeaponPowerup.WeaponType.None:
                    Lasers.Add(new Laser(playerpositiontemp, new Vector(0, -5), true));
                    break;
                default:
                    break;
            }
        }

        private void NewEnemyShoot(EnemyShip enemyship)
        {
            Point enemyshippositiontemp = new System.Windows.Point(enemyship.Position.X, enemyship.Position.Y + 23);
            double x = ((Player.Position.X) - enemyship.Position.X) / 40;
            double y = ((Player.Position.Y - 40) - enemyship.Position.Y + 23) / 40;

            int x1;
            if (enemyship.Position.X < Area.Width / 2) x1 = random.Next(4);
            else x1 = random.Next(-4, 0);

            switch (enemyship.Name)
            {
                case EnemyShip.EnemyEnum.one:
                    Lasers.Add(new Laser(enemyshippositiontemp, new Vector(0, 7)));
                    EnemyShoot?.Invoke(this, null);
                    break;
                case EnemyShip.EnemyEnum.two:
                    Lasers.Add(new Laser(enemyshippositiontemp, new Vector(1, 6)));
                    Lasers.Add(new Laser(enemyshippositiontemp, new Vector(-1, 6)));
                    EnemyShoot?.Invoke(this, null);
                    break;
                case EnemyShip.EnemyEnum.three:
                    Lasers.Add(new Laser(enemyshippositiontemp, new Vector(x, y)));
                    EnemyShoot?.Invoke(this, null);
                    break;
                case EnemyShip.EnemyEnum.four:
                    Lasers.Add(new Laser(enemyshippositiontemp, new Vector(x1, 10)));
                    EnemyShoot?.Invoke(this, null);
                    break;
                case EnemyShip.EnemyEnum.boss:
                     BossShoot(enemyship, x, y);
                    break;
                default:
                    break;
            }
            

        }

            private void BossShoot(EnemyShip enemyship, double x, double y)
            {
               Point bosspositiontemp = new System.Windows.Point(enemyship.Position.X, enemyship.Position.Y + 60);
               switch ((enemyship as Boss).BossType)
               {
                case BossName.Claec:
                    if (bossShotTimer == Bossshottimechange / 2)
                    {
                        Lasers.Add(new Laser(bosspositiontemp, new Vector(Math.Round(x) * 1.5, Math.Round(y) * 1.5), false, false, true));
                        EnemyShoot?.Invoke(this, null);
                    }
                    else if(bossShotTimer == 0)
                    {
                        Lasers.Add(new Laser(bosspositiontemp, new Vector((x * 2) - 2, y * 1.5), false, false, true));
                        Lasers.Add(new Laser(bosspositiontemp, new Vector(x * 2 + random.Next(-1, 2), y * 1.5), false, false, true));
                        Lasers.Add(new Laser(bosspositiontemp, new Vector((x * 2) + 2, y * 1.5), false, false, true));
                        EnemyShoot?.Invoke(this, null);
                    }
                    break;
                case BossName.Kasdeya:
                    if (bossShotTimer % (bossFireRate / 5) == 0)
                    {
                        Lasers.Add(new Laser(bosspositiontemp, new Vector(angleCounter, 15), false, false, true));
                        EnemyShoot?.Invoke(this, null);
                        if (angleCounter >= 5) angleCounter = -5;
                        else angleCounter++;
                    }

                    break;
                case BossName.None:
                    break;
               }
            }

        private void CountersTimeEllapses()
        {
            if (enemyShotTimer == 0) enemyShotTimer = Enemyshottimechange;
            if (enemyShotTimer > 0) enemyShotTimer--;
            if (bossShotTimer == 0) bossShotTimer = bossFireRate;
            if (bossShotTimer > 0) bossShotTimer--;

            if (playerShotTimer > 0) playerShotTimer--;
        }

        private void SeeIfGameEnds()
        {
            if (Player.Health <= 0)
            {
                Player.Health = 0;
                new ScoreBoardService().SaveNewScore(Score, PlayerName, difficulty);
                GameOver?.Invoke(this, null);

            }
        }

        private void PlayerInteractions(System.Windows.Size size)
        {
            if(Player.Position.Y > size.Height - 100)
            {
                Player.MoveUp();
            }
            else if(Player.Position.Y < size.Height - 105)
            {
                Player.MoveDown();
            }
            else
            {
                if (left)
                {
                    Player.left = true;
                    Player.MoveSideWays(size);
                }

                if (right)
                {
                    Player.left = false;
                    Player.MoveSideWays(size);
                }
            }

            if (shoot && playerShotTimer <= 0)
            {
                NewPlayerShoot();
                PlayerShoot?.Invoke(this, null);
                if (Rapid)
                {
                    playerShotTimer = 10;
                }
                else
                {
                    playerShotTimer = Firerate;
                }
            }
        }

        private void PowerupPickup(System.Windows.Size size)
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
                    if (Powerups[i].Hitbox.IntersectsWith(Player.Hitbox))
                    {

                        object obj = Powerups[i] as object;
                        switch (Powerups[i].PowerupType)
                        {
                            case Powerup.Type.ExtraScore:
                                score += 30;
                                PowerUpPickedUp?.Invoke(obj, null);
                                Coin_Pickup?.Invoke(obj, null);
                                break;
                            case Powerup.Type.MoreHealth:
                                Player.Health += 20;
                                if (Player.Health > 200) Player.Health = 200;
                                PowerUpPickedUp?.Invoke(obj, null);
                                Health_Pickup?.Invoke(obj, null);
                                break;
                            case Powerup.Type.RapidFire:
                                Rapid = true;
                                rapidFireTime = 9;
                                PowerUpPickedUp?.Invoke(obj, null);
                                Powerup_Pickup?.Invoke(obj, null);
                                break;
                            case Powerup.Type.Shield:
                                shield = true;
                                PowerUpPickedUp?.Invoke(obj, null);
                                Shield_Pickup?.Invoke(obj, null);
                                break;
                            case Powerup.Type.Stronger:
                                Strong = true;
                                strongTime = 9;
                                PowerUpPickedUp?.Invoke(obj, null);
                                Powerup_Pickup?.Invoke(obj, null);
                                break;
                            case Powerup.Type.Weapon:
                                Weaponon = true;
                                weaponTime = 9;
                                PowerUpPickedUp?.Invoke(obj, null);
                                Powerup_Pickup?.Invoke(obj, null);
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

        private bool Collide(Rect rect1, Rect rect2)
        {
            if (rect1.IntersectsWith(rect2))
            {
                return true;
            }
            else return false;
        }

        private void PowerupDrop(Asteroid asteroid)
        {
            if (random.Next(99) < Poweruprate)
            {
                int temprand = random.Next(101);
                Point position = asteroid.Position;
                switch (temprand)
                {
                    case < 25:
                        Powerups.Add(new ExtraScorePowerup(Asteroidspeed, position));
                        break;
                    case < 50:
                        Powerups.Add(new MoreHealthPowerup(Asteroidspeed, position));
                        break;
                    case < 65:
                        Powerups.Add(new RapidFirePowerup(Asteroidspeed, position));
                        break;
                    case < 80:
                        Powerups.Add(new ShieldPowerup(Asteroidspeed, position));
                        break;
                    case < 90:
                        Powerups.Add(new StrongerPowerup(Asteroidspeed, position));
                        break;
                    default:
                        int tempweaponrand = random.Next(3);
                        switch (tempweaponrand)
                        {
                            case 0:
                                Powerups.Add(new BiggerAmmo(Asteroidspeed, position));
                                break;
                            case 1:
                                Powerups.Add(new Shotgun(Asteroidspeed, position));
                                break;
                            case 2:
                                Powerups.Add(new TrippleShooter(Asteroidspeed, position));
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
                item.MoveSideWays(size);
            }
        }

        private bool DifficultyByScore()
        {

            if (score > 999 && score % 1000 < 90)
            {
                IncreaseDifficulty(score / 1000);
                return true;
            }
            return false;
        }

        private void IncreaseDifficulty(int kiloScore)
        {
            if (kiloScore < 5)
            {
                enemyFireRate = enemyFireRateTemp - (kiloScore * 5);
                if (kiloScore % 2 == 0)
                {
                    enemiesSpawnCount = enemySpawnCountTemp + (kiloScore / 2);
                }
            }
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
            if (RapidfireTime > 0) rapidFireTime--;
            else Rapid = false;
            if (strongTime > 0) strongTime--;
            else Strong = false;
            if (weaponTime > 0) weaponTime--;
            else { Weaponon = false; Player.Weapon = WeaponPowerup.WeaponType.None; }
        }
    }
}