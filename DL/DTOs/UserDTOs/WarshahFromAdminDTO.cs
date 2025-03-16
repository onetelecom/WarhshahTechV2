using DL.DTOs.SharedDTO;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.DTOs.UserDTOs
{
    public class WarshahFromAdminDTO:BaseDomainDTO
    {
        [Required]
        public string WarshahNameAr { get; set; }
        public string WarshahNameEn { get; set; }
        public decimal Long { get; set; }
        public decimal Lat { get; set; }
        public int? ParentWarshahId { get; set; }

        public string LandLineNum { get; set; }
        [Required]
        public int CountryId { get; set; }
        [Required]
        public int RegionId { get; set; }
        [Required]
        public int CityId { get; set; }

        public string Street { get; set; }
        [Required]
        public string Distrect { get; set; }
        public int UnitNum { get; set; }
        public int PostalCode { get; set; }

        public IFormFile WarshahLogo { get; set; }
        public string TaxNumber { get; set; }

        public string Terms { get; set; }
        public string CR { get; set; }
        public string WebSite { get; set; }
        public string SalesCode { get; set; }
        public string WarshahIdentifier { get; set; }
        public int SubscribtionId { get; set; }
        public bool Coupon { get; set; }
        public string CompanyName { get; set; }
  

        public string CommerialRegisterar { get; set; }

        public string Address { get; set; }
        public bool IsCompany { get; set; } = false;

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

        [Required]

        public string Password { get; set; }
        public int RoleId { get; set; }

        // Payment Props 
        public int MonthCount { get; set; }
        public decimal Price { get; set; }
    }
}
