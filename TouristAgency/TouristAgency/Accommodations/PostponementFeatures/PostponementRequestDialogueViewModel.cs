﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Accommodations.PostponementFeatures.Domain;
using TouristAgency.Base;

namespace TouristAgency.Accommodations.PostponementFeatures
{
    public class PostponementRequestDialogueViewModel : ViewModelBase
    {
        private ViewModelBase _currentVm;

        public ViewModelBase CurrentVM 
        { 
            get => _currentVm;
            set
            {
                if(_currentVm != value)
                {
                    _currentVm = value;
                    OnPropertyChanged("CurrentVM");
                }
            }
        }

        public PostponementRequestDialogueViewModel() { }

        public PostponementRequestDialogueViewModel(PostponementRequest postponementRequest)
        {
            CurrentVM = new PostponementRequestApprovalDialogueViewModel(postponementRequest);
        }
    }
}
