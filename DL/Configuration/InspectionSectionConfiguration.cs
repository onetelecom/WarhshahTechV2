using DL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Configuration
{


    class InspectionSectionConfiguration : IEntityTypeConfiguration<InspectionSection>
    {

        public void Configure(EntityTypeBuilder<InspectionSection> builder)
        {
            builder.ToTable("InspectionSections");



            builder.HasData
            (

                new InspectionSection
                {
                    Id = 1,
                    SectionNameAr = " الماكينة",
                    SectionNameEn = " Machine ",
                    InspectionTemplateId = 1,
                    IsCommon = true
                },
                new InspectionSection
                {
                    Id = 2,
                    SectionNameAr = "الجيربوكس",
                    SectionNameEn = " Limebox ",
                    InspectionTemplateId = 1,
                    IsCommon = true
                },
                 new InspectionSection
                 {
                     Id = 3,
                     SectionNameAr = "نظام الكهرباء",
                     SectionNameEn = " Electricity system ",
                     InspectionTemplateId = 1,
                     IsCommon = true
                 },
                 new InspectionSection
                 {
                     Id = 4,
                     SectionNameAr = "السوائل",
                     SectionNameEn = " Fluids ",
                     InspectionTemplateId = 1,
                     IsCommon = true
                 },
                 new InspectionSection
                 {
                     Id = 5,
                     SectionNameAr = "أسفل السيارة",
                     SectionNameEn = " Under the Car ",
                     InspectionTemplateId = 1,
                     IsCommon = true
                 },
                new InspectionSection
                {
                    Id = 6,
                    SectionNameAr = "نظام الفرامل",
                    SectionNameEn = " Brake system ",
                    InspectionTemplateId = 1,
                    IsCommon = true
                },
                 new InspectionSection
                 {
                     Id = 7,
                     SectionNameAr = "الدفرنس / الكورونا",
                     SectionNameEn = " Aldverse / Corona ",
                     InspectionTemplateId = 1,
                     IsCommon = true
                 },
                 new InspectionSection
                 {
                     Id = 8,
                     SectionNameAr = "القسم الداخلي",
                     SectionNameEn = " Internal Section ",
                     InspectionTemplateId = 1,
                     IsCommon = true
                 },
                 new InspectionSection
                 {
                     Id = 9,
                     SectionNameAr = "الهيكل الخارجي",
                     SectionNameEn = " Exterior structure ",
                     InspectionTemplateId = 1,
                     IsCommon = true
                 },






                   new InspectionSection
                   {
                       Id = 10,
                       SectionNameAr = " الماكينة",
                       SectionNameEn = " Machine ",
                       InspectionTemplateId = 2,
                       IsCommon = true
                   },
                new InspectionSection
                {
                    Id = 11,
                    SectionNameAr = "الجيربوكس",
                    SectionNameEn = " Limebox ",
                    InspectionTemplateId = 2,
                    IsCommon = true
                },
                 new InspectionSection
                 {
                     Id = 12,
                     SectionNameAr = "نظام الكهرباء",
                     SectionNameEn = " Electricity system ",
                     InspectionTemplateId = 2,
                     IsCommon = true
                 },
                 new InspectionSection
                 {
                     Id = 13,
                     SectionNameAr = "السوائل",
                     SectionNameEn = " Fluids ",
                     InspectionTemplateId = 2,
                     IsCommon = true
                 },
                 new InspectionSection
                 {
                     Id = 14,
                     SectionNameAr = "أسفل السيارة",
                     SectionNameEn = " Under the Car ",
                     InspectionTemplateId = 2,
                     IsCommon = true
                 },
                new InspectionSection
                {
                    Id = 15,
                    SectionNameAr = "نظام الفرامل",
                    SectionNameEn = " Brake system ",
                    InspectionTemplateId = 2,
                    IsCommon = true
                },
                 new InspectionSection
                 {
                     Id = 16,
                     SectionNameAr = "الدفرنس / الكورونا",
                     SectionNameEn = " Aldverse / Corona ",
                     InspectionTemplateId = 2,
                     IsCommon = true
                 },
                 new InspectionSection
                 {
                     Id = 17,
                     SectionNameAr = "القسم الداخلي",
                     SectionNameEn = " Internal Section ",
                     InspectionTemplateId = 2,
                     IsCommon = true
                 },
                 new InspectionSection
                 {
                     Id = 18,
                     SectionNameAr = "الهيكل الخارجي",
                     SectionNameEn = " Exterior structure ",
                     InspectionTemplateId = 2,
                     IsCommon = true
                 }


            );
        }
    }


}
