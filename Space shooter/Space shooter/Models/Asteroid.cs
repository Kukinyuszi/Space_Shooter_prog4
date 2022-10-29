using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Space_shooter.Models
{
    public class Asteroid
    {

        private int speed;
        private Point position;
        public int Speed { get => speed; set => speed = value; }

        public bool IsHit { get; set; }

        public Point Position { get => position; set => position = value; }

        static Random r = new Random();

        public Asteroid(System.Windows.Size area, int speed)
        {
            this.speed = speed;
            Position = new System.Windows.Point(r.Next(25, (int)area.Width - 25), -25);
        }

        public bool Move(System.Windows.Size area)
        {
            System.Windows.Point newposition =
                new System.Windows.Point(position.X, position.Y + speed);
            if (newposition.X >= 0 &&
                newposition.X <= area.Width &&
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
