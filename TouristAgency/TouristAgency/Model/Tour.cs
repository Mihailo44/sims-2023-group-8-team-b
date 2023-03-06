using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouristAgency.Model
{
    internal class Tour
    {
        private int _id;
        private string _name;
        private string _description;
        //private Location location...
        private string _language;
        private int _maxAttendants;
        private int _durationInDays;
        private DateTime _startDate;
        private List<Checkpoint> _checkpoints;
        private List<Tourist> _registeredTourists;
        private Guide _assignedGuide;
        //Slike?

        public int Id
        {
            get => _id;
            set => _id = value;
        }

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public string Description
        {
            get => _description;
            set => _description = value;
        }

        public string Language
        {
            get => _language;
            set => _language = value;
        }

        public int MaxAttendants
        {
            get => _maxAttendants; 
            set => _maxAttendants = value;
        }

        public int DurationInDays
        {
            get => _durationInDays;
            set => _durationInDays = value;
        }

        public DateTime StartDate
        {
            get => _startDate;
            set => _startDate = value;
        }

        public List<Checkpoint> Checkpoints
        {
            get => _checkpoints;
            set => _checkpoints = value;
        }

        public List<Tourist> RegisteredTourists
        {
            get => _registeredTourists;
            set => _registeredTourists = value;
        }

        public Guide AssignedGuide
        {
            get => _assignedGuide;
            set => _assignedGuide = value;
        }

        public Tour()
        {
            _id = -1;
            _name = "";
            _description = "";
            _language = "";
            _maxAttendants = -1;
            _durationInDays = -1;
            _startDate = DateTime.MinValue;
            _checkpoints = new List<Checkpoint>();
            _registeredTourists = new List<Tourist>();
            _assignedGuide = new Guide();
        }

        public Tour(int id, string name, string description, string language, int maxAttendants, int durationInDays, DateTime startDate, List<Checkpoint> checkpoints, List<Tourist> registeredTourists, Guide assignedGuide)
        {
            _id = id;
            _name = name;
            _description = description;
            _language = language;
            _maxAttendants = maxAttendants;
            _durationInDays = durationInDays;
            _startDate = startDate;
            _checkpoints = new List<Checkpoint>();
            _registeredTourists = new List<Tourist>();

            foreach (Checkpoint checkpoint in checkpoints)
            {
                _checkpoints.Add(checkpoint);
            }

            foreach (Tourist tourist in registeredTourists)
            {
                _registeredTourists.Add(tourist); //TODO Add(Tourist(tourist))?
            }

            _assignedGuide = new Guide(assignedGuide);
        }

        public Tour(Tour originalTour)
        {
            _id = originalTour.Id;
            _name = originalTour.Name;
            _description = originalTour.Description;
            _language = originalTour.Language;
            _maxAttendants = originalTour.MaxAttendants;
            _startDate = originalTour.StartDate;
            _durationInDays = originalTour.DurationInDays;
            _checkpoints = new List<Checkpoint>();
            _registeredTourists = new List<Tourist>();

            foreach (Checkpoint checkpoint in originalTour.Checkpoints)
            {
                _checkpoints.Add(checkpoint);
            }

            foreach (Tourist tourist in originalTour.RegisteredTourists)
            {
                _registeredTourists.Add(tourist);
            }

            _assignedGuide = new Guide(originalTour.AssignedGuide);
        }
    }
}
