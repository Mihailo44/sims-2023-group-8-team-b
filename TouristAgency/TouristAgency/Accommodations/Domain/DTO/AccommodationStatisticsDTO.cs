using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TouristAgency.Accommodations.Domain.DTO
{
    public class AccommodationStatisticsDTO 
    {
        private int _reservations;
        private int _cancelations;
        private int _postponations;
        private int _reccommendations;

        public int Reservations
        {
            get => _reservations;
            set
            {
                if (_reservations != value)
                {
                    _reservations = value;
                }
            }
        }

        public int Cancelations
        {
            get => _cancelations;
            set
            {
                if (_cancelations != value)
                {
                    _cancelations = value;
                }
            }
        }
        public int Postponations
        {
            get => _postponations;
            set
            {
                if (_postponations != value)
                {
                    _postponations = value;
                }
            }
        }

        public int Reccommendations
        {
            get => _reccommendations;
            set
            {
                if (_reccommendations != value)
                {
                    _reccommendations = value;
                }
            }
        }

    }
}
