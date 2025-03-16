using BL.Infrastructure;
using DL.DBContext;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Repositories
{

    public interface IServicePriceOfferRepository
    { }
    public class ServicePriceOfferRepository : Repository<ServicePriceOffer>, IServicePriceOfferRepository
    {
        public ServicePriceOfferRepository(AppDBContext ctx) : base(ctx)
        { }


    }


}
