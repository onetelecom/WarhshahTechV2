using AutoMapper;
using BL.Infrastructure;
using DL.DTOs;
using HELPER;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;

namespace WarshahTechV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyPolicy")]


    public class NotificationRepairOrderController : ControllerBase
    {

        // take instance from IUnitOfWork
        private readonly IUnitOfWork _uow;

        // take instance from IMapper
        private readonly IMapper _mapper;

        // Constractor for controller 
        public NotificationRepairOrderController(IUnitOfWork uow, IMapper mapper)
        {
            _mapper = mapper;
            _uow = uow;
        }


        #region NotificationRepairOrderCRUD

        //Create NotificationRepairOrder
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]
        [HttpPost, Route("CreateNotificationRepairOrder")]
        public IActionResult CreateNotificationRepairOrder(EditNotificationRepairOrderDTO notificationSetting)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var notification = _mapper.Map<DL.Entities.NotificationRepairOrder>(notificationSetting);
                    _uow.NotificationRepairOrderRepository.Update(notification);
                    _uow.Save();
                    return Ok(notification);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid Notification");
        }




        //Create NotificationAddingRepair
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]
        [HttpPost, Route("CreateNotificationToRepairOrder")]
        public IActionResult CreateNotificationToRepairOrder(NotificationRepairOrderAddingDTO notificationDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var notification = _mapper.Map<DL.Entities.NotificationRepairOrderAdding>(notificationDTO);
                    notification.IsDeleted = false;
                    _uow.NotificationRepairOrderAddingRepository.Add(notification);


                    _uow.Save();
                    return Ok(notification);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid Notification");
        }



        // Get All NotificationName

        [HttpGet, Route("GetAllNotificationRepair")]
        public IActionResult GetAllNotificationRepair()
        {
            var notifications = _uow.NotificationRepairOrderAddingRepository.GetAll().ToHashSet();
            return Ok(notifications);
        }

        [HttpGet, Route("GetNotificationRepairOrderActiveByWarshahId")]
        public IActionResult GetNotificationRepairOrderActiveByWarshahId(int warshahid)
        {
            var notificationactive = _uow.NotificationRepairOrderRepository.GetMany(m => m.WarshahId == warshahid).Include(a => a.NameNotification).ToHashSet(); 

            return Ok(notificationactive);
        }





        #endregion
    }
}
