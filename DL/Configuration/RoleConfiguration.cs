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
    class RoleConfiguration:IEntityTypeConfiguration<Role>
    {
     
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Role");



            builder.HasData
            (

                new Role
                {
                    Id = 1,
                    Name = "WarshahOwner"
                  

                },
                 new Role
                 {
                     Id = 2,
                     Name = "CarOwner"


                 },
                  new Role
                  {
                      Id = 3,
                      Name = "Technician"


                  },
                   new Role
                   {
                       Id = 4,
                       Name = "Receptionist"


                   },
                    new Role
                    {
                        Id = 5,
                        Name = "acountant"


                    },
                      new Role
                      {
                          Id = 6,
                          Name = "SystemAdmin"


                      }
                      ,
                      new Role
                      {
                          Id = 7,
                          Name = "TaseerAdmin"


                      },
                      new Role
                      {
                          Id = 8,
                          Name = "TaseerLeader"


                      }
                      ,
                      new Role
                      {
                          Id = 9,
                          Name = "TaseerAcountant"


                      }
                      
                       ,
                      new Role
                      {
                          Id = 10,
                          Name = "Sales"


                      }
                       ,
                      new Role
                      {
                          Id = 11,
                          Name = "Hr"


                      }



            );
        }
    }
}
