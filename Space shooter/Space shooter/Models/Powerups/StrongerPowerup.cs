using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_shooter.Models.Powerups
{
    public class StrongerPowerup : Powerup
    {
        public override Type PowerupType { get { return Type.Stronger; } }

        public StrongerPowerup(int speed, System.Windows.Point position) : base(speed, position)
        {
        }
    }
}
