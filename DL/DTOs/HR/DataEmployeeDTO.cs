using DL.DTOs.SharedDTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.DTOs.HR
{
    public class DataEmployeeDTO : BaseDomainDTO
    {


        [Required]
        public int WarshahId { get; set; }


       

        public int UserWarshahCode { get; set; }

        [Required]
        public string EmployeeNameAr { get; set; }

        public string EmployeeNameEn { get; set; }

        public int? NationalityId { get; set; }



        [Required]
        public string MobileNo { get; set; }

        public string Email { get; set; }

        public int? GenderId { get; set; }
    

        public DateTime BirthDate { get; set; }

        public int? MaritalStatusId { get; set; }



        public int? ChildrerNumber { get; set; }

        public string SocialSecurity { get; set; }

        public string MedicalInsurance { get; set; }

        public string IdCard { get; set; }

        public DateTime? IdCardStart { get; set; }

        public DateTime? IdCardEnd { get; set; }

        public string PassportNumber { get; set; }

        public DateTime? PassportIssueDate { get; set; }

        public DateTime? PassportEndDate { get; set; }


        public int? CountryId { get; set; }


        public int? RegionId { get; set; }


        public int? CityId { get; set; }
  
        public string Address { get; set; }

        public int? StatusEmploymentId { get; set; }

        public int? EmployeeShiftId { get; set; }



        public int? RoleId { get; set; }

 

        public DateTime? HiringDate { get; set; }

        public DateTime? ContractEndDate { get; set; }

        public int? WorkingDays { get; set; }

        public int? ContractTypeId { get; set; }



        public decimal? BasicSalary { get; set; }

        public decimal? Transportation { get; set; }

        public decimal? HouseAllowances { get; set; }

        public decimal? Absence { get; set; }

        public decimal? Installment { get; set; }

        public string AttendanceCode { get; set; }

        public bool? ExcludedAttendance { get; set; }

        public int? JobTitleId { get; set; }

        public string JobName { get; set; }

    }
}
