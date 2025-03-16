using BL.Infrastructure;
using DL.DBContext;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Repositories
{
    

    public interface ICategorySparePartsRepository
    { }
    public class CategorySparePartsRepository : Repository<CategorySpareParts>, ICategorySparePartsRepository
    {
        public CategorySparePartsRepository(AppDBContext ctx) : base(ctx)
        { }


    }
}
