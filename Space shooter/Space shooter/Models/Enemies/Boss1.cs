using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Space_shooter.Models.Enemies
{
    public class Boss1 : Boss
    {
        public Boss1(Size area, int health) : base(area, health)
        {
            BossType = BossName.Claec;
        }

        public override void Move(Size area)
        {
            Point newposition;
            if (Position.Y <= area.Height / 5)
            {
                newposition = new Point(Position.X, Position.Y + 5);
                Position = newposition;
                Hitbox = new Rect(Position.X - 50, Position.Y - 80, 100, 160);
            }
            else IsMoving = false;
        }
    }
}
