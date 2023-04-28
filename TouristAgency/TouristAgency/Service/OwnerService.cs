using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Interfaces;
using TouristAgency.Model;
using TouristAgency.Repository;

namespace TouristAgency.Service
{
    public class OwnerService
    {
        private readonly App app = (App)App.Current;
        public OwnerRepository OwnerRepository { get; }

        public OwnerService()
        {
            OwnerRepository = app.OwnerRepository;
        }

        public bool IsSuperOwner(List<OwnerReview> ownerReviews,out double average)
        {
            if(ownerReviews.Count() >= 5) //stavi na 50
            {
                double sum = 0;
                double ownerScore = 0;
                foreach (OwnerReview ownerReview in ownerReviews)
                {
                    sum += ((ownerReview.Cleanliness + ownerReview.Comfort + ownerReview.OwnerCorrectness + ownerReview.Location + ownerReview.Wifi)/5);
                }

                ownerScore = CalculateOwnerScore(sum,ownerReviews.Count());
                average = ownerScore;

                if(average > 2.5) // smanji tipa na 2 da bi bilo vece da vidi da radi
                {
                    return true;
                }
            }
            average = 0;
            return false;
        }

        public double CalculateOwnerScore(double gradeSum,double numOfReviews)
        {
            return gradeSum / numOfReviews;
        }
    }
}
