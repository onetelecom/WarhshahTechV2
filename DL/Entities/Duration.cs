using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Entities
{
    public class Duration:BaseDomain
    {
        public int? SubscribtionId { get; set; }
        public Subscribtion Subscribtion { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndDate { get; set; }
        public int WarshahId { get; set; }
        public string TransactionInfo { get; set; }
        
    }
}
