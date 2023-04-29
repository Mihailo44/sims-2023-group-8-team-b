using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TouristAgency.Base;
using TouristAgency.Users;

namespace TouristAgency.Tours
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
            return TourTouristCheckpointRepository.GetAll().Where(t => t.TouristID == touristID && t.InvitationStatus == INVITATION_STATUS.PENDING).ToList();
        }

        public void AcceptInvitation(int touristID, int checkpointID)
        {
            TourTouristCheckpoint tourTouristCheckpoint = TourTouristCheckpointRepository.GetAll().Find(t =>
                t.TouristID == touristID && t.TourCheckpoint.CheckpointID == checkpointID);

            tourTouristCheckpoint.InvitationStatus = INVITATION_STATUS.ACCEPTED;
            TourTouristCheckpointRepository.Update(tourTouristCheckpoint, touristID, tourTouristCheckpoint.TourCheckpoint.TourID);
        }

    }
}
