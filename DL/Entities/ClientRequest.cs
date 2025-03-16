using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Entities
{
    public class ClientRequest:BaseDomain
    {
        public int ClinetId { get; set; }
        public int WarshahId { get; set; } 
        public string RequestType { get; set; }
        public string  requestLong { get; set; }
        public string  requestLat { get; set; }
        public decimal  requestPrice { get; set; }
        public bool IsDone { get; set; }
        public bool IsGoing { get; set; }
    }
}
