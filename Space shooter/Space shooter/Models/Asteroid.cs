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
        private Rect hitbox;
        public int Speed { get => speed; set => speed = value; }

        public bool IsHit { get; set; }

        public Point Position { get => position; set => position = value; }
        public Rect Hitbox { get => hitbox; set => hitbox = value; }

        static Random r = new Random();
        public Asteroid()
        {

        }

        public Asteroid(System.Windows.Size area, int speed)
        {
            this.speed = speed;
            position = new System.Windows.Point(r.Next(25, (int)area.Width - 25), -25);
            hitbox = new Rect(position.X - 25, position.Y - 20, 50, 40);
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
                hitbox.Y = hitbox.Y + speed;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
