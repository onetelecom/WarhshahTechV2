using System;
using System.Collections.Generic;

#nullable disable

namespace WarshahTechV2.Models
{
    public partial class UnderProcess
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserPhone { get; set; }
        public string UserNationlity { get; set; }
        public int UserIdIqama { get; set; }
        public Guid WarshahId { get; set; }
        public string WharshahName { get; set; }
        public string WarshahCity { get; set; }
        public string WarshahAddress { get; set; }
        public string WarshahPhone { get; set; }
        public string PaymentPackage { get; set; }
        public int PaymentMonthsCount { get; set; }
        public int PackagePrice { get; set; }
        public int Total { get; set; }
        public DateTime PyamentDate { get; set; }
        public string WarshahLong { get; set; }
        public string WharshahLat { get; set; }
        public int? Numofbranches { get; set; }
        public bool Store { get; set; }
        public bool Api { get; set; }
        public bool Tecapp { get; set; }
        public bool Warshaadmapp { get; set; }
        public int? CountryId { get; set; }
        public int? RegionId { get; set; }
        public string Ref { get; set; }
    }
}
