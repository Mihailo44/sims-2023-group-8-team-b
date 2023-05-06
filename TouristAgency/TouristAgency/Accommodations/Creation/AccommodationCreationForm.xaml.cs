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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TouristAgency.Accommodations.Creation;
using TouristAgency.Model;
using TouristAgency.ViewModel;

namespace TouristAgency.View.Creation
{
    /// <summary>
    /// Interaction logic for AccommodationCreationForm.xaml
    /// </summary>
    public partial class AccommodationCreationForm : UserControl
    {
        public AccommodationCreationForm()
        {
            InitializeComponent();
            DataContext = new AccommodationCreationViewModel();
        }
    }
}
