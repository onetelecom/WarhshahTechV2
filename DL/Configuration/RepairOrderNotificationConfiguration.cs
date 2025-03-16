using DL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using DL.Entities;
using Microsoft.EntityFrameworkCore;


namespace DL.Configuration
{

    class RepairOrderNotificationConfiguration : IEntityTypeConfiguration<NotificationRepairOrderAdding>
    {
        public void Configure(EntityTypeBuilder<NotificationRepairOrderAdding> builder)
    {
        builder.ToTable("NotificationRepairOrderAddings");



        builder.HasData
        (

            new NotificationRepairOrderAdding
            {
                Id = 1,
                ArabicName = "بدء الإصلاح",
                EnglishName = "Start Repair"


            },

            new NotificationRepairOrderAdding
            {
                Id = 2,
                ArabicName = "تقرير الفنى",
                EnglishName = "Technical Report"

            },
            new NotificationRepairOrderAdding
            {
                Id = 3,
                ArabicName = "طلب قطع غيار",
                EnglishName = "Request Spare parts"


            },
           new NotificationRepairOrderAdding
           {
               Id = 4,
               ArabicName = "انتظار الفنى",
               EnglishName = "Waiting Technical"


           },
           new NotificationRepairOrderAdding
           {
               Id = 5,
               ArabicName = "الإصلاح",
               EnglishName = "Repair"


           },

             new NotificationRepairOrderAdding
             {
                 Id = 6,
                 ArabicName = "إغلاق أمر الإصلاح",
                 EnglishName = "Closed Repair Order"


             },

               new NotificationRepairOrderAdding
               {
                   Id = 7,
                   ArabicName = " انتظار موافقة العميل",
                   EnglishName = "Waiting Client Approved"


               }


        );
    }
}
}
