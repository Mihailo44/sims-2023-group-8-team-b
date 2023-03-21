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

        public Reservation Selected { get; set; }

        public ReservationController _controller { get; set; }

        public GuestReviewCreation(Reservation SelectedReservation)
        {
            InitializeComponent();
            DataContext = this;
            var app = (App)Application.Current;

            FillComboBoxes();

            _guestReviewController = new GuestReviewController();

            Selected = SelectedReservation;
            _controller = app.ReservationController;
            NewGuestReview = new();
            NewGuestReview.Guest = Selected.Guest;
            NewGuestReview.GuestId = Selected.GuestId;
        }

        private void FillComboBoxes()
        {
            for (int i = 1; i <= 5; i++)
            {
                cbClean.Items.Add(i.ToString());
                cbRules.Items.Add(i.ToString());
                cbComm.Items.Add(i.ToString());
                cbOver.Items.Add(i.ToString());
                cbNoise.Items.Add(i.ToString());
            }
        }

        private void ButtonRegister_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Selected.Status == REVIEW_STATUS.UNREVIEWED)
                {
                    _guestReviewController.Create(NewGuestReview);
                    Selected.Status = REVIEW_STATUS.REVIEWED;
                    _controller.Update(Selected, Selected.Id);
                    MessageBox.Show("Guest review created successfully");
                }
                else
                {
                    MessageBox.Show("This guest has already been reviewed");
                }
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
