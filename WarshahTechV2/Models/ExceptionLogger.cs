using System;
using System.Collections.Generic;

#nullable disable

namespace WarshahTechV2.Models
{
    public partial class ExceptionLogger
    {
        public int ExceptionLoggerId { get; set; }
        public string ExceptionMessage { get; set; }
        public string ExceptionStackTrack { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public DateTime? ExceptionLogTime { get; set; }
    }
}
