using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Interfaces;
using TouristAgency.Model;
using TouristAgency.Service;
using TouristAgency.Base;

namespace TouristAgency.ViewModel
{
    public class PhotoViewModel : ViewModelBase
    {
        private readonly PhotoService _photo;

        public PhotoViewModel()
        {
            _photo = new PhotoService();
        }

        public int GenerateId()
        {
            return _photo.GenerateId();
        }

        public Photo FindById(int id)
        {
            return _photo.FindById(id);
        }

        public Photo Create(Photo newPhoto)
        {
            return _photo.Create(newPhoto);
        }

        public Photo Update(Photo newPhoto, int id)
        {
            return _photo.Update(newPhoto, id);
        }

        public void Delete(int id)
        {
            _photo.Delete(id);
        }

        public List<Photo> GetAll()
        {
            return _photo.GetAll();
        }

        /*public void LoadToursToPhotos(List<Tour> tours)
        {
            _photo.LoadToursToPhotos(tours);
        }*/

        public void Subscribe(IObserver observer)
        {
            _photo.Subscribe(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _photo.Unsubscribe(observer);
        }

        public void NotifyObservers()
        {
            _photo.NotifyObservers();
        }
    }
}
