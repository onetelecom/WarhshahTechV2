using BL.Infrastructure;
using DL.DBContext;
using DL.Entities.HR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Repositories
{


    public interface IItemSalaryRepository
    { }

    public class ItemSalaryRepository : Repository<ItemSalary>, IItemSalaryRepository
    {
        public ItemSalaryRepository(AppDBContext ctx) : base(ctx)
        { }

    }


}
