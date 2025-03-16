using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Entities
{
  public  class AuthrizedPerson:BaseDomain
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
