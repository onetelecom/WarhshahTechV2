using System;
using System.Collections.Generic;

#nullable disable

namespace WarshahTechV2.Models
{
    public partial class WarshahCustomer
    {
        public Guid WarshahId { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Guid? CreatedBy { get; set; }

        public virtual Warshah Warshah { get; set; }
    }
}
