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
        private string _location;
        private string _language;
        private int _maxAttendants;
        private int _duration;
        private DateOnly _startDate;
        private List<Checkpoint> _checkpoints;
        private List<Tourist> _registeredTourists;
        private Guide _assignedGuide;
        private List<String> _photoLinks;

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

        public string Location
        {
            get => _location;
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

        public int Duration
        {
            get => _duration;
            set => _duration = value;
        }

        public DateOnly StartDate
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

        public List<String> PhotoLinks
        {
            get => _photoLinks;
            set => _photoLinks = value;
        }

        public Tour()
        {
            _id = -1;
            _maxAttendants = -1;
            _duration = -1;
            _startDate = DateOnly.MinValue;
            _checkpoints = new List<Checkpoint>();
            _registeredTourists = new List<Tourist>();
            _assignedGuide = new Guide();
        }

        public Tour(int id, string name, string description,string location, string language, int maxAttendants, int duration, DateOnly startDate, List<Checkpoint> checkpoints, List<Tourist> registeredTourists, Guide assignedGuide)
        {
            _id = id;
            _name = name;
            _description = description;
            _location = location;
            _language = language;
            _maxAttendants = maxAttendants;
            _duration = duration;
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
            _location = originalTour.Location;
            _language = originalTour.Language;
            _maxAttendants = originalTour.MaxAttendants;
            _startDate = originalTour.StartDate;
            _duration = originalTour.Duration;
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
