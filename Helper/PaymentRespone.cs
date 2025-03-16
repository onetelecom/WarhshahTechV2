using System;
using System.Collections.Generic;
using System.Text;

namespace Helper
{
    public class PaymentRespone
    {
        public string tranRef { get; set; }
        public string cartId { get; set; }
        public string respStatus { get; set; }
        public string respCode { get; set; }
        public string respMessage { get; set; }
        public string acquirerRRN { get; set; }
        public string acquirerMessage { get; set; }
        public string token { get; set; }
        public string customerEmail { get; set; }
        public string signature { get; set; }
    }
}
