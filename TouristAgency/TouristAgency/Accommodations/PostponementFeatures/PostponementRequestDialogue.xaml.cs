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
using TouristAgency.Accommodations.PostponementFeatures.Domain;

namespace TouristAgency.Accommodations.PostponementFeatures
{
    /// <summary>
    /// Interaction logic for PostponementRequestDialogueWindow.xaml
    /// </summary>
    public partial class PostponementRequestDialogue : Window
    {
        public PostponementRequestDialogue(PostponementRequest postponementRequest)
        {
            InitializeComponent();
            DataContext = new PostponementRequestDialogueViewModel(postponementRequest);
        }
    }
}
