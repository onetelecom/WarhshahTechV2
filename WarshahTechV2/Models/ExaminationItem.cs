using System;
using System.Collections.Generic;

#nullable disable

namespace WarshahTechV2.Models
{
    public partial class ExaminationItem
    {
        public ExaminationItem()
        {
            ExaminationReports = new HashSet<ExaminationReport>();
        }

        public int ExaminationItemsId { get; set; }
        public string ExaminationItemsDesc { get; set; }
        public string ExaminationItemsDescAr { get; set; }

        public virtual ICollection<ExaminationReport> ExaminationReports { get; set; }
    }
}
