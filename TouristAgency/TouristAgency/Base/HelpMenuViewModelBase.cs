using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TouristAgency.Base
{
    public class HelpMenuViewModelBase : ViewModelBase
    {
        private string _isHelpMenuVisible;
        private App _app = (App)Application.Current;

        public DelegateCommand ShowHelpMenuCmd { get; set; }

        public void InstantiateHelpMenuCommands()
        {
            IsHelpMenuVisible = "Hidden";
            ShowHelpMenuCmd = new DelegateCommand(param => ShowHelpMenuExecute(), param => AlwaysExecutes());
        }

        public string IsHelpMenuVisible
        {
            get => _isHelpMenuVisible;
            set
            {
                if (value != _isHelpMenuVisible)
                {
                    _isHelpMenuVisible = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool AlwaysExecutes()
        {
            return true;
        }

        public void ShowHelpMenuExecute()
        {
            if(IsHelpMenuVisible == "Hidden")
            {
                IsHelpMenuVisible = "Visible";
            }
            else
            {
                IsHelpMenuVisible = "Hidden";
            }
        }


    }
}
