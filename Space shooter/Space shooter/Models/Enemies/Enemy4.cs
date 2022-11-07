﻿using System;
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
        private Random random = new Random();
        public override List<Laser> Shoot(Size area, List<Laser> Lasers, Point playerPosition)
        {
            Point enemyshippositiontemp = new System.Windows.Point(Position.X, Position.Y + 23);
            int x1;
            if (Position.X < area.Width / 2) x1 = random.Next(4);
            else x1 = random.Next(-4, 0);

            Lasers.Add(new Laser(enemyshippositiontemp, new Vector(x1, 5), false, false));

            return Lasers;
        }
    }
}
