using Space_shooter.Renderer.Interfaces;
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
    /// Interaction logic for SettingsMenuWindow.xaml
    /// </summary>
    public partial class SettingsMenuWindow : Window
    {
        IDisplaySettings displaysettings;
        SoundPlayerService sounds;
        public SettingsMenuWindow(IDisplaySettings _displaysettings, SoundPlayerService sounds)
        {
            this.displaysettings = _displaysettings;
            this.sounds = sounds;
            InitializeComponent();
            SetupSounds();
            this.DataContext = displaysettings;


        }
        private void SetupSounds()
        {
            if (sounds.MusicVolume == 0) lb_music.Background = Brushes.Red;
            else lb_music.Background = Brushes.Green;
            if (sounds.SoundVolume == 0) lb_sound.Background = Brushes.Red;
            else lb_sound.Background = Brushes.Green;
            sd_music.Value = sounds.MusicVolume;
            sd_sound.Value = sounds.SoundVolume;
        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in MyGrid.Children)
            {

                if (item is ComboBox t)
                {
                    displaysettings.WindowResolution = (IDisplaySettings.Resolution)t.SelectedIndex;
                }
                else if(item is CheckBox c) c.GetBindingExpression(CheckBox.IsCheckedProperty).UpdateSource();
            }
            this.DialogResult = true;
            this.Close();
        }

        private void sd_music_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            sounds.MusicVolumeChange(sd_music.Value);
            if (sd_music.Value != 0) lb_music.Background = Brushes.Green;
            else lb_music.Background = Brushes.Red;
        }

        private void sd_sound_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            sounds.SoundVolume = sd_sound.Value;
            if(sd_sound.Value != 0) lb_sound.Background = Brushes.Green;
            else lb_sound.Background = Brushes.Red;
        }
    }
}
