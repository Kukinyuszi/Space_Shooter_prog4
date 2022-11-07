using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Space_shooter.Models
{
    public class Boss : EnemyShip
    {
        private Point position;
        private int health;
        private Rect hitbox;
        public override EnemyEnum Name { get; set; }
        public override Point Position { get => position; set => position = value; }
        public int Health { get => health; set => health = value; }
        public override Rect Hitbox { get => hitbox; set => hitbox = value; }

        public Boss()
        {

        }

        public Boss(System.Windows.Size area, int health) : base(area)
        {
            IsMoving = true;
            position = new System.Windows.Point((int)area.Width / 2, -40);
            hitbox = new Rect(position.X - 50, position.Y - 80, 100, 160);
            Name = EnemyEnum.boss;
            this.health = health;
        }

        public override void Move(System.Windows.Size area)
        {
            Point newposition;
            if (position.Y <= area.Height / 5)
            {
                newposition = new Point(position.X, position.Y + 5);
                position = newposition;
                hitbox.Y = hitbox.Y + 5;
            }
            else IsMoving = false;
        }
    }
}
