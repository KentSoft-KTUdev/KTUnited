using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContract.Objects
{
    public class Resident
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public Int64 PersonalCode { get; set; }
        public int RoomID { get; set; }
        public Dormitory Dormitory { get; set; }
        public List<Visit> Visits { get; set; }
        public Room Room { get; set; }
    }
}
