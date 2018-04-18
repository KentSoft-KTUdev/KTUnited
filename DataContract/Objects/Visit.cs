using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContract.Objects
{
    public class Visit
    {
        public int ID { get; set; }
        public DateTime VisitRegDateTime { get; set; }
        public bool IsOver { get; set; }
        public DateTime VisitEndDateTime { get; set; }
        public Int64 ResidentPersonalCode { get; set; }
        public Int64 GuardPersonalCode { get; set; }
        public Dormitory Dormitory { get; set; }
        public Guest Guest { get; set; }
    }
}
