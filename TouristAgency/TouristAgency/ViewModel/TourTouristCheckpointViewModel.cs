using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Interfaces;
using TouristAgency.Model;
using TouristAgency.Service;

namespace TouristAgency.ViewModel
{
    public class TourTouristCheckpointViewModel
    {
        private readonly TourTouristCheckpointService _tourTouristCheckpointService;

        public TourTouristCheckpointViewModel()
        {
            _tourTouristCheckpointService = new TourTouristCheckpointService();
        }

        public void Create(TourTouristCheckpoint tourTouristCheckpoint)
        {
            _tourTouristCheckpointService.Create(tourTouristCheckpoint);
        }

        public void Delete(int touristID)
        {
            _tourTouristCheckpointService.Delete(touristID);
        }

        public List<TourTouristCheckpoint> GetAll()
        {
            return _tourTouristCheckpointService.GetAll();
        }

        public ObservableCollection<Tourist> FilterTouristsOnCheckpoint(int tourID, int checkpointID,
            ObservableCollection<Tourist> allTourists)
        {
            return _tourTouristCheckpointService.FilterTouristsOnCheckpoint(tourID, checkpointID, allTourists);
        }


        public List<TourTouristCheckpoint> GetPendingInvitations(int touristID)
        {
            return _tourTouristCheckpointService.GetPendingInvitations(touristID);
        }

        public void AcceptInvitation(int touristID, int checkpointID)
        {
            _tourTouristCheckpointService.AcceptInvitation(touristID, checkpointID);
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
