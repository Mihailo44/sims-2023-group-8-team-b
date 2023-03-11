using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Model;

namespace TouristAgency.Test
{
    public class TourTest
    {



        public void scenarioA()
        {
            Checkpoint miletic = new Checkpoint(0, "Trg slobode", true, new Address("Trg slobode", "", "Novi Sad", "Srbija"));

            Checkpoint dunavskiPark = new Checkpoint(1, "Dunavski Park", false, new Address("Dunavska", "31", "Novi Sad", "Srbija"));

            Checkpoint petrovaradinska = new Checkpoint(2, "Petrovaradin fortress", false, new Address("Tvrđava BB Petrovaradinska tvrđava", "", "Novi Sad", "Srbija"));

            List<Checkpoint> checkpoints = new List<Checkpoint>();

            checkpoints.Add(miletic);
            checkpoints.Add(dunavskiPark);
            checkpoints.Add(petrovaradinska);

            User user = new User(0, "ognjenm", "test", "Ognjen", "Milojevic", DateOnly.Parse("01.02.2001"), "ogi@gmail.com", new Address("DD", "38", "Novi Sad", "Srbija"), "38162111111");
            Guide ognjen = new Guide(user);
            List<Tourist> tourists = new List<Tourist>();
            Tour tour1 = new Tour(0, "Novosadska poseta", "neki opis", "Novi Sad, Srbija", "English", 20, 6,
                DateOnly.Parse("11.03.2023"), checkpoints, tourists, ognjen);
            ognjen.AssignedTours.Add(tour1);
            Console.WriteLine("Test");

        }
    }
}

