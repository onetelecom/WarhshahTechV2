using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Entities
{
    public class WarshahWithCarOwner : BaseDomain
    {
        public int WarshahId { get; set; }

        public int CarOwnerId { get; set; }

        public User CarOwner { get; set; }

    }
}
