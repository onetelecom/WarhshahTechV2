using DL.DTOs.SharedDTO;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace DL.DTOs.JobCardDtos
{
  public  class RepairOrderImageDTO:BaseDomainDTO
    {

        public int RepairOrderId { get; set; }
        public IFormFile ImageName { get; set; }
    }
}
