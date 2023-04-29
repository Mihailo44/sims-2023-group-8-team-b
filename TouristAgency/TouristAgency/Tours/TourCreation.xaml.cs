using System.Windows;
using TouristAgency.Users;
using TouristAgency.Tours;

namespace TouristAgency.View.Creation
{
    /// <summary>
    /// Interaction logic for TourCreation.xaml
    /// </summary>
    public partial class TourCreation : Window
    {
        public TourCreation(Guide guide)
        {
            InitializeComponent();
            DataContext = new TourCreationViewModel(guide, this);
        }
    }
}