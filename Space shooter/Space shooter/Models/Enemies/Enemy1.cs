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
    }
}
