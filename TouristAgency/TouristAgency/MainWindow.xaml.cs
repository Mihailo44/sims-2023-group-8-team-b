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
using TouristAgency.Controller;
using TouristAgency.Test;
using TouristAgency.View.Creation;
using TouristAgency.View.Display;
using TouristAgency.View.Home;

namespace TouristAgency
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CheckpointController _checkpointController;
        private TourController _tourController;
        public MainWindow()
        {
            InitializeComponent();
            _checkpointController = new CheckpointController();
            TourTest test = new TourTest();
            test.scenarioA();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OwnerHome x = new OwnerHome();
            x.Show();
        }

        private void TourButton_Click(object sender, RoutedEventArgs e)
        {
            TourCreation creation = new TourCreation(_tourController, _checkpointController);
            creation.Show();
        }

        private void TourDisplay_Click(object sender, RoutedEventArgs e)
        {
            TourDisplay display = new TourDisplay();
            display.Show();
        }
    }
}
