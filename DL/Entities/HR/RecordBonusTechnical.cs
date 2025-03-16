using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.Entities.HR
{
    public class RecordBonusTechnical : BaseDomain
    {
    [Required]
        public int WarshahId { get; set; }
        public Warshah Warshah { get; set; }

        [Required]
    public int UserId { get; set; }
    public User User { get; set; }

    public decimal? Bonus { get; set; }


}
}