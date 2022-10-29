using Newtonsoft.Json;
using Space_shooter.Logic;
using Space_shooter.Logic.Interfaces;
using Space_shooter.Models;
using Space_shooter.Models.Powerups;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Space_shooter.Logic.Interfaces.ISettings;

namespace Space_shooter.Services
{
    public class Save_LoadGameService
    {
        public class GameSave : IGameModel
        {
            public List<Asteroid> Asteroids { get; set; }
            public List<EnemyShip> EnemyShips { get; set; }
            public List<Powerup> Powerups { get; set; }
            public List<Laser> Lasers { get; set; }
            public Player Player { get; set; }
            public Boss Boss { get; set; }
            public int Score { get; set; }
            public int Health { get; set; }
            public int Asteroidspeed { get; set; }
            public int Firerate { get; set; }
            public int Poweruprate { get; set; }
            public int Enemyshottimechange { get; set; }
            public int Bossshottimechange { get; set; }
            public bool Godmode { get; set; }
            public bool Sound { get; set; }
            public string PlayerName { get; set; }
            public int EnemySpawnCount { get; set; }
            public int BossHealth { get; set; }
            public Difficulty Difficultyness { get; set; }
            public DateTime Date { get; set; }
            public int RapidfireTime { get; set; }
            public int StrongTime { get; set; }
            public int WeaponTime { get; set; }

            public event EventHandler Changed;


        }

        public Save_LoadGameService()
        {
        }

        public void SaveGame(SpaceShooterLogic model)           // Ezt valaki oldja meg hogy ne kelljen végigmenni az egészen, 
        {                                                       // hanem valami gyerek, vagy interface segítségével, mert nem tudtam, mindig ha átadom neki 
            GameSave gs = SetupSaveData(model);                 //  GameModelel akkor null ot vesz át, ha interface ha utód ha bármi
            string json = JsonConvert.SerializeObject(gs);
            File.WriteAllText("Gamesave.json", json);
        }

        private GameSave SetupSaveData(SpaceShooterLogic model)
        {
            GameSave gs = new GameSave();
            gs.Asteroids = model.Asteroids;
            gs.EnemyShips = model.EnemyShips;
            gs.Lasers = model.Lasers;
            gs.Player = model.Player;
            gs.Boss = model.Boss;
            gs.Score = model.Score;
            gs.Health = model.Health;
            gs.Poweruprate = model.Poweruprate;
            gs.Powerups = model.Powerups;
            gs.Firerate = model.Firerate;
            gs.PlayerName = model.PlayerName;
            gs.WeaponTime = model.WeaponTime;
            gs.StrongTime = model.StrongTime;
            gs.RapidfireTime = model.RapidfireTime;
            gs.Difficultyness = model.Difficultyness;
            gs.BossHealth = model.BossHealth;
            gs.Godmode = model.Godmode;
            gs.Asteroidspeed = model.Asteroidspeed;
            gs.Bossshottimechange = model.Bossshottimechange;
            gs.Enemyshottimechange = model.Enemyshottimechange;
            gs.EnemySpawnCount = model.EnemySpawnCount;
            gs.Date = DateTime.Today;

            return gs;

        }

        public GameSave LoadGame()
        {
            if (File.Exists("Gamesave.json"))
            {
                string jsonscores = File.ReadAllText("Gamesave.json");
                return JsonConvert.DeserializeObject<GameSave>(jsonscores);
            }
            return null;
        }
    }
}
