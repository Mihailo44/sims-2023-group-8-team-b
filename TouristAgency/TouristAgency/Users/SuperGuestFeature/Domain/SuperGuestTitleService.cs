using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Accommodations.PostponementFeatures.Domain;
using TouristAgency.Accommodations.ReservationFeatures.Domain;

namespace TouristAgency.Users.SuperGuestFeature.Domain
{
    public class SuperGuestTitleService
    {
        private readonly App app = (App)System.Windows.Application.Current;
        public SuperGuestTitleRepository SuperGuestTitleRepository { get; }

        public SuperGuestTitleService()
        {
            SuperGuestTitleRepository = app.SuperGuestTitleRepository;
        } 

        public void RefreshSuperGuestTitles(List<Guest> guests, List<Reservation> reservations)
        {
            CleanUpSuperGuestTitles(guests, reservations);

            foreach (Guest guest in guests)
            {
                if (GetNumOfTitles(guest) == 0 && GetNumOfReservations(guest, reservations) >= 10)
                {
                    SuperGuestTitle newSuperGuestTitle = new SuperGuestTitle(guest.ID, guest, true, 5, GetNumOfReservations(guest, reservations), DateTime.Now);
                    SuperGuestTitleRepository.Create(newSuperGuestTitle);
                } 
                else if(GetNumOfTitles(guest) == 1 && GetNumOfReservations(guest, reservations) >= 10 && GetByGuestId(guest.ID).LastUpdated.Year != DateTime.Now.Year)
                {
                    SuperGuestTitle superGuestTitle = SuperGuestTitleRepository.GetById(guest.ID);
                    superGuestTitle.LastUpdated = DateTime.Now;
                    superGuestTitle.Points = 5;
                    SuperGuestTitleRepository.Update(superGuestTitle, superGuestTitle.Id);
                }
            }
        }

        public void CleanUpSuperGuestTitles(List<Guest> guests, List<Reservation> reservations)
        {
            List<SuperGuestTitle> superGuestTitles = new List<SuperGuestTitle>();

            foreach (Guest guest in guests)
            {
                foreach (SuperGuestTitle superGuestTitle in SuperGuestTitleRepository.GetAll())
                {
                    if (superGuestTitle.LastUpdated.Year != DateTime.Now.Year || GetNumOfReservations(guest, reservations) < 10)
                    {
                        superGuestTitles.Add(superGuestTitle);
                    }
                }
            }

            foreach(SuperGuestTitle superGuestTitle in superGuestTitles)
            {
                SuperGuestTitleRepository.Delete(superGuestTitle.Id);
            }
            
        }

        public SuperGuestTitle GetByGuestId(int guestId)
        {
            return SuperGuestTitleRepository.GetAll().FirstOrDefault(s => s.GuestId == guestId);
        }

        public int GetNumOfTitles(Guest guest)
        {
            int numOfTitles = 0;
            foreach(SuperGuestTitle title in SuperGuestTitleRepository.GetAll())
            {
                if(title.GuestId == guest.ID)
                {
                    numOfTitles++;
                }
            }
            return numOfTitles;
        }

        public int GetNumOfReservations(Guest guest, List<Reservation> reservations)
        {
            int numOfReservations = 0;
            foreach (Reservation reservation in reservations)
            {
                if (reservation.GuestId == guest.ID && DateTime.Now.Year == reservation.Start.Year)
                {
                    numOfReservations++;
                }
            }
            return numOfReservations;
        }

        public void UsePoint(int guestId)
        {
            SuperGuestTitle superGuestTitle = GetByGuestId(guestId);
            if (superGuestTitle != null)
            {
                if (superGuestTitle.Points >= 1)
                {
                    superGuestTitle.Points--;
                    SuperGuestTitleRepository.Update(superGuestTitle, superGuestTitle.Id);
                }
            }
        }
    }
}
