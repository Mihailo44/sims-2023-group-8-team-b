using System;
using TouristAgency.Interfaces;
using TouristAgency.Util;

namespace TouristAgency.Tours.BeginTourFeature.Domain
{
    public class TourTouristCheckpoint : ISerializable
    {
        private TourCheckpoint _tourCheckpoint;
        private int _touristID;
        private InvitationStatus _invitationStatus;

        public TourTouristCheckpoint()
        {
            _tourCheckpoint = new TourCheckpoint();
            _tourCheckpoint.TourID = -1;
            _tourCheckpoint.CheckpointID = -1;
            _touristID = -1;
            _invitationStatus = InvitationStatus.PENDING;
        }

        public TourTouristCheckpoint(int tourID, int touristID, int checkpointID)
        {
            _tourCheckpoint = new TourCheckpoint();
            _tourCheckpoint.TourID = tourID;
            _tourCheckpoint.CheckpointID = checkpointID;
            _touristID = touristID;
            _invitationStatus = InvitationStatus.PENDING;
        }

        public TourCheckpoint TourCheckpoint
        {
            get => _tourCheckpoint;
            set => _tourCheckpoint = value;
        }

        public int TouristID
        {
            get => _touristID;
            set
            {
                if (value != _touristID)
                {
                    _touristID = value;
                }
            }
        }

        public InvitationStatus InvitationStatus
        {
            get => _invitationStatus;
            set => _invitationStatus = value;
        }


        public string[] ToCSV()
        {
            string[] csvValues =
            {
                _tourCheckpoint.TourID.ToString(),
                _touristID.ToString(),
                _tourCheckpoint.CheckpointID.ToString(),
                _invitationStatus.ToString()

            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            _tourCheckpoint.TourID = int.Parse(values[0]);
            _touristID = int.Parse(values[1]);
            _tourCheckpoint.CheckpointID = int.Parse(values[2]);
            _invitationStatus = Enum.Parse<InvitationStatus>(values[3]);
        }
    }
}
