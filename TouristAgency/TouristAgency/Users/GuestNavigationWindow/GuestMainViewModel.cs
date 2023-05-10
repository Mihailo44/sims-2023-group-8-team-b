using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TouristAgency.Base;
using TouristAgency.Users.HomeDisplayFeature;

namespace TouristAgency.Users.GuestNavigationWindow
{
    public class GuestMainViewModel : ViewModelBase
    {
        private App app = (App)App.Current;
        private Window _window;

        public ViewModelBase CurrentVM => app.CurrentVM;

        public GuestMainViewModel()
        {
            _window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.Name == "GuestStart");
            app.CurrentVM = new GuestHomeViewModel(app.LoggedUser, _window);
            app.CurrentVMChanged += OnCurrentVMChanged;
        }

        private void OnCurrentVMChanged()
        {
            OnPropertyChanged(nameof(CurrentVM));
        }
    }
}
