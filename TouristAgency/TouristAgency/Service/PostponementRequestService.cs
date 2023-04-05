﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Interfaces;
using TouristAgency.Model;
using TouristAgency.Storage;
using TouristAgency.Model.Enums;

namespace TouristAgency.Service
{
    public class PostponementRequestService : ICrud<PostponementRequest>, ISubject
    {
        private readonly PostponementRequestStorage _storage;
        private readonly List<PostponementRequest> _requests;
        private List<IObserver> _observers;

        public PostponementRequestService()
        {
            _storage = new PostponementRequestStorage();
            _requests = _storage.Load();
            _observers = new List<IObserver>();
        }

        public int GenerateId()
        {
            return _requests.Count() == 0 ? 0 : _requests.Max(g => g.Id) + 1;
        }

        public PostponementRequest FindById(int id)
        {
            return _requests.Find(g => g.Id == id);
        }

        public PostponementRequest Create(PostponementRequest newRequest)
        {
            newRequest.Id = GenerateId();
            _requests.Add(newRequest);
            _storage.Save(_requests);
            NotifyObservers();

            return newRequest;
        }

        public PostponementRequest Update(PostponementRequest updatedRequest, int id)
        {
            PostponementRequest currentRequest = FindById(id);

            if (currentRequest == null)
            {
                return null;
            }

            currentRequest.Comment = updatedRequest.Comment;
            currentRequest.Status = updatedRequest.Status;

            return currentRequest;
        }

        public void Delete(int id)
        {
            PostponementRequest deletedRequest = FindById(id);
            _requests.Remove(deletedRequest);
            _storage.Save(_requests);
            NotifyObservers();
        }

        public List<PostponementRequest> GetAll()
        {
            return _requests;
        }

        public List<PostponementRequest> GetByOwnerId(int ownerId)
        {
            return _requests.FindAll(r => r.Reservation.Accommodation.OwnerId == ownerId && r.Status == PostponementRequestStatus.PENDING); // treba dodati ?? operator
        }

        public void LoadReservationsToPostponementRequests(List<Reservation> reservations)
        {
            foreach(var request in _requests)
            {
                Reservation reservation = reservations.Find(r => r.Id == request.ReservationId);
                if(reservation != null)
                {
                    request.Reservation = reservation;
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