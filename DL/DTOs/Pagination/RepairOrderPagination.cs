using DL.DTOs.JobCardDtos;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DL.DTOs.Pagination
{
    public class RepairOrderPagination
    {
        public List<ViewRepairOrderDTO> RepairOrders { get; set; } = new List<ViewRepairOrderDTO>();
        public int Pages { get; set; }

        public int CurrentPage { get; set; }


    }
}
