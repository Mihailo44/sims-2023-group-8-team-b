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

namespace TouristAgency.Users.GuestNavigationWindow
{
    /// <summary>
    /// Interaction logic for GuestMain.xaml
    /// </summary>
    public partial class GuestMain : Window
    {
        public GuestMain()
        {
            InitializeComponent();
            DataContext = new GuestMainViewModel();
        }
    }
}
