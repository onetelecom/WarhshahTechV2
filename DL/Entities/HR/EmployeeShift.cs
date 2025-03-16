using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Entities.HR
{
    public class EmployeeShift : BaseDomain
    {

        public string ShiftNameAr { get; set; }

        public string ShiftNameEn { get; set; }

        public DateTime StartShift { get; set; }

        public DateTime EndShift { get; set; }
    }
}
