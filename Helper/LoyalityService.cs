using BL.Infrastructure;
using DL.DTOs.InvoiceDTOs;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Helper
{
    public interface ILoyalityService
    {
        bool SetLoyalityPoints(int WarshahId,int CarOwnerId , decimal PointsAmount);
    }
    public class LoyalityService : ILoyalityService
    {
        private readonly IUnitOfWork _uow;

        public LoyalityService(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public bool SetLoyalityPoints(int WarshahId, int CarOwnerId, decimal InvoiceAmmount)
        {
            var LoyalitySetting = _uow.LoyalitySettingRepository.GetMany(a=>a.WarshahId == WarshahId).FirstOrDefault();
            var ExitingPoints = _uow.LoyalityPointRepository.GetMany(a=>a.WarshahId==WarshahId&&a.CarOwnerId==CarOwnerId).FirstOrDefault();
            if (LoyalitySetting != null)
            {
                if (ExitingPoints == null)
                {
                    var NewLoyalityPoints = new LoyalityPoint();
                    NewLoyalityPoints.WarshahId = WarshahId;
                    NewLoyalityPoints.CarOwnerId = CarOwnerId;
                    NewLoyalityPoints.Points = InvoiceAmmount / LoyalitySetting.LoyalityPointsPerCurrancy;
                    _uow.LoyalityPointRepository.Add(NewLoyalityPoints);
                    _uow.Save();
                    return true;
                }
                ExitingPoints.Points += (InvoiceAmmount / LoyalitySetting.LoyalityPointsPerCurrancy);
                _uow.LoyalityPointRepository.Update(ExitingPoints);
                _uow.Save();

            }
           

            return true;
                
        }
    }
}
