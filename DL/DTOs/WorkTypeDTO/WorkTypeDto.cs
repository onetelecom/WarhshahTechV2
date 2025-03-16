using DL.DTOs.SharedDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DL.DTOs.WorkType
{
    public class WorkTypeDTO : BaseDomainDTO
    {

        public string ActionCode { get; set; }
        public string Namear { get; set; }
        public string NameEn { get; set; }

    }
}
