//using DL.Entities;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace DL.MailModels
//{
//   public class PersonModel
//    {
//        /// <summary>
//        /// User Nae
//        /// </summary>
//        /// 
//        public int Id { get; set; }
//        [Required]
//        [MinLength(1), MaxLength(50)]
//        public string FirstNameAr { get; set; }

//        [Required]
//        [MinLength(1), MaxLength(50)]
//        public string SecondNameAr { get; set; }

//        [Required]
//        [MinLength(1), MaxLength(50)]
//        public string ThirdNameAr { get; set; }


//        [Required]
//        [MinLength(1), MaxLength(50)]
//        public string FamilyNameAr { get; set; }


//        [Required]
//        [MinLength(1), MaxLength(50)]
//        public string FirstNameEN { get; set; }

//        [Required]
//        [MinLength(1), MaxLength(50)]
//        public string SecondNameEN { get; set; }

//        [Required]
//        [MinLength(1), MaxLength(50)]
//        public string ThirdNameEN { get; set; }


//        [Required]
//        [MinLength(1), MaxLength(50)]
//        public string FamilyNameEN { get; set; }




//        [MinLength(1), MaxLength(50)]
//        public string civilID { get; set; }



//        [Required]
//        [MinLength(1), MaxLength(50)]
//        public string passportNum { get; set; }


//        [Required]
//        [MinLength(1), MaxLength(50)]
//        public string phoneNum { get; set; }


//        [Display(Name = "Email address")]
//        [Required(ErrorMessage = "The email address is required")]
//        [EmailAddress(ErrorMessage = "Invalid Email Address")]
//        public string email { get; set; }



//        [Required]
//        [DataType(DataType.Date)]
//        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
//        public string dateOfBirth { get; set; }


//        [Required]
//        [DataType(DataType.Date)]
//        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
//        public string residencyStartDate { get; set; }


//        [Required]
//        [DataType(DataType.Date)]
//        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
//        public string residencyEndDate { get; set; }


//        [Required]
//        [MinLength(1), MaxLength(50)]
//        public string jada { get; set; }


//        [Required]
//        [MinLength(1), MaxLength(50)]
//        public string block { get; set; }


//        [Required]
//        [MinLength(1), MaxLength(50)]
//        public string building { get; set; }


//        [Required]
//        [MinLength(1), MaxLength(50)]
//        public string flat { get; set; }


//        [Required]
//        [MinLength(1), MaxLength(50)]
//        public string apartmentNum { get; set; }




     


       




      



//        public int? UserId { get; set; }
//        public User User { get; set; }



      
//        public virtual List<Casher> Casher { get; set; }
//        public virtual List<Certificate> Certificate { get; set; }
//        public virtual List<Appointment> personAppoinment { get; set; }


//    }
//}
