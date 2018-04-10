﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContract.Objects
{
    public class Room
    {
        public int ID { get; set; }
        public int DormitoryID { get; set; }
        public int Number { get; set; }
        Dormitory Dormitory { get; set; }
        public List<Resident> Resident { get; set; }
    }
}
