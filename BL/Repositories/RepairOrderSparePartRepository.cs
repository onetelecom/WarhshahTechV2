using BL.Infrastructure;
using DL.DBContext;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Repositories
{
    

    public interface IRepairOrderSparePartRepository
    { }
    public class RepairOrderSparePartRepository : Repository<RepairOrderSparePart>, IRepairOrderSparePartRepository
    {
        public RepairOrderSparePartRepository(AppDBContext ctx) : base(ctx)
        { }


    }
}
