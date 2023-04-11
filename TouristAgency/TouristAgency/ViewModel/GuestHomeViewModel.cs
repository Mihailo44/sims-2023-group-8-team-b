using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TouristAgency.Base;
using TouristAgency.Interfaces;
using TouristAgency.Model;
using TouristAgency.View.Creation;
using TouristAgency.View.Display;

namespace TouristAgency.ViewModel
{
    public class GuestHomeViewModel : ViewModelBase, ICloseable
    {
        private Guest _loggedInGuest;
        private App _app;
        private Window _window;

        public DelegateCommand AccommodationDisplayCmd { get; }
        public DelegateCommand PostponementRequestDisplayCmd { get; }
        public DelegateCommand OwnerReviewCreationCmd { get; }
        public DelegateCommand CloseCmd { get; }

        public GuestHomeViewModel(Guest guest, Window window)
        {
            _app = (App)Application.Current;
            _loggedInGuest = guest;
            _window = window;

            AccommodationDisplayCmd = new DelegateCommand(param => OpenAccommodationDisplayCmdExecute(),
                param => CanOpenAccommodationDisplayCmdExecute());
            PostponementRequestDisplayCmd = new DelegateCommand(param => OpenPostponementRequestDisplayCmdExecute(),
                param => CanOpenPostponementRequestDisplayCmdExecute());
            OwnerReviewCreationCmd = new DelegateCommand(param => OpenOwnerReviewCreationCmdExecute(),
                param => CanOpenOwnerReviewCreationCmdExecute());
            CloseCmd = new DelegateCommand(param => CloseCmdExecute(), param => CanCloseCmdExecute());
        }

        public bool CanOpenAccommodationDisplayCmdExecute()
        {
            return true;
        }

        public void OpenAccommodationDisplayCmdExecute()
        {
            AccommodationDisplay display = new AccommodationDisplay(_loggedInGuest);
            display.Show();
        }

        public bool CanOpenPostponementRequestDisplayCmdExecute()
        {
            return true;
        }

        public void OpenPostponementRequestDisplayCmdExecute()
        {
            PostponementRequestDisplay display = new PostponementRequestDisplay(_loggedInGuest);
            display.Show();
        }

        public bool CanOpenOwnerReviewCreationCmdExecute()
        {
            return true;
        }

        public void OpenOwnerReviewCreationCmdExecute()
        {
            OwnerReviewCreation creation = new OwnerReviewCreation(_loggedInGuest);
            creation.Show();
        }

        public bool CanCloseCmdExecute()
        {
            return true;
        }

        public void CloseCmdExecute()
        {
            _window.Close();
        }
    }
}
