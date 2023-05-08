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

namespace TouristAgency.Users.SuperGuestFeature
{
    /// <summary>
    /// Interaction logic for SuperGuestDisplay.xaml
    /// </summary>
    public partial class SuperGuestDisplay : Window
    {
        private Guest _loggedInGuest;
        public SuperGuestDisplay(Guest guest)
        {
            InitializeComponent();
        }
    }
}
