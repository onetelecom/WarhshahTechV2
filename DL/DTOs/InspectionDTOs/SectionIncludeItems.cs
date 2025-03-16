using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DL.DTOs.InspectionDTOs
{
    public class SectionIncludeItems
    {
    public string SectionName { get; set; }

     public string SectionNameEn { get; set; }

        public bool IsCommon { get; set; }

        public bool IsActive { get; set; }
        public int SectionID { get; set; }
    public HashSet <InspectionItem> Items { get; set; }

}
}
