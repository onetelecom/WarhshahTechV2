using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Entities
{
    public  class NoticeProduct : BaseDomain
    {
        public int DebitAndCreditorId { get; set; }
        public DebitAndCreditor DebitAndCreditor { get; set; }
        public int PartId { get; set; }

        public string SparePartNameAr { get; set; }
        public string SparePartNameEn { get; set; }
        public int Quantity { get; set; }
        public decimal PeacePrice { get; set; }      
    }
}
