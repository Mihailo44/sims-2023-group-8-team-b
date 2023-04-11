using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using TouristAgency.ViewModel;
using TouristAgency.Model;
using TouristAgency.View.Home;

namespace TouristAgency.View.Display
{
    /// <summary>
    /// Interaction logic for AccommodationDisplay.xaml
    /// </summary>
    public partial class AccommodationDisplay : Window
    {
        private Guest _loggedInGuest;

        public AccommodationDisplay(Guest guest)
        {
            InitializeComponent();
            DataContext = new AccommodationDisplayViewModel(guest, this);
            _loggedInGuest = guest;
        }
    }
}
