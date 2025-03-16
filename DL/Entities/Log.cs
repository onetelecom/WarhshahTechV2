using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL.Entities
{
  public  class Log
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }
        public string Ip { get; set; }
        public string CountryName { get; set; }
        public string Message { get; set; }
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
        
        public string LogType { get; set; }


    }
}
