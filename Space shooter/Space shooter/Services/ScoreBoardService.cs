using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Space_shooter.Logic.Interfaces.ISettings;

namespace Space_shooter.Services
{
    public class ScoreBoardService
    {
        internal class ScoreList
        {
            public List<Score> Scores { get; set; }

        }
        internal class Score : IComparable
        {
            public string PlayerName { get; set; }
            public int Scoreamount { get; set; }
            public string Time { get; set; }
            public Difficulty Difficulty { get; set; }

            public Score(string playerName, int scoreamount, string time, Difficulty difficulty)
            {
                PlayerName = playerName;
                Scoreamount = scoreamount;
                Time = time;
                Difficulty = difficulty;
            }

            public int CompareTo(object? obj)
            {
                if (this.Scoreamount > (obj as Score).Scoreamount) return -1;
                if (this.Scoreamount < (obj as Score).Scoreamount) return 1;
                else return 0;
            }
            public override string ToString()
            {
                return $"{Time}\t{Difficulty}\t\t{PlayerName}\t{Scoreamount}";
            }
        }


        public ScoreBoardService()
        {

        }
        public void SaveNewScore(int score, string playername, Difficulty difficulty)
        {
            List<Score> scorelist = new List<Score>();
            ScoreList sl;
            if (File.Exists("saves.json"))
            {
                string jsonscores = File.ReadAllText("saves.json");
                sl = JsonConvert.DeserializeObject<ScoreList>(jsonscores);
                sl.Scores.Add(new Score(playername, score, DateTime.Today.ToShortDateString(), difficulty));
            }
            else
            {
                sl = new ScoreList() { Scores = new List<Score>() };
                sl.Scores.Add(new Score(playername, score, DateTime.Today.ToShortDateString(), difficulty));
            }
            string json = JsonConvert.SerializeObject(sl);
            File.WriteAllText("saves.json", json);

        }
        public int GetHighScore()
        {
            if (File.Exists("saves.json"))
            {
                string jsonscores = File.ReadAllText("saves.json");
                ScoreList sl = JsonConvert.DeserializeObject<ScoreList>(jsonscores);
                sl.Scores.Sort();
                return sl.Scores[0].Scoreamount;
            }
            else return 0;
        }
        public List<string> GetScoresList()
        {
            List<string> scores = new List<string>();
            if (File.Exists("saves.json"))
            {
                string jsonscores = File.ReadAllText("saves.json");
                ScoreList sl = JsonConvert.DeserializeObject<ScoreList>(jsonscores);
                sl.Scores.Sort();
                foreach (Score score in sl.Scores) scores.Add(score.ToString());
            }
            return scores;
        }
    }
}
