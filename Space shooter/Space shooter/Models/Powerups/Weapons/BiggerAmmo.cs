﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_shooter.Models.Powerups.Weapons
{
    public class BiggerAmmo : WeaponPowerup
    {
        public override WeaponType TypeofWeapon { get { return WeaponType.Biggerammo; } }

        public BiggerAmmo(int speed, System.Windows.Point position) : base(speed, position)
        {
        }
    }
}
