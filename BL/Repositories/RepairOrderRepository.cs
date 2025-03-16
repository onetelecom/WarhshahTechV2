using BL.Infrastructure;
using DL.DBContext;
using DL.Entities;
using System.Collections.Generic;

namespace BL.Repositories
{
    public interface IRepairOrderRepository
    { }

    public class RepairOrderRepository : Repository<RepairOrder>, IRepairOrderRepository
    {
        public RepairOrderRepository(AppDBContext ctx) : base(ctx)
        { }

       
    }
}
