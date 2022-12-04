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
    /// Interaction logic for HowToPlayWindow.xaml
    /// </summary>
    public partial class HowToPlayWindow : Window
    {
        private MainMenuWindow _mainmenu;
        public HowToPlayWindow(MainMenuWindow _mainmenu)
        {
            this._mainmenu = _mainmenu;
            InitializeComponent();

        }

        private void Next_Button_Click(object sender, RoutedEventArgs e)
        {
            HowToPlayPage2Window howToPlay = new HowToPlayPage2Window();
            howToPlay.Template = this.Template;
            this.Visibility = Visibility.Hidden;
            if (howToPlay.ShowDialog() == true) this.Visibility = Visibility.Visible;
            else
            {
                _mainmenu.Visibility = Visibility.Visible;
                this.Close();
            }
        }
        private void Close_Button_Click(object sender, RoutedEventArgs e)
        {
            _mainmenu.Visibility = Visibility.Visible;
            this.Close();
        }
    }
}
