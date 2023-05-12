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
using TouristAgency.View.Home;
using TouristAgency.Users;
using TouristAgency.Accommodations.ReservationFeatures.CreationFeature;

namespace TouristAgency.Accommodations.ReservationFeatures.CreationFeature
{
    /// <summary>
    /// Interaction logic for AccommodationDisplay.xaml
    /// </summary>
    public partial class ReservationCreation : UserControl
    {
        private Guest _loggedInGuest;

        public ReservationCreation()
        {
            InitializeComponent();
            //DataContext = new AccommodationDisplayViewModel(guest, this);
            //_loggedInGuest = guest;
        }
    }
}
