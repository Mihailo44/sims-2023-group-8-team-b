using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Interfaces;
using TouristAgency.Model;
using TouristAgency.Service;
using TouristAgency.Storage;
using TouristAgency.Storage.FileStorage;

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
