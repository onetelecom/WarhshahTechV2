using BL.Infrastructure;
using DL.DBContext;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Repositories
{
 
      public interface IWarshahTechServiceRepository
    {


    }

    public class WarshahTechServiceRepository : Repository<WarshahTechService>, IWarshahTechServiceRepository
    {

        public WarshahTechServiceRepository(AppDBContext ctx) : base(ctx)
        { }





    }
}
