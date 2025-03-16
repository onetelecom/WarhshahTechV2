using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.Entities
{
    public class RegisterForm : BaseDomain
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }


        //Uniqe Properties
        [Required]
        public string Phone { get; set; }

        public string WarshahNameAr { get; set; }
        public string WarshahNameEn { get; set; }
     
        public string Distrect { get; set; }

        public string TaxNumber { get; set; }
        public string CR { get; set; }


    }
}
