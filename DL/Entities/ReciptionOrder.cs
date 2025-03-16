using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.Entities
{
  public  class ReciptionOrder:BaseDomain
    {



        [Required]
        public int CarOwnerId { get; set; }
        public virtual User CarOwner { get; set; }
        [Required]
        public int MotorsId { get; set; }
        public virtual Motors MotorId { get; set; }
        [Required]
        public int ReciptionId { get; set; }
        public virtual User Reciption { get; set; }
        [Required]
        public int TecnicanId { get; set; }
        public virtual User Tecnican { get; set; }
        public decimal? KM_In { get; set; }
        public decimal? AdvancePayment { get; set; }
        public decimal? CheckPrice { get; set; }
        [Required]
        public string CarOwnerDescribtion { get; set; }
        [Required]
        public string ReciptionDescribtion { get; set; }
        public int StatusId { get; set; }
        public int warshahId { get; set; }
        public bool IsCheck { get; set; }
       
        public int? PaymentTypeInvoiceId { get; set; }
        public virtual PaymentTypeInvoice PaymentTypeInvoice { get; set; }

        public int? InvoiceCategoryId { get; set; }

    }
}
