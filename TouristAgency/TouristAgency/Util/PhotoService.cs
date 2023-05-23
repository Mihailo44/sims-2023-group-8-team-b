using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Interfaces;

namespace TouristAgency.Util
{
    public class PhotoService
    {
        private readonly App _app;
        public PhotoRepository PhotoRepository { get; }

        public PhotoService()
        {
            _app = (App)System.Windows.Application.Current;
            PhotoRepository = _app.PhotoRepository;
            RegeneratePhotoPaths(GetAll());
        }
        public int GenerateId()
        {
            return PhotoRepository.GenerateId();
        }
        public List<Photo> GetAll()
        {
            return PhotoRepository.GetAll();
        }
        public Photo GetById(int id)
        {
            return PhotoRepository.GetById(id);
        }

        public Photo GetByLink(Photo photo)
        {
            return GetAll().FirstOrDefault(p => p.Link == photo.Link);
        }

        public Photo Create(Photo newPhoto)
        {
            return PhotoRepository.Create(newPhoto);
        }

        public List<String> SelectPhotoPaths()
        {
            List<String> photoPaths = new List<string>();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.CurrentDirectory;
            openFileDialog.Filter = "PNG (*.png)|*.png|JPG (*.jpg)|*.jpg|JPEG (*.jpeg)|*.jpeg|All files (*.*)|*.*";
            openFileDialog.Multiselect = true;

            openFileDialog.ShowDialog();
            foreach(string path in openFileDialog.FileNames)
            {
                photoPaths.Add(path);
            }


            return photoPaths;
        }

        public List<string> CopyToResourceDirectory(List<string> oldPaths)
        {
            List<string> newPaths = new List<string>();
            string resourcePath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\Resources\\Image\\TourImage";
            foreach(string oldPath in oldPaths)
            {
                string newPath = resourcePath + "\\" + oldPath.Split('\\')[^1];
                newPaths.Add(newPath);
                if(!File.Exists(newPath))
                    File.Copy(oldPath, newPath);
            }
            return newPaths;
        }

        public void RegeneratePhotoPaths(List<Photo> photos)
        {
            foreach(Photo photo in photos)
            {
                if (photo.Link.Contains("http"))
                    continue;
                else if (!File.Exists(photo.Link))
                {
                    photo.Link = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\Resources\\Image\\TourImage\\" + photo.Link.Split("\\")[^1];
                    Update(photo, photo.ID);
                }
            }
        }

        public Photo UniqueCreate(Photo newPhoto)
        {
            if (IsUnique(newPhoto))
            {
                return Create(newPhoto);
            }
            else
                return GetByLink(newPhoto);
        }

        public bool IsUnique(Photo newPhoto)
        {
            foreach (Photo photo in GetAll())
            {
                if (newPhoto.Link == photo.Link)
                    return false;
            }
            return true;
        }

        public Photo Update(Photo newPhoto, int id)
        {
            return PhotoRepository.Update(newPhoto, id);
        }

        public void Delete(int id)
        {
            PhotoRepository.Delete(id);
        }

    }
}
