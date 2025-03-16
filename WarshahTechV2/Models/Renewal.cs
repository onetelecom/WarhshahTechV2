using System;
using System.Collections.Generic;

#nullable disable

namespace WarshahTechV2.Models
{
    public partial class Renewal
    {
        public int Id { get; set; }
        public string WarshahId { get; set; }
        public DateTime Date { get; set; }
        public decimal Total { get; set; }
        public bool IsDone { get; set; }
        public int MounthCount { get; set; }
    }
}
