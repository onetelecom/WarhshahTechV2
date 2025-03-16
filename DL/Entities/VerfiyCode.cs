using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL.Entities
{
    public partial class VerfiyCode
    {
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int VirfeyCode { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
    }
}
