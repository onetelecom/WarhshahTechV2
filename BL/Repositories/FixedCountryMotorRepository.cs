using BL.Infrastructure;
using DL.DBContext;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Repositories
{

    public interface IFixedCountryMotorRepository
    { }
    public class FixedCountryMotorRepository : Repository<FixedCountryMotor>, IFixedCountryMotorRepository
    {
        public FixedCountryMotorRepository(AppDBContext ctx) : base(ctx)
        { }


    }



}
