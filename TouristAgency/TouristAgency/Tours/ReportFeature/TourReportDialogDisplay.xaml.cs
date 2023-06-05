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

namespace TouristAgency.Tours.ReportFeature
{
    /// <summary>
    /// Interaction logic for ReportDialogDisplay.xaml
    /// </summary>
    public partial class TourReportDialogDisplay : Window
    {
        public TourReportDialogDisplay()
        {
            InitializeComponent();
            DataContext = new TourReportDialogDisplayViewModel(this);
        }
    }
}
