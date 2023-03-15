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
using TouristAgency.View.Creation;

namespace TouristAgency.View.Home
{
    /// <summary>
    /// Interaction logic for OwnerHome.xaml
    /// </summary>
    public partial class OwnerHome : Window
    {
        public OwnerHome()
        {
            InitializeComponent();

            //dodati ovde pozive fcija za loadovanje svih podataka u objekte
        }

        private void MenuNewAccommodation_Click(object sender, RoutedEventArgs e)
        {
            AccommodationCreation x = new AccommodationCreation();
            x.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AccommodationCreation x = new AccommodationCreation();
            x.Show();
        }
    }
}
