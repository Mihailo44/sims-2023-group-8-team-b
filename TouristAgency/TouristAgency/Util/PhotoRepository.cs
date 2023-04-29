using System.Collections.Generic;
using System.Linq;
using TouristAgency.Interfaces;

namespace TouristAgency.Util
{
    public class PhotoRepository : ICrud<Photo>, ISubject
    {
        private readonly IStorage<Photo> _storage;
        private readonly List<Photo> _photos;
        private List<IObserver> _observers;

        public PhotoRepository(IStorage<Photo> storage)
        {
            _storage = storage;
            _photos = _storage.Load();
            _observers = new List<IObserver>();
        }

        public int GenerateId()
        {
            if (_photos.Count == 0)
            {
                return 0;
            }
            return _photos.Max(p => p.ID) + 1;
        }

        public Photo FindById(int id)
        {
            return _photos.Find(t => t.ID == id);
        }

        public Photo Create(Photo newPhoto)
        {
            newPhoto.ID = GenerateId();
            _photos.Add(newPhoto);
            _storage.Save(_photos);
            NotifyObservers();
            return newPhoto;
        }

        public Photo Update(Photo newPhoto, int id)
        {
            Photo currentPhoto = FindById(id);
            if (currentPhoto == null)
            {
                return null;
            }
            currentPhoto.Link = newPhoto.Link;
            currentPhoto.Type = newPhoto.Type;
            NotifyObservers();
            return currentPhoto;
        }

        public void Delete(int id)
        {
            Photo deletedPhoto = FindById(id);
            _photos.Remove(deletedPhoto);
            _storage.Save(_photos);
            NotifyObservers();
        }

        public List<Photo> GetAll()
        {
            return _photos;
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
