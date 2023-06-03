using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Base;

namespace TouristAgency.Interfaces
{
    public interface IValidate
    {
       Dictionary<string, string> ValidationErrors { get; set; }
       //void ValidateSelf();
       bool IsValid { get; }
    }
}
