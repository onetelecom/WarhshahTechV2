using DL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Configuration
{

    class WorkTypeConfiguration : IEntityTypeConfiguration<WorkType>
    {

        public void Configure(EntityTypeBuilder<WorkType> builder)
        {
            builder.ToTable("WorkTypes");



            builder.HasData
            (

                new WorkType
                {
                    Id = 1,
                    ActionCode = "WarshaOwner",
                    NameAr = "  صاحب ورشة  ",
                    NameEn = " Warshah Owner"


                },

                  new WorkType
                  {
                      Id = 2,
                      ActionCode = "CarOwner",
                      NameAr = " مالك سيارة",
                      NameEn = " Car Owner",

                  }

            );
        }
    }

}
