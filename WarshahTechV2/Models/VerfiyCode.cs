using System;
using System.Collections.Generic;

#nullable disable

namespace WarshahTechV2.Models
{
    public partial class VerfiyCode
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public int VirfeyCode { get; set; }
        public DateTime Date { get; set; }
        public string PhoneNumber { get; set; }
    }
}
