using System;
using System.Collections.Generic;

#nullable disable

namespace WarshahTechV2.Models
{
    public partial class Warshah
    {
        public Warshah()
        {
            OilTypes = new HashSet<OilType>();
            WarshahCustomers = new HashSet<WarshahCustomer>();
        }

        public Guid WarshahId { get; set; }
        public string WarshahName { get; set; }
        public string WarshahNameAr { get; set; }
        public bool? IsBranch { get; set; }
        public Guid? ParentWarshahId { get; set; }
        public int WarshahSequence { get; set; }
        public string BranchFullId { get; set; }
        public string City { get; set; }
        public string PhoneNo { get; set; }
        public string Address { get; set; }
        public int? UnitNo { get; set; }
        public bool? IsActive { get; set; }
        public decimal Vat { get; set; }
        public Guid? WarshahThemeId { get; set; }
        public string TaxNumber { get; set; }
        public string Cr { get; set; }
        public string Conditions { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public int? RegionId { get; set; }
        public int? CountryId { get; set; }
        public bool Hidden { get; set; }
        public string Distrect { get; set; }
        public string PostalCode { get; set; }
        public string Street { get; set; }
        public string Commercialregistercopy { get; set; }
        public string Taxcertificatecopy { get; set; }
        public string Ref { get; set; }

        public virtual WarshahTheme WarshahTheme { get; set; }
        public virtual ICollection<OilType> OilTypes { get; set; }
        public virtual ICollection<WarshahCustomer> WarshahCustomers { get; set; }
    }
}
