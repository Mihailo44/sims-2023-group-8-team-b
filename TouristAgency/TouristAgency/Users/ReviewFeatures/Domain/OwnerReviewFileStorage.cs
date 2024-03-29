﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Interfaces;
using TouristAgency.Serialization;

namespace TouristAgency.Users.ReviewFeatures.Domain
{
    public class OwnerReviewFileStorage : IStorage<OwnerReview>
    {
        private Serializer<OwnerReview> _serializer;
        private readonly string _file = "ownerreviews.txt";

        public OwnerReviewFileStorage()
        {
            _serializer = new Serializer<OwnerReview>();
        }

        public List<OwnerReview> Load()
        {
            return _serializer.FromCSV(_file);
        }

        public void Save(List<OwnerReview> ownerReviews)
        {
            _serializer.ToCSV(_file, ownerReviews);
        }
    }
}
