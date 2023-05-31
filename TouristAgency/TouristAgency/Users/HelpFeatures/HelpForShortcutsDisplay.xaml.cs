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

namespace TouristAgency.Users.HelpFeatures
{
    /// <summary>
    /// Interaction logic for HelpForShortcutsDisplay.xaml
    /// </summary>
    public partial class HelpForShortcutsDisplay : Window
    {
        public HelpForShortcutsDisplay(Tourist tourist)
        {
            InitializeComponent();
            DataContext = new HelpForShortcutsDisplayViewModel(tourist, this);
        }
    }
}
