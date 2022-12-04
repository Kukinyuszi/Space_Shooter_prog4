using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Space_shooter.Models.Interfaces
{
    public interface IShip
    {
        public Point Position { get; set; }
        public bool IsDead { get; set; }
        public bool IsMoving { set; get; }
        public void MoveSideWays(System.Windows.Size area);
    }
}
