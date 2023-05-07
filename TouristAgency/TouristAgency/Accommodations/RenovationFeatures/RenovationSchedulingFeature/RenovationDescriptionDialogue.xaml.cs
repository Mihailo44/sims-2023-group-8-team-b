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
using TouristAgency.Accommodations.RenovationFeatures.DomainA;

namespace TouristAgency.Accommodations.RenovationFeatures.RenovationSchedulingFeature
{
    /// <summary>
    /// Interaction logic for RenovationDescriptionDialogue.xaml
    /// </summary>
    public partial class RenovationDescriptionDialogue : Window
    {
        public RenovationDescriptionDialogue(Renovation renovation)
        {
            InitializeComponent();
            DataContext = new RenovationDescriptionDialogueViewModel(renovation,this);
        }
    }
}
