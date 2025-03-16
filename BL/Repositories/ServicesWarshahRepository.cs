using BL.Infrastructure;
using DL.DBContext;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Repositories
{
 

    public interface IServicesWarshahRepository
    { }
    public class ServicesWarshahRepository : Repository<ServiceWarshah>, IServicesWarshahRepository
    {
        public ServicesWarshahRepository(AppDBContext ctx) : base(ctx)
        { }


    }
}
