using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.Entities
{
    public class InspectionWarshahReport : BaseDomain
    {
        [Required]
        public int WarshahId { get; set; }

        public int TemplateId { get; set; }

        public int CarOwnerId { get; set; }
        public virtual User CarOwner { get; set; }
   
        public int MotorsId { get; set; }
        public virtual Motors Motors { get; set; }

        public int PaymentInvoiceId { get; set; }

        public decimal? KM_IN { get; set; }

        public int TechnicalID { get; set; }

        public bool? DiscountPoint { get; set; }

    }

}
