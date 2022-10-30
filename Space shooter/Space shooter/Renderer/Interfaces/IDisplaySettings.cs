using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_shooter.Renderer.Interfaces
{
    public interface IDisplaySettings
    {
        public enum Resolution
        {
            High, Medium, Low
        }
        Resolution WindowResolution { get; set; }
        bool Animation { get; set; }
        bool Hitboxes { get; set; }
    }
}
