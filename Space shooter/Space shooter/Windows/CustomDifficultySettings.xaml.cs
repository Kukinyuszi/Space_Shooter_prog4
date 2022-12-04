using Space_shooter.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static Space_shooter.Logic.Interfaces.ISettings;

namespace Space_shooter.Windows
{
    /// <summary>
    /// Interaction logic for CustomDifficultySettings.xaml
    /// </summary>
    public partial class CustomDifficultySettings : Window
    {
        bool music;
        public Difficulty difficulty;
        public CustomDifficultySettings(ISettings settings)
        {
            InitializeComponent();
            this.DataContext = settings;
        }

        public bool Music { get => music; set => music = value; }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in MyStackpanel.Children)
            {
                if (item is StackPanel)
                {
                    foreach (var item2 in (item as StackPanel).Children)
                    {

                        if (item2 is TextBox t) t.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                        else if(item2 is Slider s) s.GetBindingExpression(Slider.ValueProperty).UpdateSource();
                        else if (item2 is CheckBox c) c.GetBindingExpression(CheckBox.IsCheckedProperty).UpdateSource();
                    }
                }

            }
            this.DialogResult = true;
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
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
