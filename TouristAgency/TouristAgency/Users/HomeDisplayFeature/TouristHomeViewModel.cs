using IronPdf;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Markup;
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
using TouristAgency.Util;
using TouristAgency.View.Creation;
using TouristAgency.View.Display;
using TouristAgency.Vouchers;

namespace TouristAgency.Users
{
    public class TouristHomeViewModel : ViewModelBase, ICloseable
    {
        private Tourist _loggedInTourist;
        private App _app;

        private SeriesCollection _tourCountSeries;
        private string _username;
        private string _firstName;
        private string _lastName;
        private DateOnly _dateOfBirth;
        private string _email;
        private int _fullLocationID;
        private string _address;
        private Window _window;

        private TourTouristCheckpointService _ttcService;
        private CheckpointService _checkpointService;
        private VoucherService _voucherService;
        private TourService _tourService;

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
            TourCountSeries = new();
            InstantiateServices();
            InstantiateCollection();
            InstantiateCommands();
            WelcomeUser();
        }

        private void InstantiateServices()
        {
            _ttcService = new TourTouristCheckpointService();
            _checkpointService = new CheckpointService();
            _voucherService = new VoucherService();
            _tourService = new TourService();
        }

        private void InstantiateCollection()
        {
            List<Tour> Tours = _tourService.GetAll().FindAll(t => t.RegisteredTourists.Contains(_loggedInTourist) && t.StartDateTime.Year == DateTime.Now.Year);
            TourCountSeries.AddRange(new List<ColumnSeries> {
            new ColumnSeries
            {
                //Values = new ChartValues<int> { data.Value },
                //Title = data.Title,
                Values = new ChartValues<int> {_tourService.GetAll().FindAll(t => t.RegisteredTourists.Contains(_loggedInTourist) && t.StartDateTime.Month == 1).Count},
                Title = "Jan",
                DataLabels = true
            },
            new ColumnSeries
            {
                //Values = new ChartValues<int> { data.Value },
                //Title = data.Title,
                Values = new ChartValues<int> {_tourService.GetAll().FindAll(t => t.RegisteredTourists.Contains(_loggedInTourist) && t.StartDateTime.Month == 2).Count},
                Title = "Feb",
                DataLabels = true
            },
            new ColumnSeries
            {
                //Values = new ChartValues<int> { data.Value },
                //Title = data.Title,
                Values = new ChartValues<int> {_tourService.GetAll().FindAll(t => t.RegisteredTourists.Contains(_loggedInTourist) && t.StartDateTime.Month == 3).Count},
                Title = "Mar",
                DataLabels = true
            },
            new ColumnSeries
            {
                //Values = new ChartValues<int> { data.Value },
                //Title = data.Title,
                Values = new ChartValues<int> {_tourService.GetAll().FindAll(t => t.RegisteredTourists.Contains(_loggedInTourist) && t.StartDateTime.Month == 4).Count},
                Title = "Apr",
                DataLabels = true
            },
            new ColumnSeries
            {
                //Values = new ChartValues<int> { data.Value },
                //Title = data.Title,
                Values = new ChartValues<int> {_tourService.GetAll().FindAll(t => t.RegisteredTourists.Contains(_loggedInTourist) && t.StartDateTime.Month == 5).Count},
                Title = "May",
                DataLabels = true
            },
            new ColumnSeries
            {
                //Values = new ChartValues<int> { data.Value },
                //Title = data.Title,
                Values = new ChartValues<int> {_tourService.GetAll().FindAll(t => t.RegisteredTourists.Contains(_loggedInTourist) && t.StartDateTime.Month == 6).Count},
                Title = "Jun",
                DataLabels = true
            },
            new ColumnSeries
            {
                //Values = new ChartValues<int> { data.Value },
                //Title = data.Title,
                Values = new ChartValues<int> {_tourService.GetAll().FindAll(t => t.RegisteredTourists.Contains(_loggedInTourist) && t.StartDateTime.Month == 7).Count},
                Title = "Jul",
                DataLabels = true
            },
            new ColumnSeries
            {
                //Values = new ChartValues<int> { data.Value },
                //Title = data.Title,
                Values = new ChartValues<int> {_tourService.GetAll().FindAll(t => t.RegisteredTourists.Contains(_loggedInTourist) && t.StartDateTime.Month == 8).Count},
                Title = "Avg",
                DataLabels = true
            },
            new ColumnSeries
            {
                //Values = new ChartValues<int> { data.Value },
                //Title = data.Title,
                Values = new ChartValues<int> {_tourService.GetAll().FindAll(t => t.RegisteredTourists.Contains(_loggedInTourist) && t.StartDateTime.Month == 9).Count},
                Title = "Sept",
                DataLabels = true
            },
            new ColumnSeries
            {
                //Values = new ChartValues<int> { data.Value },
                //Title = data.Title,
                Values = new ChartValues<int> {_tourService.GetAll().FindAll(t => t.RegisteredTourists.Contains(_loggedInTourist) && t.StartDateTime.Month == 10).Count},
                Title = "Oct",
                DataLabels = true
            },
            new ColumnSeries
            {
                //Values = new ChartValues<int> { data.Value },
                //Title = data.Title,
                Values = new ChartValues<int> {_tourService.GetAll().FindAll(t => t.RegisteredTourists.Contains(_loggedInTourist) && t.StartDateTime.Month == 11).Count},
                Title = "Nov",
                DataLabels = true
            },
            new ColumnSeries
            {
                //Values = new ChartValues<int> { data.Value },
                //Title = data.Title,
                Values = new ChartValues<int> {_tourService.GetAll().FindAll(t => t.RegisteredTourists.Contains(_loggedInTourist) && t.StartDateTime.Month == 12).Count},
                Title = "Dec",
                DataLabels = true
            }}
            );
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
            FirstName = _loggedInTourist.FirstName;
            LastName = _loggedInTourist.LastName;
            DateOfBirth = _loggedInTourist.DateOfBirth;
            Email = _loggedInTourist.Email;
            FullLocationID = _loggedInTourist.FullLocationID;
            Address = _loggedInTourist.FullLocation.Street + " " + _loggedInTourist.FullLocation.StreetNumber;
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

        public string FirstName
        {
            get => _firstName;
            set
            {
                if (value != _firstName)
                {
                    _firstName = value;
                    OnPropertyChanged("FirstName");
                }
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                if (value != _lastName)
                {
                    _lastName = value;
                    OnPropertyChanged("LastName");
                }
            }
        }

        public DateOnly DateOfBirth
        {
            get => _dateOfBirth;
            set
            {
                if (value != _dateOfBirth)
                {
                    _dateOfBirth = value;
                    OnPropertyChanged("DateOfBirth");
                }
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                if (value != _email)
                {
                    _email = value;
                    OnPropertyChanged("Email");
                }
            }
        }

        public int FullLocationID
        {
            get => _fullLocationID;
            set
            {
                if (value != _fullLocationID)
                {
                    _fullLocationID = value;
                    OnPropertyChanged("FullLocationID");
                }
            }
        }

        public string Address
        {
            get => _address;
            set
            {
                if (value != _address)
                {
                    _address = value;
                    OnPropertyChanged("Address");
                }
            }
        }

        public SeriesCollection TourCountSeries
        {
            get => _tourCountSeries;
            set
            {
                if (value != _tourCountSeries)
                {
                    _tourCountSeries = value;
                    OnPropertyChanged("TourCountSeries");
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
            string HtmlString = "<h1 style='text-align:center'>Report on currently valid vouchers</h1>" +
                "<br>" +
                "<h3> Tourist agancy </h3>" +
                "<p> Generated at: " + DateTime.Now + "</p>" +
                "<p>Vouchers belong to tourists: " + _loggedInTourist.FirstName + " " + _loggedInTourist.LastName + "</p>" +
                "<br>" +
                "<p style='text-align:justify'>This is a report of all your currently valid vouchers. " +
                "You can use any of these vouchers when booking a tour. " +
                "<br><br>" +
                "Vouchers can be won if you have booked 5 tours within a year. " +
                "Vouchers won in this way are valid for 6 months and can be used for any tour. " +
                "If a tour you booked is canceled for any reason, in that case you will receive " +
                "a compensation voucher that you can also use for any other tour.</p>" +
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

            HtmlString += "</table><br><hr><br><p>The total number of your currently valid vouchers: " + count + "</p>";

            ChromePdfRenderer renderer = new ChromePdfRenderer();

            PdfDocument newPdf = renderer.RenderHtmlAsPdf(HtmlString);
            newPdf.SaveAs(fileName);
            MessageBox.Show("A PDF report of your currently valid vouchers has been successfully generated.", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
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
