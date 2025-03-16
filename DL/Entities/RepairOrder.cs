using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Entities
{
  public  class RepairOrder:BaseDomain
    {
        public decimal? KMOut { get; set; }
        public string TechReview { get; set; }
        public string Garuntee { get; set; }
        public decimal FixingPrice { get; set; }
        public decimal Deiscount { get; set; }
        public decimal BeforeDiscount { get; set; }
        public decimal AfterDiscount { get; set; }
        public decimal VatMoney { get; set; }
        public decimal Total { get; set; }
        public int RepairOrderStatus { get; set; }
        public int WarshahId { get; set; }
        public Warshah Warshah { get; set; }

        public int? InspectionTemplateId { get; set; }
        public virtual InspectionTemplate InspectionTemplate { get; set; }
        public int TechId { get; set; }
        public User Tech { get; set; }
        public int? ReciptionOrderId { get; set; }
        public ReciptionOrder ReciptionOrder { get; set; }

        public int? InspectionWarshahReportId { get; set; }

        public InspectionWarshahReport InspectionWarshahReport { get; set; }
        public bool AddedDiscountForSpareParts { get; set; }
        public bool AddedDiscountForFixingPrice { get; set; }


        public decimal DiscSpareMoney { get; set; }

        public decimal DiscFixingMoney { get; set; }

        public bool VatSpareParts { get; set; }
        public bool VatFixingPrice { get; set; }

        public decimal VatSpareMoney { get; set; }

        public decimal VatFixingMoney { get; set; }
        public decimal SparePartsTotal { get; set; }

        public bool? DiscPoint { get; set; }
    }
}
