using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL.Entities
{
  public  class User: BaseDomain
    {

        /// <summary>
        /// User Nae
        /// </summary>
        /// 
        public string CompanyName { get; set; }

        [MinLength(1), MaxLength(15)]
        public string TaxNumber { get; set; }

        public string CommerialRegisterar { get; set; }
        
        public string Address { get; set; }
        public bool IsCompany { get; set; }

        public int? PostCodeCompany { get; set; }

        public int? RegionId { get; set; }
        public Region Region { get; set; }

        public int? CityId { get; set; }
        public City City { get; set; }

        public int? UnitNum { get; set; }


        public string FirstName { get; set; }
       
        public string LastName { get; set; }


        //Uniqe Properties
        [Required]
        public string Phone { get; set; }
        public string CivilId { get; set; }
        public string Email { get; set; }
        public bool IsPhoneConfirmed { get; set; }

        public int? WarshahId { get; set; }
        public Warshah Warshah { get; set; }
        [Required]

        public string Password { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public string IdImage { get; set; }





    }
}
