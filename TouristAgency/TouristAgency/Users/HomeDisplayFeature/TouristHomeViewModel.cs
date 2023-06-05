using IronPdf;
using System;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Windows;
using TouristAgency.Base;
using TouristAgency.Interfaces;
using TouristAgency.Statistics;
using TouristAgency.TourRequests;
using TouristAgency.Tours;
using TouristAgency.Tours.BeginTourFeature.Domain;
using TouristAgency.Tours.ComplexTourRequestFeatures.DisplayFeature;
using TouristAgency.Tours.TourRequestFeatures.CreationFeature;
using TouristAgency.Tours.TourRequestFeatures.DisplayFeature;
using TouristAgency.Tours.VoucherFeatures.DisplayFeature;
using TouristAgency.Users.HelpFeatures;
using TouristAgency.View.Creation;
using TouristAgency.View.Display;
using TouristAgency.Vouchers;

namespace TouristAgency.Users
{
    public class TouristHomeViewModel : ViewModelBase, ICloseable
    {
        private Tourist _loggedInTourist;
        private App _app;

        private string _username;
        private Window _window;

        private TourTouristCheckpointService _ttcService;
        private CheckpointService _checkpointService;
        private VoucherService _voucherService;

        public DelegateCommand TourDisplayCmd { get; set; }
        public DelegateCommand TourGuideReviewCmd { get; set; }
        public DelegateCommand TourAttendanceCmd { get; set; }
        public DelegateCommand NotificationCmd { get; set; }
        public DelegateCommand TourRequestCmd { get; set; }
        public DelegateCommand TourRequestStatisticsCmd { get; set; }
        public DelegateCommand ComplexTourRequestCmd { get; set; }
        public DelegateCommand HelpForVoucherCmd { get; set; }
        public DelegateCommand VouchersCmd { get; set; }
        public DelegateCommand ShortcutsCmd {  get; set; }
        public DelegateCommand ListOfTourRequestsCmd { get; set; }
        public DelegateCommand ListOfComplexTourRequestsCmd { get; set; }
        public DelegateCommand GenerateCmd { get; set; }
        public DelegateCommand CloseCmd { get; set; }

        public TouristHomeViewModel(Tourist tourist, Window window)
        {
            _app = (App)Application.Current;
            _loggedInTourist = tourist;
            _window = window;
            InstantiateServices();
            InstantiateCommands();
            WelcomeUser();
        }

        private void InstantiateServices()
        {
            _ttcService = new TourTouristCheckpointService();
            _checkpointService = new CheckpointService();
            _voucherService = new VoucherService();
        }

        private void InstantiateCommands()
        {
            TourDisplayCmd = new DelegateCommand(param => TourDisplayExecute(), param => CanTourDisplayExecute());
            TourGuideReviewCmd = new DelegateCommand(param => TourGuideReviewExecute(), param => CanTourGuideReviewExecute());
            TourAttendanceCmd = new DelegateCommand(param => TourAttendanceExecute(), param => CanTourAttendanceExecute());
            NotificationCmd = new DelegateCommand(param => NotificationExecute(), param => CanNotificationExecute());
            TourRequestCmd = new DelegateCommand(param => TourRequestExecute(), param => CanTourRequestExecute());
            ComplexTourRequestCmd = new DelegateCommand(param => ComplexTourRequestExecute(), param => CanComplexTourRequestExecute());
            TourRequestStatisticsCmd = new DelegateCommand(param => TourRequestStatisticsExecute(), param => CanTourRequestStatisticsExecute());
            HelpForVoucherCmd = new DelegateCommand(param => HelpForVoucherExecute(), param => CanHelpForVoucherExecute());
            VouchersCmd = new DelegateCommand(param => VouchersExecute(), param => CanVouchersExecute());
            ShortcutsCmd = new DelegateCommand(param => ShortcutsExecute(), param => CanShortcutsExecute());
            ListOfTourRequestsCmd = new DelegateCommand(param => ListOfTourRequestsExecute(), param => CanListOfTourRequestsExecute());
            ListOfComplexTourRequestsCmd = new DelegateCommand(param => ListOfComplexTourRequestsExecute(), param => CanListOfComplexTourRequestsExecute());
            GenerateCmd = new DelegateCommand(param => GenerateExecute(), param => CanGenerateExecute());
            CloseCmd = new DelegateCommand(param => CloseExecute(), param => CanCloseExecute());
        }

        private void WelcomeUser()
        {
            Username = "Welcome, " + _loggedInTourist.Username + "...";
        }

        public string Username
        {
            get => _username;
            set
            {
                if (value != _username)
                {
                    _username = value;
                    OnPropertyChanged("Username");
                }
            }
        }

        public bool CanTourDisplayExecute()
        {
            return true;
        }

        public void TourDisplayExecute()
        {
            TourDisplay display = new TourDisplay(_loggedInTourist);
            display.Show();
        }

        public bool CanTourGuideReviewExecute()
        {
            return true;
        }

        public void TourGuideReviewExecute()
        {
            GuideReviewCreation creation = new GuideReviewCreation(_loggedInTourist);
            creation.Show();
        }

        public bool CanTourAttendanceExecute()
        {
            return true;
        }

        public void TourAttendanceExecute()
        {
            TourAttendanceDisplay attendance = new TourAttendanceDisplay(_loggedInTourist);
            attendance.Show();
        }

        public bool CanNotificationExecute()
        {
            return true;
        }

        public void NotificationExecute()
        {
            NotificationDisplay display = new NotificationDisplay(_loggedInTourist);
            display.Show();
        }

        public bool CanTourRequestExecute()
        {
            return true;
        }

        public void TourRequestExecute()
        {
            TourRequestCreation creation = new TourRequestCreation(_loggedInTourist);
            creation.Show();
        }

        public bool CanComplexTourRequestExecute()
        {
            return true;
        }

        public void ComplexTourRequestExecute()
        {
            ComplexTourRequestCreation creation = new ComplexTourRequestCreation(_loggedInTourist);
            creation.Show();
        }

        public bool CanTourRequestStatisticsExecute()
        {
            return true;
        }

        public void TourRequestStatisticsExecute()
        {
            TourRequestStatisticsDisplay display = new TourRequestStatisticsDisplay();
            display.Show();
        }

        public bool CanHelpForVoucherExecute()
        {
            return true;
        }

        public void HelpForVoucherExecute()
        {
            HelpForVouchersDisplay display = new HelpForVouchersDisplay(_loggedInTourist);
            display.Show();
        }

        public bool CanVouchersExecute()
        {
            return true;
        }

        public void VouchersExecute()
        {
            VoucherDisplay display = new VoucherDisplay(_loggedInTourist);
            display.Show();
        }

        public bool CanShortcutsExecute()
        {
            return true;
        }

        public void ShortcutsExecute()
        {
            HelpForShortcutsDisplay display = new HelpForShortcutsDisplay(_loggedInTourist);
            display.Show();
        }

        public bool CanListOfTourRequestsExecute()
        {
            return true;
        }

        public void ListOfTourRequestsExecute()
        {
            TourRequestDisplay display = new TourRequestDisplay(_loggedInTourist);
            display.Show();
        }

        public bool CanListOfComplexTourRequestsExecute()
        {
            return true;
        }

        public void ListOfComplexTourRequestsExecute()
        {
            ComplexTourRequestDisplay display = new ComplexTourRequestDisplay(_loggedInTourist);
            display.Show();
        }

        public bool CanGenerateExecute()
        {
            return true;
        }

        public void GenerateExecute()
        {
            string fileName = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "/Resources/PDF/VouchersReport" + DateTime.Now.ToString("ddMMMyyyyHHmmss") + ".pdf";
            IronPdf.License.LicenseKey = "IRONPDF.OGNJENMILOJEVIC2001.11160-6DF7F18EC0-PQ5YOKIDDHF4X-PDSDZ265OZUC-DSOYMURM3EY4-U3X2FINUULAL-T4IS3FXVY3HX-OVXCDF-TUNPAYY3BA2KEA-DEPLOYMENT.TRIAL-FU7CHU.TRIAL.EXPIRES.05.JUL.2023";
            string HtmlString = "<h1>Accepted tours report</h1>" +
                "<br>" +
                "<p>Generated at: " + DateTime.Now + "</p>" +
                "<p>Requested by: " + _loggedInTourist.FirstName + " " + _loggedInTourist.LastName + "</p>" +
                "<br><hr>" +
                "<table style='width:100%;'>" +
                "<tr style='border:1px solid black'><th>Voucher name</th> <th>Expiration date</th>";
            int count = 0;
            foreach (Voucher voucher in _voucherService.GetByTouristID(_loggedInTourist.ID))
            {
                count++;
                HtmlString += "<tr style='text-align:center;border:1px solid black'><td>" + voucher.Name + "</td>" +
                            "<td>" + voucher.ExpirationDate + "</td></tr>";
            }

            HtmlString += "</table><br><hr><br><p>Total vouchers: " + count + "</p>";

            ChromePdfRenderer renderer = new ChromePdfRenderer();

            PdfDocument newPdf = renderer.RenderHtmlAsPdf(HtmlString);
            newPdf.SaveAs(fileName);
            MessageBox.Show("Valid vouchers report is generated.", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public bool CanCloseExecute()
        {
            return true;
        }

        public void CloseExecute()
        {
            _window.Close();
        }
    }
}
