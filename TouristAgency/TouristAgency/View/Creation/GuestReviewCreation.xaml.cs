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
using System.ComponentModel;
using TouristAgency.Model;
using TouristAgency.Controller;

namespace TouristAgency.View.Creation
{
    /// <summary>
    /// Interaction logic for GuestReviewCreation.xaml
    /// </summary>
    public partial class GuestReviewCreation : Window
    {
        private readonly GuestReviewController _guestReviewController;

        public GuestReview NewGuestReview { get; set; }

        public GuestReviewCreation(Reservation SelectedReservation)
        {
            InitializeComponent();
            DataContext = this;

            for(int i = 1; i <= 5; i++)
            {
                cbClean.Items.Add(i.ToString());
                cbRules.Items.Add(i.ToString());
            }

            _guestReviewController = new GuestReviewController();
            NewGuestReview = new();
            NewGuestReview.Guest = SelectedReservation.Guest;
            NewGuestReview.GuestId = SelectedReservation.GuestId;
        }

        private void ButtonRegister_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _guestReviewController.Create(NewGuestReview);
                MessageBox.Show("Guest review created successfully");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Close();
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
