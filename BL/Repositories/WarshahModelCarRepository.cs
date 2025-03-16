using BL.Infrastructure;
using DL.DBContext;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Repositories
{
    public interface IWarshahModelCarRepository
    {


    }

    public class WarshahModelCarRepository : Repository<WarshahModelsCar>, IWarshahModelCarRepository
    {

        public WarshahModelCarRepository(AppDBContext ctx) : base(ctx)
        { }





    }
}
