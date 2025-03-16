using DL.DTOs.SharedDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DL.DTOs.HR
{
    public class EmployeeShiftDTO : BaseDomainDTO
    {
        public string ShiftNameAr { get; set; }

        public string ShiftNameEn { get; set; }

        public DateTime StartShift { get; set; }

        public DateTime EndShift { get; set; }

    }
}