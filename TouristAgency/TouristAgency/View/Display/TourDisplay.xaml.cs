using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace TouristAgency.View.Display
{
    /// <summary>
    /// Interaction logic for TourDisplay.xaml
    /// </summary>
    public partial class TourDisplay : Window, INotifyPropertyChanged
    {
        TourController _tourController;
        private ObservableCollection<Tour> _tours;
        private ObservableCollection<string> _cities;
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Tour> Tours
        {
            get => _tours;
            set
            {
                if(value != _tours) 
                {
                    _tours = value;
                    //OnPropertyChanged("Tours");
                }
            }
        }

        public ObservableCollection<string> Cities
        {
            get => _cities;
            set
            {
                if (value != _cities)
                {
                    _cities = value;
                    OnPropertyChanged("Cities");
                }
            }
        }

        public TourDisplay(TourController tourController)
        {
            InitializeComponent();
            DataContext = this;
            _tourController = tourController;
            Tours = new ObservableCollection<Tour>(tourController.GetAll());
            Cities = tourController.GetAllCitites();


            /*Checkpoint miletic = new Checkpoint(0, 0, "Trg slobode", true, new Location("Trg slobode", "", "Novi Sad", "Srbija"));
            Checkpoint dunavskiPark = new Checkpoint(1, 0, "Dunavski Park", false, new Location("Dunavska", "31", "Novi Sad", "Srbija"));
            Checkpoint petrovaradinska = new Checkpoint(2, 0, "Petrovaradin fortress", false, new Location("Tvrđava BB Petrovaradinska tvrđava", "", "Novi Sad", "Srbija"));
            User user = new User("ognjenm", "test", "Ognjen", "Milojevic", DateOnly.Parse("01.02.2001"), "ogi@gmail.com", new Location("DD", "38", "Novi Sad", "Srbija"), "38162111111");
            Tour tour1 = new Tour(0, "Novosadska poseta", "neki opis", new Location("Novi Sad", "Srbija"), "English", 20, 6,
                DateTime.Parse("11.03.2023"));
            tour1.Checkpoints.Add(miletic);
            tour1.Checkpoints.Add(dunavskiPark);
            tour1.Checkpoints.Add(petrovaradinska);
            Tours.Add(tour1);*/
        }

        private void Filter_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
                        
        }

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
