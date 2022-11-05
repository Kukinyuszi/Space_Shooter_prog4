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
        public SettingsMenuWindow(IDisplaySettings _displaysettings)
        {
            this.displaysettings = _displaysettings;
            InitializeComponent();
            this.DataContext = displaysettings;
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

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
