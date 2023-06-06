using GalaSoft.MvvmLight;
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

namespace TouristAgency.Users.TutorialFeature
{
    /// <summary>
    /// Interaction logic for GuideTutorialDialogue.xaml
    /// </summary>
    public partial class GuideTutorialDialogue : Window
    {
        private string _videoPath;
        public GuideTutorialDialogue(String videoPath)
        {
            InitializeComponent();
            MediaEl.Source = new System.Uri(videoPath);
            MediaEl.Play();
        }

        private void OnMouseDownPlayMedia(object sender, MouseButtonEventArgs e)
        {
            MediaEl.Play();
        }

        private void OnMouseDownPauseMedia(object sender, MouseButtonEventArgs e)
        {
            MediaEl.Pause();
        }

        private void OnMouseDownStopMedia(object sender, MouseButtonEventArgs e)
        {
            MediaEl.Stop();
        }

        private void SeekToMediaPosition(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int totalInSeconds = MediaEl.NaturalDuration.TimeSpan.Seconds + 60 * MediaEl.NaturalDuration.TimeSpan.Minutes;
            double percentage = (double)timelineSlider.Value/100;
            int sliderValue = Convert.ToInt32(totalInSeconds * percentage);
            TimeSpan ts = new TimeSpan(0, 0, 0, sliderValue, 0);
            MediaEl.Position = ts;
        }
    }
}
