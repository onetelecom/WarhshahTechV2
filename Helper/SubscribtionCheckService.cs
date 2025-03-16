using BL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Helper
{
    public interface ISubscribtionCheckService
    {
        public bool CheckIfWarshahSubscribtionStillValid(int? WarshahId);
        public bool CheckIfWarshahSubscribtionEndTimeStillVaild(int? WarshahId);

    }
    public class SubscribtionCheckService : ISubscribtionCheckService
    {
       
        private readonly IUnitOfWork _unitOfWork;



        public SubscribtionCheckService(IUnitOfWork unitOfWork)
        {

            _unitOfWork = unitOfWork;
     

        }

        public bool CheckIfWarshahSubscribtionEndTimeStillVaild(int? WarshahId)
        {
            var Duration = _unitOfWork.DurationRepository.GetMany(x => x.WarshahId == WarshahId).FirstOrDefault();
            if (DateTime.Compare(Duration.EndDate,DateTime.Now)>0)
            {
                return true;
            }
            return false;
        }

        public bool CheckIfWarshahSubscribtionStillValid(int? WarshahId)
        {
            var Duration = _unitOfWork.DurationRepository.GetMany(x => x.WarshahId==WarshahId).FirstOrDefault();
            return Duration.IsActive;
        }
    }
}
