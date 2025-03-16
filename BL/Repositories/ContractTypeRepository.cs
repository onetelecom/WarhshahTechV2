using BL.Infrastructure;
using DL.DBContext;
using DL.Entities.HR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Repositories
{
        public interface IContractTypeRepository
        { }
        public class ContractTypeRepository : Repository<ContractType>, IContractTypeRepository
        {
            public ContractTypeRepository(AppDBContext ctx) : base(ctx)
            { }


        }
    }