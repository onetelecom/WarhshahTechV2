using AutoMapper;
using BL.Infrastructure;
using DL.DTOs;
using DL.Entities;
using DocumentFormat.OpenXml.Wordprocessing;
using HELPER;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WarshahTechV2.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyPolicy")]


    public class NotificationSettingController : ControllerBase
    {

        // take instance from IUnitOfWork
        private readonly IUnitOfWork _uow;

        // take instance from IMapper
        private readonly IMapper _mapper;

        // Constractor for controller 
        public NotificationSettingController(IUnitOfWork uow, IMapper mapper)
        {
            _mapper = mapper;
            _uow = uow;
        }


        #region NotificationSettingCRUD

        //Create NotificationSetting
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]
        [HttpPost, Route("CreateNotificationSetting")]
        public IActionResult CreateNotificationSetting(EditNotificationSettingDTO notificationSetting)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var notification = _mapper.Map<DL.Entities.NotificationSetting>(notificationSetting);
                    _uow.NotificationSettingRepository.Update(notification);
                    _uow.Save();
                    return Ok(notification);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid RegionName");
        }




        //Create NotificationName
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]
        [HttpPost, Route("CreateNotificationName")]
        public IActionResult CreateNotificationName(NameNotificationDTO notificationDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var notification = _mapper.Map<DL.Entities.NameNotification>(notificationDTO);
                    notification.IsDeleted = false;
                    _uow.NameNotificationRepository.Add(notification);


                    _uow.Save();
                    return Ok(notification);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid RegionName");
        }



        // Get All NotificationName

        [HttpGet, Route("GetAllNotificationName")]
        public IActionResult GetAllNotificationName()
        {
            var notifications = _uow.NameNotificationRepository.GetAll().ToHashSet();
            return Ok(notifications);
        }
       
        [HttpGet, Route("GetNotificationActiveByWarshahId")]
        public IActionResult GetNotificationSettingByWarshahId(int warshahid)
        {
            var notificationactive = _uow.NotificationSettingRepository.GetMany(m => m.WarshahId == warshahid);

            return Ok(notificationactive);
        }





        [HttpGet, Route("GetAllNotificationActiveByWarshahId")]
        public IActionResult GetAllNotificationActiveByWarshahId(int warshahid)

        {

           var notifications = _uow.NotificationSettingRepository.GetMany(a=>a.WarshahId==warshahid).Include(a=>a.NameNotification).ToHashSet();
            
            return Ok(notifications);
        }


        [HttpGet]

        public async Task<ActionResult<List<NotificationSetting>>> Getpagining()
        {
            var noti = await   _uow.NotificationSettingRepository.GetAll().ToListAsync();
            return Ok(noti);
        }


        #endregion
    }
}
