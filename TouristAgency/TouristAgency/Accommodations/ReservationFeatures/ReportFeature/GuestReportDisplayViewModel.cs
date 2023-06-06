using IronPdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TouristAgency.Accommodations.Domain;
using TouristAgency.Accommodations.PostponementFeatures.CreationFeature;
using TouristAgency.Accommodations.ReservationFeatures.CreationFeature;
using TouristAgency.Accommodations.ReservationFeatures.Domain;
using TouristAgency.Base;
using TouristAgency.Tours;
using TouristAgency.Users;
using TouristAgency.Users.ForumFeatures.DisplayFeature;
using TouristAgency.Users.HomeDisplayFeature;
using TouristAgency.Users.ReviewFeatures;
using TouristAgency.Users.SuperGuestFeature;
using TouristAgency.Users.SuperGuestFeature.Domain;

namespace TouristAgency.Accommodations.ReservationFeatures.ReportFeature
{
    public class GuestReportDisplayViewModel : HelpMenuViewModelBase
    {
        private App _app;
        private Guest _loggedInGuest;
        private Window _window;

        private DateTime _start;
        private DateTime _end;

        private ReservationService _reservationService;
        private AccommodationService _accommodationService;

        public DelegateCommand GenerateRegularReportCmd { get; set; }
        public DelegateCommand GenerateCanceledReportCmd { get; set; }
        public DelegateCommand AccommodationDisplayCmd { get; set; }
        public DelegateCommand PostponementRequestDisplayCmd { get; set; }
        public DelegateCommand OwnerReviewCreationCmd { get; set; }
        public DelegateCommand SuperGuestDisplayCmd { get; set; }
        public DelegateCommand GuestReviewDisplayCmd { get; set; }
        public DelegateCommand AnywhereAnytimeCreationCmd { get; set; }
        public DelegateCommand ForumDisplayCmd { get; set; }
        public DelegateCommand GuestReportDisplayCmd { get; set; }
        public DelegateCommand CloseCmd { get; set; }
        public DelegateCommand HomeCmd { get; set; }

        public GuestReportDisplayViewModel(Guest guest, Window window)
        {
            _app = (App)Application.Current;
            _loggedInGuest = guest;
            _window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.Name == "GuestHome");
            Start = DateTime.Now;
            End = DateTime.Now;

            InstantiateCommands();
            InstantiateServices();
        }

        private void InstantiateServices()
        {
            _reservationService = new ReservationService();
            _accommodationService = new AccommodationService();
        }

        private void InstantiateCommands()
        {
            AccommodationDisplayCmd = new DelegateCommand(param => OpenAccommodationDisplayCmdExecute(),
                param => CanOpenAccommodationDisplayCmdExecute());
            PostponementRequestDisplayCmd = new DelegateCommand(param => OpenPostponementRequestDisplayCmdExecute(),
                param => CanOpenPostponementRequestDisplayCmdExecute());
            OwnerReviewCreationCmd = new DelegateCommand(param => OpenOwnerReviewCreationCmdExecute(),
                param => CanOpenOwnerReviewCreationCmdExecute());
            CloseCmd = new DelegateCommand(param => CloseCmdExecute(), param => CanCloseCmdExecute());
            SuperGuestDisplayCmd = new DelegateCommand(param => OpenSuperGuestDisplayCmdExecute(), param => CanOpenSuperGuestDisplayCmdExecute());
            HomeCmd = new DelegateCommand(param => OpenHomeCmdExecute(), param => CanOpenHomeCmdExecute());
            GuestReviewDisplayCmd = new DelegateCommand(param => OpenGuestReviewDisplayCmdExecute(), param => CanOpenGuestReviewDisplayCmdExecute());
            AnywhereAnytimeCreationCmd = new DelegateCommand(param => OpenAnywhereAnytimeCreationCmdExecute(), param => CanOpenAnywhereAnytimeCreationCmdExecute());
            ForumDisplayCmd = new DelegateCommand(param => OpenForumDisplayCmdExecute(), param => CanOpenForumDisplayCmdExecute());
            GuestReportDisplayCmd = new DelegateCommand(param => OpenGuestReportDisplayCmdExecute(), param => CanOpenGuestReportDisplayCmdExecute());
            GenerateRegularReportCmd = new DelegateCommand(param => GenerateRegularReportCmdExecute(), param => CanGenerateRegularReportCmdExecute());
            GenerateCanceledReportCmd = new DelegateCommand(param => GenerateCanceledReportCmdExecute(), param => CanGenerateCanceledReportCmdExecute());
        }

        public DateTime Start
        {
            get => _start;
            set
            {
                if (_start != value)
                {
                    _start = value;
                    OnPropertyChanged("Start");
                }
            }
        }

        public DateTime End
        {
            get => _end;
            set
            {
                if (_end != value)
                {
                    _end = value;
                    OnPropertyChanged("End");
                }
            }
        }

        public bool CanOpenAccommodationDisplayCmdExecute()
        {
            return true;
        }

        public void OpenAccommodationDisplayCmdExecute()
        {
            _app.CurrentVM = new ReservationCreationViewModel(_loggedInGuest, _window);
        }

        public bool CanOpenPostponementRequestDisplayCmdExecute()
        {
            return true;
        }

        public void OpenPostponementRequestDisplayCmdExecute()
        {
            _app.CurrentVM = new PostponementRequestCreationViewModel(_loggedInGuest, _window);
        }

        public bool CanOpenOwnerReviewCreationCmdExecute()
        {
            return true;
        }

        public void OpenOwnerReviewCreationCmdExecute()
        {
            _app.CurrentVM = new OwnerReviewCreationViewModel(_loggedInGuest, _window);
        }

        public bool CanOpenSuperGuestDisplayCmdExecute()
        {
            return true;
        }

        public void OpenSuperGuestDisplayCmdExecute()
        {
            _app.CurrentVM = new SuperGuestDisplayViewModel(_loggedInGuest, _window);
        }

        public bool CanOpenGuestReviewDisplayCmdExecute()
        {
            return true;
        }

        public void OpenGuestReviewDisplayCmdExecute()
        {
            _app.CurrentVM = new GuestReviewDisplayViewModel(_loggedInGuest, _window);
        }

        public bool CanOpenAnywhereAnytimeCreationCmdExecute()
        {
            return true;
        }

        public void OpenAnywhereAnytimeCreationCmdExecute()
        {
            _app.CurrentVM = new AnywhereAnytimeCreationViewModel(_loggedInGuest, _window);
        }

        public bool CanOpenForumDisplayCmdExecute()
        {
            return true;
        }

        public void OpenForumDisplayCmdExecute()
        {
            _app.CurrentVM = new GuestForumDisplayViewModel(_loggedInGuest, _window);
        }

        public bool CanOpenGuestReportDisplayCmdExecute()
        {
            return true;
        }

        public void OpenGuestReportDisplayCmdExecute()
        {
            _app.CurrentVM = new GuestReportDisplayViewModel(_loggedInGuest, _window);
        }

        public bool CanOpenHomeCmdExecute()
        {
            return true;
        }

        public void OpenHomeCmdExecute()
        {
            _app.CurrentVM = new GuestHomeViewModel(_loggedInGuest, _window);
        }

        public bool CanCloseCmdExecute()
        {
            return true;
        }

        public void CloseCmdExecute()
        {
            _window.Close();
        }

        public bool CanGenerateRegularReportCmdExecute()
        {
            return true;
        }

        public void GenerateRegularReportCmdExecute()
        {
            string fileName = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "/Resources/PDF/RegularReservationReport" + DateTime.Now.ToString("ddMMMyyyyHHmmss") + ".pdf";
            IronPdf.License.LicenseKey = "IRONPDF.OGNJENMILOJEVIC2001.11160-6DF7F18EC0-PQ5YOKIDDHF4X-PDSDZ265OZUC-DSOYMURM3EY4-U3X2FINUULAL-T4IS3FXVY3HX-OVXCDF-TUNPAYY3BA2KEA-DEPLOYMENT.TRIAL-FU7CHU.TRIAL.EXPIRES.05.JUL.2023";
            string HtmlString = "<h1>Regular reservations report</h1>" +
                "<br>" +
                "<p>Generated at: " + DateTime.Now + "</p>" +
                "<p>Requested by: " + _loggedInGuest.FirstName + " " + _loggedInGuest.LastName + "</p>" +
                "<p>Date ranges: " + Start.Date.ToString() + " - " + End.Date.ToString() + "</p>" +
                "<br><hr>" +
                "<table style='width:100%;'>" +
                "<tr style='border:1px solid black'><th>Accommodation name</th> <th>Location</th> <th>Owner</th> <th>Start date</th>" +
                "<th>End date</th></tr>";
            int count = 0;
            foreach (Reservation reservation in _reservationService.GetInInterval(Start, End, _loggedInGuest.ID))
            {
                count++;
                HtmlString += "<tr style='text-align:center;border:1px solid black'><td>" + reservation.Accommodation.Name + "</td>" +
                            "<td>" + reservation.Accommodation.Location.Country + ", " + reservation.Accommodation.Location.City + "</td>" +
                            "<td>" + reservation.Accommodation.Owner.FirstName + ", " + reservation.Accommodation.Owner.LastName + "</td>" + "<td>" + reservation.Start + "</td>" +
                            "<td>" + reservation.End + "</td></tr>";
            }

            HtmlString += "</table><br><hr><br><p>Total reservations: " + count + "</p>";

            ChromePdfRenderer renderer = new ChromePdfRenderer();

            PdfDocument newPdf = renderer.RenderHtmlAsPdf(HtmlString);
            newPdf.SaveAs(fileName);
            MessageBox.Show("Successfully created regular reservation report.");
        }

        public bool CanGenerateCanceledReportCmdExecute()
        {
            return true;
        }

        public void GenerateCanceledReportCmdExecute()
        {
            string fileName = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "/Resources/PDF/CanceledReservationReport" + DateTime.Now.ToString("ddMMMyyyyHHmmss") + ".pdf";
            IronPdf.License.LicenseKey = "IRONPDF.OGNJENMILOJEVIC2001.11160-6DF7F18EC0-PQ5YOKIDDHF4X-PDSDZ265OZUC-DSOYMURM3EY4-U3X2FINUULAL-T4IS3FXVY3HX-OVXCDF-TUNPAYY3BA2KEA-DEPLOYMENT.TRIAL-FU7CHU.TRIAL.EXPIRES.05.JUL.2023";
            string HtmlString = "<h1>Canceled reservations report</h1>" +
                "<br>" +
                "<p>Generated at: " + DateTime.Now + "</p>" +
                "<p>Requested by: " + _loggedInGuest.FirstName + " " + _loggedInGuest.LastName + "</p>" +
                "<p>Date ranges: " + Start.Date.ToString() + " - " + End.Date.ToString() + "</p>" +
                "<br><hr>" +
                "<table style='width:100%;'>" +
                "<tr style='border:1px solid black'><th>Accommodation name</th> <th>Location</th> <th>Owner</th> <th>Start date</th>" +
                "<th>End date</th></tr>";
            int count = 0;
            foreach (Reservation reservation in _reservationService.GetInCanceledInterval(Start, End, _loggedInGuest.ID))
            {
                count++;
                HtmlString += "<tr style='text-align:center;border:1px solid black'><td>" + reservation.Accommodation.Name + "</td>" +
                            "<td>" + reservation.Accommodation.Location.Country + ", " + reservation.Accommodation.Location.City + "</td>" +
                            "<td>" + reservation.Accommodation.Owner.FirstName + ", " + reservation.Accommodation.Owner.LastName + "</td>" + "<td>" + reservation.Start + "</td>" +
                            "<td>" + reservation.End + "</td></tr>";
            }

            HtmlString += "</table><br><hr><br><p>Total reservations: " + count + "</p>";

            ChromePdfRenderer renderer = new ChromePdfRenderer();

            PdfDocument newPdf = renderer.RenderHtmlAsPdf(HtmlString);
            newPdf.SaveAs(fileName);
            MessageBox.Show("Successfully created canceled reservation report.");
        }
    }
}
