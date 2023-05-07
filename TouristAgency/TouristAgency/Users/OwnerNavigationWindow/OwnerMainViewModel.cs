using TouristAgency.Base;
using TouristAgency.Users.HomeDisplayFeature;

namespace TouristAgency.Users.OwnerNavigationWindow
{
    public class OwnerMainViewModel : ViewModelBase
    {
        public App app = (App)System.Windows.Application.Current;
        public ViewModelBase CurrentVM
        {
            get => app.CurrentVM;
        }

        public OwnerMainViewModel()
        {
            app.CurrentVM = new OwnerHomeViewModel();
            app.CurrentVMChanged += OnCurrentVMChanged;
        }

        private void OnCurrentVMChanged()
        {
            OnPropertyChanged(nameof(CurrentVM));
        }
    }
}
