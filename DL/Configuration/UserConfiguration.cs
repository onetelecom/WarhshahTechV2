using DL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL.Configuration
{
    class UserConfiguration:IEntityTypeConfiguration<User>
    {
     
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
             
          
            //builder.Property(s => s.NameArabic)
            //    .IsRequired(false);
            //builder.Property(s => s.IsActive)
            //    .HasDefaultValue(true);
            //builder.Property(s => s.IsDeleted)
            //   .HasDefaultValue(false);
            //builder.HasData
            //(
                
            //    new User
            //    {
            //        Id=1,
            //        FirstName = "Mahmoud  ",
            //        SocondName = "Elsayed",
            //         ThirdName = "Ahmed",
            //        FamilyName = "Saleh  ",
            //        Phone = "01098009525",
            //         Email = "Track.eg02@gmail.com",
            //        CivilId = "123456789000",
            //        PassportNumber = "203040ABC",
            //        Password = "szsrP0sXRwt3/qJsiT6EvQ==",
                   
            //    },
            //new User
            //{
            //    Id = 2,
            //    FirstName = "Omar",
            //    SocondName = "Omar",
            //    ThirdName = "Omar",
            //    FamilyName = "Omar ",
            //    Phone = "+20 106 186 1636",
            //    Email = "track.eg04@gmail.com",
            //    CivilId = "123456789555  ",
            //    PassportNumber = "555ABC",
            //    Password = "szsrP0sXRwt3/qJsiT6EvQ==",
            //}

            //);
        }
    }
}
