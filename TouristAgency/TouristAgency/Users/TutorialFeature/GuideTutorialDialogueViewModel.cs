using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TouristAgency.Base;

namespace TouristAgency.Users.TutorialFeature
{
    public class GuideTutorialDialogueViewModel : ViewModelBase
    {
        private string _videoPath;
        private Control _mediaEl;
        public GuideTutorialDialogueViewModel(string videoPath, Control mediaEl)
        {
            VideoPath = videoPath;
            _mediaEl = mediaEl;
        }

        public string VideoPath
        {
            get => _videoPath;
            set
            {
                if (_videoPath != value)
                {
                    _videoPath = value;
                    OnPropertyChanged(nameof(VideoPath));
                }
            }
        }
    }
}
