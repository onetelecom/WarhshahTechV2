using BL.Infrastructure;
using DL.DBContext;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Repositories
{
    
    public interface ISubCategoryPartsRepository
    { }
    public class SubCategoryPartsRepository : Repository<SubCategoryParts>, ISubCategoryPartsRepository
    {
        public SubCategoryPartsRepository(AppDBContext ctx) : base(ctx)
        { }


    }
}
