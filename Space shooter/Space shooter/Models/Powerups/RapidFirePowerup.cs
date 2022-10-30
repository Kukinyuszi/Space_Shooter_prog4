using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_shooter.Models.Powerups
{
    public class RapidFirePowerup : Powerup
    {

        public override Type PowerupType { get { return Type.RapidFire; } }


        public RapidFirePowerup(int speed, System.Windows.Point position) : base(speed, position)
        {
        }
    }
}
