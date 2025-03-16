using DL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Configuration
{
 




            class WarshahFixedTypeConfiguration : IEntityTypeConfiguration<WarshahFixedType>
    {


        public void Configure(EntityTypeBuilder<WarshahFixedType> builder)
        {
            builder.ToTable("WarshahFixedTypes");



            builder.HasData
            (

                new WarshahFixedType
                {
                    Id = 1,
                    NameType = "الميكانيكا و الإصلاحات العامة"


                },

                 new WarshahFixedType
                 {
                     Id = 2,
                     NameType = "كهرباء و تكييف"


                 },

                  new WarshahFixedType
                  {
                      Id = 3,
                      NameType = "الكترونيات السيارات"


                  },

                   new WarshahFixedType
                   {
                       Id = 4,
                       NameType = "سمكرة وبوية ودهانات"


                   },
                   new WarshahFixedType
                   {
                       Id = 5,
                       NameType = "تغير الكفرات والاطارات بنشر"


                   },

                   new WarshahFixedType
                   {
                       Id = 6,
                       NameType = "غيار الزيوت والتشحيم"


                   },

                   new WarshahFixedType
                   {
                       Id = 7,
                       NameType = "الخراطة والتصفية"


                   },

                    new WarshahFixedType
                    {
                        Id = 8,
                        NameType = "غسيل السيارات"


                    },

                     new WarshahFixedType
                     {
                         Id = 9,
                         NameType = "التظليل والتلميع و التنظيف الداخلي"


                     },

                     new WarshahFixedType
                     {
                         Id = 10,
                         NameType = "ورشة شركات التأمين"


                     },

                     new WarshahFixedType
                     {
                         Id = 11,
                         NameType = "ورشة مركز تأجير سيارات"


                     }


            );
        }
    }
}
