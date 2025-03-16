using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.Entities.HR
{
    public class ItemSalary : BaseDomain
    {

        public int year { get; set; }

        public int Month { get; set; }

        [Required]
        public int WarshahId { get; set; }
        public Warshah Warshah { get; set; }

        [Required]
        public int DataEmployeeId { get; set; }

        public DataEmployee DataEmployee { get; set; }

        public decimal? BasicSalary { get; set; }

        public decimal? Transportation { get; set; }

        public decimal? HouseAllowances { get; set; }

        public decimal? OthersAllowances { get; set; }

        public decimal? Bonus { get; set; }

        public decimal? TotalBenifites { get; set; }

        public decimal? Absence { get; set; }

        public decimal? Installment { get; set; }

        public decimal? OtherDeduction { get; set; }

        public decimal? TotalDeduction { get; set; }

        public decimal? NetSalary { get; set; }



    }
}
