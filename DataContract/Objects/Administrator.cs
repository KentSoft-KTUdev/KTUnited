using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContract.Objects
{
    public class Administrator
    {
        public Int64 PersonalCode { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int DormitoryId { get; set; }
    }
}
