using iText.IO.Font;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Pdfa;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            try
            {
                string filePath = @"..\Resources\PDF\sRGB_CS_profile.icm";
                string fontFile = @"..\Resources\PDF\FreeSans.ttf";
                string fileName = @"..\Files\sample" + DateTime.Now.ToString("ddMMMyyyyHHmmss") + ".pdf";

                PdfADocument pdf = new PdfADocument(
                new PdfWriter(fileName),
                PdfAConformanceLevel.PDF_A_1B,
                new PdfOutputIntent("Custom", "", "http://www.color.org", "sRGB IEC61966-2.1",
                new FileStream(filePath, FileMode.Open, FileAccess.Read)));

                PdfFont font = PdfFontFactory.CreateFont(fontFile, PdfEncodings.WINANSI,
                PdfFontFactory.EmbeddingStrategy.FORCE_EMBEDDED);
                Document document = new Document(pdf);

                document.SetFont(font);

                Paragraph header = new Paragraph("ACCOMMODATION STATISTICS").SetTextAlignment(TextAlignment.CENTER).SetFontSize(20);
                document.Add(header);

                LineSeparator ls = new LineSeparator(new SolidLine());
                document.Add(ls);

                Paragraph sellerHeader = new Paragraph("Accommodation:").SetBold().SetTextAlignment(TextAlignment.LEFT);
                Paragraph sellerDetail = new Paragraph(SelectedAccommodation.Name).SetTextAlignment(TextAlignment.LEFT);
                Paragraph sellerAddress = new Paragraph(SelectedAccommodation.Location.Country + "," + SelectedAccommodation.Location.City).SetTextAlignment(TextAlignment.LEFT);

                document.Add(sellerHeader);
                document.Add(sellerDetail);
                document.Add(sellerAddress);

                Paragraph customerHeader = new Paragraph("Customer details:").SetBold().SetTextAlignment(TextAlignment.RIGHT);
                Paragraph customerDetail = new Paragraph("Customer ABC").SetTextAlignment(TextAlignment.RIGHT);
                Paragraph customerAddress1 = new Paragraph("R783, Rose Apartments, Santacruz (E)").SetTextAlignment(TextAlignment.RIGHT);
                Paragraph customerAddress2 = new Paragraph("Mumbai 400054, Maharashtra India").SetTextAlignment(TextAlignment.RIGHT);

                Paragraph customerContact = new Paragraph("+91 0000000000").SetTextAlignment(TextAlignment.RIGHT);

                document.Add(customerHeader);
                document.Add(customerDetail);
                document.Add(customerAddress1);
                document.Add(customerAddress2);
                document.Add(customerContact);

                Paragraph orderNo = new Paragraph("Order No:15484659").SetBold().SetTextAlignment(TextAlignment.LEFT);
                Paragraph invoiceTimestamp = new Paragraph("Date: 30/05/2021 04:25:37 PM").SetTextAlignment(TextAlignment.LEFT);

                document.Add(orderNo);
                document.Add(invoiceTimestamp);

                Table table = new Table(4, true);

                table.SetFontSize(9);
                Cell headerProductId = new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).Add(new Paragraph("Reservations"));
                Cell headerProduct = new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).Add(new Paragraph("Cancelations"));
                Cell headerProductPrice = new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).Add(new Paragraph("Postponations"));
                Cell headerProductQty = new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).Add(new Paragraph("Renovation Reccommendations"));

                table.AddCell(headerProductId);
                table.AddCell(headerProduct);
                table.AddCell(headerProductPrice);
                table.AddCell(headerProductQty);

                double grandTotalVal = 0;
                    Cell productid = new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).Add(new Paragraph(MonthlyStats.Reservations.ToString()));
                    Cell product = new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).Add(new Paragraph(MonthlyStats.Cancelations.ToString()));
                    Cell price = new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).Add(new Paragraph(MonthlyStats.Postponations.ToString()));
                    Cell qty = new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).Add(new Paragraph(MonthlyStats.Reccommendations.ToString()));

                    table.AddCell(productid);
                    table.AddCell(product);
                    table.AddCell(price);
                    table.AddCell(qty);

                Cell grandTotalHeader = new Cell(1, 4).SetTextAlignment(TextAlignment.RIGHT).Add(new Paragraph("Total: "));
                Cell grandTotal = new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).Add(new Paragraph(" " + grandTotalVal.ToString()));

                table.AddCell(grandTotalHeader);
                table.AddCell(grandTotal);

                document.Add(table);
                table.Flush();
                table.Complete();
                document.Close();

                System.Diagnostics.Process.Start(fileName);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
