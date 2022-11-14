using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Space_shooter.Models
{
    public class Laser
    {
        private Point position;
        private int counter;
        private Rect hitbox;
        private bool fromBoss;

        public Vector Laservector { get; set; }
        public double Angle
        {
            get
            {
                double temp = Vector.AngleBetween(Laservector, new Vector(0, 1));
                if (Laservector.X < 0) return Math.Abs(temp) + 90;
                else return -temp + 90;
            }
        }

        public int Speed { get; set; }
        public bool Fromplayer { get; set; }
        public bool Big { get; set; }
        public bool IsHit { get; set; }
        public int Ammosize
        {
            get
            {
                if (Big) return 30;
                else return 10;
            }
        }

        public int Counter { get => counter; set => counter = value; }
        public Rect Hitbox { get => hitbox; set => hitbox = value; }
        public Point Position { get => position; set => position = value; }
        public bool FromBoss { get => fromBoss; set => fromBoss = value; }

        public Laser()
        {

        }
        public Laser(System.Windows.Point position, Vector laservector, bool fromplayer = false, bool big = false, bool fromBoss = false)
        {
            Position = position;
            Laservector = laservector;
            Fromplayer = fromplayer;
            this.fromBoss = fromBoss;
            Big = big;
            hitbox = new Rect(position.X - (Ammosize / 2), position.Y - (Ammosize / 2), Ammosize, Ammosize);
        }


        public bool Move(System.Windows.Size area)
        {
            System.Windows.Point newposition =
                new System.Windows.Point(Position.X + (int)Laservector.X,
                Position.Y + (int)Laservector.Y);
            if (newposition.X >= 0 &&
                newposition.X <= area.Width &&
                newposition.Y >= 0 &&
                newposition.Y <= area.Height
                )
            {
                Position = newposition;
                hitbox.X = hitbox.X + (int)Laservector.X;
                hitbox.Y = hitbox.Y + (int)Laservector.Y;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
