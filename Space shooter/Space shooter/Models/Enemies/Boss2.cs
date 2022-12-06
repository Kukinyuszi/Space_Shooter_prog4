using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Space_shooter.Models.Enemies
{
    public class Boss2 : Boss
    {
        private bool left;

        public Boss2(Size area, int health) : base(area, health)
        {
            BossType = BossName.Kasdeya;
            left = true;
        }

        public override void MoveSideWays(Size area)
        {
            Point newposition;
            if (Position.Y <= area.Height / 5)
            {
                newposition = new Point(Position.X, Position.Y + 5);
                Position = newposition;
                Hitbox = new Rect(Position.X - 50, Position.Y - 80, 100, 160);
            }
            else
            {

                if (left)
                {
                    newposition = new Point(Position.X - 1, Position.Y);
                    if (newposition.X <= (area.Width / 3))
                    {
                        left = false;
                    }
                    if (newposition.X >= 0 && newposition.X <= area.Width && newposition.Y >= area.Height / 5 && newposition.Y <= area.Height / 3)
                    {
                        Position = newposition;
                        Hitbox = new Rect(Position.X - 50, Position.Y - 80, 100, 160);
                    }
                }
                else
                {
                    newposition = new Point(Position.X + 1, Position.Y);
                    if (newposition.X >= area.Width / 3 * 2)
                    {
                        left = true;
                    }
                    if (newposition.X >= 0 && newposition.X <= area.Width && newposition.Y >= area.Height / 5 && newposition.Y <= area.Height / 3)
                    {
                        Position = newposition;
                        Hitbox = new Rect(Position.X - 50, Position.Y - 80, 100, 160);
                    }

                }
            }
        }
    }
}
