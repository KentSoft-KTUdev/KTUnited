using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContract.Objects
{
    public class Dormitory
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public List<Resident> Residents { get; set; }
        public List<Guard> Guard { get; set; }
        public Administrator Administrator { get; set; }
        public List<Visit> Visits { get; set; }
        public List<Room> Rooms { get; set; }
    }
}
