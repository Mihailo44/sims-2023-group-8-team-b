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
using TouristAgency.Users.OwnerStart;
using TouristAgency.ViewModel;

namespace TouristAgency.View.Main
{
    /// <summary>
    /// Interaction logic for OwnerMain.xaml
    /// </summary>
    public partial class OwnerMain : Window
    {
        public OwnerMain()
        {
            InitializeComponent();
            DataContext = new OwnerMainViewModel();
        }
    }
}
