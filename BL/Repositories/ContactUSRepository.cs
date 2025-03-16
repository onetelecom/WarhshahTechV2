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
    public interface IContactUSRepository
    {
     

    }

    public class ContactUSRepository : Repository<ContactUs>, IContactUSRepository
    {

        public ContactUSRepository(AppDBContext ctx) : base(ctx)
        { }


       


    }
}
