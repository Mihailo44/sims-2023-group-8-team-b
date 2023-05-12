using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TouristAgency.Users;
using TouristAgency.Util;

namespace TouristAgency.Tours.BeginTourFeature.Domain
{
    public class TourTouristCheckpointService
    {

        private readonly App _app;
        public TourTouristCheckpointRepository TourTouristCheckpointRepository { get; set; }

        public TourTouristCheckpointService()
        {
            _app = (App)System.Windows.Application.Current;
            TourTouristCheckpointRepository = _app.TourTouristCheckpointRepository;
        }






        public ObservableCollection<Tourist> FilterTouristsOnCheckpoint(int tourID, int checkpointID, ObservableCollection<Tourist> allTourists)
        {
            //TTC sadrzi ID ture, checkpoint i turiste, samim tim onda kada se poklope prva dva, to nam je turista
            //koji nam treba.
            ObservableCollection<Tourist> filtered = new ObservableCollection<Tourist>();
            foreach (TourTouristCheckpoint ttc in TourTouristCheckpointRepository.GetAll())
            {
                bool isSameTour = tourID == ttc.TourCheckpoint.TourID;
                bool isSameCheckpoint = checkpointID == ttc.TourCheckpoint.CheckpointID;
                if (isSameTour && isSameCheckpoint)
                {
                    filtered.Add(allTourists.Single(t => t.ID == ttc.TouristID));
                }
            }
            return filtered;
        }


        public List<TourTouristCheckpoint> GetPendingInvitations(int touristID)
        {
            return TourTouristCheckpointRepository.GetAll().Where(t => t.TouristID == touristID && t.InvitationStatus == InvitationStatus.PENDING).ToList();
        }

        public TourTouristCheckpoint GetPendingInvitationsByCheckpoint(int checkpointID)
        {
            return TourTouristCheckpointRepository.GetAll().FirstOrDefault(t => t.TourCheckpoint.CheckpointID == checkpointID && t.InvitationStatus == InvitationStatus.PENDING);
        }

        public void AcceptInvitation(int touristID, int checkpointID)
        {
            TourTouristCheckpoint tourTouristCheckpoint = TourTouristCheckpointRepository.GetAll().Find(t =>
                t.TouristID == touristID && t.TourCheckpoint.CheckpointID == checkpointID);

            tourTouristCheckpoint.InvitationStatus = InvitationStatus.ACCEPTED;
            TourTouristCheckpointRepository.Update(tourTouristCheckpoint, touristID, tourTouristCheckpoint.TourCheckpoint.TourID);
        }

    }
}
