using System;
using System.Collections.Generic;

#nullable disable

namespace WarshahTechV2.Models
{
    public partial class SubCategory
    {
        public int SubCategoryId { get; set; }
        public int? CategoryId { get; set; }
        public string SubCategoryNameEn { get; set; }
        public string SubCategoryName { get; set; }
        public string Test { get; set; }

        public virtual Category Category { get; set; }
    }
}
