using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Space_shooter.Models
{
    public class Enemy3 : EnemyShip
    {
        public Enemy3(Size area) : base(area)
        {
            Name = EnemyEnum.three;
        }
        public override List<Laser> Shoot(Size area, List<Laser> Lasers, Point playerPosition)
        {
            Point enemyshippositiontemp = new System.Windows.Point(Position.X, Position.Y + 23);
            double x = ((playerPosition.X) - Position.X) / 40;
            double y = ((playerPosition.Y - 40) - Position.Y + 23) / 40;

            Lasers.Add(new Laser(enemyshippositiontemp, new Vector(x, y), false, false));

            return Lasers;
        }
    }
}
