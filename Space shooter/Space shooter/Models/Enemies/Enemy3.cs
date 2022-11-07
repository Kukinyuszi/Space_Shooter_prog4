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
    }
}
