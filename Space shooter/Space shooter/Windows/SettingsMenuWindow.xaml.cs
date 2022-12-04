using Space_shooter.Renderer.Interfaces;
using Space_shooter.Services;
using System;
using System.Collections.Generic;
using System.IO;
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
        SoundPlayerService sps;
        public SettingsMenuWindow(IDisplaySettings _displaysettings, SoundPlayerService sps)
        {
            this.displaysettings = _displaysettings;
            this.sps = sps;
            InitializeComponent();
            SetupSounds();
            this.DataContext = displaysettings;


        }
        private void SetupSounds()
        {
            if (sps.MusicVolume == 0) img_music.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "\\Images\\Music_BTN_up.png", UriKind.RelativeOrAbsolute));
            else img_music.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "\\Images\\Music_BTN.png", UriKind.RelativeOrAbsolute));
            if (sps.SoundVolume == 0) img_sound.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "\\Images\\Sound_BTN_up.png", UriKind.RelativeOrAbsolute));
            else img_sound.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "\\Images\\Sound_BTN.png", UriKind.RelativeOrAbsolute));
            sd_music.Value = sps.MusicVolume;
            sd_sound.Value = sps.SoundVolume;
        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in MyGrid.Children)
            {
                 if(item is CheckBox c) c.GetBindingExpression(CheckBox.IsCheckedProperty).UpdateSource();
            }
            this.DialogResult = true;
            this.Close();
        }

        private void sd_music_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            sps.MusicVolumeChange(sd_music.Value);
            SetupSounds();

        }

        private void sd_sound_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            sps.SoundVolume = sd_sound.Value;
            SetupSounds();
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
