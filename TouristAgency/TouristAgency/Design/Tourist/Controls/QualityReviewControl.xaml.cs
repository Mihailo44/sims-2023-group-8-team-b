using Prism.Commands;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TouristAgency.Design.Tourist.Controls
{
    /// <summary>
    /// Interaction logic for ReviewControl.xaml
    /// </summary>
    public partial class QualityReviewControl : UserControl
    {



        public QualityReviewControl()
        {
            InitializeComponent();
        }

        public string Review
        {
            get { return (string)GetValue(ReviewProperty); }
            set { SetValue(ReviewProperty, value); }
        }


        public static readonly DependencyProperty ReviewProperty
        = DependencyProperty.Register(
          "Review",
          typeof(string),
          typeof(QualityReviewControl),
          new PropertyMetadata("one")
      );
    }
}
