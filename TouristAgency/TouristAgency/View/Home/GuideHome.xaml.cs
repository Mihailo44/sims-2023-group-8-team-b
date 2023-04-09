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
using TouristAgency.ViewModel;

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
            DataContext = new GuideHomeViewModel(guide, this);
        }
    }
}
