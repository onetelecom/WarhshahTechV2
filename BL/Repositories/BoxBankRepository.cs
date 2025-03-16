using BL.Helpers;
using BL.Infrastructure;
using DL.DBContext;

using DL.DTOs.SharedDTO;
using DL.Entities;
using DL.Entities.HR;
using System.Collections.Generic;
using System.Linq;


namespace BL.Repositories
{

     public interface IBoxBankRepository
    {


    }

    public class BoxBankRepository : Repository<BoxBank>, IBoxBankRepository
    {

        public BoxBankRepository(AppDBContext ctx) : base(ctx)
        { }





    }
}
