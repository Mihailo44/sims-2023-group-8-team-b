using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouristAgency.Model.Enums
{
    public enum ReviewStatus {  REVIEWED, UNREVIEWED, EXPIRED }
    public enum TYPE { HOTEL, HUT, APARTMENT };
    public enum PostponementRequestStatus { PENDING,APPROVED,DENIED}
    public enum STATUS { NOT_STARTED, IN_PROGRESS, ENDED, CANCELLED }
    public enum INVITATION_STATUS { PENDING, ACCEPTED }
    public enum UserType { OWNER,GUIDE,TOURIST,GUEST }
}
