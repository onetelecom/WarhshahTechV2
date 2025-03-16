using DL.DTOs.SharedDTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.DTOs.UserDTOs
{
   public class EditUserDTO:BaseDomainDTO
    {
        public string CompanyName { get; set; }
        [MinLength(1), MaxLength(15)]
        public string TaxNumber { get; set; }
    
        public string CommerialRegisterar { get; set; }
        public string Companycr { get; set; }

        public string Address { get; set; }
        public bool IsCompany { get; set; }

        public int? PostCodeCompany { get; set; }

        public int? RegionId { get; set; }
       

        public int? CityId { get; set; }
       

        public int? UnitNum { get; set; }
        public int Id { get; set; }
        /// <summary>
        /// User Nae
        /// </summary>
        [MinLength(1), MaxLength(50)]
        public string FirstName { get; set; }
        [MinLength(1), MaxLength(50)]
        public string LastName { get; set; }


        //Uniqe Properties
        [Required]
        public string Phone { get; set; }
    
        public string CivilId { get; set; }
        public string Email { get; set; }
        public bool IsPhoneConfirmed { get; set; }

        public int? WarshahId { get; set; }
      
     
        public int RoleId { get; set; }
     

    }
}
