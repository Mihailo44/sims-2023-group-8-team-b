using System.Windows;
using TouristAgency.Users;

namespace TouristAgency.Tours.TourRequestFeatures.CreationFeature
{
    /// <summary>
    /// Interaction logic for NewPartOfComplexTourCreation.xaml
    /// </summary>
    public partial class NewPartOfComplexTourCreation : Window
    {
        public NewPartOfComplexTourCreation(Tourist tourist)
        {
            InitializeComponent();
            DataContext = new NewPartOfComplexTourCreationViewModel(tourist);
        }
    }
}
