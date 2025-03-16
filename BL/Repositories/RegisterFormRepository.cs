using BL.Infrastructure;
using DL.DBContext;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Repositories
{
    public interface IRegisterFormRepository
    { }
    public class RegisterFormRepository : Repository<RegisterForm>, IRegisterFormRepository
    {
        public RegisterFormRepository(AppDBContext ctx) : base(ctx)
        { }


    }
}
