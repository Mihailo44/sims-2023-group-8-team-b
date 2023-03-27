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
    public class TourTouristCheckpointController
    {
        private readonly TourTouristCheckpointDAO _tourTouristCheckpointDAO;

        public TourTouristCheckpointController()
        {
            _tourTouristCheckpointDAO = new TourTouristCheckpointDAO();
        }

        public void Create(TourTouristCheckpoint tourTouristCheckpoint)
        {
            _tourTouristCheckpointDAO.Create(tourTouristCheckpoint);
        }

        public void Delete(int touristID)
        {
            _tourTouristCheckpointDAO.Delete(touristID);
        }

        public List<TourTouristCheckpoint> GetAll()
        {
            return _tourTouristCheckpointDAO.GetAll();
        }

        public ObservableCollection<Tourist> FilterTouristsOnCheckpoint(int tourID, int checkpointID,
            ObservableCollection<Tourist> allTourists)
        {
            return _tourTouristCheckpointDAO.FilterTouristsOnCheckpoint(tourID, checkpointID, allTourists);
        }


        public List<TourTouristCheckpoint> GetPendingInvitations(int touristID)
        {
            return _tourTouristCheckpointDAO.GetPendingInvitations(touristID);
        }

        public void AcceptInvitation(int touristID, int checkpointID)
        {
            _tourTouristCheckpointDAO.AcceptInvitation(touristID, checkpointID);
        }

        public void Subscribe(IObserver observer)
        {
            _tourTouristCheckpointDAO.Subscribe(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _tourTouristCheckpointDAO.Subscribe(observer);
        }

        public void NotifyObservers()
        {
            _tourTouristCheckpointDAO.NotifyObservers();
        }
    }
}
