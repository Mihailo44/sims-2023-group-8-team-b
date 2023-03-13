using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Interfaces;
using TouristAgency.Storage;

namespace TouristAgency.Model.DAO
{
    public class PhotoDAO : ICrud<Photo>, ISubject
    {
        private readonly PhotoStorage _storage;
        private readonly List<Photo> _photos;
        private List<IObserver> _observers;

        public PhotoDAO()
        {
            _storage = new PhotoStorage();
            _photos = new List<Photo>();
            _observers = new List<IObserver>();
        }
        public int GenerateId()
        {
            return _photos.Max(p => p.ID);
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

        public Photo Delete(int id)
        {
            Photo deletedPhoto = FindById(id);
            if (deletedPhoto == null)
            {
                return null;
            }
            _photos.Remove(deletedPhoto);
            _storage.Save(_photos);
            NotifyObservers();
            return deletedPhoto; //TODO VOID
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
