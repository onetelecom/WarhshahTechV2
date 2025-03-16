using System;
using System.Collections.Generic;

#nullable disable

namespace WarshahTechV2.Models
{
    public partial class Attachment
    {
        public Guid AttachmentId { get; set; }
        public int AttachmentTypeId { get; set; }
        public string AttachmentName { get; set; }
        public string AttachmentOrigName { get; set; }
        public string AttachmentPath { get; set; }
        public int AttachmentSize { get; set; }
        public int? MotorId { get; set; }
        public Guid? RepairOrderId { get; set; }
        public Guid? WarshahId { get; set; }
        public Guid TempId { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }

        public virtual AttachmentType AttachmentType { get; set; }
    }
}
