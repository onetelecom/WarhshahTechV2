using System;
using System.Collections.Generic;

#nullable disable

namespace WarshahTechV2.Models
{
    public partial class MotorYear
    {
        public MotorYear()
        {
            Motors = new HashSet<Motor>();
        }

        public int MotorYearId { get; set; }
        public int? EngineId { get; set; }
        public int? MotorModelId { get; set; }
        public int? MotorMakeId { get; set; }
        public string YearName { get; set; }

        public virtual MotorModel MotorModel { get; set; }
        public virtual ICollection<Motor> Motors { get; set; }
    }
}
