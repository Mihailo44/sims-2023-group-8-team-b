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
using TouristAgency.Controller;
using TouristAgency.Model;
using System.Collections.ObjectModel;

namespace TouristAgency.View.Creation
{
    /// <summary>
    /// Interaction logic for Accommodation.xaml
    /// </summary>
    public partial class Accommodation : Window
    {
        private readonly AccommodationController _controller;

        public TouristAgency.Model.Accommodation NewAccommodation { get; set; }

        public Accommodation(AccommodationController accommodationController)
        {
            InitializeComponent();
            DataContext = this;

            cbType.Items.Add(TYPE.HOTEL);
            cbType.Items.Add(TYPE.APARTMENT);
            cbType.Items.Add(TYPE.HUT);

            NewAccommodation = new TouristAgency.Model.Accommodation();
            _controller = accommodationController;
        }

        private void ButtonRegister_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
