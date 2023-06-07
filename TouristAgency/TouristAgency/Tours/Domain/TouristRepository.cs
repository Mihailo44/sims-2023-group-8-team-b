using System.Collections.Generic;
using System.Linq;
using TouristAgency.Interfaces;
using TouristAgency.Tours;
using TouristAgency.Users.Domain;
using TouristAgency.Util;
using TouristAgency.Vouchers;

namespace TouristAgency.Users
{
    public class TouristRepository : ICrud<Tourist>, ISubject
    {
        private readonly IStorage<Tourist> _storage;
        private readonly List<Tourist> _tourists;
        private List<IObserver> _observers;

        public TouristRepository(IStorage<Tourist> storage)
        {
            _storage = storage;
            _tourists = _storage.Load();
            _observers = new List<IObserver>();
        }

        public int GenerateId()
        {
            return _tourists.Max(t => t.ID) + 1;
        }

        public Tourist GetById(int id)
        {
            return _tourists.Find(t => t.ID == id);
        }

        public Tourist Create(Tourist newTourist)
        {
            newTourist.ID = GenerateId();
            _tourists.Add(newTourist);
            _storage.Save(_tourists);
            NotifyObservers();

            return newTourist;
        }

        public Tourist Update(Tourist newTourist, int id)
        {
            Tourist currentTourist = GetById(id);

            if (currentTourist == null)
            {
                return null;
            }

            currentTourist.Username = newTourist.Username;
            currentTourist.Password = newTourist.Password;
            currentTourist.FirstName = newTourist.FirstName;
            currentTourist.LastName = newTourist.LastName;
            currentTourist.DateOfBirth = newTourist.DateOfBirth;
            currentTourist.Email = newTourist.Email;
            currentTourist.FullLocation = newTourist.FullLocation;
            currentTourist.Phone = newTourist.Phone;

            return currentTourist;
        }

        public void Delete(int id)
        {
            Tourist deletedTourist = GetById(id);
            _tourists.Remove(deletedTourist);
            _storage.Save(_tourists);
            NotifyObservers();
        }

        public List<Tourist> GetAll()
        {
            return _tourists;
        }

        public void LoadToursToTourist(List<TourTourist> tourTourists, List<Tour> tours)
        {
            foreach (TourTourist tourTourist in tourTourists)
            {
                Tourist selectedTourist = GetById(tourTourist.TouristID);
                foreach (Tour tour in tours)
                {
                    if (tour.ID == tourTourist.TourID)
                    {
                        selectedTourist.AppliedTours.Add(tour);
                    }
                }
            }
        }

        public void LoadVouchersToTourist(List<Voucher> vouchers)
        {
            foreach (Voucher voucher in vouchers)
            {
                foreach (Tourist tourist in _tourists)
                {
                    if (voucher.TouristID == tourist.ID)
                    {
                        tourist.WonVouchers.Add(voucher);
                    }
                }
            }
        }

        public void LoadUsersToTourists(List<User> users)
        {
            foreach (User user in users)
            {
                foreach (Tourist tourist in _tourists)
                {
                    if (user.ID == tourist.ID)
                    {
                        tourist.Username = user.Username;
                    }
                }
            }
        }

        public void LoadLocationsToTourists(List<Location> locations)
        {
            foreach (Location location in locations)
            {
                foreach (Tourist tourist in _tourists)
                {
                    if (location.ID == tourist.FullLocationID)
                    {
                        tourist.FullLocation = location;
                    }
                }
            }
        }

        public void Subscribe(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            foreach (IObserver observer in _observers)
            {
                observer.Update();
            }
        }
    }
}
