﻿using TouristAgency.Base;
using TouristAgency.Users.HomeDisplayFeature;

namespace TouristAgency.Users.OwnerStart
{
    public class OwnerMainViewModel : ViewModelBase
    {
        private App app = (App)System.Windows.Application.Current;

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