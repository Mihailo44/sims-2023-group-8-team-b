using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouristAgency.Model
{
    internal class Guide : User
    {
        private List<Tour> _assignedTours;

        public List<Tour> AssignedTours
        {
            get { return _assignedTours; }
            set { _assignedTours = value; }
        }

        public Guide()
        {

        }

        public Guide(User user) : base(user)
        {
            _assignedTours = new List<Tour>();
        }

        public Guide(Guide originalGuide)
            : base(originalGuide)
        {
            foreach (Tour tour in originalGuide.AssignedTours)
            {
                _assignedTours.Add(tour); //! TODO Proveri
            }
        }

    }
}
