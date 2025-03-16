using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DL.DTOs.Sales
{
    public class GroupSalesDTO
    {
        public Motors MotorId { get; set; }
        public List<SalesRequest> Requests  { get; set; }
    }
}
