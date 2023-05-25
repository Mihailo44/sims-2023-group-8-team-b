using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Interfaces;
using TouristAgency.Storage.FileStorage;
using TouristAgency.Users;
using TouristAgency.Util;
using TouristAgency.Tours;
using TouristAgency.Vouchers;
using TouristAgency.Users.Domain;
using TouristAgency.Accommodations.Domain;
using TouristAgency.TourRequests;
using TouristAgency.Review.Domain;
using TouristAgency.Accommodations.RenovationFeatures.DomainA;
using TouristAgency.Accommodations.ReservationFeatures.Domain;
using TouristAgency.Tours.BeginTourFeature.Domain;
using TouristAgency.Accommodations.PostponementFeatures.Domain;
using TouristAgency.Users.ReviewFeatures.Domain;
using TouristAgency.Notifications;
using TouristAgency.Users.SuperGuestFeature.Domain;
using TouristAgency.Notifications.Domain;

namespace TouristAgency.Base
{
    public class InjectorService
    {
        private static Dictionary<Type, object> _implementations = new Dictionary<Type, object>
        {
            { typeof(IStorage<Tour>), new TourFileStorage() },
            { typeof(IStorage<Checkpoint>), new CheckpointFileStorage() },
            { typeof(IStorage<Guide>), new GuideFileStorage() },
            { typeof(IStorage<Photo>), new PhotoFileStorage() },
            { typeof(IStorage<TourCheckpoint>), new TourCheckpointFileStorage() },
            { typeof(IStorage<TourTourist>), new TourTouristFileStorage() },
            { typeof(IStorage<TourTouristCheckpoint>), new TourTouristCheckpointFileStorage() },
            { typeof(IStorage<Accommodation>), new AccommodationFileStorage()},
            { typeof(IStorage<Owner>), new OwnerFileStorage()},
            { typeof(IStorage<Reservation>), new ReservationFileStorage()},
            { typeof(IStorage<User>), new UserFileStorage()},
            { typeof(IStorage<GuestReview>), new GuestReviewFileStorage()},
            { typeof(IStorage<PostponementRequest>), new PostponementRequestFileStorage()},
            { typeof(IStorage<GuideReview>), new GuideReviewFileStorage()},
            { typeof(IStorage<Location>), new LocationFileStorage()},
            { typeof(IStorage<Tourist>), new TouristFileStorage()},
            { typeof(IStorage<Voucher>), new VoucherFileStorage()},
            { typeof(IStorage<Guest>), new GuestFileStorage()},
            { typeof(IStorage<OwnerReview>), new OwnerReviewFileStorage()},
            { typeof(IStorage<TourRequest>), new TourRequestFileStorage()},
            { typeof(IStorage<TouristNotification>), new TouristNotificationFileStorage()},
            { typeof(IStorage<Renovation>),new RenovationFileStorage()},
            { typeof(IStorage<RenovationRecommendation>), new RenovationRecommendationFileStorage()},
            { typeof(IStorage<GuestReviewNotification>),new GuestReviewNotificationFileStorage() },
            { typeof(IStorage<SuperGuestTitle>), new SuperGuestTitleFileStorage()}
        };

        public static T CreateInstance<T>()
        {
            Type type = typeof(T);
            if (_implementations.ContainsKey(type))
            {
                return (T)_implementations[type];
            }
            throw new ArgumentException($"No implementation found for type {type}");
        }
    }
}
