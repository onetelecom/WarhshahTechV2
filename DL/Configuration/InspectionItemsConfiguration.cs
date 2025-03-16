using DL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Configuration
{

    class InspectionItemsConfiguration : IEntityTypeConfiguration<InspectionItem>
    {

        public void Configure(EntityTypeBuilder<InspectionItem> builder)
        {
            builder.ToTable("InspectionItems");



            builder.HasData
            (

                new InspectionItem
                {
                    Id = 1,
                    ItemNameAr = "لصوفة الامامية",
                    ItemNameEn = " Front wool ",
                    InspectionSectionId = 1,
                    IsCommon = true

                     ,
                    Status = true

                },
                 new InspectionItem
                 {
                     Id = 2,
                     ItemNameAr = "حالة الماكينة",
                     ItemNameEn = " Machine condition ",
                     InspectionSectionId = 1,
                     IsCommon = true
                      ,
                     Status = true
                 },
                  new InspectionItem
                  {
                      Id = 3,
                      ItemNameAr = "الصوفة الخلفية",
                      ItemNameEn = " Rear wool ",
                      InspectionSectionId = 1,
                      IsCommon = true
                       ,
                      Status = true
                  },
                   new InspectionItem
                   {
                       Id = 4,
                       ItemNameAr = "تصفية ماكينة",
                       ItemNameEn = " Filter machine ",
                       InspectionSectionId = 1,
                       IsCommon = true
                        ,
                       Status = true
                   },
                    new InspectionItem
                    {
                        Id = 5,
                        ItemNameAr = "كرسي الماكينة",
                        ItemNameEn = " Machine chair ",
                        InspectionSectionId = 1,
                        IsCommon = true
                         ,
                        Status = true
                    },
                     new InspectionItem
                     {
                         Id = 6,
                         ItemNameAr = "وجه كرتير الماكينة",
                         ItemNameEn = " The face of the machine carter ",
                         InspectionSectionId = 1,
                         IsCommon = true
                          ,
                         Status = true
                     },
                     new InspectionItem
                     {
                         Id = 7,
                         ItemNameAr = "وجه غطاء البلوف",
                         ItemNameEn = "The face of the cabbage cover ",
                         InspectionSectionId = 1,
                         IsCommon = true
                          ,
                         Status = true
                     },
                      new InspectionItem
                      {
                          Id = 8,
                          ItemNameAr = "قاعدة فلتر الزيت ",
                          ItemNameEn = " Oil filter base ",
                          InspectionSectionId = 1,
                          IsCommon = true
                           ,
                          Status = true
                      },
                       new InspectionItem
                       {
                           Id = 9,
                           ItemNameAr = "وجه الثلاجة ",
                           ItemNameEn = "The face of the fridge",
                           InspectionSectionId = 1,
                           IsCommon = true
                            ,
                           Status = true
                       },
                        new InspectionItem
                        {
                            Id = 10,
                            ItemNameAr = "تهريبات ماء ",
                            ItemNameEn = " Water smuggling",
                            InspectionSectionId = 1,
                            IsCommon = true
                             ,
                            Status = true
                        },
                        new InspectionItem
                        {
                            Id = 11,
                            ItemNameAr = "رادياتير الماء ",
                            ItemNameEn = " Water radiator",
                            InspectionSectionId = 1,
                            IsCommon = true
                             ,
                            Status = true
                        },
                         new InspectionItem
                         {
                             Id = 12,
                             ItemNameAr = " وجه صدر الماكينة امام / خلفي ",
                             ItemNameEn = "Front/rear face of the machine's chest",
                             InspectionSectionId = 1,
                             IsCommon = true
                              ,
                             Status = true
                         },
                          new InspectionItem
                          {
                              Id = 13,
                              ItemNameAr = "طرمبة الماء ",
                              ItemNameEn = "Water pump",
                              InspectionSectionId = 1,
                              IsCommon = true
                               ,
                              Status = true
                          },
                          new InspectionItem
                          {
                              Id = 14,
                              ItemNameAr = "السيور ",
                              ItemNameEn = "Belts",
                              InspectionSectionId = 1,
                              IsCommon = true
                               ,
                              Status = true
                          },
                            new InspectionItem
                            {
                                Id = 15,
                                ItemNameAr = "صفايه البنزين ",
                                ItemNameEn = "Gas can",
                                InspectionSectionId = 1,
                                IsCommon = true
                                 ,
                                Status = true
                            },
                              new InspectionItem
                              {
                                  Id = 16,
                                  ItemNameAr = "فلتر الهواء  ",
                                  ItemNameEn = " Air filter",
                                  InspectionSectionId = 1,
                                  IsCommon = true
                                   ,
                                  Status = true
                              },


                              // Section 2    الجيربوكس
                              new InspectionItem
                              {
                                  Id = 17,
                                  ItemNameAr = "وجه طرمبة الزيت",
                                  ItemNameEn = "The face of the oil pump",
                                  InspectionSectionId = 2,
                                  IsCommon = true
                                   ,
                                  Status = true
                              },
                              new InspectionItem
                              {
                                  Id = 18,
                                  ItemNameAr = " الصوفة الامامية",
                                  ItemNameEn = "Front wool",
                                  InspectionSectionId = 2,
                                  IsCommon = true
                                   ,
                                  Status = true
                              },
                              new InspectionItem
                              {
                                  Id = 19,
                                  ItemNameAr = "الصوفة الخلفية",
                                  ItemNameEn = "Rear wool",
                                  InspectionSectionId = 2,
                                  IsCommon = true
                                   ,
                                  Status = true
                              },
                               new InspectionItem
                               {
                                   Id = 20,
                                   ItemNameAr = "كرسي الجير بوكس",
                                   ItemNameEn = "Lime Box Chair",
                                   InspectionSectionId = 2,
                                   IsCommon = true
                                    ,
                                   Status = true
                               },
                               new InspectionItem
                               {
                                   Id = 21,
                                   ItemNameAr = "وجه كرتير الجير ",
                                   ItemNameEn = "The face of the lime carter.",
                                   InspectionSectionId = 2,
                                   IsCommon = true
                                    ,
                                   Status = true
                               },
                                new InspectionItem
                                {
                                    Id = 22,
                                    ItemNameAr = "صوف عصا الجير  ",
                                    ItemNameEn = "Lime stick wool",
                                    InspectionSectionId = 2,
                                    IsCommon = true
                                     ,
                                    Status = true
                                },
                                 new InspectionItem
                                 {
                                     Id = 23,
                                     ItemNameAr = "ماسورة مبرد الجير ",
                                     ItemNameEn = "Lime cooler pipe",
                                     InspectionSectionId = 2,
                                     IsCommon = true
 ,
                                     Status = true
                                 },
                                 new InspectionItem
                                 {
                                     Id = 24,
                                     ItemNameAr = "حالة الجير ",
                                     ItemNameEn = "Lime case",
                                     InspectionSectionId = 2,
                                     IsCommon = true
                                      ,
                                     Status = true
                                 },


                                  // template 1  Section 3    نظام الكهرباء


                                  new InspectionItem
                                  {
                                      Id = 25,
                                      ItemNameAr = " البطارية ",
                                      ItemNameEn = "Battery",
                                      InspectionSectionId = 3,
                                      IsCommon = true
                                       ,
                                      Status = true
                                  },
                                 new InspectionItem
                                 {
                                     Id = 26,
                                     ItemNameAr = "الدينمو",
                                     ItemNameEn = "the dynamo",
                                     InspectionSectionId = 3,
                                     IsCommon = true
                                      ,
                                     Status = true
                                 },
                                 new InspectionItem
                                 {
                                     Id = 27,
                                     ItemNameAr = "السلف ",
                                     ItemNameEn = "Predecessor",
                                     InspectionSectionId = 3,
                                     IsCommon = true
                                      ,
                                     Status = true
                                 },
                                  new InspectionItem
                                  {
                                      Id = 28,
                                      ItemNameAr = "الانوار ",
                                      ItemNameEn = "Lights",
                                      InspectionSectionId = 3,
                                      IsCommon = true
                                       ,
                                      Status = true
                                  },
                                 new InspectionItem
                                 {
                                     Id = 29,
                                     ItemNameAr = "لمكيف كمبروسر ",
                                     ItemNameEn = "For a kambroser air conditioner.  ",
                                     InspectionSectionId = 3,
                                     IsCommon = true
                                      ,
                                     Status = true
                                 },
                                 new InspectionItem
                                 {
                                     Id = 30,
                                     ItemNameAr = "السنتر لوك ",
                                     ItemNameEn = "Center lock",
                                     InspectionSectionId = 3,
                                     IsCommon = true
                                      ,
                                     Status = true
                                 },
                                 new InspectionItem
                                 {
                                     Id = 31,
                                     ItemNameAr = "المساحات ",
                                     ItemNameEn = "wiper ",
                                     InspectionSectionId = 3,
                                     IsCommon = true
                                      ,
                                     Status = true
                                 },
                                 new InspectionItem
                                 {
                                     Id = 32,
                                     ItemNameAr = "شاشة المكيف ",
                                     ItemNameEn = "Air conditioner screen",
                                     InspectionSectionId = 3,
                                     IsCommon = true
                                      ,
                                     Status = true
                                 },
                                 new InspectionItem
                                 {
                                     Id = 33,
                                     ItemNameAr = "كودات الكمبيوتر  ",
                                     ItemNameEn = "Computer codes",
                                     InspectionSectionId = 3,
                                     IsCommon = true
                                      ,
                                     Status = true
                                 },
                                 new InspectionItem
                                 {
                                     Id = 34,
                                     ItemNameAr = "ثلاجة مكيف",
                                     ItemNameEn = " Air-conditioned refrigerator ",
                                     InspectionSectionId = 3,
                                     IsCommon = true
                                      ,
                                     Status = true
                                 },
                                 new InspectionItem
                                 {
                                     Id = 35,
                                     ItemNameAr = "رديتر مكيف",
                                     ItemNameEn = " Redditor air conditioner ",
                                     InspectionSectionId = 3,
                                     IsCommon = true
                                      ,
                                     Status = true
                                 },



                                 // template 1  Section 4    السوائل 


                                 new InspectionItem
                                 {
                                     Id = 36,
                                     ItemNameAr = "زيت الماكينة",
                                     ItemNameEn = " Machine oil ",
                                     InspectionSectionId = 4,
                                     IsCommon = true
                                       ,
                                     Status = true

                                 },
                                 new InspectionItem
                                 {
                                     Id = 37,
                                     ItemNameAr = "زيت الجير",
                                     ItemNameEn = " Lime oil ",
                                     InspectionSectionId = 4,
                                     IsCommon = true
                                       ,
                                     Status = true
                                 },


                                  new InspectionItem
                                  {
                                      Id = 38,
                                      ItemNameAr = "زيت الفرامل",
                                      ItemNameEn = " Brake oil ",
                                      InspectionSectionId = 4,
                                      IsCommon = true
                                        ,
                                      Status = true
                                  },
                     new InspectionItem
                     {
                         Id = 39,
                         ItemNameAr = "ماء الردياتير",
                         ItemNameEn = " Water of the radyter",
                         InspectionSectionId = 4,
                         IsCommon = true
                           ,
                         Status = true
                     },
                     new InspectionItem
                     {
                         Id = 40,
                         ItemNameAr = "ماء المساحات",
                         ItemNameEn = " Water spaces ",
                         InspectionSectionId = 4,
                         IsCommon = true
                           ,
                         Status = true
                     },



                      // template 1     section = 5 


                      new InspectionItem
                      {
                          Id = 41,
                          ItemNameAr = "الأذرعة ",
                          ItemNameEn = " Arms ",
                          InspectionSectionId = 5,
                          IsCommon = true
                            ,
                          Status = true
                      },
                       new InspectionItem
                       {
                           Id = 42,
                           ItemNameAr = "الركبة اليمنى واليسرى ",
                           ItemNameEn = "Right and left knee",
                           InspectionSectionId = 5,
                           IsCommon = true
                             ,
                           Status = true
                       },
                        new InspectionItem
                        {
                            Id = 43,
                            ItemNameAr = "ذراع شاص",
                            ItemNameEn = "A chasing arm",
                            InspectionSectionId = 5,
                            IsCommon = true
                              ,
                            Status = true
                        },
                        new InspectionItem
                        {
                            Id = 44,
                            ItemNameAr = "ذراع الدودة ",
                            ItemNameEn = "Worm arm",
                            InspectionSectionId = 5,
                            IsCommon = true
                              ,
                            Status = true
                        },
                         new InspectionItem
                         {
                             Id = 45,
                             ItemNameAr = "علبة الدركسيون (طلمبة)",
                             ItemNameEn = "Gendarmerie box (Talamba)",
                             InspectionSectionId = 5,
                             IsCommon = true
                               ,
                             Status = true
                         },
                          new InspectionItem
                          {
                              Id = 46,
                              ItemNameAr = "لبات الدركسيون",
                              ItemNameEn = "Pat Draconian",
                              InspectionSectionId = 5,
                              IsCommon = true
                                ,
                              Status = true
                          },

                          new InspectionItem
                          {
                              Id = 47,
                              ItemNameAr = "الدودة",
                              ItemNameEn = "The worm",
                              InspectionSectionId = 5,
                              IsCommon = true
                                ,
                              Status = true
                          },

                            new InspectionItem
                            {
                                Id = 48,
                                ItemNameAr = "عامود الدركسيون",
                                ItemNameEn = "Gendarmerie column",
                                InspectionSectionId = 5,
                                IsCommon = true
                                  ,
                                Status = true
                            },

                              new InspectionItem
                              {
                                  Id = 49,
                                  ItemNameAr = "المساعدات الامامية / كراسي مساعدات",
                                  ItemNameEn = "Front aid / aid chairs",
                                  InspectionSectionId = 5,
                                  IsCommon = true
                                    ,
                                  Status = true
                              },

                              new InspectionItem
                              {
                                  Id = 50,
                                  ItemNameAr = "المساعدات الخلفية",
                                  ItemNameEn = "Rear aid",
                                  InspectionSectionId = 5,
                                  IsCommon = true
                                    ,
                                  Status = true
                              },


                               new InspectionItem
                               {
                                   Id = 51,
                                   ItemNameAr = "مسمام عامود التوازن",
                                   ItemNameEn = "Balance column valve",
                                   InspectionSectionId = 5,
                                   IsCommon = true
                                     ,
                                   Status = true
                               },

                              new InspectionItem
                              {
                                  Id = 52,
                                  ItemNameAr = "صليب عامود الدوران",
                                  ItemNameEn = "Spin column cross",
                                  InspectionSectionId = 5,
                                  IsCommon = true
                                    ,
                                  Status = true
                              },

                               new InspectionItem
                               {
                                   Id = 53,
                                   ItemNameAr = "جلد المقص العلوي",
                                   ItemNameEn = "Upper scissor skin",
                                   InspectionSectionId = 5,
                                   IsCommon = true
                                     ,
                                   Status = true
                               },

                               new InspectionItem
                               {
                                   Id = 54,
                                   ItemNameAr = "جلد المقص السفلي",
                                   ItemNameEn = "Leather lower scissors",
                                   InspectionSectionId = 5,
                                   IsCommon = true
                                     ,
                                   Status = true
                               },


                                new InspectionItem
                                {
                                    Id = 55,
                                    ItemNameAr = "اليايات ",
                                    ItemNameEn = "The Yayat",
                                    InspectionSectionId = 5,
                                    IsCommon = true
                                      ,
                                    Status = true
                                },



                                 // template 1 section = 6


                                 new InspectionItem
                                 {
                                     Id = 56,
                                     ItemNameAr = "رامل (امامية / خلفية)",
                                     ItemNameEn = "Ramel (front/back)",
                                     InspectionSectionId = 6,
                                     IsCommon = true
                                       ,
                                     Status = true
                                 },

                                 new InspectionItem
                                 {
                                     Id = 57,
                                     ItemNameAr = "هوب (امامي / خلفي)  ",
                                     ItemNameEn = "Hope (front/back) ",
                                     InspectionSectionId = 6,
                                     IsCommon = true
                                       ,
                                     Status = true
                                 },

                                  new InspectionItem
                                  {
                                      Id = 58,
                                      ItemNameAr = "فلنجات (امامية / خلفية) ",
                                      ItemNameEn = "Flangat (front/back)",
                                      InspectionSectionId = 6,
                                      IsCommon = true
                                        ,
                                      Status = true
                                  },
                                 new InspectionItem
                                 {
                                     Id = 59,
                                     ItemNameAr = "علبة الفرامل الرئيسية",
                                     ItemNameEn = "Main brake box",
                                     InspectionSectionId = 6,
                                     IsCommon = true
                                       ,
                                     Status = true
                                 },

                                 new InspectionItem
                                 {
                                     Id = 60,
                                     ItemNameAr = "باكم الفرامل ",
                                     ItemNameEn = "bakem - brakes",
                                     InspectionSectionId = 6,
                                     IsCommon = true
                                       ,
                                     Status = true
                                 },

                                  new InspectionItem
                                  {
                                      Id = 61,
                                      ItemNameAr = "سلندر (امامي / خلفي) ",
                                      ItemNameEn = "Slender (front/back)",
                                      InspectionSectionId = 6,
                                      IsCommon = true
                                        ,
                                      Status = true
                                  },
                                 new InspectionItem
                                 {
                                     Id = 62,
                                     ItemNameAr = "سلك فرامل اليد (جلنط) ",
                                     ItemNameEn = "Hand brake wire (galent)",
                                     InspectionSectionId = 6,
                                     IsCommon = true
                                       ,
                                     Status = true
                                 },

                                 new InspectionItem
                                 {
                                     Id = 63,
                                     ItemNameAr = "ليات فرامل",
                                     ItemNameEn = "Brake nights",
                                     InspectionSectionId = 6,
                                     IsCommon = true
                                       ,
                                     Status = true
                                 },


                                 // template 1 section 7



                                 new InspectionItem
                                 {
                                     Id = 64,
                                     ItemNameAr = "العكوس الامامية ",
                                     ItemNameEn = "Forward inverse",
                                     InspectionSectionId = 7,
                                     IsCommon = true
                                       ,
                                     Status = true
                                 },
                                 new InspectionItem
                                 {
                                     Id = 65,
                                     ItemNameAr = "العكوس الخلفية",
                                     ItemNameEn = " Rear inverse",
                                     InspectionSectionId = 7,
                                     IsCommon = true
                                       ,
                                     Status = true
                                 },

 new InspectionItem
 {
     Id = 66,
     ItemNameAr = "العكوس الدفرنس",
     ItemNameEn = " French inverse",
     InspectionSectionId = 7,
     IsCommon = true
                                       ,
     Status = true
 },
 new InspectionItem
 {
     Id = 67,
     ItemNameAr = "دفرنس امام / خلفي",
     ItemNameEn = " Frances in front/back",
     InspectionSectionId = 7,
     IsCommon = true
                                       ,
     Status = true
 },
 new InspectionItem
 {
     Id = 68,
     ItemNameAr = "صوفة عكس (امامي / خلفي)",
     ItemNameEn = " Reverse wool (front/back)",
     InspectionSectionId = 7,
     IsCommon = true
                                       ,
     Status = true
 },


 // template 1 section = 8


 new InspectionItem
 {
     Id = 69,
     ItemNameAr = "مقاعد وأحزمة",
     ItemNameEn = " Seats and belts",
     InspectionSectionId = 8,
     IsCommon = true
                                       ,
     Status = true
 },
 new InspectionItem
 {
     Id = 70,
     ItemNameAr = "الضوابط والمفاتيح الداخلية",
     ItemNameEn = " Internal controls and keys",
     InspectionSectionId = 8,
     IsCommon = true
                                       ,
     Status = true
 },
 new InspectionItem
 {
     Id = 71,
     ItemNameAr = "فتحة السقف والنوافذ ",
     ItemNameEn = " Sunroof and windows ",
     InspectionSectionId = 8,
     IsCommon = true
                                       ,
     Status = true
 },
 new InspectionItem
 {
     Id = 72,
     ItemNameAr = "عداد الوقود ودرجة الحرارة",
     ItemNameEn = " Fuel meter and temperature",
     InspectionSectionId = 8,
     IsCommon = true
                                       ,
     Status = true
 },
 new InspectionItem
 {
     Id = 73,
     ItemNameAr = "لوحة القيادة وأجهزة القياس",
     ItemNameEn = " Dashboard and measuring devices",
     InspectionSectionId = 8,
     IsCommon = true
                                       ,
     Status = true
 },
 new InspectionItem
 {
     Id = 74,
     ItemNameAr = "نظام راديو / موسيقى",
     ItemNameEn = " Radio/Music System",
     InspectionSectionId = 8,
     IsCommon = true
                                       ,
     Status = true
 },
 new InspectionItem
 {
     Id = 75,
     ItemNameAr = "وسائد هوائية",
     ItemNameEn = " Airbags",
     InspectionSectionId = 8,
     IsCommon = true
                                       ,
     Status = true
 },
 new InspectionItem
 {
     Id = 76,
     ItemNameAr = "إمالة / قفل عجلة القيادة",
     ItemNameEn = " Tilt/lock steering wheel",
     InspectionSectionId = 8,
     IsCommon = true
                                       ,
     Status = true
 },
 new InspectionItem
 {
     Id = 77,
     ItemNameAr = "المرايا",
     ItemNameEn = " Mirrors",
     InspectionSectionId = 8,
     IsCommon = true
                                       ,
     Status = true
 },




 // template 1 section 9




 new InspectionItem
 {
     Id = 78,
     ItemNameAr = "الجهة الامامية",
     ItemNameEn = " Front",
     InspectionSectionId = 9,
     IsCommon = true
                                       ,
     Status = true
 },
 new InspectionItem
 {
     Id = 79,
     ItemNameAr = "الجهة الخلفية",
     ItemNameEn = " Back side",
     InspectionSectionId = 9,
     IsCommon = true
                                       ,
     Status = true
 },
 new InspectionItem
 {
     Id = 80,
     ItemNameAr = "الجهة اليمنى",
     ItemNameEn = " Right side",
     InspectionSectionId = 9,
     IsCommon = true
                                       ,
     Status = true
 },
    new InspectionItem
    {
        Id = 81,
        ItemNameAr = "الجهة اليسرى",
        ItemNameEn = " Left side",
        InspectionSectionId = 9,
        IsCommon = true
                                       ,
        Status = true
    },
 new InspectionItem
 {
     Id = 82,
     ItemNameAr = "اعلى السيارة ( التنده)",
     ItemNameEn = " Top of the car ( Ceiling)",
     InspectionSectionId = 9,
     IsCommon = true
                                       ,
     Status = true
 }





            );
        }
    }
}
