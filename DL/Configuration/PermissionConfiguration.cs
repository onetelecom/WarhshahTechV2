using DL.Entities;
using HELPER;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DL.Configuration
{
    class PermissionConfiguration:IEntityTypeConfiguration<permission>
    {
     
        public void Configure(EntityTypeBuilder<permission> builder)
        {
            builder.ToTable("permission");

            Type t = typeof(RoleConstant);
            FieldInfo[] fields = t.GetFields(BindingFlags.Static | BindingFlags.Public);
            permission[] permissions = new permission[fields.Length];

        
            for (int i = 0; i < fields.Length; i++)
            {

                permissions[i] = new permission { Id = i+1, Name = fields[i].Name };


            }
            
            builder.HasData
            (
                permissions

            );
        }
    }
}
