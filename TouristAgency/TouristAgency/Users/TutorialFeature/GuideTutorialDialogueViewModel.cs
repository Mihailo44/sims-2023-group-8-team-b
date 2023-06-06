using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Base;

namespace TouristAgency.Users.TutorialFeature
{
    public class GuideTutorialDialogueViewModel : ViewModelBase
    {
        private string _videoPath;

        public GuideTutorialDialogueViewModel(string videoPath)
        {
            VideoPath = videoPath;
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
