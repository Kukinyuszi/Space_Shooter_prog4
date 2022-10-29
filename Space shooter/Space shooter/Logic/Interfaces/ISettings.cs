using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_shooter.Logic.Interfaces
{
    public interface ISettings
    {
        public enum Difficulty
        {
            Easy, Medium, Hard, Custom
        }

        int Asteroidspeed { get; set; }
        int Firerate { get; set; }
        int Poweruprate { get; set; }
        int Enemyshottimechange { get; set; }
        int Bossshottimechange { get; set; }
        bool Godmode { get; set; }
        bool Sound { get; set; }
        string PlayerName { get; set; }
        int EnemySpawnCount { get; set; }
        int BossHealth { get; set; }
        Difficulty Difficultyness { get; set; }
    }
}
