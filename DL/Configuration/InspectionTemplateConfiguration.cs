using DL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Configuration
{
    

    class InspectionTemplateConfiguration : IEntityTypeConfiguration<InspectionTemplate>
    {

        public void Configure(EntityTypeBuilder<InspectionTemplate> builder)
        {
            builder.ToTable("InspectionTemplates");



            builder.HasData
            (

                new InspectionTemplate
                {
                    Id = 1,
                    InspectionNameAr = "مجاني",
                    InspectionNameEn = " Free Inspection ",
                    Price = 0,
                    Hours = 1 ,
                    IsCommon = true
                   
                    

                },
                 new InspectionTemplate
                 {
                     Id = 2,
                     InspectionNameAr = "فحص متقدم",
                     InspectionNameEn = "Advance Inspection ",
                     Price = 100,
                     Hours = 1,
                     IsCommon = true


                 }


            );
        }
    }

}
