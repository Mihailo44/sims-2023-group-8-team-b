using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouristAgency.Model
{
    public class Checkpoint
    {
        private int _id;
        private int _tourId;
        private string _attractionName;
        private bool _isVisited;
        private Address _address;

        public int Id
        {
            get => _id;
            set => _id = value;
        }

        public int TourId
        {
            get => _tourId;
            set => _tourId = value;
        }

        public string AttractionName
        {
            get => _attractionName;
            set => _attractionName = value;
        }

        public bool IsVisited
        {
            get => _isVisited;
            set => _isVisited = value;
        }

        public Address Address
        {
            get => _address;
            set => _address = value;
        }

        public Checkpoint()
        {
            _id = -1;
            _tourId = -1;
            _attractionName = "";
            _address = new Address();
            _isVisited = false;
        }

        public Checkpoint(int id, int tourId, string attractionName, bool isVisited, Address address)
        {
            _id = id;
            _tourId = tourId;
            _attractionName = attractionName;
            _isVisited = isVisited;
            _address = new Address(address);
        }

        public Checkpoint(Checkpoint originalCheckpoint)
        {
            _id = originalCheckpoint.Id;
            _tourId = originalCheckpoint.TourId;
            _attractionName = originalCheckpoint.AttractionName;
            _isVisited = originalCheckpoint.IsVisited;
            _address = new Address(originalCheckpoint.Address);
        }
    }
}
