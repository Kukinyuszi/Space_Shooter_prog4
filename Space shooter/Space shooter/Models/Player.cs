using Space_shooter.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static Space_shooter.Models.Powerups.WeaponPowerup;

namespace Space_shooter.Models
{
    public class Player : IShip
    {

        Point position;
        public bool left;
        private Rect hitbox;
        public Point Position { get => position; set => position = value; }
        public WeaponType Weapon { get; set; }
        public bool IsDead { get; set; }
        public bool IsMoving { get; set; }
        public Rect Hitbox { get => hitbox; set => hitbox = value; }

        public Player()
        {

        }

        public Player(Point position)
        {
            IsMoving = false;
            this.position = position;
            hitbox = new Rect(position.X - 15, position.Y - 12, 30, 25);
            Weapon = WeaponType.None;
        }
        public void Move(System.Windows.Size area)
        {
            if (left) MoveLeft(area);
            else MoveRight(area);
        }
        public void MoveLeft(System.Windows.Size area)
        {
            Point newposition = new System.Windows.Point(position.X - 10, position.Y);
            if (newposition.X >= 0)
            {
                Position = newposition;
                hitbox.X = hitbox.X - 10;
            }
        }
        public void MoveRight(System.Windows.Size area)
        {
            Point newposition = new System.Windows.Point(position.X + 10, position.Y);
            if (newposition.X <= area.Width)
            {
                Position = newposition;
                hitbox.X = hitbox.X + 10;
            }
        }
    }
}
