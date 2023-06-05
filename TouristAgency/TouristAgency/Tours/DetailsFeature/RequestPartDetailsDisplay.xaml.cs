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
using TouristAgency.TourRequests;

namespace TouristAgency.Tours.DetailsFeature
{
    /// <summary>
    /// Interaction logic for RequestPartDetailsDisplay.xaml
    /// </summary>
    public partial class RequestPartDetailsDisplay : Window
    {
        public RequestPartDetailsDisplay(string name, TourRequest tourRequest)
        {
            InitializeComponent();
            DataContext = new RequestPartDetailsDisplayViewModel(name, tourRequest, this);
        }
    }
}
