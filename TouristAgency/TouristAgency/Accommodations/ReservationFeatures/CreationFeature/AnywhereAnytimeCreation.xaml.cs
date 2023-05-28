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
using TouristAgency.Users;

namespace TouristAgency.Accommodations.ReservationFeatures.CreationFeature
{
    /// <summary>
    /// Interaction logic for AnywhereAnytimeCreation.xaml
    /// </summary>
    public partial class AnywhereAnytimeCreation : UserControl
    {
        private Guest _loggedInGuest;
        public AnywhereAnytimeCreation()
        {
            InitializeComponent();
        }

    }
}
