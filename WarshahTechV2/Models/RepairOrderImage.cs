using System;
using System.Collections.Generic;

#nullable disable

namespace WarshahTechV2.Models
{
    public partial class RepairOrderImage
    {
        public int Id { get; set; }
        public string ImageName { get; set; }
        public Guid Roid { get; set; }
    }
}
