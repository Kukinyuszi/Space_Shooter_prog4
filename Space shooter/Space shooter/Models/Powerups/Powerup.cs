using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Space_shooter.Models.Powerups
{
    public class Powerup
    {
        private int speed;
        private Point position;
        private int counter;

        public int Counter { get => counter; set => counter = value; }
        public int Speed { get => speed; set => speed = value; }
        public Point Position { get => position; set => position = value; }
        public virtual Type PowerupType { get { return Type.None; } }

        public Powerup(System.Windows.Size area, int speed, Point position)
        {
            this.Speed = speed;
            Position = position;
            counter = 600;
        }

        public enum Type
        {
            ExtraScore, MoreHealth, RapidFire, Shield, Stronger, Weapon, None
        }

        public bool Move(System.Windows.Size area)
        {
            System.Windows.Point newposition =
                new System.Windows.Point(position.X, position.Y + speed);
            if (newposition.X >= 0 &&
                newposition.X <= area.Width &&
                newposition.Y >= 0 &&
                newposition.Y <= area.Height
                )
            {
                Position = newposition;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
