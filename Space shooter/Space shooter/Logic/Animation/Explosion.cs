using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Space_shooter.Logic.Animation
{
    public class Explosion
    {
        public Point Position { get; set; }
        public int Counter { get; set; }
        public bool FromPlayer { get; set; }
        public bool FromBoss { get; set; }

        public bool IsLaser { get; set; }

        public Explosion(Point position, int counter, bool fromplayer, bool islaser, bool fromBoss = false)
        {
            Position = position;
            Counter = counter;
            FromPlayer = fromplayer;
            FromBoss = fromBoss;
            IsLaser = islaser;
        }
    }
}
