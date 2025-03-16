using DL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Configuration
{
   
    class ExpensesCategoryConfiguration : IEntityTypeConfiguration<ExpensesCategory>
    {

        public void Configure(EntityTypeBuilder<ExpensesCategory> builder)
        {
            builder.ToTable("ExpensesCategories");



            builder.HasData
            (

                new ExpensesCategory
                {
                    Id = 1,
                    ExpensesCategoryNameAr = "خاضع للضريبة",
                    ExpensesCategoryNameEn = " Taxable ",
                  

                },

                new ExpensesCategory
                {
                    Id = 2,
                    ExpensesCategoryNameAr = "غير خاضع للضريبة",
                    ExpensesCategoryNameEn = "  Not Taxable ",


                }


            );
        }
    }
}
