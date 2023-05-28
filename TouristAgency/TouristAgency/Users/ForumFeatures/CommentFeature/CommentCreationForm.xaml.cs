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
using TouristAgency.Users.ForumFeatures.Domain;

namespace TouristAgency.Users.ForumFeatures.CommentFeature
{
    /// <summary>
    /// Interaction logic for CommentCreationForm.xaml
    /// </summary>
    public partial class CommentCreationForm : Window
    {
        public CommentCreationForm(Forum selectedForum)
        {
            InitializeComponent();
            DataContext = new CommentCreationViewModel(selectedForum,this);
        }
    }
}
