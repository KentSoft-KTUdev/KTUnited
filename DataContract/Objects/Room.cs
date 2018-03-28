using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContract.Objects
{
    public class DormitoryRef
    {
        public List<object> Resident { get; set; }
        public List<object> Guard { get; set; }
        public object Administrator { get; set; }
        public List<object> Visit { get; set; }
        public List<object> Room { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
    }

    public class Room
    {
        public DormitoryRef Dormitory { get; set; }
        public List<object> Resident { get; set; }
        public int ID { get; set; }
        public int DormitoryID { get; set; }
        public int Number { get; set; }
    }
}
