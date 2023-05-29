using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using TouristAgency.Accommodations.Domain;
using TouristAgency.Users;
using TouristAgency.Users.Domain;
using TouristAgency.Users.ForumFeatures.Domain;

namespace TouristAgency.Converter
{
    public class UsefulCommentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            OwnerService ownerService = new();
            GuestService guestService = new();
            User user  = value as User;
            Forum forum = parameter as Forum;

            if (user.UserType == Util.UserType.OWNER)
            {
                //Owner owner = ownerService.OwnerRepository.GetById(user.ID);

                //if (owner.Accommodations.Find(a => a.LocationId == forum.LocationId) != null)
                //{
                    return "../../../Resources/Image/check.png";
                //}
            }
            else if (user.UserType == Util.UserType.GUEST)
            {
                Guest guest = guestService.GuestRepository.GetById(user.ID);
                if (guest.Reservations.Find(r => r.Accommodation.LocationId == forum.LocationId) != null)
                {
                    return "../../../Resources/Image/check.png";
                }
            }
            else
            {
                return "../../../Resources/Image/blank.png";
            }


            return "../../../Resources/Image/blank.png";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
