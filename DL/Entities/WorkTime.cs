using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Entities
{
    public class WorkTime : BaseDomain
    {
        public int WarshahId { get; set; }
        public string HourFrom { get; set; }
        public string HourTo { get; set; }
        public string DayFrom { get; set; }
        public string DayTo { get; set; }
        public int WarshahShiftId { get; set; }
        public WarshahShift WarshahShift { get; set; }
    }
}
