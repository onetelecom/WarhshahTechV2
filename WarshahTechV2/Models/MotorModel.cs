using System;
using System.Collections.Generic;

#nullable disable

namespace WarshahTechV2.Models
{
    public partial class MotorModel
    {
        public MotorModel()
        {
            MotorYears = new HashSet<MotorYear>();
        }

        public int MotorModelId { get; set; }
        public int MotorMakeId { get; set; }
        public string ModelName { get; set; }

        public virtual MotorMake MotorMake { get; set; }
        public virtual ICollection<MotorYear> MotorYears { get; set; }
    }
}
