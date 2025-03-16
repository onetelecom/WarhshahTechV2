using System;
using System.Collections.Generic;

#nullable disable

namespace WarshahTechV2.Models
{
    public partial class ExamValue
    {
        public ExamValue()
        {
            ExaminationReports = new HashSet<ExaminationReport>();
        }

        public int ExamValueId { get; set; }
        public string ExamValueIdDesc { get; set; }
        public string ExamValueIdDescAr { get; set; }

        public virtual ICollection<ExaminationReport> ExaminationReports { get; set; }
    }
}
