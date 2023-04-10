using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Model;

namespace TouristAgency.Interfaces
{
    public interface IStorage<T>
    {
        public List<T> Load();
        public void Save(List<T> list);
    }
}
