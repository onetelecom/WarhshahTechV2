using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.Entities
{
   public class BaseDomain
    {
        public int Id { get; set; }
        /// <summary>
        /// The Date Created The Ops On
        /// </summary>
        public string Describtion { get; set; }
        /// 

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreatedOn { get; set; }
        /// <summary>
        /// Is That Entity Active Variable
        /// </summary>
        public bool IsActive { get; set; } = true;
        /// <summary>
        /// UpdatedOn Date
        /// </summary>    

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? UpdatedOn { get; set; }
        /// <summary>
        /// CreatedBy User
        /// </summary>
        public int CreatedBy { get; set; }
        /// <summary>
        /// UpdatedBy User
        /// </summary>
        public int? UpdatedBy { get; set; }

        public bool IsDeleted { get; set; } = false;
       
    }
}
