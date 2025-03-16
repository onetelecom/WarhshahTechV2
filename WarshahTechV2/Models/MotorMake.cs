using System;
using System.Collections.Generic;

#nullable disable

namespace WarshahTechV2.Models
{
    public partial class MotorMake
    {
        public MotorMake()
        {
            MotorModels = new HashSet<MotorModel>();
        }

        public int MotorMakeId { get; set; }
        public string MakeName { get; set; }

        public virtual ICollection<MotorModel> MotorModels { get; set; }
    }
}
