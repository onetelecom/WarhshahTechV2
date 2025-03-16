using BL.Infrastructure;
using DL.DBContext;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Repositories
{
    public interface IDebitAndCreditorRepository
    {


    }

    public class DebitAndCreditorRepository : Repository<DebitAndCreditor>, IDebitAndCreditorRepository
    {

        public DebitAndCreditorRepository(AppDBContext ctx) : base(ctx)
        { }

        public object ChangeTracker { get; set; }
    }
}
