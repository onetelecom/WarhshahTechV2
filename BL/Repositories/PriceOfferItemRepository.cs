using BL.Infrastructure;
using DL.DBContext;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Repositories
{
       public interface IPriceOfferItemRepository
    { }
    public class PriceOfferItemRepository : Repository<PriceOfferItem>, IPriceOfferItemRepository
    {
        public PriceOfferItemRepository(AppDBContext ctx) : base(ctx)
        { }


    }
}
