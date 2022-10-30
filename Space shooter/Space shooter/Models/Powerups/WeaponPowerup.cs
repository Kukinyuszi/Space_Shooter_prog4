using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_shooter.Models.Powerups
{
    public class WeaponPowerup : Powerup
    {
        public override Type PowerupType { get { return Type.Weapon; } }
        public virtual WeaponType TypeofWeapon { get { return WeaponType.None; } }

        public WeaponPowerup(int speed, System.Windows.Point position) : base(speed, position)
        {
        }
        public enum WeaponType
        {
            Doubleshooter, Tripplehooter, Biggerammo, None
        }

    }
}
