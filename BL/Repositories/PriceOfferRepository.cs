using BL.Infrastructure;
using DL.DBContext;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Repositories
{
  
  public interface IPriceOfferRepository
    { }
    public class PriceOfferRepository : Repository<PriceOffer>, IPriceOfferRepository
    {
        public PriceOfferRepository(AppDBContext ctx) : base(ctx)
        { }


    }
}