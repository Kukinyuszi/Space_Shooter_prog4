using Space_shooter.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Space_shooter.Models
{
    public class EnemyShip : IShip
    {

        public enum EnemyEnum
        {
            one, two, three, four, boss
        }

        private Point position;
        private bool left;
        private EnemyEnum name;
        private Rect hitbox;

        public virtual Point Position { get => position; set => position = value; }
        public virtual EnemyEnum Name { get => name; set => name = value; }
        public virtual bool IsHit { get; set; }
        public bool IsDead { get; set; }
        public virtual bool IsMoving { get; set; }
        public virtual Rect Hitbox { get => hitbox; set => hitbox = value; }

        static Random random = new Random();
        public EnemyShip()
        {

        }

        public EnemyShip(System.Windows.Size area)
        {
            position = new System.Windows.Point(random.Next(25, (int)area.Width - 25), -40);
            hitbox = new Rect(position.X - 25, position.Y - 20, 50, 40);
            IsMoving = true;
        }

        public virtual void Move(System.Windows.Size area)
        {
            Point newposition;
            if (position.Y <= area.Height / 5)
            {
                newposition = new Point(position.X, position.Y + 5);
                position = newposition;
                hitbox.Y = hitbox.Y + 5;
            }
            else
            {
                IsMoving = false;

                if (left)
                {
                    newposition = new Point(position.X - 1, position.Y);
                    if (newposition.X <= 25)
                    {
                        left = false;
                    }
                    if (newposition.X >= 0 && newposition.X <= area.Width && newposition.Y >= area.Height / 5 && newposition.Y <= area.Height / 3)
                    {
                        position = newposition;
                        hitbox.X = hitbox.X - 1;
                    }
                }
                else
                {
                    newposition = new Point(position.X + 1, position.Y);
                    if (newposition.X >= (int)area.Width - 25)
                    {
                        left = true;
                    }
                    if (newposition.X >= 0 && newposition.X <= area.Width && newposition.Y >= area.Height / 5 && newposition.Y <= area.Height / 3)
                    {
                        position = newposition;
                        hitbox.X = hitbox.X + 1;
                    }

                }
            }
        }
    }
}
