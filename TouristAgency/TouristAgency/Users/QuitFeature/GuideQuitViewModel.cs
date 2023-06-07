using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TouristAgency.Base;
using TouristAgency.Interfaces;
using TouristAgency.Tours;
using TouristAgency.Users.HomeDisplayFeature;
using TouristAgency.Vouchers;

namespace TouristAgency.Users.QuitFeature
{
    public class GuideQuitViewModel : BurgerMenuViewModelBase, ICloseable
    {
        private App _app;
        private Window _window;
        private Guide _loggedInGuide;
        private bool _consent;


        private TourService _tourService;
        private VoucherService _voucherService;
        private GuideService _guideService;

        public DelegateCommand CloseCmd { get; set; }
        public DelegateCommand QuitCmd { get; set; }

        public GuideQuitViewModel()
        {
            _app = (App)Application.Current;
            _loggedInGuide = _app.LoggedUser;
            _window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.Name == "GuideStart");
            MenuVisibility = "Hidden";
            InstantiateServices();
            InstantiateCommands();
            InstantiateCollections();
            InstantiateMenuCommands();
        }

        private void InstantiateServices()
        {
            _tourService = new TourService();
            _voucherService = new VoucherService();
            _guideService = new GuideService();
        }

        private void InstantiateCollections()
        {

        }

        private void InstantiateCommands()
        {
            CloseCmd = new DelegateCommand(param => CloseExecute(), param => CanCloseExecute());
            QuitCmd = new DelegateCommand(param =>  QuitExecute(), param => CanQuitExecute());
        }

        public bool Consent
        {
            get => _consent;
            set
            {
                if (value != _consent)
                {
                    _consent = value;
                    OnPropertyChanged("Consent");
                }
            }
        }

        public bool CanCloseExecute()
        {
            return true;
        }

        public void CloseExecute()
        {
            _app.CurrentVM = new GuideHomeViewModel();
        }

        public bool CanQuitExecute()
        {
            return true;
        }

        public void QuitExecute()
        {
            if (Consent == true)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure?", "Question", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    List<Tour> tours = _tourService.GetAll().FindAll(t => t.AssignedGuideID == _loggedInGuide.ID && t.Status != Util.TourStatus.ENDED);
                    foreach (Tour tour in tours)
                    {
                        foreach(Tourist tourist in tour.RegisteredTourists)
                        {
                            DateTime oneYear = DateTime.Now.AddYears(2);
                            Voucher newVoucher = new Voucher(tourist.ID, tour.ID, "Comp. voucher [q]", false, oneYear);
                            _voucherService.VoucherRepository.Create(newVoucher);
                            tourist.WonVouchers.Add(newVoucher);
                        }
                        tour.Status = Util.TourStatus.CANCELLED;
                        _tourService.Update(tour, tour.ID);
                    }
                    _loggedInGuide.IsAccountDisabled = true;
                    _guideService.Update(_loggedInGuide, _loggedInGuide.ID);
                    _window.Close();
                }
            }
            else
                MessageBox.Show("Please read the text carefully and check the box if you agree");
        }
    }
}
