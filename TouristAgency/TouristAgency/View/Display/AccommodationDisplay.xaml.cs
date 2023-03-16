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
using TouristAgency.Controller;
using TouristAgency.Model;

namespace TouristAgency.View.Display
{
    /// <summary>
    /// Interaction logic for AccommodationDisplay.xaml
    /// </summary>
    public partial class AccommodationDisplay : Window
    {
        private List<Accommodation> _accommodations;
        private AccommodationController _accommodationController;

        public List<Accommodation> Accommodations
        {
            get => _accommodations;
            set => _accommodations = value;
        }
        public AccommodationDisplay(AccommodationController accommodationController)
        {
            _accommodationController = accommodationController;
            Accommodations = new List<Accommodation>();
            InitializeComponent();
            DataContext = this;

            Owner owner1 = new Owner("njutro", "njutro123", "Nikola", "Todic", new DateOnly(1990, 5, 6),
                "njutro123@gmail.com", new Location("Pariz", "Francuska"), "851455");
          //  Accommodation accommodation1 = new Accommodation("PartyHouse", owner1, new Location("Pariz", "Francuska"), TYPE.APARTMENT, 5, 2, 5);
          //  Accommodations.Add(accommodation1);

            //Accommodations = _accommodationController.GetAll();
           
        }

       
    }
}
