using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouristAgency.Interfaces
{
    public interface ICrud<T>
    {
        int GenerateId();
        T FindById(int id);
        T Create(T obj);
        T Update(T obj,int id);
        void Delete(int id);
        List<T> GetAll();
    }
}
