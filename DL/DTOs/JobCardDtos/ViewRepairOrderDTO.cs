using System;
using System.Collections.Generic;
using System.Text;

namespace DL.DTOs.JobCardDtos
{
   public class ViewRepairOrderDTO
    {
        public int Id { get; set; }
        public int TechId { get; set; }
        public string TechName { get; set; }

        public string CarOwner { get; set; }
        public string Car { get; set; }
        public string Status { get; set; }
        public int SalesRequestStatus { get; set; }
        

    }
}
