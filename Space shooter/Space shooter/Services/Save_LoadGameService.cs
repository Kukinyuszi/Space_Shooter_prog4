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
        public class GameSave : IGameModel, ISettings
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
            public bool Shield { get; set; }
            public bool Rapid { get; set; }
            public bool Strong { get; set; }
            public bool Weapon { get; set; }
            public DateTime Date { get; set; }

            public event EventHandler Changed;
        }

        public Save_LoadGameService()
        {
        }

        public void SaveGame(SpaceShooterLogic model)
        {
            GameSave gs = model as GameSave;
            gs.Date = DateTime.Today;

            string json = JsonConvert.SerializeObject(gs);
            File.WriteAllText("Gamesave.json", json);
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
