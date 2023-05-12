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
using TouristAgency.Review;
using TouristAgency.Users;

namespace TouristAgency.Users.ReviewFeatures
{
    /// <summary>
    /// Interaction logic for OwnerReviewCreation.xaml
    /// </summary>
    public partial class OwnerReviewCreation : UserControl
    {
        public OwnerReviewCreation()
        {
            InitializeComponent();
            //DataContext = new OwnerReviewCreationViewModel(guest, this);
        }
    }
}
