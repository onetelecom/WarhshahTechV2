using BL.Infrastructure;
using DL.DTOs.AddressDTOs;
using DL.Entities;
using DocumentFormat.OpenXml.Wordprocessing;
using HELPER;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace WarshahTechV2.Controllers
{
    [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IUnitOfWork uow;

        public AppointmentController(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        [HttpPost,Route("AddAppointment")]
        public IActionResult AddAppointment(Appointment appointment)
        {
            if (CheckAppointment(appointment.ReservationOn,appointment.WarshahId))
            {
                return BadRequest(new { Reservation = "Reservation Taken" });
            }
            uow.AppointmentRepository.Add(appointment);
            uow.Save();
            return Ok(appointment);
        }
        [HttpGet,Route("GetAppointment")]
        public IActionResult GetAppointment(int warshahId,bool NextDay)
        {
            if (NextDay)
            {
                var Alla = uow.AppointmentRepository.GetMany(a=>a.ReservationOn>DateTime.Now&&a.WarshahId==warshahId);
                return Ok(Alla);
            }
            var All = uow.AppointmentRepository.GetMany(a =>  a.WarshahId == warshahId);
            return Ok(All);
        }
        [HttpPost, Route("DoneAppointment")]
        public IActionResult DoneAppointment(int AppointmentId)
        {
            var Appo = uow.AppointmentRepository.GetById(AppointmentId);
            Appo.IsDone= true;
            uow.AppointmentRepository.Update(Appo);
            uow.Save();
            return Ok(Appo);
        }
        [HttpGet,Route("GetClientRequestType")]
         public IActionResult GetClientRequestType()
        {
            List<ClientRequestTypeDto> clientRequestTypeDtos = new List<ClientRequestTypeDto>();
            clientRequestTypeDtos.Add(new ClientRequestTypeDto
            {
                Code=1,
                NameAr="زياره فنيه",
                NameEn= "Technical Visit"
            });
            clientRequestTypeDtos.Add(new ClientRequestTypeDto
            {
                Code=2,
                NameAr = "سحب سياره",
                NameEn = "Car Wench"
            });
            return Ok(clientRequestTypeDtos);
        }
        [HttpGet,Route("SetRequestIsGoing")]
        public IActionResult SetRequestIsGoing(int requestId)
        {
       var d= uow.ClientRequestRepository.GetById(requestId);
        d.IsGoing = true;
            d.IsDone = false;
            uow.ClientRequestRepository.Update(d); uow.Save();
            return Ok(d);
        }
        [HttpGet, Route("SetRequestIsDone")]
        public IActionResult SetRequestIsDone(int requestId)
        {
            var d = uow.ClientRequestRepository.GetById(requestId);
            d.IsDone = true;
            d.IsGoing = false;
            uow.ClientRequestRepository.Update(d); uow.Save();
            return Ok(d);
        }

        [HttpGet,Route("GetRequestsByWarshah")]
        public IActionResult GetRequestsByWarshah(int warshahId , int pagenumber, int pagecount)
        {
            var request = uow.ClientRequestRepository.GetMany(a => a.WarshahId == warshahId).ToHashSet();
            return Ok(new { pagenumber = pagenumber, pagesize = pagecount, totalrow = request.Count(), Listinvoice = request.ToPagedList(pagenumber, pagecount) });

        }



        [HttpGet, Route("GetAllRequestsByWarshah")]
        public IActionResult GetAllRequestsByWarshah(int pagenumber , int pagecount)
        {
            var request = uow.ClientRequestRepository.GetAll().ToHashSet();
            return Ok(new { pagenumber = pagenumber, pagesize = pagecount, totalrow = request.Count(), Listinvoice = request.ToPagedList(pagenumber, pagecount) });
        }

            [HttpPost,Route("AddClientRequest")]
        public IActionResult AddClientRequest(ClientRequest clientRequest)
        {
            clientRequest.CreatedOn= DateTime.Now;
           uow.ClientRequestRepository.Add(clientRequest);
            uow.Save();
            return Ok(clientRequest);
        }
        [NonAction]
        public bool CheckAppointment(DateTime dateTime,int warshahId)
        {
            var IsThere = uow.AppointmentRepository.GetMany(a => a.ReservationOn == dateTime && a.WarshahId == warshahId).ToHashSet() ;
            if (IsThere.Count!=0)
            {
                return true;
            }
            return false;

        }
    }
}
