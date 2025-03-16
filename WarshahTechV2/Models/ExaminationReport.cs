using System;
using System.Collections.Generic;

#nullable disable

namespace WarshahTechV2.Models
{
    public partial class ExaminationReport
    {
        public int ExaminationReportId { get; set; }
        public Guid RepairOrderId { get; set; }
        public int ExaminationItemsId { get; set; }
        public int ExamValueId { get; set; }

        public virtual ExamValue ExamValue { get; set; }
        public virtual ExaminationItem ExaminationItems { get; set; }
    }
}
