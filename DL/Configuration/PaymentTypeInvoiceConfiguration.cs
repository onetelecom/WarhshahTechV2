using DL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Configuration
{
    class PaymentTypeInvoiceConfiguration :  IEntityTypeConfiguration<PaymentTypeInvoice>
    {


        public void Configure(EntityTypeBuilder<PaymentTypeInvoice> builder)
        {
            builder.ToTable("PaymentTypeInvoices");



            builder.HasData
            (

                new PaymentTypeInvoice
                {
                    Id = 1,
                   PaymentTypeNameAr = "نقدا",
                   PaymentTypeNameEn = " Cash "
                   

                } ,

                new PaymentTypeInvoice
                {
                    Id = 2,
                    PaymentTypeNameAr = "بطاقة ائتمان",
                    PaymentTypeNameEn = " Card "

                } ,
                new PaymentTypeInvoice
                {
                    Id = 3,
                    PaymentTypeNameAr = "شيكات",
                    PaymentTypeNameEn = " cheque "


                } ,
                new PaymentTypeInvoice
                {
                    Id = 4,
                    PaymentTypeNameAr = "حوالات",
                    PaymentTypeNameEn = " Transfer "


                }


            );
        }
    }
}
