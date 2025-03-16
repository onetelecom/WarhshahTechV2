using BL.Infrastructure;
using DL.DBContext;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Repositories
{
 
    public interface IInspectionItems
    { }
    public class InspectionItemsRepository : Repository<InspectionItem>, IInspectionItems
    {
        public InspectionItemsRepository(AppDBContext ctx) : base(ctx)
        { }


    }

}
