﻿using Space_shooter.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Space_shooter.Windows
{
    /// <summary>
    /// Interaction logic for HighScoresWindow.xaml
    /// </summary>
    public partial class HighScoresWindow : Window
    {
        public HighScoresWindow()
        {
            InitializeComponent();
            SetupScoreBoard();
        }
        private void SetupScoreBoard()
        {
            ScoreBoardService scoreBoardService = new ScoreBoardService();
            List<string> scores = scoreBoardService.GetScoresList();
            if (scores.Count > 0)
            {
                foreach (string score in scores)
                {
                    Label l = new Label()
                    {
                        Content = score,
                        Margin = new Thickness(10, 1, 10, 1)
                    };
                    sp_scores.Children.Add(l);
                }
            }

        }
    }
}
