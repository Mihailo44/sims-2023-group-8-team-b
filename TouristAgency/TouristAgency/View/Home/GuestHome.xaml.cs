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
using TouristAgency.Model;
using TouristAgency.View.Creation;
using TouristAgency.View.Display;

namespace TouristAgency.View.Home
{
    /// <summary>
    /// Interaction logic for GuestHome.xaml
    /// </summary>
    public partial class GuestHome : Window
    {
        private Guest _loggedInGuest;
        public GuestHome(Guest guest)
        {
            InitializeComponent();
            DataContext = this;
            _loggedInGuest = guest;
        }

        private void AccommodationDisplay_Click(object sender, RoutedEventArgs e)
        {
            AccommodationDisplay display = new AccommodationDisplay(_loggedInGuest);
            display.Show();
        }

        private void PostponementRequestDisplay_Click(object sender, RoutedEventArgs e)
        {
            PostponementRequestDisplay display = new PostponementRequestDisplay(_loggedInGuest);
            display.Show();
        }

        private void OwnerReviewCreation_Click(object sender, RoutedEventArgs e)
        {
            OwnerReviewCreation display = new OwnerReviewCreation(_loggedInGuest);
            display.Show();
        }
    }
}
