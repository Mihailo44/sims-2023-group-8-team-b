using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronPdf;
using IronPdf.Engines.Chrome;
using IronPdf.Rendering;
using System.Windows;
using TouristAgency.Base;
using TouristAgency.Review.Domain;
using TouristAgency.Users;

namespace TouristAgency.Tours.ReportFeature
{
    public class TourReportDialogDisplayViewModel : ViewModelBase
    {
        private App _app;
        private Guide _loggedInGuide;
        private Window _window;

        private TourService _tourService;
        private GuideService _guideService;
        private GuideReviewService _guideReviewService;

        private List<Tour> _tours;

        public DateTime StartDate
        {
            get;
            set; 
        }

        public DateTime EndDate
        {
            get;
            set;
        }

        public DelegateCommand CloseCmd { get; set; }
        public DelegateCommand GenerateReportCmd { get; set; }
        public TourReportDialogDisplayViewModel(Window window)
        {
            _app = (App)Application.Current;
            _loggedInGuide = _app.LoggedUser;
            _window = window;
            InstantiateServices();
            InstantiateCommands();
            InstantiateCollections();
        }

        private void InstantiateServices()
        {
            _tourService = new TourService();
            _guideService = new GuideService();
            _guideReviewService = new GuideReviewService();
        }

        private void InstantiateCollections()
        {
            _tours = new List<Tour>();
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;
        }

        private void InstantiateCommands()
        {
            CloseCmd = new DelegateCommand(param => CloseExecute(), param => AlwaysExecute());
            GenerateReportCmd = new DelegateCommand(param => GenerateReportExecute(), param => AlwaysExecute());
        }

        public bool AlwaysExecute()
        {
            return true;
        }

        public void CloseExecute()
        {
            _window.Close();
        }

        public void GenerateReportExecute()
        {
            string fileName = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "/Resources/PDF/TourReport" +  DateTime.Now.ToString("ddMMMyyyyHHmmss") + ".pdf";
            IronPdf.License.LicenseKey = "IRONPDF.OGNJENMILOJEVIC2001.11160-6DF7F18EC0-PQ5YOKIDDHF4X-PDSDZ265OZUC-DSOYMURM3EY4-U3X2FINUULAL-T4IS3FXVY3HX-OVXCDF-TUNPAYY3BA2KEA-DEPLOYMENT.TRIAL-FU7CHU.TRIAL.EXPIRES.05.JUL.2023";
            string HtmlString = "<h1>Accepted tours report</h1>" +
                "<br>" +
                "<p>Generated at: " + DateTime.Now + "</p>" +
                "<p>Requested by: " + _loggedInGuide.FirstName + " " + _loggedInGuide.LastName + "</p>" +
                "<p>Date ranges: " + StartDate.Date.ToString() + " - " + EndDate.Date.ToString() + "</p>" +
                "<br><hr>" +
                "<table style='width:100%;border-collapse: collapse;'>" +
                "<tr style='border:1px solid black;background-color:#5dade2'><th>Tour name</th> <th>Location</th> <th>Language</th> <th>Max atted.</th>" +
                "<th>Cur. atted.</th> <th>Guide</th></tr>";
            int count = 0;
            int color = 1;
            foreach(Tour tour in _tourService.GetToursForReport(StartDate,EndDate))
            {
                count++;
                if (color % 2 == 0)
                {
                    HtmlString += "<tr style='text-align:center;border:1px solid black;background-color:#eaf2f8'><td>" + tour.Name + "</td>" +
                                "<td>" + tour.ShortLocation.Country + ", " + tour.ShortLocation.City + "</td>" +
                                "<td>" + tour.Language + "</td>" + "<td>" + tour.MaxAttendants + "</td>" +
                                "<td>" + tour.CurrentAttendants + "</td>" + "<td>" + tour.AssignedGuide.FirstName + " " + tour.AssignedGuide.LastName + "</td></tr>";
                }
                else
                {
                    HtmlString += "<tr style='text-align:center;border:1px solid black'><td>" + tour.Name + "</td>" +
                                "<td>" + tour.ShortLocation.Country + ", " + tour.ShortLocation.City + "</td>" +
                                "<td>" + tour.Language + "</td>" + "<td>" + tour.MaxAttendants + "</td>" +
                                "<td>" + tour.CurrentAttendants + "</td>" + "<td>" + tour.AssignedGuide.FirstName + " " + tour.AssignedGuide.LastName + "</td></tr>";
                }
                color++;
            }

            HtmlString += "</table><br><hr><br><p>Total tours: " + count+"</p>";

            HtmlString += "<div style='width:100%'><div style='float:left'><p>Guide signature (bellow):</p>";
            HtmlString += "<br><hr style='width:200px'></div>";

            HtmlString += "<div style='float:right'><p>HR signature (bellow):</p>";
            HtmlString += "<br><hr style='width:200px'></div></div>";
            HtmlString += "<br><center><img width='200px' height='200px' src='https://img.freepik.com/premium-vector/web-blue-approved-rubber-stamp-seal-with-grunge-vector-illustration_723710-132.jpg'></center>";
            //

            ChromePdfRenderer renderer = new ChromePdfRenderer();

            PdfDocument newPdf = renderer.RenderHtmlAsPdf(HtmlString);
            newPdf.SaveAs(fileName);
            MessageBox.Show("Succesfully created a report!");
        }
    }
}
