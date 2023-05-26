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

namespace TouristAgency.Tours.TourRequestFeatures.CreationFeature
{
    /// <summary>
    /// Interaction logic for ComplexTourRequestCreation.xaml
    /// </summary>
    public partial class ComplexTourRequestCreation : Window
    {
        public ComplexTourRequestCreation(Tourist tourist)
        {
            InitializeComponent();
            DataContext = new ComplexTourRequestCreationViewModel(tourist);
        }
    }
}
