using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouristAgency.Model
{
    internal class Checkpoint
    {
        private int _id;
        private string _attractionName;
        private bool _isVisited;
        //private Address ...

        public int Id
        {
            get => _id;
            set => _id = value;
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

        public Checkpoint()
        {
            _id = -1;
            _attractionName = "";
            _isVisited = false;
        }

        public Checkpoint(int id, string attractionName, bool isVisited)
        {
            _id = id;
            _attractionName = attractionName;
            _isVisited = isVisited;
        }

        public Checkpoint(Checkpoint originalCheckpoint)
        {
            _id = originalCheckpoint.Id;
            _attractionName = originalCheckpoint.AttractionName;
            _isVisited = originalCheckpoint.IsVisited;
        }
    }
}
