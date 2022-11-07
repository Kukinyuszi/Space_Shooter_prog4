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
        private bool shootType;
        public Boss1(Size area, int health) : base(area, health)
        {
            BossType = BossName.Claec;
        }

        private Random random = new Random();

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
        public override List<Laser> Shoot(Size area, List<Laser> Lasers, Point playerPosition)
        {
            Point bosspositiontemp = new System.Windows.Point(Position.X, Position.Y + 60);
            double x = ((playerPosition.X) - Position.X) / 40;
            double y = ((playerPosition.Y - 40) - Position.Y + 23) / 40;

            if (shootType) Lasers.Add(new Laser(bosspositiontemp, new Vector(Math.Round(x) * 1.5, Math.Round(y) * 1.5), false, false));
            else
            {
                Lasers.Add(new Laser(bosspositiontemp, new Vector((x * 2) - 2, y * 1.5), false, false));
                Lasers.Add(new Laser(bosspositiontemp, new Vector(x * 2 + random.Next(-1, 2), y * 1.5), false, false));
                Lasers.Add(new Laser(bosspositiontemp, new Vector((x * 2) + 2, y * 1.5), false, false));

            }
            return Lasers;
        }
    }
}
