using DL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.DTOs.SuppliersDTOs
{
    public class EditSupplierDTO : BaseDomain
    {

        [Required]
        public int Id { get; set; }

        [Required]
        public string SupplierNameAr { get; set; }
        public string SupplierNameEn { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int? CountryId { get; set; }
        public int? RegionId { get; set; }
        public int? CityId { get; set; }
        public string Distrect { get; set; }
        public string Street { get; set; }
        public string TaxNumber { get; set; }
        public string CR { get; set; }

        [Required]
        public int? WarshahId { get; set; }






    }
}
