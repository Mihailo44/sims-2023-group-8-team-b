using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TouristAgency.Base;
using TouristAgency.Model;

namespace TouristAgency.ViewModel
{
    public class CancelTourDisplayViewModel : ViewModelBase
    {
        private App _app;
        private Guide _guide;
        private ObservableCollection<Tour> _availableTours;

        public DelegateCommand CancelTourCmd { get; }

        public CancelTourDisplayViewModel(Guide guide, Window window)
        {
            _app = (App)Application.Current;
            _guide = guide;
            AvailableTours = new ObservableCollection<Tour>(_app.TourService.GetCancellabeTours());
            CancelTourCmd = new DelegateCommand(param => CancelToursExecute(), param => CanCancelToursExecute());
        }

        public ObservableCollection<Tour> AvailableTours
        {
            get => _availableTours;
            set
            {
                if (value != _availableTours)
                {
                    _availableTours = value;
                    OnPropertyChanged("AvailableTours");
                }
            }
        }

        public bool CanCancelToursExecute()
        {
            return true;
        }

        public void CancelToursExecute()
        {
            List<Tour> tourToBeDeleted = new List<Tour>();

            foreach(Tour tour in AvailableTours) 
            {
                if(tour.IsSelected)
                {
                    tourToBeDeleted.Add(tour);
                    _app.TourService.ChangeTourStatus(tour.ID, Model.Enums.STATUS.CANCELLED);
                    foreach(Tourist tourist in tour.RegisteredTourists)
                    {
                        DateTime oneYear = DateTime.Now.AddYears(1);
                        Voucher newVoucher = new Voucher(tourist.ID, tour.ID, "Compensation voucher", false, oneYear);
                        //int touristId, int tourID, string name, bool isUsed, DateTime expirationDate
                        _app.VoucherService.Create(newVoucher);
                        tourist.WonVouchers.Add(newVoucher);
                    }
                }
            }

            foreach(Tour tour in tourToBeDeleted)
            {
                AvailableTours.Remove(tour);
            }
        }
    }
}
