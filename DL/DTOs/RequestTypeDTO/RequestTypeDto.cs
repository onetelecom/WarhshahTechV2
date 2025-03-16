using DL.DTOs.SharedDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DL.DTOs.RequestType
{
    public class RequestTypeDto : BaseDomainDTO
    {

        public string ActionCode { get; set; }
        public string Namear { get; set; }
        public string NameEn { get; set; }

    }
}
