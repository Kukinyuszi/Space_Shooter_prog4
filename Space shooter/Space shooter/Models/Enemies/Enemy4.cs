using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Space_shooter.Models
{
    public class Enemy4 : EnemyShip
    {
        public Enemy4(Size area) : base(area)
        {
            Name = EnemyEnum.four;
        }
    }
}
