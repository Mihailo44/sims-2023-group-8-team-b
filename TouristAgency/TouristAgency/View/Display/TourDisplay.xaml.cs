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

namespace TouristAgency.View.Display
{
    /// <summary>
    /// Interaction logic for TourDisplay.xaml
    /// </summary>
    public partial class TourDisplay : Window
    {
        private List<Tour> _tours;

        public List<Tour> Tours
        {
            get => _tours;
            set => _tours = value;
        }

        public TourDisplay()
        {
            InitializeComponent();
            Tours = new List<Tour>();
            DataContext = this;

            Checkpoint miletic = new Checkpoint(0, 0, "Trg slobode", true, new Location("Trg slobode", "", "Novi Sad", "Srbija"));
            Checkpoint dunavskiPark = new Checkpoint(1, 0, "Dunavski Park", false, new Location("Dunavska", "31", "Novi Sad", "Srbija"));
            Checkpoint petrovaradinska = new Checkpoint(2, 0, "Petrovaradin fortress", false, new Location("Tvrđava BB Petrovaradinska tvrđava", "", "Novi Sad", "Srbija"));
            User user = new User("ognjenm", "test", "Ognjen", "Milojevic", DateOnly.Parse("01.02.2001"), "ogi@gmail.com", new Location("DD", "38", "Novi Sad", "Srbija"), "38162111111");
            Tour tour1 = new Tour(0, "Novosadska poseta", "neki opis", new Location("Novi Sad", "Srbija"), "English", 20, 6,
                DateTime.Parse("11.03.2023"));
            tour1.Checkpoints.Add(miletic);
            tour1.Checkpoints.Add(dunavskiPark);
            tour1.Checkpoints.Add(petrovaradinska);
            Tours.Add(tour1);
        }

        private void Filter_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
                        
        }
    }
}
