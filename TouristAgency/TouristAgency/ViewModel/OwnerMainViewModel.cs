using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Base;
using TouristAgency.Model;

namespace TouristAgency.ViewModel
{
    public class OwnerMainViewModel : ViewModelBase
    {
        private App app = (App)App.Current;
        
        public ViewModelBase CurrentVM => app.CurrentVM;

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
