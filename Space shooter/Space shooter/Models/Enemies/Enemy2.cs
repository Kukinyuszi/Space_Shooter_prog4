using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Space_shooter.Models
{
    public class Enemy2 : EnemyShip
    {
        public Enemy2(Size area) : base(area)
        {
            Name = EnemyEnum.two;
        }
    }
}
