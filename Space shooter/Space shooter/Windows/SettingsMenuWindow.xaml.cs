using Space_shooter.Renderer;
using Space_shooter.Renderer.Interfaces;
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
        public SettingsMenuWindow()
        {
            displaysettings = new Display();
            InitializeComponent();
        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in MyGrid.Children)
            {
                if (item is WrapPanel)
                {
                    foreach (var item2 in (item as WrapPanel).Children)
                    {

                        if (item2 is ComboBox t)
                        {
                            displaysettings.WindowResolution = (IDisplaySettings.Resolution)t.SelectedIndex;
                        }
                    }
                }
            }
            this.Close();
        }
    }
}
