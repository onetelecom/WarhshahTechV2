using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.DTOs.UserDTOs
{
    public class UserBackofficeDTO
    {

     
        public string FirstName { get; set; }
     
        public string SocondName { get; set; }
       
        public string ThirdName { get; set; }
     
        public string FamilyName { get; set; }
        //Uniqe Properties
     
        public string Phone { get; set; }
      
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    

        public string CivilId { get; set; }
    

        public string PassportNumber { get; set; }
      

        public string Password { get; set; }
        public bool IsCompany { get; set; }
        public int? CompanyId { get; set; }

        public int? AccountTypeId { get; set; }
        public bool IsActive { get; set; }
    }
}
