using Newtonsoft.Json;
using Space_shooter.Logic;
using Space_shooter.Logic.Interfaces;
using Space_shooter.Models;
using Space_shooter.Models.Powerups;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Space_shooter.Logic.Interfaces.ISettings;
using System.Windows;


namespace Space_shooter.Services
{
    public class Save_LoadGameService
    {
        public void SaveGame(SpaceShooterLogic model)          
        {                 
            string json = JsonConvert.SerializeObject(model,Formatting.Indented);
            File.WriteAllText("Saves/Gamesave.json", json);
        }

        public SpaceShooterLogic LoadGame()
        {
            if (File.Exists("Saves/Gamesave.json"))
            {

                string jsonscores = File.ReadAllText("Saves/Gamesave.json");
                return JsonConvert.DeserializeObject<SpaceShooterLogic>(jsonscores);
                
            }
            return null;
        }
    }
}
