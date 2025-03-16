using System;
using System.Collections.Generic;

#nullable disable

namespace WarshahTechV2.Models
{
    public partial class AttachmentType
    {
        public AttachmentType()
        {
            Attachments = new HashSet<Attachment>();
        }

        public int AttachmentTypeId { get; set; }
        public string AttachmentTypeDesc { get; set; }
        public string AttachmentTypeDescAr { get; set; }

        public virtual ICollection<Attachment> Attachments { get; set; }
    }
}
