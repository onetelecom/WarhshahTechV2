using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Entities
{
    public class GlobalSetting:BaseDomain
    {
        public string Terms { get; set; }
        public string Long { get; set; }
        public string Lat { get; set; }
        public string Email { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string WebSite { get; set; }
        public string PrivacyPolicy { get; set; }
        public string Logo { get; set; }
    }
}
