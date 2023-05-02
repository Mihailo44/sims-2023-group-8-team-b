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
using TouristAgency.Tours;
using TouristAgency.Users;

namespace TouristAgency.TourRequests
{
    /// <summary>
    /// Interaction logic for TourRequestCreation.xaml
    /// </summary>
    public partial class TourRequestCreation : Window
    {
        public TourRequestCreation(Tourist tourist)
        {
            InitializeComponent();
            DataContext = new TourRequestViewModel(tourist);
        }
    }
}
