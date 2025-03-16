using BL.Helpers;
using BL.Infrastructure;
using DL.DBContext;

using DL.DTOs.SharedDTO;
using DL.Entities;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;

namespace BL.Repositories
{
    public interface IServiceRepository
    {
     

    }

    public class ServiceRepository : Repository<Service>, IServiceRepository
    {

        public ServiceRepository(AppDBContext ctx) : base(ctx)
        { }


       


    }
}
