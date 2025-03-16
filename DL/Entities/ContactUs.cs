using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Entities
{
    public class ContactUs:BaseDomain
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Service { get; set; }
        public string Subject { get; set; }
        public string Phone { get; set; }
    }
}
