using BL.Infrastructure;
using DL.DBContext;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Repositories
{
    public interface IWarshahCountryMotorRepository
    { }
    public class WarshahCountryMotorRepository : Repository<WarshahCountryMotor>, IWarshahCountryMotorRepository
    {
        public WarshahCountryMotorRepository(AppDBContext ctx) : base(ctx)
        { }


    }


}
