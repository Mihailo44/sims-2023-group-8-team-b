using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Base;
using TouristAgency.Users.HomeDisplayFeature;

namespace TouristAgency.Users.GuideStart
{
    public class GuideMainViewModel : ViewModelBase
    {

        private App app = (App)App.Current;

        public ViewModelBase CurrentVM => app.CurrentVM;

        public GuideMainViewModel()
        {
            app.CurrentVM = new GuideHomeViewModel();
            app.CurrentVMChanged += OnCurrentVMChanged;
        }

        private void OnCurrentVMChanged()
        {
            OnPropertyChanged(nameof(CurrentVM));
        }
    }
}
