using DL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;


namespace DL.Configuration
{


             class FixedBankConfiguration : IEntityTypeConfiguration<FixedBank>
    {


        public void Configure(EntityTypeBuilder<FixedBank> builder)
        {
            builder.ToTable("FixedBanks");



            builder.HasData
            (

                new FixedBank
                {
                    Id = 1,
                    BankNameAr = "البنك الأهلي السعودي",
                    BankNameEn = "Saudi National Bank"


                },

                 new FixedBank
                 {
                     Id = 2,
                     BankNameAr = "مصرف الراجحي",
                     BankNameEn = "Al Rajhi Bank"


                 },


                  new FixedBank
                  {
                      Id = 3,
                      BankNameAr = "بنك الرياض",
                      BankNameEn = "Riyad Bank"


                  },

                  new FixedBank
                  {
                      Id = 4,
                      BankNameAr = "البنك السعودي البريطاني",
                      BankNameEn = "The Saudi British Bank"


                  },

                    new FixedBank
                    {
                        Id = 5,
                        BankNameAr = "البنك السعودي البريطاني",
                        BankNameEn = "The Saudi British Bank"


                    },

                     new FixedBank
                     {
                         Id = 6,
                         BankNameAr = "البنك العربي الوطني",
                         BankNameEn = "Arab National Bank"


                     },

                      new FixedBank
                      {
                          Id = 7,
                          BankNameAr = "مصرف الانماء",
                          BankNameEn = "alinma bank"


                      },

                       new FixedBank
                       {
                           Id = 8,
                           BankNameAr = "البنك السعودي الفرنسي",
                           BankNameEn = "Banque Saudi Fransi"


                       },

                        new FixedBank
                        {
                            Id = 9,
                            BankNameAr = "البنك السعودي للاستثمار",
                            BankNameEn = "Saudi Investment Bank"


                        },

                         new FixedBank
                         {
                             Id = 10,
                             BankNameAr = "بنك الجزيرة",
                             BankNameEn = "Bank AlJazira"


                         },

                          new FixedBank
                          {
                              Id = 11,
                              BankNameAr = "بنك البلاد",
                              BankNameEn = "Bank AlBilad"


                          },

                           new FixedBank
                           {
                               Id = 12,
                               BankNameAr = "بنك الخليج الدولي - السعودية",
                               BankNameEn = "Gulf International Bank Saudi Arabia (GIB-SA)"


                           },

                           new FixedBank
                           {
                               Id = 13,
                               BankNameAr = "بنك إس تي سي",
                               BankNameEn = "STC bank"


                           },


                           new FixedBank
                           {
                               Id = 14,
                               BankNameAr = "البنك السعودي الرقمي",
                               BankNameEn = "Saudi Digital Bank"


                           },

                           new FixedBank
                           {
                               Id = 15,
                               BankNameAr = "بنك دال ثلاثمائة وستون",
                               BankNameEn = "D360 Bank"


                           },

                            new FixedBank
                            {
                                Id = 16,
                                BankNameAr = "بنك الامارات دبي الوطني",
                                BankNameEn = "Emirates NBD"


                            },

                             new FixedBank
                             {
                                 Id = 17,
                                 BankNameAr = "بنك البحرين الوطني",
                                 BankNameEn = "National Bank of Bahrain (NBB)"


                             },

                               new FixedBank
                               {
                                   Id = 18,
                                   BankNameAr = "بنك الكويت الوطني",
                                   BankNameEn = "National Bank of Kuwait (NBK)"


                               },

                                new FixedBank
                                {
                                    Id = 19,
                                    BankNameAr = "بنك مسقط",
                                    BankNameEn = "Muscat Bank"


                                },
                                new FixedBank
                                {
                                    Id = 20,
                                    BankNameAr = "دويتشه بنك",
                                    BankNameEn = "Deutsche Bank"


                                },

                                   new FixedBank
                                   {
                                       Id = 21,
                                       BankNameAr = "بي إن باريبا",
                                       BankNameEn = "BNP Paribas"

                                   },


                                   new FixedBank
                                   {
                                       Id = 22,
                                       BankNameAr = "جي بي مورقان تشيز إن ايه",
                                       BankNameEn = "J.P. Morgan Chase N.A"

                                   },

                                      new FixedBank
                                      {
                                          Id = 23,
                                          BankNameAr = "بانك باكستان الوطني",
                                          BankNameEn = "National Bank Of Pakistan (NBP)"

                                      },


                                       new FixedBank
                                       {
                                           Id = 24,
                                           BankNameAr = "بنك تي سي زراعات بانكاسي",
                                           BankNameEn = "T.C.ZIRAAT BANKASI A.S."

                                       },

                                       new FixedBank
                                       {
                                           Id = 25,
                                           BankNameAr = "بنك الصين للصناعة والتجارة",
                                           BankNameEn = "Industrial and Commercial Bank of China (ICBC)"

                                       },


                                        new FixedBank
                                        {
                                            Id = 26,
                                            BankNameAr = "بنك قطر الوطني",
                                            BankNameEn = "Qatar National Bank"

                                        },


                                           new FixedBank
                                           {
                                               Id = 27,
                                               BankNameAr = "بنك إم يوم إف جي المحدودة",
                                               BankNameEn = "MUFG Bank, Ltd."

                                           },

                                            new FixedBank
                                            {
                                                Id = 28,
                                                BankNameAr = "بنك أبو ظبي الأول",
                                                BankNameEn = "First Abu Dhabi Bank"

                                            },

                                             new FixedBank
                                             {
                                                 Id = 29,
                                                 BankNameAr = "بنك كريدت سويس",
                                                 BankNameEn = "Credit Suisse Bank"

                                             },

                                             new FixedBank
                                             {
                                                 Id = 30,
                                                 BankNameAr = "بنك ستاندرد تشارترد",
                                                 BankNameEn = "Standard Chartered Bank"

                                             },
                                              new FixedBank
                                              {
                                                  Id = 31,
                                                  BankNameAr = "المصرف العراقي للتجارة",
                                                  BankNameEn = "Trade bank of Iraq"

                                              },

                                               new FixedBank
                                               {
                                                   Id = 32,
                                                   BankNameAr = "بنك الصين المحدود",
                                                   BankNameEn = "Bank of China Limited"

                                               },

                                                new FixedBank
                                                {
                                                    Id = 33,
                                                    BankNameAr = "بنك مصر",
                                                    BankNameEn = "Banque Misr"

                                                },

                                                 new FixedBank
                                                 {
                                                     Id = 34,
                                                     BankNameAr = "المصرف الأهلي العراقي",
                                                     BankNameEn = "National Bank of Iraq"

                                                 },

                                                  new FixedBank
                                                  {
                                                      Id = 35,
                                                      BankNameAr = "البنك الأهلي المصري",
                                                      BankNameEn = "National Bank of Egypt"

                                                  },

                                                    new FixedBank
                                                    {
                                                        Id = 36,
                                                        BankNameAr = "بنك صحار الدولي",
                                                        BankNameEn = "Sohar International Bank"

                                                    }


            );
        }
    }
}















                         