using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Model;
using TouristAgency.Model.DAO;
using TouristAgency.Interfaces;

namespace TouristAgency.Controller
{
    public class OwnerController
    {
        private readonly OwnerDAO _owner;

        public OwnerController()
        {
            _owner = new OwnerDAO();
        }

        public List<Owner> GetAll()
        {
            return _owner.GetAll();
        }

        public void Create(Owner newOwner)
        {
            _owner.Create(newOwner);
        }

        public void Update(Owner updatedOwner,int id)
        {
            _owner.Update(updatedOwner, id);
        }

        public void Delete(Owner newOwner)
        {
            _owner.Delete(newOwner.ID);
        }

        public void Subscribe(IObserver observer)
        {
            _owner.Subscribe(observer);
        }
    }
}
