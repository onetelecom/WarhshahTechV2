using DL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Configuration
{

    class RequestTypeConfiguration : IEntityTypeConfiguration<RequestType>
    {

        public void Configure(EntityTypeBuilder<RequestType> builder)
        {
            builder.ToTable("RequestTypes");



            builder.HasData
            (

                new RequestType
                {
                    Id = 1,
                    ActionCode = "salesandsubscriptions",
                    Namear = " مبيعات واشتراكات ",
                    NameEn = "Sales and subscriptions"


                },

                  new RequestType
                  {
                      Id = 2,
                      ActionCode = "techsupport",
                      Namear = "دعم فني",
                      NameEn = "Technical Support",

                  },

                   new RequestType
                   {
                       Id = 3,
                       ActionCode = "generalInquire",
                       Namear = "إستفسار عام",
                       NameEn = "General Inquire",


                   }

            );
        }
    }

}
