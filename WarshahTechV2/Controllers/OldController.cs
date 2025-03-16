using AutoMapper;
using BL.Infrastructure;
using BL.Security;
using DL.DTOs;
using DL.Entities;
using DL.MailModels;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Vml;
using HELPER;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Writers;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using WarshahTechV2.Models;
using City = DL.Entities.City;
using Country = DL.Entities.Country;
using MotorColor = DL.Entities.MotorColor;
using MotorMake = DL.Entities.MotorMake;
using MotorModel = DL.Entities.MotorModel;
using Region = DL.Entities.Region;
using SparePart = DL.Entities.SparePart; 
using User = DL.Entities.User;

namespace WarshahTechV2.Controllers
{
    [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]
    [Route("api/[controller]")]
    [ApiController]
    public class OldController : ControllerBase
    {
        WarshahTechContext warshahTechContext = new WarshahTechContext();
        private readonly IUnitOfWork unitOfWork;
     
        public OldController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;         
           
        }




        

        //[HttpGet,Route("CopyAllWarshah")]
        //public IActionResult CopyAllWarshah()
        //{
        //    var d = warshahTechContext.Warshahs.ToList();
        //    foreach (var item in d)
        //    {
        //        //if (item.WarshahId == Guid.Parse("E7EFA4EB-5902-4564-BBC0-8FCECFE5098D"))
        //        //{
        //            var Warshah = new DL.Entities.Warshah();

        //            Warshah.WarshahNameAr = item.WarshahName;
        //            Warshah.CR = item.Cr;
        //            Warshah.TaxNumber = item.TaxNumber;
        //            Warshah.LandLineNum = item.PhoneNo;
        //            Warshah.Distrect = item.Distrect != null? item.Distrect:"Null";
        //            Warshah.Street = item.Street;
        //            Warshah.WarshahLogo = "";
        //            Warshah.CityId = unitOfWork.CityRepository.GetAll().FirstOrDefault().Id;
        //            Warshah.CountryId = unitOfWork.CountryRepository.GetAll().FirstOrDefault().Id;
        //            Warshah.RegionId = unitOfWork.RegionRepository.GetAll().FirstOrDefault().Id;
        //            Warshah.Describtion = item.WarshahId.ToString();
        //            Warshah.IsActive = true;
        //            Warshah.UnitNum = 1;
        //            unitOfWork.WarshahRepository.Add(Warshah);
        //            unitOfWork.Save();
        //            var OldUser = warshahTechContext.Users.FirstOrDefault(a => a.WarshahId == Guid.Parse(Warshah.Describtion)&&a.RoleId==2);
        //        if (OldUser!=null)
        //        {
        //            var User = new DL.Entities.User();
        //            User.WarshahId = Warshah.Id;
        //            User.RoleId = 1;
        //            User.Password = "b/H7XbXULWwKt9kN7fx+LQ==";
        //            User.Address = "";
        //            User.FirstName = OldUser.FullName;
        //            User.LastName = "";
        //            User.Phone = "+" + OldUser.MobileNo;
        //            User.CivilId = OldUser.Idiqama;
        //            User.CommerialRegisterar = OldUser.Cr;
        //            User.IsActive = true;
        //            User.Email = OldUser.Email;
        //            User.IsPhoneConfirmed = true;
        //            User.IsCompany = false;
        //            User.IsDeleted = false;
        //            unitOfWork.UserRepository.Add(User);
        //            unitOfWork.Save();
        //            AddUserRoles(User);
        //        }
               
        //            var OldSub = warshahTechContext.PaymentOps.FirstOrDefault(a => a.WarshahId == Guid.Parse(Warshah.Describtion));
        //        if (OldSub!=null)
        //        {
        //            Duration duration = new Duration();
        //            duration.WarshahId = Warshah.Id;
        //            duration.SubscribtionId = 12;
        //            duration.StartTime = DateTime.Now;
        //            duration.EndDate = OldSub.ExpiryDate;
        //            duration.IsActive = true;
        //            duration.TransactionInfo = "From Old";
        //            unitOfWork.DurationRepository.Add(duration);
        //            unitOfWork.Save();

        //        }



        //    }
        //    return Ok();
        //}
    
        [NonAction]
        public void AddUserRoles(DL.Entities.User user)
        {
            //warshah Owner Roles
            if (user.RoleId == 1)
            {
                Type t = typeof(RoleConstant);
                FieldInfo[] fields = t.GetFields(BindingFlags.Static | BindingFlags.Public);
                permission[] permissions = new permission[fields.Length];


                for (int i = 0; i < fields.Length; i++)
                {

                    unitOfWork.UserpermissionRepository.Add(new UserPermission { PermissionId = i + 1, UserId = user.Id });



                }
                unitOfWork.Save();
                //Car Owner Roles
            }
            else if (user.RoleId == 2)
            {
                var Per = unitOfWork.PermissionRepository.GetMany(a => a.Name == RoleConstant.Common).FirstOrDefault();

                unitOfWork.UserpermissionRepository.Add(new UserPermission { PermissionId = Per.Id, UserId = user.Id });




                unitOfWork.Save();
            }
            //System Admin Roles
            else if (user.RoleId == 6)
            {
                Type t = typeof(RoleConstant);
                FieldInfo[] fields = t.GetFields(BindingFlags.Static | BindingFlags.Public);
                permission[] permissions = new permission[fields.Length];


                for (int i = 0; i < fields.Length; i++)
                {

                    unitOfWork.UserpermissionRepository.Add(new UserPermission { PermissionId = i + 1, UserId = user.Id });



                }
                unitOfWork.Save();
            }
            //acountant
            else if (user.RoleId == 5)
            {
                var Per = unitOfWork.PermissionRepository.GetMany(a => a.Name == RoleConstant.Accountant).FirstOrDefault();
                var Per2 = unitOfWork.PermissionRepository.GetMany(a => a.Name == RoleConstant.Common).FirstOrDefault();

                unitOfWork.UserpermissionRepository.Add(new UserPermission { PermissionId = Per2.Id, UserId = user.Id });
                unitOfWork.UserpermissionRepository.Add(new UserPermission { PermissionId = Per.Id, UserId = user.Id });




                unitOfWork.Save();
            }
            //Tech  Roles
            else if (user.RoleId == 3)
            {
                var Per = unitOfWork.PermissionRepository.GetMany(a => a.Name == RoleConstant.tech).FirstOrDefault();
                var Per2 = unitOfWork.PermissionRepository.GetMany(a => a.Name == RoleConstant.Common).FirstOrDefault();

                unitOfWork.UserpermissionRepository.Add(new UserPermission { PermissionId = Per2.Id, UserId = user.Id });
                unitOfWork.UserpermissionRepository.Add(new UserPermission { PermissionId = Per.Id, UserId = user.Id });




                unitOfWork.Save();
            } //Receptionist  Roles
            else if (user.RoleId == 4)
            {
                var Per = unitOfWork.PermissionRepository.GetMany(a => a.Name == RoleConstant.Recicp).FirstOrDefault();
                var Per2 = unitOfWork.PermissionRepository.GetMany(a => a.Name == RoleConstant.Common).FirstOrDefault();

                unitOfWork.UserpermissionRepository.Add(new UserPermission { PermissionId = Per2.Id, UserId = user.Id });
                unitOfWork.UserpermissionRepository.Add(new UserPermission { PermissionId = Per.Id, UserId = user.Id });




                unitOfWork.Save();
            }
            //Sales Role
            else if (user.RoleId == 10)
            {
                var Per = unitOfWork.PermissionRepository.GetMany(a => a.Name == RoleConstant.Sales).FirstOrDefault();
                var Per2 = unitOfWork.PermissionRepository.GetMany(a => a.Name == RoleConstant.Common).FirstOrDefault();



                unitOfWork.UserpermissionRepository.Add(new UserPermission { PermissionId = Per.Id, UserId = user.Id });

                unitOfWork.UserpermissionRepository.Add(new UserPermission { PermissionId = Per2.Id, UserId = user.Id });




                unitOfWork.Save();
            }
            //TaseerAdmin (Sales Admin) Role
            else if (user.RoleId == 7)
            {
                var Per = unitOfWork.PermissionRepository.GetMany(a => a.Name == RoleConstant.TaseerAdmin).FirstOrDefault();



                unitOfWork.UserpermissionRepository.Add(new UserPermission { PermissionId = Per.Id, UserId = user.Id });




                unitOfWork.Save();
            }
            //TaseerLeader Role
            else if (user.RoleId == 8)
            {
                var Per = unitOfWork.PermissionRepository.GetMany(a => a.Name == RoleConstant.TaseerLeader).FirstOrDefault();



                unitOfWork.UserpermissionRepository.Add(new UserPermission { PermissionId = Per.Id, UserId = user.Id });




                unitOfWork.Save();
            }
            //Taseer Addountant Role
            else if (user.RoleId == 9)
            {
                var Per = unitOfWork.PermissionRepository.GetMany(a => a.Name == RoleConstant.TaseerAccountant).FirstOrDefault();
                var Per2 = unitOfWork.PermissionRepository.GetMany(a => a.Name == RoleConstant.Common).FirstOrDefault();


                unitOfWork.UserpermissionRepository.Add(new UserPermission { PermissionId = Per.Id, UserId = user.Id });
                unitOfWork.UserpermissionRepository.Add(new UserPermission { PermissionId = Per2.Id, UserId = user.Id });




                unitOfWork.Save();
            }



        }


        [HttpGet, Route("Addjobs")]
        public IActionResult Addjobs()
        {
            var jobs = warshahTechContext.JobTitles.ToList();

            List <OldJobtitle> jobsList = new List <OldJobtitle>();

            foreach (var job in jobs)
            {
                OldJobtitle jobTitle = new OldJobtitle();
                //jobTitle.Id = job.JobTitleId;
                jobTitle.JobTitleAr = job.TitleNameAr;
                jobTitle.JobTitleEn = job.TitleName;

                jobsList.Add(jobTitle);
                unitOfWork.OldjobRepository.Add(jobTitle);
            }

            unitOfWork.Save();

            return Ok(jobsList);
        }

        [HttpGet, Route("Getjobs")]
        public IActionResult Getjobs()
        {
            var jobs = unitOfWork.OldjobRepository.GetAll();

            return Ok(jobs);
        }



        // Get Country From V1 to V2


        [HttpGet, Route("AddCountry")]
        public IActionResult AddCountry()
        {
            var Countries = warshahTechContext.Countries1.ToList();

            List<Country> CountriesList = new List<Country>();

            foreach (var Contry in Countries)
            {
                Country Country = new Country();
                Country.CountryNameAr = Contry.NameAr;
                Country.CountryNameEn = Contry.NameEn;

                CountriesList.Add(Country);
                unitOfWork.CountryRepository.Add(Country);
            }

            unitOfWork.Save();

            return Ok(CountriesList);
        }


        // Get Region From V1 to V2


        [HttpGet, Route("AddRegion")]
        public IActionResult AddRegion()
        {
            var OldRegions = warshahTechContext.Regions.ToList();

            List<DL.Entities.Region> RegionList = new List<DL.Entities.Region>();
           
            foreach (var region in OldRegions)
            {

                Region NewRegion = new Region();
                var currentcountry = warshahTechContext.Countries1.FirstOrDefault(a => a.Id == region.CountryId);

                var newCountry = unitOfWork.CountryRepository.GetMany(a => a.CountryNameAr == currentcountry.NameAr).FirstOrDefault();
                if (newCountry != null)
                {
                    NewRegion.CountryId = newCountry.Id;

                }
                else
                {
                    NewRegion.CountryId = 1;
                }
               
               
                NewRegion.RegionNameAr = region.NameAr; 
                if(region.NameEn != null)
                {
                    NewRegion.RegionNameEn = region.NameEn;
                }
                else
                {
                    NewRegion.RegionNameEn = region.NameAr;
                }


                RegionList.Add(NewRegion);
                unitOfWork.RegionRepository.Add(NewRegion);
            }

            unitOfWork.Save();

            return Ok(RegionList);
        }



        // Get Region From V1 to V2


        [HttpGet, Route("AddCities")]
        public IActionResult AddCities()
        {
            var OldCities = warshahTechContext.Cities.ToList();

            List<DL.Entities.City> CitiesList = new List<DL.Entities.City>();

            foreach (var city in OldCities)
            {
                City Newcity = new City();


                if (city.RegionId == null)
                {
                    city.RegionId = 2;
                }

                var currentregion = warshahTechContext.Regions.FirstOrDefault(a => a.Id == city.RegionId);

                var newregion = unitOfWork.RegionRepository.GetMany(a => a.RegionNameAr == currentregion.NameAr).FirstOrDefault();
                if (newregion != null)
                {
                    Newcity.RegionId = newregion.Id;

                }
                else
                {
                    Newcity.RegionId = 1;
                }

               
                Newcity.RegionId = (int)city.RegionId;
                Newcity.CityNameAr = city.NameAr;
                if(city.NameEn != null) {
                    Newcity.CityNameEn = city.NameEn;

                }
                else
                {
                    Newcity.CityNameEn = city.NameAr;

                }

                CitiesList.Add(Newcity);
                unitOfWork.CityRepository.Add(Newcity);
            }

            unitOfWork.Save();

            return Ok(CitiesList);
        }



        // Get Make From V1 to V2


        [HttpGet, Route("AddMake")]
        public IActionResult AddMake()
        {
            var Carmakes = warshahTechContext.MotorMakes.ToList();

            List<MotorMake> makesList = new List<MotorMake>();

            foreach (var make in Carmakes)
            {
                MotorMake newmake = new MotorMake();
                newmake.MakeNameAr = make.MakeName;
                newmake.MakeNameEn = make.MakeName;

                makesList.Add(newmake);
                unitOfWork.MotorMakeRepository.Add(newmake);
            }

            unitOfWork.Save();

            return Ok(makesList);
        }




        // Get Model From V1 to V2


        [HttpGet, Route("AddModel")]
        public IActionResult AddModel()
        {
            var OldRegions = warshahTechContext.MotorModels.ToList();

            List<DL.Entities.MotorModel> RegionList = new List<DL.Entities.MotorModel>();

            foreach (var region in OldRegions)
            {

                MotorModel NewRegion = new MotorModel();
                var currentcountry = warshahTechContext.MotorMakes.FirstOrDefault(a => a.MotorMakeId == region.MotorMakeId);

                var newCountry = unitOfWork.MotorMakeRepository.GetMany(a => a.MakeNameAr == currentcountry.MakeName).FirstOrDefault();
                if (newCountry != null)
                {
                    NewRegion.MotorMakeId = newCountry.Id;

                }
                else
                {
                    NewRegion.MotorMakeId = 1;
                }


                NewRegion.ModelNameAr = region.ModelName;
                NewRegion.ModelNameEn = region.ModelName;


                RegionList.Add(NewRegion);
                unitOfWork.MotorModelRepository.Add(NewRegion);
            }

            unitOfWork.Save();

            return Ok(RegionList);
        }



        // Get Color From V1 to V2


        [HttpGet, Route("AddColor")]
        public IActionResult AddColor()
        {
            var Countries = warshahTechContext.MotorColors.ToList();

            List<MotorColor> CountriesList = new List<MotorColor>();

            foreach (var Contry in Countries)
            {
                MotorColor Country = new MotorColor();
                Country.ColorNameAr = Contry.ColorNameAr;
                Country.ColorNameEn = Contry.CorlorName;

                CountriesList.Add(Country);
                unitOfWork.MotorColorRepository.Add(Country);
            }

            unitOfWork.Save();

            return Ok(CountriesList);
        }


         
        // Get Years From V1 to V2


        [HttpGet, Route("AddYear")]
        public IActionResult AddYear()
        {
            int FromYear = 1970;
            int ToYear = 2040;

            List<DL.Entities.MotorYear> YearList = new List<DL.Entities.MotorYear>();

            for (int i = 1970 ; i <= 2040; i++  )
            {
                DL.Entities.MotorYear year = new DL.Entities.MotorYear();

                year.Year = i;
                YearList.Add(year);
                unitOfWork.MotorYearRepository.Add(year);
            }

            unitOfWork.Save();

            return Ok(YearList);
        }






        // Get Spareparts  From V1 to V2  by warshah


        [HttpGet, Route("AddSpareparts")]
        public IActionResult AddSpareparts(string warshahmobile)
        {


            var mobile = warshahmobile;

            var OldUser = warshahTechContext.Users.FirstOrDefault(a => a.MobileNo == mobile );
            var oldwarshah = warshahTechContext.Warshahs.FirstOrDefault(a => a.WarshahId == OldUser.WarshahId);



            var newmobile = "+" + warshahmobile;
            var currentUser = unitOfWork.UserRepository.GetMany(a => a.Phone == newmobile).FirstOrDefault();
            int warshahid = 0 ;
            if(currentUser != null)
            {
                warshahid = (int)currentUser.WarshahId;

            }

            List<SparePart> Addparts = new List<SparePart>();

            if (oldwarshah != null)
            {
                var oldparts = warshahTechContext.SpareParts.Where(a=>a.WarshahId == oldwarshah.WarshahId).ToList();

                if(oldparts.Count != 0)
                {
                   

                    foreach(var part in oldparts)
                    
                    {
                        SparePart Newpart = new SparePart();

                        Newpart.WarshahId = warshahid;
                        Newpart.SparePartName = part.SparePartName;
                        Newpart.Quantity = (int)part.Quantity;
                        Newpart.PeacePrice = (decimal)part.PeacePrice;
                        Newpart.BuyingPrice = (decimal)part.Buyingprice;


                        var v = 15;
                        decimal Vat = (((decimal)v) / (100));
                        decimal V = (decimal)System.Convert.ToDouble(1.15);

                        Newpart.BuyBeforeVat = Newpart.BuyingPrice / V;
                        Newpart.VatBuy = Newpart.BuyingPrice - Newpart.BuyBeforeVat;
                        Newpart.SellBeforeVat = Newpart.PeacePrice / V;
                        Newpart.VatSell = Newpart.PeacePrice - Newpart.SellBeforeVat;



                        if (part.MinQuantity == null)
                        {
                            Newpart.MinimumRecordQuantity = 0;
                        }
                        else
                        {
                            Newpart.MinimumRecordQuantity = (int)part.MinQuantity;

                        }
                      
                        Newpart.Describtion = part.PartDescribtion;
                        Newpart.PartNum = part.PartNum;
                        
                        if(part.MotorMakeId != null || part.MotorMakeId !< 0)
                        {
                            var oldmake = warshahTechContext.MotorMakes.Where(a=>a.MotorMakeId == part.MotorMakeId).FirstOrDefault();
                            if(oldmake != null)
                            {
                                var newmake = unitOfWork.MotorMakeRepository.GetMany(a => a.MakeNameAr == oldmake.MakeName).FirstOrDefault();

                                if (newmake != null)
                                {
                                    Newpart.MotorMakeId = newmake.Id;
                                }

                                else
                                {
                                    Newpart.MotorMakeId = 2;
                                }
                            }
                           

                        }

                   
                        else
                        {
                            Newpart.MotorMakeId = 2;
                        }

                        if (part.MotorModelId != null || part.MotorModelId !< 0)
                        {
                            var oldmake = warshahTechContext.MotorModels.Where(a => a.MotorModelId == part.MotorModelId).FirstOrDefault();

                            if(oldmake != null)
                            {
                                var newmake = unitOfWork.MotorModelRepository.GetMany(a => a.ModelNameAr == oldmake.ModelName).FirstOrDefault();

                                if (newmake != null)
                                {
                                    Newpart.MotorModelId = newmake.Id;
                                }

                                else
                                {
                                    Newpart.MotorModelId = 2;
                                }

                            }

                        }

                        if (part.MotorModelId == -1)
                        {
                            Newpart.MotorModelId = 52;
                        }

                        else
                        {
                            Newpart.MotorModelId = 2;
                        }

                        if (part.MotorYearId != null || part.MotorYearId !< 0)
                        {
                            var oldmake = warshahTechContext.MotorYears.Where(a => a.MotorYearId == part.MotorYearId).FirstOrDefault();
                            if(oldmake != null)
                            {
                                var yearname = System.Convert.ToInt16(oldmake.YearName);

                                var newmake = unitOfWork.MotorYearRepository.GetMany(a => a.Year == yearname).FirstOrDefault();

                                if (newmake != null && newmake.Id != 0)
                                {
                                    Newpart.MotorYearId = newmake.Id;
                                }
                                else
                                {
                                    Newpart.MotorYearId = 2;
                                }
                            }


                        

                        }

                        else
                        {
                            Newpart.MotorYearId = 2;
                        }


                        Addparts.Add(Newpart);
                        unitOfWork.SparePartRepository.Add(Newpart);

                        unitOfWork.Save();


                    }



                }

              

            }
           

         

            return Ok(Addparts);
        }




        // Get Invoices  From V1 to V2  by warshah

        [HttpGet, Route("AddOldInvoices")]
        public IActionResult AddOldInvoices(string warshahmobile)
        {
            var mobile = warshahmobile;

            var OldUser = warshahTechContext.Users.FirstOrDefault(a => a.MobileNo == mobile);
            var oldwarshah = warshahTechContext.Warshahs.FirstOrDefault(a => a.WarshahId == OldUser.WarshahId);

            var newmobile = "+" + warshahmobile; ;
            var currentUser = unitOfWork.UserRepository.GetMany(a => a.Phone == newmobile).FirstOrDefault();
            var newwarshah = unitOfWork.WarshahRepository.GetMany(a => a.Id == currentUser.WarshahId).FirstOrDefault();

            var oldinvoices = warshahTechContext.Invoices.Where(a=>a.WarshahId == oldwarshah.WarshahId && a.InvoiceSerial < 9805).ToList().OrderByDescending(a=>a.CreatedOn);

            List<OldInvoice> allInvoices = new List<OldInvoice>();

            foreach (var Oinvoice in oldinvoices)                      
            {
                DL.Entities.OldInvoice AddInvoice = new DL.Entities.OldInvoice();

                AddInvoice.Id = Oinvoice.InvoiceId;
                AddInvoice.InvoiceSerial = Oinvoice.InvoiceSerial.ToString();
                AddInvoice.InvoiceNumber = (int)Oinvoice.InvoiceNumber;
                AddInvoice.InvoiceStatusId = Oinvoice.InvoiceStatusId;
                AddInvoice.InvoiceTypeId = (int)Oinvoice.InvoiceTypeId;
                AddInvoice.Deiscount = (decimal)Oinvoice.Discount;
                AddInvoice.VatMoney = (decimal)Oinvoice.Vat;
                AddInvoice.AfterDiscount = (decimal)Oinvoice.Subtotal;
                AddInvoice.Total = (decimal)Oinvoice.Total;
                AddInvoice.RepairOrderId = Oinvoice.RepairOrderId;
                AddInvoice.WarshahId = Oinvoice.WarshahId;
                AddInvoice.OldCreatedon = Oinvoice.CreatedOn;

                // warshah data

                AddInvoice.WarhshahCondition = oldwarshah.Conditions;
                AddInvoice.WarshahPhone = oldwarshah.PhoneNo;
                AddInvoice.WarshahCR = oldwarshah.Cr;
                AddInvoice.WarshahCity = unitOfWork.CityRepository.GetMany(c => c.Id == newwarshah.CityId)?.FirstOrDefault().CityNameAr;
                AddInvoice.WarshahAddress = unitOfWork.RegionRepository.GetMany(c => c.Id == newwarshah.RegionId)?.FirstOrDefault().RegionNameAr;
                if(oldwarshah.PostalCode == null)
                {
                    AddInvoice.WarshahPostCode = "12345";
                }
                else
                {
                    AddInvoice.WarshahPostCode = oldwarshah.PostalCode.ToString();
                }
               
                AddInvoice.WarshahDescrit = oldwarshah.Distrect;
                AddInvoice.WarshahName = newwarshah.WarshahNameAr;
                AddInvoice.WarshahTaxNumber = oldwarshah.TaxNumber;
                AddInvoice.WarshahStreet = oldwarshah.Street;

                // Car owner data
               

                var repairOrder = warshahTechContext.RepairOrders.Where(r => r.RepairOrderId == Oinvoice.RepairOrderId).FirstOrDefault();

                var rod = warshahTechContext.ReceptionistRepairOrders.Where(a => a.RepairOrderId == repairOrder.RepairOrderId).FirstOrDefault();
                AddInvoice.TechReview = repairOrder.TechnicianMalfunctionDesc;
               

                if (rod == null)
                {
                    AddInvoice.KMIn = 0;
                }
                else
                {
                    AddInvoice.KMIn = 0;
                   
                   var rodid = rod.ReceptionistRepairOrderId;
                   var Items = warshahTechContext.FastServices.Where(a => a.RepairOrderId == rodid).ToList();
                    var motor = warshahTechContext.Motors.Include(t => t.MotorYear).Where(m => m.MotorId == repairOrder.MotorId).FirstOrDefault();
                    var carowner = warshahTechContext.Clients.Where(r => r.UserId == motor.MotorOwner).FirstOrDefault();
                   
                    if (carowner != null)
                    {
                        AddInvoice.CarOwnerName = carowner.FullName;
                        AddInvoice.CarOwnerPhone = carowner.MobileNo;
                        AddInvoice.CarOwnerCivilId = carowner.Idiqama;
                        AddInvoice.CarOwnerAddress = carowner.Address;
                        var cartype = warshahTechContext.MotorModels.Where(m => m.MotorModelId == motor.MotorYear.MotorModelId).FirstOrDefault();
                        AddInvoice.CarType = cartype.ModelName;
                    }
                   

                }
                AddInvoice.KMOut =0;


                unitOfWork.OldInvoicesRepository.Add(AddInvoice);
                allInvoices.Add(AddInvoice);
                unitOfWork.Save(); 



                 var Parts = warshahTechContext.RepairOrderParts.Where(p => p.RepairOrderId == repairOrder.RepairOrderId).ToList();
                //var OrderServices = _uow.RepairOrderServicesRepository.GetMany(a => a.OrderId == invoice.RepairOrderId).ToHashSet();
                List<InvoiceItem> ItemList = new List<InvoiceItem>();

                foreach (var item in Parts)
                {
                    //var ItemExactly = warshahTechContext.SpareParts.Where(a=>a.SparePartId == item.SparePartId).FirstOrDefault();

                    var Additem = new OldInvoiceItem();
                    //if (ItemExactly != null)
                    //{
                        Additem.OldInvoiceId = Oinvoice.InvoiceId;
                        if (item.ServiceId != null)
                        {

                        

                            Additem.SparePartNameAr  = warshahTechContext.Services.Where(a=>a.ServiceId == item.ServiceId).FirstOrDefault().ServiceName;

                        }
                        else
                        {
                            Additem.SparePartNameAr = warshahTechContext.SpareParts.Where(a=>a.SparePartId == item.SparePartId).FirstOrDefault().SparePartName;
                        }
                        Additem.Quantity = (int)item.Quantity;
                        Additem.PeacePrice = (decimal)item.PeacePrice;
                        Additem.Garuntee = item.Gurantee;
                        Additem.CreatedOn = DateTime.Now;                       
                        unitOfWork.OldItemInvoiceRepository.Add(Additem);
                        unitOfWork.Save();

                       

                    //}

                }


            }



            return Ok(allInvoices.Count());
        }


        // get old invoice by mobilewarshah



        [HttpGet, Route("GetOldInvoices")]
        public IActionResult GetOldInvoices(string warshahmobile)
        {
            var mobile = warshahmobile;
            var OldUser = warshahTechContext.Users.FirstOrDefault(a => a.MobileNo == mobile);
            decimal oldtotal = 0;
            decimal oldcountinvoice = 0;
            if (OldUser != null)
            {
                var oldwarshah = warshahTechContext.Warshahs.FirstOrDefault(a => a.WarshahId == OldUser.WarshahId);


                var oldinvoice = warshahTechContext.Invoices.Where(a => a.WarshahId == oldwarshah.WarshahId && a.InvoiceStatusId == 3).ToList();
                oldtotal = (decimal)oldinvoice.Sum(a => a.Total);
                oldcountinvoice = oldinvoice.Count();

            }
        

            var newmobile = "+" + warshahmobile; ;
            var currentUser = unitOfWork.UserRepository.GetMany(a => a.Phone == newmobile).FirstOrDefault();
            decimal newtotal = 0;
            decimal newcountinvoice = 0;


            if (currentUser != null)
            {
                var newwarshah = unitOfWork.WarshahRepository.GetMany(a => a.Id == currentUser.WarshahId).FirstOrDefault();
                var newinvoices = unitOfWork.InvoiceRepository.GetMany(a => a.WarshahId == newwarshah.Id && a.InvoiceStatusId == 2).ToHashSet();
                newtotal = newinvoices.Sum(a => a.Total);
                newcountinvoice = newinvoices.Count();
            }
          
         



            var Totalcount = oldcountinvoice + newcountinvoice;
            var Totalinvoice = oldtotal + newtotal;


            return Ok(new { TotalCount = Totalcount, TotalMoney = Totalinvoice });
        }

        // get old invoice by mobilewarshah



        [HttpGet, Route("AddWarshahDtails")]
        public IActionResult AddWarshahDtails()
        {
            unitOfWork.WarshahReportRepository.Add(new WarshahReport { WarshahName = "عيادة السيارات الفاخرة", UserName = "جمال الشرباصي", Phone = "+966568055625" , CreatedOn = DateTime.Now });
            unitOfWork.WarshahReportRepository.Add(new WarshahReport { WarshahName = "ElZamam", UserName = "أحمد دحيمان", Phone = "+966535984862", CreatedOn = DateTime.Now });
            unitOfWork.WarshahReportRepository.Add(new WarshahReport { WarshahName = "أستاد العربة", UserName = "أستاد العربة", Phone = "+966564202554", CreatedOn = DateTime.Now });
            unitOfWork.WarshahReportRepository.Add(new WarshahReport { WarshahName = "قصر العملاق", UserName = "مصطفى جواد الناصر", Phone = "+966599934900", CreatedOn = DateTime.Now });
            unitOfWork.WarshahReportRepository.Add(new WarshahReport { WarshahName = "هشام الجرفيين", UserName = "هشام محمد الجرفين", Phone = "+966503055597", CreatedOn = DateTime.Now });
            unitOfWork.WarshahReportRepository.Add(new WarshahReport { WarshahName = "TAMIM CENTER", UserName = "اسية محمد فرج تميم", Phone = "+966551252500", CreatedOn = DateTime.Now });
            unitOfWork.WarshahReportRepository.Add(new WarshahReport { WarshahName = "عبد الله جعفر", UserName = "عبد الله جعفر", Phone = "+966569745798", CreatedOn = DateTime.Now });
            unitOfWork.WarshahReportRepository.Add(new WarshahReport { WarshahName = "العاليه", UserName = "احمد", Phone = "+966592728680", CreatedOn = DateTime.Now });
            unitOfWork.WarshahReportRepository.Add(new WarshahReport { WarshahName = "golden tiger", UserName = "حمد فهد حمد ابو حيمد", Phone = "+966555677139", CreatedOn = DateTime.Now });
            unitOfWork.WarshahReportRepository.Add(new WarshahReport { WarshahName = "مركز غادي", UserName = "عبدالسلام فهد اللهيمي", Phone = "+966599196777", CreatedOn = DateTime.Now });
            unitOfWork.WarshahReportRepository.Add(new WarshahReport { WarshahName = "مركز ماي كار", UserName = "فهد حمد محيميد الخميسي", Phone = "+966506044000", CreatedOn = DateTime.Now });
            unitOfWork.WarshahReportRepository.Add(new WarshahReport { WarshahName = "ورشة ركن الأسمر ", UserName = "خليل", Phone = "+966555277992", CreatedOn = DateTime.Now });
            unitOfWork.WarshahReportRepository.Add(new WarshahReport { WarshahName = "شركة أزف المتحدة للتجارة ", UserName = "شركة أزف المتحدة للتجارة", Phone = "+966592599773", CreatedOn = DateTime.Now });
            unitOfWork.WarshahReportRepository.Add(new WarshahReport { WarshahName = "ورشة عبد العزيز مشاري محمد حكمي ", UserName = "عبد العزيز مشاري الحكمي", Phone = "+966551776647", CreatedOn = DateTime.Now });
            unitOfWork.WarshahReportRepository.Add(new WarshahReport { WarshahName = "مركز العربة الفخمه لصيانة السيارات ", UserName = "فهدعبد العزيز العضيب", Phone = "+966505124259", CreatedOn = DateTime.Now });
            unitOfWork.WarshahReportRepository.Add(new WarshahReport { WarshahName = " مركز اس بلس لصيانة السيارات ", UserName = "علي محمد العريني", Phone = "+966550231231", CreatedOn = DateTime.Now });
            unitOfWork.WarshahReportRepository.Add(new WarshahReport { WarshahName = " ورشة حنان للصيانة و الخراطة ", UserName = "حنان محمد عبده", Phone = "+966539363236", CreatedOn = DateTime.Now });
            unitOfWork.WarshahReportRepository.Add(new WarshahReport { WarshahName = " ورشة عمدة الاتقان لصيانة السيارات ", UserName = "عماد عايش عبدالله السكران", Phone = "+966559998638", CreatedOn = DateTime.Now });
            unitOfWork.WarshahReportRepository.Add(new WarshahReport { WarshahName = "مركز سرعة البرمجيات لصيانة السيارات ", UserName = "علي صالح الشيخ", Phone = "+966506057542", CreatedOn = DateTime.Now });
            unitOfWork.WarshahReportRepository.Add(new WarshahReport { WarshahName = "Khalij Alyammah ", UserName = "مركز الخليج اليمامة لصيانة السيارات", Phone = "+966500853741", CreatedOn = DateTime.Now });
            unitOfWork.WarshahReportRepository.Add(new WarshahReport { WarshahName = "7PART WORKSHOP ", UserName = "Abu Mohammed", Phone = "+966537344044", CreatedOn = DateTime.Now });
            unitOfWork.WarshahReportRepository.Add(new WarshahReport { WarshahName = "ورشة الاوائل ", UserName = "معاذ يوسف عوض الاحمدي", Phone = "+966555013116", CreatedOn = DateTime.Now });
            unitOfWork.WarshahReportRepository.Add(new WarshahReport { WarshahName = "ورشة محمد عبدالحفيظ احمد مؤمن ", UserName = "محمد عبدالحفيظ مؤمن", Phone = "+966552719023", CreatedOn = DateTime.Now });
            unitOfWork.WarshahReportRepository.Add(new WarshahReport { WarshahName = "ورشة امجاد المجد لصيانة السيارات ", UserName = "ماجد جبر السبيعي", Phone = "+966554435688", CreatedOn = DateTime.Now });
            unitOfWork.WarshahReportRepository.Add(new WarshahReport { WarshahName = "مركز التسامح ", UserName = "محمد علي القحطاني", Phone = "+966537315555", CreatedOn = DateTime.Now });
            unitOfWork.WarshahReportRepository.Add(new WarshahReport { WarshahName = "مؤسسة اصلاح إكس ", UserName = "مؤسسة اصلاح إكس", Phone = "+966556846000", CreatedOn = DateTime.Now });
            unitOfWork.WarshahReportRepository.Add(new WarshahReport { WarshahName = "مركز ديزاين الرياض لصيانة السيارات ", UserName = "وردة مسعد سعد الصرير", Phone = "+966555000278", CreatedOn = DateTime.Now });
            unitOfWork.WarshahReportRepository.Add(new WarshahReport { WarshahName = "مركز بن طامي لصيانة السيارات ", UserName = "عبدالعزيز علي طامي ال طامي", Phone = "+966592599773", CreatedOn = DateTime.Now });
            unitOfWork.WarshahReportRepository.Add(new WarshahReport { WarshahName = "مؤسسة اروى راضي الدوسري ", UserName = "مشعل", Phone = "+966555277992", CreatedOn = DateTime.Now });
            unitOfWork.WarshahReportRepository.Add(new WarshahReport { WarshahName = "MASTER SPEED ", UserName = "مركزماستر", Phone = "+966501966866", CreatedOn = DateTime.Now });
            unitOfWork.WarshahReportRepository.Add(new WarshahReport { WarshahName = "مركز ديزاين الرياض لصيانة السيارات ", UserName = "خالد عبد الله", Phone = "+966533112111", CreatedOn = DateTime.Now });
            unitOfWork.WarshahReportRepository.Add(new WarshahReport { WarshahName = "مركز مؤسسة سرعة مبر ", UserName = "رامى عبد الله", Phone = "+966503275579", CreatedOn = DateTime.Now });
            unitOfWork.WarshahReportRepository.Add(new WarshahReport { WarshahName = "إنجاز لصيانة السيارات ", UserName = "سلطان الحربى", Phone = "+966558273277", CreatedOn = DateTime.Now });
            unitOfWork.WarshahReportRepository.Add(new WarshahReport { WarshahName = "المحركات الذهبية ", UserName = "غانم يوسف الحواس", Phone = "+966590779277", CreatedOn = DateTime.Now });



            unitOfWork.Save();


            return Ok();
        }


            [HttpGet, Route("GetReportDetailsWarshahTech")]
        public IActionResult GetReportDetailsWarshahTech(string warshahmobile)
        {


      



            var mobile = warshahmobile;
            var OldUser = warshahTechContext.Users.FirstOrDefault(a => a.MobileNo == mobile);
            decimal oldtotal = 0;
            decimal oldcountinvoice = 0;
            if (OldUser != null)
            {
                var oldwarshah = warshahTechContext.Warshahs.FirstOrDefault(a => a.WarshahId == OldUser.WarshahId);


                var oldinvoice = warshahTechContext.Invoices.Where(a => a.WarshahId == oldwarshah.WarshahId && a.InvoiceStatusId == 3).ToList();
                oldtotal = (decimal)oldinvoice.Sum(a => a.Total);
                oldcountinvoice = oldinvoice.Count();

            }


            var newmobile = "+" + warshahmobile; ;
            var currentUser = unitOfWork.UserRepository.GetMany(a => a.Phone == newmobile).FirstOrDefault();
            decimal newtotal = 0;
            decimal newcountinvoice = 0;


            if (currentUser != null)
            {
                var newwarshah = unitOfWork.WarshahRepository.GetMany(a => a.Id == currentUser.WarshahId).FirstOrDefault();
                var newinvoices = unitOfWork.InvoiceRepository.GetMany(a => a.WarshahId == newwarshah.Id && a.InvoiceStatusId == 2).ToHashSet();
                newtotal = newinvoices.Sum(a => a.Total);
                newcountinvoice = newinvoices.Count();
            }





            var Totalcount = oldcountinvoice + newcountinvoice;
            var Totalinvoice = oldtotal + newtotal;


            return Ok(new { TotalCount = Totalcount, TotalMoney = Totalinvoice });
        }


        // Get Clients  From V1 to V2  by warshah

        [HttpGet, Route("GetClientsByOldwarshah")]
        public IActionResult GetClientsByOldwarshah(string warshahmobile)

        {
            var OldUser = warshahTechContext.Users.FirstOrDefault(a => a.MobileNo == warshahmobile);
            var oldwarshah = warshahTechContext.Warshahs.FirstOrDefault(a => a.WarshahId == OldUser.WarshahId);


            var customers = warshahTechContext.WarshahCustomers.Where(a => a.WarshahId == oldwarshah.WarshahId).ToList();

            return Ok(customers.Count);

        }

            // Get Clients  From V1 to V2  by warshah

            [HttpGet, Route("AddClientsBywarshah")]
        public IActionResult AddClientsBywarshah(string warshahmobile)

        {
            var mobile = warshahmobile;

            var OldUser = warshahTechContext.Users.FirstOrDefault(a => a.MobileNo == mobile);
            var oldwarshah = warshahTechContext.Warshahs.FirstOrDefault(a => a.WarshahId == OldUser.WarshahId);


            var customers = warshahTechContext.WarshahCustomers.Where(a => a.WarshahId == oldwarshah.WarshahId).ToList();
            
            List<User> users = new List<User>();

            foreach ( var customer in customers )
            {
                var oldclient = warshahTechContext.Clients.Where(a=>a.UserId == customer.CustomerId).FirstOrDefault();
                if( oldclient != null)
                {
                    var pass = OldEncrptAndDecrypt.DecryptText(oldclient.Password);

                    var Password = EncryptANDDecrypt.EncryptText(pass);

                    var ouser = new User();

                    ouser.Address = oldclient.Address;
                    ouser.IsCompany = false;
                    ouser.FirstName = oldclient.FullName;
                    ouser.Phone = "+" + oldclient.MobileNo;
                    ouser.CivilId = oldclient.Idiqama;
                    ouser.Email = oldclient.Email;
                    ouser.IsPhoneConfirmed = true;
                    ouser.WarshahId = null;
                    ouser.Password = Password;
                    ouser.RoleId = 2;
                    ouser.CreatedOn = DateTime.Now;
                    users.Add(ouser);
                    unitOfWork.UserRepository.Add(ouser);
                    unitOfWork.Save();
                    var UserPermission = new UserPermission();
                    UserPermission.PermissionId = 5;
                    UserPermission.UserId = ouser.Id;
                    unitOfWork.UserpermissionRepository.Add(UserPermission);
                    unitOfWork.Save();

                }


            }

          

            return Ok(users);

        }



        // Get List  Clients  by warshah

        [HttpGet, Route("AddListClientsBywarshah")]
        public IActionResult AddListClientsBywarshah(int warshahid)
        {
            var warshah = unitOfWork.WarshahRepository.GetById(warshahid);

            

            var AllcarownersWarshah = warshahTechContext.WarshahCustomers.Where(a=>a.WarshahId == warshah.OldWarshahId).ToList();

            List<WarshahWithCarOwner> carOwners = new List<WarshahWithCarOwner>();    


            //if (warshah.IsOld == true)

            //{
            //    var invoices = unitOfWork.OldInvoicesRepository.GetMany(o => o.WarshahId == warshah.OldWarshahId).ToHashSet();
               foreach (var carowner in AllcarownersWarshah)

            {
                var carwarshah = new WarshahWithCarOwner();

                if(carowner.CustomerId != null)
                {
                    var client = warshahTechContext.Clients.Where(a => a.UserId == carowner.CustomerId).FirstOrDefault();
                    if (client != null)
                    {
                        var carmobile = client.MobileNo;
                        var newmibile = '+' + carmobile;
                        var ownerid = unitOfWork.UserRepository.GetMany(a => a.Phone == newmibile).FirstOrDefault().Id;

                        carwarshah.CarOwnerId = ownerid;
                        carwarshah.WarshahId = warshahid;
                        unitOfWork.WarshahCarOwnersRepository.Add(carwarshah);
                        carOwners.Add(carwarshah);
                    }
                }
               
              

            }



            unitOfWork.Save();




            return Ok(carOwners);
        }

        // Get List  Motor  by carowner

        [HttpGet, Route("AddListMotorsByClientId")]
        public IActionResult AddListMotorsByClientId(Guid oldwarshahid)
        {


            //var newwarshah = unitOfWork.WarshahRepository.GetMany(a => a.OldWarshahId == oldwarshahid).FirstOrDefault();

            //var mobile = "966535426540";

           
            //var oldwarshah = warshahTechContext.Warshahs.FirstOrDefault(a => a.WarshahId == OldUser.WarshahId);

            //var newmobile = "+966535426540";


            var allusers = warshahTechContext.WarshahCustomers.Where(a => a.WarshahId == oldwarshahid).ToList();
            List<Motors> AddMotors = new List<Motors>();

            foreach (var user in allusers)
            {
                if(user.CustomerId != null)
                {
                    var OldUser = warshahTechContext.Clients.FirstOrDefault(a => a.UserId == user.CustomerId);
                    if (OldUser != null)
                    {
                        var newmobile = "+" + OldUser.MobileNo;
                        var currentUser = unitOfWork.UserRepository.GetMany(a => a.Phone == newmobile).FirstOrDefault();
                        var oldmotors = warshahTechContext.Motors.Where(a => a.MotorOwner == OldUser.UserId).ToList();




                        foreach (var motor in oldmotors)
                        {
                            Motors Newpart = new Motors();

                            Newpart.ChassisNo = motor.ChassisNo;
                            if(motor.PlateNo == null)
                            {
                                motor.PlateNo = "0";
                            }
                            Newpart.PlateNo = motor.PlateNo;
                            Newpart.CarOwnerId = currentUser.Id;

                            if (motor.MotorYearId != null || motor.MotorYearId! < 0)
                            {
                                var oldmake = warshahTechContext.MotorYears.Where(a => a.MotorYearId == motor.MotorYearId).FirstOrDefault();
                                if (oldmake != null)
                                {
                                    var yearname = System.Convert.ToInt16(oldmake.YearName);

                                    var newmake = unitOfWork.MotorYearRepository.GetMany(a => a.Year == yearname).FirstOrDefault();

                                    if (newmake != null && newmake.Id != 0)
                                    {
                                        Newpart.MotorYearId = newmake.Id;
                                    }
                                    else
                                    {
                                        Newpart.MotorYearId = 2;
                                    }
                                }

                            }

                            else
                            {
                                Newpart.MotorYearId = 2;
                            }


                            if (motor.MotorColorId != null || motor.MotorColorId! < 0)
                            {
                                var oldmake = warshahTechContext.MotorColors.Where(a => a.MotorColorId == motor.MotorColorId).FirstOrDefault();
                                if (oldmake != null)
                                {
                                    //var yearname = System.Convert.ToInt16(oldmake.ColorNameAr);

                                    var newmake = unitOfWork.MotorColorRepository.GetMany(a => a.ColorNameAr == oldmake.CorlorName).FirstOrDefault();

                                    if (newmake != null && newmake.Id != 0)
                                    {
                                        Newpart.MotorColorId = newmake.Id;
                                    }
                                    else
                                    {
                                        Newpart.MotorColorId = 2;
                                    }
                                }

                            }

                            else
                            {
                                Newpart.MotorColorId = 2;
                            }

                            var make = warshahTechContext.MotorYears.Where(a => a.MotorYearId == motor.MotorYearId).FirstOrDefault().MotorMakeId;

                            if (make != null || make! < 0)
                            {
                                var oldmake = warshahTechContext.MotorMakes.Where(a => a.MotorMakeId == make).FirstOrDefault();
                                if (oldmake != null)
                                {
                                    var newmake = unitOfWork.MotorMakeRepository.GetMany(a => a.MakeNameAr == oldmake.MakeName).FirstOrDefault();

                                    if (newmake != null)
                                    {
                                        Newpart.MotorMakeId = newmake.Id;
                                    }

                                    else
                                    {
                                        Newpart.MotorMakeId = 2;
                                    }
                                }


                            }


                            else
                            {
                                Newpart.MotorMakeId = 2;
                            }





                            var model = warshahTechContext.MotorYears.Where(a => a.MotorYearId == motor.MotorYearId).FirstOrDefault().MotorModelId;



                            if (model != null || model! < 0)
                            {
                                var oldmake = warshahTechContext.MotorModels.Where(a => a.MotorModelId == model).FirstOrDefault();

                                if (oldmake != null)
                                {
                                    var newmake = unitOfWork.MotorModelRepository.GetMany(a => a.ModelNameAr == oldmake.ModelName).FirstOrDefault();

                                    if (newmake != null)
                                    {
                                        Newpart.MotorModelId = newmake.Id;
                                    }

                                    else
                                    {
                                        Newpart.MotorModelId = 2;
                                    }

                                }

                            }

                            if (model == -1)
                            {
                                Newpart.MotorModelId = 52;
                            }

                            else
                            {
                                Newpart.MotorModelId = 2;
                            }



                            AddMotors.Add(Newpart);
                            unitOfWork.MotorsRepository.Add(Newpart);







                        }
                    }
                   

                }
               

             

            }
            unitOfWork.Save();
            return Ok(AddMotors);






        }




        [HttpGet, Route("UpdateListMotorsByClientId")]
        public IActionResult UpdateListMotorsByClientId(Guid oldwarshah)
        {

            //var allmotors = unitOfWork.

            var motors = warshahTechContext.Motors.Where(a=>a.WarshahId == oldwarshah).ToList();

            foreach(var motor in motors)
            {

                var currentmotor = unitOfWork.MotorsRepository.GetMany(a => a.PlateNo == motor.PlateNo).FirstOrDefault();

                if(currentmotor != null)
                {
                    var oldmodel = warshahTechContext.MotorYears.Where(a => a.MotorYearId == motor.MotorYearId).FirstOrDefault().MotorModelId;
                    var model = warshahTechContext.MotorModels.Where(a => a.MotorModelId == oldmodel).FirstOrDefault();
                    var newmodwl = unitOfWork.MotorModelRepository.GetMany(a => a.ModelNameAr == model.ModelName).FirstOrDefault();
                    if(newmodwl != null)
                    {
                        currentmotor.MotorModelId = newmodwl.Id;
                        currentmotor.UpdatedOn = DateTime.Now;
                        unitOfWork.MotorsRepository.Update(currentmotor);
                        unitOfWork.Save();
                        unitOfWork.MotorsRepository.NoTracking(currentmotor);
                    }

                    if(newmodwl == null)
                    {
                        currentmotor.MotorModelId = 2;
                        currentmotor.UpdatedOn = DateTime.Now;
                        unitOfWork.MotorsRepository.Update(currentmotor);
                        unitOfWork.Save();
                        unitOfWork.MotorsRepository.NoTracking(currentmotor);


                    }
                   
                }
                //var oldmotors = warshahTechContext.Motors.Where(a => a.PlateNo == palteno).FirstOrDefault();
                
            }

           
            return Ok();






        }

    }
}
