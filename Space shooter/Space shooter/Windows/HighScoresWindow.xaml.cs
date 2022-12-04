using Space_shooter.Services;
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
            Label label = new Label()
            {
                Content = "   Date  \t        Difficulty\t   Player\t    Score",
                FontSize = 15,
                Foreground = Brushes.White,
                Margin = new Thickness(10, 10, 10, 10),
                FontWeight = FontWeights.Bold
            };
            sp_scores.Children.Add(label);
            if (scores.Count > 0)
            {
                for (int i = 0; i < 15; i++)
                {
                    Label l = new Label()
                    {
                        Content = scores[i],
                        FontSize = 13,
                        Margin = new Thickness(10, 10, 10, 10),
                        Foreground = Brushes.White,
                        FontWeight = FontWeights.Bold

                    };
                    sp_scores.Children.Add(l);
                }
            }

        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Close();
            }
        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Owner.Visibility = Visibility.Hidden;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this.Owner.Visibility = Visibility.Visible;
        }
    }
}
