using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Entities
{
    public class TransactionInventory : BaseDomain
    {

        public int WarshahId { get; set; }
        public Warshah Warshah { get; set; }

        public string TransactionName { get; set; }
        public int NoofQuentity { get; set; }
        public int OldQuentity { get; set; }
        public int CurrentQuentity { get; set; }
     
        public string SparePartName { get; set; }

    }
}
