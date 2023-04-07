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
    /// Interaction logic for GuideHome.xaml
    /// </summary>
    public partial class GuideHome : Window
    {
        private Guide _loggedInGuide;
        public GuideHome(Guide guide)
        {
            InitializeComponent();
            DataContext = this;
            _loggedInGuide = guide;
        }

        private void TourButton_Click(object sender, RoutedEventArgs e)
        {
            TourCreation creation = new TourCreation(_loggedInGuide);
            creation.Show();
        }


        private void ActiveTourDisplayButton_Click(object sender, RoutedEventArgs e)
        {
            ActiveTourDisplay activeTour = new ActiveTourDisplay(_loggedInGuide);
            activeTour.Show();
        }

        private void CancelTourDisplayButton_Click(object sender, RoutedEventArgs e)
        {
            CancelTourDisplay cancelTour = new CancelTourDisplay(_loggedInGuide);
            cancelTour.Show();
        }

        private void TourStatisticsDisplayButton_Click(object sender, RoutedEventArgs e)
        {
            TourStatisticsDisplay tourStatistics = new TourStatisticsDisplay(_loggedInGuide);
            tourStatistics.Show();
        }

        private void GuideProfileDisplayButton_Click(object sender, RoutedEventArgs e)
        {
            GuideProfileDisplay profileDisplay = new GuideProfileDisplay(_loggedInGuide);
            profileDisplay.Show();
        }
    }
}
