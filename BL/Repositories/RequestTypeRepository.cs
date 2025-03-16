using BL.Infrastructure;
using DL.DBContext;
using DL.Entities;
using DL.Entities.HR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Repositories
{
    public interface IRequestTypeRepository
    { }
    public class RequestTypeRepository : Repository<RequestType>, IRequestTypeRepository
    {
        public RequestTypeRepository(AppDBContext ctx) : base(ctx)
        { }


    }
}
