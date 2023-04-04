using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Interfaces;
using TouristAgency.Model;
using TouristAgency.Service;

namespace TouristAgency.ViewModel
{
    public class TourTouristViewModel
    {
        private readonly TourTouristService _tourTouristCheckpointService;

        public TourTouristViewModel()
        {
            _tourTouristCheckpointService = new TourTouristService();
        }

        public void Create(TourTourist tourTourist)
        {
            _tourTouristCheckpointService.Create(tourTourist);
        }

        public void Delete(int touristID)
        {
            _tourTouristCheckpointService.Delete(touristID);
        }

        public List<TourTourist> GetAll()
        {
            return _tourTouristCheckpointService.GetAll();
        }

        public void Subscribe(IObserver observer)
        {
            _tourTouristCheckpointService.Subscribe(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _tourTouristCheckpointService.Subscribe(observer);
        }

        public void NotifyObservers()
        {
            _tourTouristCheckpointService.NotifyObservers();
        }
    }
}
