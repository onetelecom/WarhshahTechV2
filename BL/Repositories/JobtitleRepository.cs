using BL.Infrastructure;
using DL.DBContext;
using DL.Entities.HR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Repositories
{
  
     public interface IJobtitleRepository
    { }
    public class JobtitleRepository : Repository<JobTitle>, IJobtitleRepository
    {
        public JobtitleRepository(AppDBContext ctx) : base(ctx)
        { }


    }
}
