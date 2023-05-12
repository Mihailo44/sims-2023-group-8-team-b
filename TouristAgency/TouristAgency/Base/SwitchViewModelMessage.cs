using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouristAgency.Base
{
    public class SwitchViewModelMessage
    {
        public ViewModelBase ViewModel { get; }

        public SwitchViewModelMessage(ViewModelBase viewModel)
        {
            ViewModel = viewModel;
        }
    }
}
