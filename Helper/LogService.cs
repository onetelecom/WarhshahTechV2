using BL.Infrastructure;
using DL.Entities;
using HELPER.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace HELPER
{
    public interface ILog 
    {
        public void LogErorr(Log log);
        public void LogAudit(Log log);
        public IEnumerable<Log> GetAllErorrs();
        public IEnumerable<Log> GetAllAudits();
        public string GetCountryName(string Ip);
    }
    public class LogService:ILog
    {
        private IHttpContextAccessor httpConnection;
        private readonly IUnitOfWork _unitOfWork;
       


        public LogService(  IUnitOfWork unitOfWork, IHttpContextAccessor httpConnection)
        {
          
            _unitOfWork = unitOfWork;
            this.httpConnection = httpConnection;
           
        }

        public string GetCountryName(string Ip)
        {
            
            //string smsURL = "https://ipinfo.io/"+ Ip + "?token=b1b1781e065b4f";
            string smsURL = "https://ipinfo.io/105.207.71.214?token=b1b1781e065b4f";

            System.Net.WebClient webClient = new System.Net.WebClient();
            string result = webClient.DownloadString(smsURL);
            JObject json = JObject.Parse(result);
            return result;
        }

        public void LogAudit(Log log)
        {
            
            var id =httpConnection.HttpContext.User.Claims.Where(a => a.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value;
            var UserId = 0;
            if (id!=null)
            {
                UserId = int.Parse(id);
            }
            log.UserId = UserId;
            string ipAddress = httpConnection.HttpContext.Connection.RemoteIpAddress?.ToString();
           // var CountryName = GetCountryName(ipAddress);
            log.LogType = LogEnum.Audit.ToString();
            log.CreatedAt = DateTime.Now;
            //log.CountryName = CountryName;
            log.Ip = ipAddress;


            _unitOfWork.LogRepository.Add(log);
            _unitOfWork.Save();
        }

        public void LogErorr(Log log)
        {
            var id = httpConnection.HttpContext.User.Claims.Where(a => a.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value;
            var UserId = 0;
            if (id != null)
            {
                UserId = int.Parse(id);
            }
            log.UserId = UserId;
            string ipAddress = httpConnection.HttpContext.Connection.RemoteIpAddress?.ToString();
            var CountryName = GetCountryName(ipAddress);
            log.LogType = LogEnum.Erorr.ToString();
            log.CreatedAt = DateTime.Now;
            log.CountryName = CountryName;
            log.Ip = ipAddress;

            _unitOfWork.LogRepository.Add(log);
            _unitOfWork.Save();

        }

        public IEnumerable<Log> GetAllErorrs()
        {
            var All = _unitOfWork.LogRepository.GetMany(a => a.LogType == LogEnum.Erorr.ToString()).ToHashSet();
            return All;
        }

        public IEnumerable<Log> GetAllAudits()
        {
            var All = _unitOfWork.LogRepository.GetMany(a => a.LogType == LogEnum.Audit.ToString()).ToHashSet();
            return All;
        }
    }
}
