using System;
using System.Collections.Generic;

#nullable disable

namespace WarshahTechV2.Models
{
    public partial class Country
    {
        public string CountryCode { get; set; }
        public string CountryEnName { get; set; }
        public string CountryArName { get; set; }
        public string CountryEnNationality { get; set; }
        public string CountryArNationality { get; set; }
        public decimal? Sort { get; set; }
    }
}
