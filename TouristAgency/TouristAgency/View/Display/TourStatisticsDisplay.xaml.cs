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
using TouristAgency.ViewModel;

namespace TouristAgency.View.Display
{
    /// <summary>
    /// Interaction logic for TourStatisticsDisplay.xaml
    /// </summary>
    public partial class TourStatisticsDisplay : Window
    {
        public TourStatisticsDisplay(Guide guide)
        {
            InitializeComponent();
            DataContext = new TourStatisticsDisplayViewModel(guide, this);
        }
    }
}
