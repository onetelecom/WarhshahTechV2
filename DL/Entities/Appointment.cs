using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Entities
{
    public class Appointment:BaseDomain
    {
        public DateTime ReservationOn { get; set; }
        public string Phone { get; set; }
        public string Car { get; set; }
        public string Name { get; set; }
        public bool IsDone { get; set; }
        public int WarshahId { get; set; }


    }
}
