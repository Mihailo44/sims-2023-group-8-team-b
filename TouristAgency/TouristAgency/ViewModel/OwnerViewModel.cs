using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Model;
using TouristAgency.Interfaces;
using TouristAgency.Service;
using TouristAgency.Base;

namespace TouristAgency.ViewModel
{
    public class OwnerViewModel : ViewModelBase 
    {
        private readonly OwnerService _owner;
        private App app = (App)App.Current;

        public OwnerViewModel()
        {
            _owner = app.OwnerService;
        }

        public List<Owner> GetAll()
        {
            return _owner.GetAll();
        }

        public void Create(Owner newOwner)
        {
            _owner.Create(newOwner);
        }

        public void Update(Owner updatedOwner, int id)
        {
            _owner.Update(updatedOwner, id);
        }

        public void Delete(Owner newOwner)
        {
            _owner.Delete(newOwner.ID);
        }

        public void LoadAccommodationsToOwners(List<Accommodation> accommodations)
        {
            _owner.LoadAccommodationsToOwners(accommodations);
        }

        public void LoadLocationsToOwners(List<Location> locations)
        {
            _owner.LoadLocationsToOwners(locations);
        }

        public void Subscribe(IObserver observer)
        {
            _owner.Subscribe(observer);
        }
    }
}
