﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouristAgency.Util
{
    public enum ReviewStatus { REVIEWED, UNREVIEWED, EXPIRED }
    public enum TYPE { HOTEL, HUT, APARTMENT };
    public enum PostponementRequestStatus { PENDING, APPROVED, DENIED }
    public enum TourStatus { NOT_STARTED, IN_PROGRESS, ENDED, CANCELLED }
    public enum TourCreationScenario { DEFAULT, ACCEPT_TOURREQ, MOST_POPULAR_TOURREQ }
    public enum TourRequestStatus { PENDING, INVALID, ACCEPTED }
    public enum ComplexTourRequestStatus { PENDING, INVALID, ACCEPTED }
    public enum TourRequestType { SINGLE, MULTI }
    public enum InvitationStatus { PENDING, ACCEPTED }
    public enum UserType { OWNER, GUIDE, TOURIST, GUEST }
    public enum TouristNotificationType { TOUR_REQUEST_ACCEPTED, SUGGESTED_TOUR_LOCATION, SUGGESTED_TOUR_LANGUAGE, ATTENDANCE, CANCELED_TOUR, VOUCHER, MESSAGE }
    public enum GuestForumCommentType { COMMENT_STAR, COMMENT }
}
