using DL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Configuration
{
  
      class NameNotificationConfiguration : IEntityTypeConfiguration<NameNotification>
    {


        public void Configure(EntityTypeBuilder<NameNotification> builder)
        {
            builder.ToTable("NameNotifications");



            builder.HasData
            (

                new NameNotification
                {
                    Id = 1,
                    ArabicName = "فاتورة أمر إصلاح",
                    EnglishName = "Invoice Repair Order"


                },

                new NameNotification
                {
                    Id = 2,
                    ArabicName = "فاتورة أمر فحص",
                    EnglishName = "Invoice Inspection Order"

                },
                new NameNotification
                {
                    Id = 3,
                    ArabicName = "فاتورة مسددة",
                    EnglishName = "Invoice Paid"


                },
               new NameNotification
               {
                   Id = 4,
                   ArabicName = "فاتورة خدمة فورية",
                   EnglishName = "Invoice Imediate"


               },
               new NameNotification
               {
                   Id = 5,
                   ArabicName = "فاتورة أمر سريع",
                   EnglishName = "Invoice Fast"


               },

                 new NameNotification
                 {
                     Id = 6,
                     ArabicName = "أمر استقبال",
                     EnglishName = "Reciption Order"


                 },

                   new NameNotification
                   {
                       Id = 7,
                       ArabicName = "تحويل أمر فحص لأمر اصلاح",
                       EnglishName = "Change from Inspection to Repair"


                   },

                     new NameNotification
                     {
                         Id = 8,
                         ArabicName = "رفض أمر اصلاح",
                         EnglishName = "Reject Repair Order"


                     },
                      new NameNotification
                      {
                          Id = 20,
                          ArabicName = "إنشاء مصروف",
                          EnglishName = "Create Expense"


                      },
                       new NameNotification
                       {
                           Id = 21,
                           ArabicName = "طلب تسعير",
                           EnglishName = "Create Sales Order"


                       },
                        new NameNotification
                        {
                            Id = 22,
                            ArabicName = "تنبيهات الموارد البشرية",
                            EnglishName = "HR Notification"


                        },
                         new NameNotification
                         {
                             Id = 23,
                             ArabicName = "تعديل بيانات الورشة و المستخدم",
                             EnglishName = "Edit Warshah and User"


                         }


            );
        }
    }
}
