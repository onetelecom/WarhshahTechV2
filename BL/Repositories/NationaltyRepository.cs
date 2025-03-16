using BL.Infrastructure;
using DL.DBContext;
using DL.Entities.HR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Repositories
{
    public interface INationaltyRepository
    { }
    public class NationaltyRepository : Repository<Nationality>, INationaltyRepository
    {
        public NationaltyRepository(AppDBContext ctx) : base(ctx)
        { }


    }
}
