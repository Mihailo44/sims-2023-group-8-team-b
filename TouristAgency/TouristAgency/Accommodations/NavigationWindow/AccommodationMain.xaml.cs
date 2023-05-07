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
using TouristAgency.Accommodations.Domain;

namespace TouristAgency.Accommodations.NavigationWindow
{
    /// <summary>
    /// Interaction logic for AccommodationMain.xaml
    /// </summary>
    public partial class AccommodationMain : Window
    {
        public AccommodationMain(Accommodation accommodation)
        {
            InitializeComponent();
            DataContext = new AccommodationMainViewModel(accommodation);
        }
    }
}
