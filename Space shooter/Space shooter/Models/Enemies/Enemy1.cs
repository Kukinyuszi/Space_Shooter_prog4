using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Space_shooter.Models
{
    public class Enemy1 : EnemyShip
    {
        public Enemy1(Size area) : base(area)
        {
            Name = EnemyEnum.one;
        }

        public override List<Laser> Shoot(Size area , List<Laser> Lasers, Point playerPosition)
        {
            Point enemyshippositiontemp = new System.Windows.Point(Position.X, Position.Y + 23);

            Lasers.Add(new Laser(enemyshippositiontemp, new Vector(0, 5), false, false));

            return Lasers;
        }
    }
}
