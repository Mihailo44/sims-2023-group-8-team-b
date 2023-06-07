using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using TouristAgency.Accommodations.Domain;
using TouristAgency.Accommodations.Domain.DTO;
using TouristAgency.Accommodations.PostponementFeatures.Domain;
using TouristAgency.Accommodations.RenovationFeatures.Domain;
using TouristAgency.Accommodations.ReservationFeatures.Domain;
using TouristAgency.Base;
using TouristAgency.Converter;

namespace TouristAgency.Accommodations.StatisticsFeature
{
    public class AccommodationStatisticsViewModel : ViewModelBase
    {
        private AccommodationService _accommodationService;
        private ReservationService _reservationService;
        private PostponementRequestService _postponementRequestService;
        private RenovationRecommendationService _renovationRecommendationService;

        private int _selectedYear;
        private string _selectedMonth;
        private string _busiestMonth;

        public int SelectedYear
        {
            get => _selectedYear;
            set
            {
                if (_selectedYear != value)
                {
                    _selectedYear = value;
                    OnPropertyChanged();
                }
            }
        }

        public string BusiestMonth
        {
            get => _busiestMonth;
            set
            {
                if (_busiestMonth != value)
                {
                    _busiestMonth = value;
                    OnPropertyChanged();
                }
            }
        }

        public string SelectedMonth
        {
            get => _selectedMonth;
            set
            {
                if (_selectedMonth != value)
                {
                    _selectedMonth = value;
                    OnPropertyChanged();
                }
            }
        }

        public List<int> CbYearValues { get; set; }
        public List<string> CbMonthValues { get; set; }

        public AccommodationStatisticsDTO YearlyStats { get; set; }
        public AccommodationStatisticsDTO MonthlyStats { get; set; }
        public ObservableCollection<AccommodationStatisticsDTO> StatsList { get; set; }
        public Accommodation SelectedAccommodation { get; }
        public DelegateCommand ShowYearStatsCmd { get; set; }
        public DelegateCommand ShowMonthStatsCmd { get; set; }
        public DelegateCommand PrintReportCmd { get; set; }

        public AccommodationStatisticsViewModel(Accommodation accommodation)
        {
            SelectedAccommodation = accommodation;
            CbYearValues = new();
            CbMonthValues = new();
            YearlyStats = new();
            MonthlyStats = new();
            StatsList = new();
            InstantiateServices();
            FillCombos();
            SelectedYear = DateTime.Today.Year;
            GetStatsByYear();
            GetStatsByMonth();

            InstantiateCommands();
        }

        private void InstantiateServices()
        {
            _accommodationService = new();
            _reservationService = new();
            _postponementRequestService = new();
            _renovationRecommendationService = new();
        }

        private void InstantiateCommands()
        {
            ShowYearStatsCmd = new DelegateCommand(param => ShowYearStatsCmdExecute(), param => CanShowYearStatsCmdExecute());
            ShowMonthStatsCmd = new DelegateCommand(param => ShowMonthStatsCmdExecute(), param => CanShowMonthStatsCmdExecute());
            PrintReportCmd = new DelegateCommand(param => PrintReportCmdExecute(), param => CanPrintReportCmdExecute());
        }

        private void FillCombos()
        {
            CbYearValues.Clear();
            CbMonthValues.Clear();

            int currentYear = DateTime.Today.Year;

            for (int i = 2010; i <= currentYear; i++)
            {
                CbYearValues.Add(i);
            }

            CbYearValues.Reverse();

            CbMonthValues.Add("January");
            CbMonthValues.Add("February");
            CbMonthValues.Add("March");
            CbMonthValues.Add("April");
            CbMonthValues.Add("May");
            CbMonthValues.Add("June");
            CbMonthValues.Add("July");
            CbMonthValues.Add("August");
            CbMonthValues.Add("September");
            CbMonthValues.Add("October");
            CbMonthValues.Add("November");
            CbMonthValues.Add("December");
        }

        private void GetStatsByYear()
        {
            List<int> stats = _accommodationService.GetAccommodationStatsByYear(_reservationService, _postponementRequestService, _renovationRecommendationService, SelectedAccommodation, SelectedYear);
            YearlyStats.Reservations = stats[0];
            YearlyStats.Cancelations = stats[1];
            YearlyStats.Postponations = stats[2];
            YearlyStats.Reccommendations = stats[3];
            BusiestMonth = MonthConverter.GetMonthName(stats[4]);
            SelectedMonth = BusiestMonth;
            GetStatsByMonth();
        }

        private void GetStatsByMonth()
        {
            int monthNumber = MonthConverter.GetMonthNumber(SelectedMonth);
            MonthlyStats = _accommodationService.GetAccommodationStatsByMonth(_reservationService, _postponementRequestService, _renovationRecommendationService, SelectedAccommodation, SelectedYear, monthNumber);
            StatsList.Clear();
            StatsList.Add(MonthlyStats);
        }

        public bool CanShowYearStatsCmdExecute()
        {
            return true;
        }

        public void ShowYearStatsCmdExecute()
        {
            GetStatsByYear();
        }

        public bool CanShowMonthStatsCmdExecute()
        {
            return true;
        }

        public void ShowMonthStatsCmdExecute()
        {
            GetStatsByMonth();
        }

        public bool CanPrintReportCmdExecute()
        {
            return true;
        }

        public void PrintReportCmdExecute()
        {
            Document document = new Document();

            // Set up the output stream
            string filePath = "stats_report.pdf";
            FileStream fileStream = new FileStream(filePath, FileMode.Create);
            PdfWriter writer = PdfWriter.GetInstance(document, fileStream);

            // Open the document
            document.Open();

            // Add the title and subtitle
            Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 24);
            Paragraph title = new Paragraph("ACCOMMODATION STATISTICS", titleFont);
            title.Alignment = Element.ALIGN_CENTER;
            document.Add(title);

            Font subtitleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16);
            Paragraph subtitle = new Paragraph(DateTime.Now.ToShortDateString(), subtitleFont);
            subtitle.Alignment = Element.ALIGN_CENTER;
            document.Add(subtitle);

            // Add a separator
            LineSeparator separator = new LineSeparator();
            document.Add(new Chunk(separator));

            Font infoFont = FontFactory.GetFont(FontFactory.HELVETICA, 12);
            Font boldFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
            Paragraph organizedBy = new Paragraph();
            organizedBy.Alignment = Element.ALIGN_LEFT;
            organizedBy.Add(new Chunk("Organized by:\n", boldFont));
            organizedBy.Add(new Chunk("TourAdvisor\n", infoFont));
            organizedBy.Add(new Chunk("Novi Sad, Serbia, 21000\n", infoFont));
            organizedBy.Add(new Chunk("touradvisor@gmail.com", infoFont));
            document.Add(organizedBy);
            // Add spacing after the "Organized by" section
            document.Add(new Paragraph("\n"));

            // Add the "Customer details" section
            Paragraph customerDetails = new Paragraph();
            customerDetails.Alignment = Element.ALIGN_RIGHT;
            customerDetails.Add(new Chunk("Accommodation details:\n", boldFont));

            // Access the name and email properties of the tourist
            string name = SelectedAccommodation.Name;
            string owner = SelectedAccommodation.Owner.FirstName + " " + SelectedAccommodation.Owner.LastName;

            // Add the tourist name and email to the "Customer details" section
            customerDetails.Add(new Chunk(name + "\n", infoFont));
            customerDetails.Add(new Chunk(owner + "\n", infoFont));
            // Add the "Customer details" section above the "From: Start Date" section
            document.Add(customerDetails);

            // Add spacing before the "From: Start Date" section
            document.Add(new Paragraph("\n"));

            // Add the date range information
            Paragraph dateRange = new Paragraph();
            dateRange.Add(new Chunk("Year: ", boldFont));
            dateRange.Add(new Chunk(SelectedYear.ToString(), infoFont));
            dateRange.Add(new Chunk("\nMonth: ", boldFont));
            dateRange.Add(new Chunk(SelectedMonth.ToString(), infoFont));
            document.Add(dateRange);

            // Add spacing after the date range
            document.Add(new Paragraph("\n"));

            // Add a new paragraph of text
            Paragraph paragraph = new Paragraph($"Your accommodation stats report for {SelectedMonth.ToString()},{SelectedYear.ToString()}:", infoFont);
            document.Add(paragraph);

            // Add two rows of space
            document.Add(new Paragraph("\n\n"));

            // Create the table
            PdfPTable table = new PdfPTable(4);
            table.WidthPercentage = 100;

            // Set the column widths
            float[] columnWidths = { 2f, 2f, 2f, 2f };
            table.SetWidths(columnWidths);

            // Add table headers
            PdfPCell headerCell = new PdfPCell();
            headerCell.BackgroundColor = BaseColor.LIGHT_GRAY;
            headerCell.Padding = 5;
            headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
            headerCell.VerticalAlignment = Element.ALIGN_MIDDLE;

            headerCell.Phrase = new Phrase("Reservations", infoFont);
            table.AddCell(headerCell);

            headerCell.Phrase = new Phrase("Cancelations", infoFont);
            table.AddCell(headerCell);

            headerCell.Phrase = new Phrase("Postponations", infoFont);
            table.AddCell(headerCell);

            headerCell.Phrase = new Phrase("Renovation reccomendations", infoFont);
            table.AddCell(headerCell);

            table.AddCell(new PdfPCell(new Phrase(MonthlyStats.Reservations.ToString(), infoFont)));
            table.AddCell(new PdfPCell(new Phrase(MonthlyStats.Cancelations.ToString(), infoFont)));
            table.AddCell(new PdfPCell(new Phrase(MonthlyStats.Postponations.ToString(), infoFont)));
            table.AddCell(new PdfPCell(new Phrase(MonthlyStats.Reccommendations.ToString(), infoFont)));

            // Add the table to the document
            document.Add(table);

            // Close the document
            document.Close();

            // Open the PDF document with the default application
            Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
        }
    }
}
