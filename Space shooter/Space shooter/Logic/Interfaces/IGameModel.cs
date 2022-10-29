using Space_shooter.Models;
using Space_shooter.Models.Powerups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_shooter.Logic.Interfaces
{
    public interface IGameModel
    {

        event EventHandler Changed;

        Player Player { get; set; }
        Boss Boss { get; set; }
        List<Laser> Lasers { get; set; }
        List<Asteroid> Asteroids { get; set; }
        List<EnemyShip> EnemyShips { get; set; }
        List<Powerup> Powerups { get; set; }

        int Health { get; }
        int Score { get; }
        int HighScore { get; }

    }
}
