using System;
using System.Collections.Generic;
using System.Text;

namespace Helper
{
    public class PaymentModel
    {
        public int profile_id { get; set; }
        public string tran_type { get; set; }
        public string tran_class { get; set; }
        public string cart_description { get; set; }
        public string cart_id { get; set; }
        public string cart_currency { get; set; }
        public double cart_amount { get; set; }
        public string callback { get; set; }
        public string Return { get; set; }

    }
}
