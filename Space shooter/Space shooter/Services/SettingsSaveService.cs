using Newtonsoft.Json;
using Space_shooter.Renderer;
using Space_shooter.Renderer.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_shooter.Services
{
    public class SettingsSaveService
    {
        public class Settings_Convert
        {
            public IDisplaySettings displaySettings = new Display();
            public SoundPlayerService soundPlayerService;
        }
        public class DisplaySettings : IDisplaySettings
        {
            private bool animation, hitboxes, fullScreen;

            public bool Animation { get => animation; set => animation = value; }
            public bool Hitboxes { get => hitboxes; set => hitboxes = value; }
            public bool FullScreen { get => fullScreen; set => fullScreen = value; }
        }

        public void SaveSettings(IDisplaySettings _displaysettings, SoundPlayerService sps)
        {
            DisplaySettings settings_display = new DisplaySettings()
            {
                Animation = _displaysettings.Animation,
                Hitboxes = _displaysettings.Hitboxes,
                FullScreen = _displaysettings.FullScreen
            };
            Settings_Convert settings = new Settings_Convert()
            {
                displaySettings = settings_display,
                soundPlayerService = sps
            };
            string json = JsonConvert.SerializeObject(settings, Formatting.Indented);
            File.WriteAllText("Saves/SettingsSave.json", json);
        }

        public Settings_Convert LoadSettings()
        {
            if (File.Exists("Saves/SettingsSave.json"))
            {

                string jsonscores = File.ReadAllText("Saves/SettingsSave.json");
                return JsonConvert.DeserializeObject<Settings_Convert>(jsonscores);

            }
            return null;
        }
    }
}

