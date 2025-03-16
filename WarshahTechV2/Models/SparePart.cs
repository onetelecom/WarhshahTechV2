using System;
using System.Collections.Generic;

#nullable disable

namespace WarshahTechV2.Models
{
    public partial class SparePart
    {
        public SparePart()
        {
            RepairOrderParts = new HashSet<RepairOrderPart>();
        }

        public int SparePartId { get; set; }
        public int? SubCategoryId { get; set; }
        public string SparePartName { get; set; }
        public int? Quantity { get; set; }
        public decimal? PeacePrice { get; set; }
        public Guid? WarshahId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Guid? CreatedBy { get; set; }
        public int? MinQuantity { get; set; }
        public string SupplierId { get; set; }
        public decimal? Buyingprice { get; set; }
        public int? MotorMakeId { get; set; }
        public int? CategoryId { get; set; }
        public int? MotorYearId { get; set; }
        public int? MotorModelId { get; set; }
        public string PartDescribtion { get; set; }
        public string PartNum { get; set; }
        public string PartImage { get; set; }

        public virtual ICollection<RepairOrderPart> RepairOrderParts { get; set; }
    }
}
