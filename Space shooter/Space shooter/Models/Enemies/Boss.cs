using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Space_shooter.Models
{
    public class Boss : EnemyShip
    {
        public enum BossName
        {
            Claec, Kasdeya, None
        }

        private Point position;
        private int health;
        private Rect hitbox;
        private BossName bossName;
        public override EnemyEnum Name { get; set; }
        public override Point Position { get => position; set => position = value; }
        public int Health { get => health; set => health = value; }
        public override Rect Hitbox { get => hitbox; set => hitbox = value; }
        public virtual BossName BossType { get => bossName; set => bossName = value; }

        public Boss()
        {
        }

        public Boss(System.Windows.Size area, int health) : base(area)
        {
            IsMoving = true;
            position = new System.Windows.Point((int)area.Width / 2, -40);
            hitbox = new Rect(position.X - 50, position.Y - 80, 100, 160);
            Name = EnemyEnum.boss;
            BossType = BossName.None;
            this.health = health;
        }
    }
}
