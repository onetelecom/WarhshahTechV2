using AutoMapper;
using BL.Infrastructure;
using DL.DTOs.InspectionDTOs;
using DL.Entities;
using HELPER;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WarshahTechV2.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyPolicy")]
    public class InspectionSectionController : ControllerBase
    {
         // take instance from IUnitOfWork
        private readonly IUnitOfWork _uow;

        // take instance from IMapper
        private readonly IMapper _mapper;

        // Constractor for controller 
        public InspectionSectionController(IUnitOfWork uow, IMapper mapper)
        {
            _mapper = mapper;
            _uow = uow;
        }


        #region InspectionSectionCRUD

        //Create InspectionSection
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Admin)]
        [HttpPost, Route("CreateInspectionSection")]
        public IActionResult CreateInspectionSection(InspectionSectionDTO inspectionSectionDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var Section = _mapper.Map<DL.Entities.InspectionSection>(inspectionSectionDTO);
                    if (Section.WarshahId != null)
                    {
                        Section.IsCommon = false;
                    }
                    Section.IsDeleted = false;
                    _uow.InspectionSectionRepository.Add(Section);
                    _uow.Save();
                    return Ok(Section);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid SectionName");
        }

        // Get All InspectionSection

        // Add Defualt sections to Template


        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]
        [HttpPost, Route("AddDefualtSection")]
        public IActionResult AddDefualtSection(int? warshahid, int? templateid)
        {


            // add items to section 1

            var Section1 = new InspectionSection
            {
                SectionNameAr = "الماكينة ",
                SectionNameEn = " Machine  ",
                InspectionTemplateId = templateid,
                IsCommon = false,
                WarshahId = warshahid ,
                IsActive = true

            };

            _uow.InspectionSectionRepository.Add(Section1);
            _uow.Save();

            var t1 = new InspectionItem
            {

                ItemNameAr = "لصوفة الامامية",
                ItemNameEn = " Front wool ",
                InspectionSectionId = Section1.Id,
                IsCommon = false,
                WarshahId = warshahid
                  ,
                Status = true
            };
            var t2 = new InspectionItem
            {

                ItemNameAr = "حالة الماكينة",
                ItemNameEn = " Machine condition ",
                InspectionSectionId = Section1.Id,
                IsCommon = false,
                WarshahId = warshahid
                  ,
                Status = true
            };
            var t3 = new InspectionItem
            {

                ItemNameAr = "الصوفة الخلفية",
                ItemNameEn = " Rear wool ",
                InspectionSectionId = Section1.Id,
                IsCommon = false,
                WarshahId = warshahid
                  ,
                Status = true
            };
            var t4 = new InspectionItem
            {

                ItemNameAr = "تصفية ماكينة",
                ItemNameEn = " Filter machine ",
                InspectionSectionId = Section1.Id,
                IsCommon = false,
                WarshahId = warshahid
                  ,
                Status = true
            };
            var t5 = new InspectionItem
            {

                ItemNameAr = "كرسي الماكينة",
                ItemNameEn = " Machine chair ",
                InspectionSectionId = Section1.Id,
                IsCommon = false,
                WarshahId = warshahid
                 ,
                Status = true
            };
            var t6 = new InspectionItem
            {

                ItemNameAr = "وجه كرتير الماكينة",
                ItemNameEn = " The face of the machine carter ",
                InspectionSectionId = Section1.Id,
                IsCommon = false,
                WarshahId = warshahid
                  ,
                Status = true
            };
            var t7 = new InspectionItem
            {

                ItemNameAr = "وجه غطاء البلوف",
                ItemNameEn = "The face of the cabbage cover ",
                InspectionSectionId = Section1.Id,
                IsCommon = false,
                WarshahId = warshahid
                  ,
                Status = true
            };
            var t8 = new InspectionItem
            {

                ItemNameAr = "قاعدة فلتر الزيت ",
                ItemNameEn = " Oil filter base ",
                InspectionSectionId = Section1.Id,
                IsCommon = false,
                WarshahId = warshahid
                  ,
                Status = true
            };

            var t9 = new InspectionItem
            {

                ItemNameAr = "وجه الثلاجة ",
                ItemNameEn = "The face of the fridge",
                InspectionSectionId = Section1.Id,
                IsCommon = false,
                WarshahId = warshahid
                  ,
                Status = true
            };

            var t10 = new InspectionItem
            {

                ItemNameAr = "تهريبات ماء ",
                ItemNameEn = " Water smuggling",
                InspectionSectionId = Section1.Id,
                IsCommon = false,
                WarshahId = warshahid
                  ,
                Status = true
            };

            var t11 = new InspectionItem
            {
                ItemNameAr = "رادياتير الماء ",
                ItemNameEn = " Water radiator",
                InspectionSectionId = Section1.Id,
                IsCommon = false,
                WarshahId = warshahid
                  ,
                Status = true
            };

            var t12 = new InspectionItem
            {

                ItemNameAr = " وجه صدر الماكينة امام / خلفي ",
                ItemNameEn = "Front/rear face of the machine's chest",
                InspectionSectionId = Section1.Id,
                IsCommon = false,
                WarshahId = warshahid
                  ,
                Status = true
            };

            var t13 = new InspectionItem
            {

                ItemNameAr = "طرمبة الماء ",
                ItemNameEn = "Water pump",
                InspectionSectionId = Section1.Id,
                IsCommon = false,
                WarshahId = warshahid
                  ,
                Status = true
            };

            var t14 = new InspectionItem
            {

                ItemNameAr = "السيور ",
                ItemNameEn = "Belts",
                InspectionSectionId = Section1.Id,
                IsCommon = false,
                WarshahId = warshahid
                  ,
                Status = true
            };

            var t15 = new InspectionItem
            {
                ItemNameAr = "صفايه البنزين ",
                ItemNameEn = "Gas can",
                InspectionSectionId = Section1.Id,
                IsCommon = false,
                WarshahId = warshahid
                  ,
                Status = true
            };

            var t16 = new InspectionItem
            {

                ItemNameAr = "فلتر الهواء  ",
                ItemNameEn = " Air filter",
                InspectionSectionId = Section1.Id,
                IsCommon = false,
                WarshahId = warshahid
                  ,
                Status = true
            };
            // add items to section 2 

            var Section2 = new InspectionSection
            {
                SectionNameAr = "الجيربوكس",
                SectionNameEn = " Limebox  ",
                InspectionTemplateId = templateid,
                IsCommon = false,
                WarshahId = warshahid,
                IsActive = true

            };

            _uow.InspectionSectionRepository.Add(Section2);
            _uow.Save();

            var t17 = new InspectionItem
            {

                ItemNameAr = "وجه طرمبة الزيت",
                ItemNameEn = "The face of the oil pump",
                InspectionSectionId = Section2.Id,
                IsCommon = false,
                WarshahId = warshahid
                  ,
                Status = true
            };
            var t18 = new InspectionItem
            {

                ItemNameAr = " الصوفة الامامية",
                ItemNameEn = "Front wool",
                InspectionSectionId = Section2.Id,
                IsCommon = false,
                WarshahId = warshahid
                  ,
                Status = true
            };
            var t19 = new InspectionItem
            {

                ItemNameAr = "الصوفة الخلفية",
                ItemNameEn = "Rear wool",
                InspectionSectionId = Section2.Id,
                IsCommon = false,
                WarshahId = warshahid
                  ,
                Status = true
            };
            var t20 = new InspectionItem
            {

                ItemNameAr = "كرسي الجير بوكس",
                ItemNameEn = "Lime Box Chair",
                InspectionSectionId = Section2.Id,
                IsCommon = false,
                WarshahId = warshahid
                  ,
                Status = true
            };
            var t21 = new InspectionItem
            {

                ItemNameAr = "وجه كرتير الجير ",
                ItemNameEn = "The face of the lime carter.",
                InspectionSectionId = Section2.Id,
                IsCommon = false,
                WarshahId = warshahid
                  ,
                Status = true
            };
            var t22 = new InspectionItem
            {
                ItemNameAr = "صوف عصا الجير  ",
                ItemNameEn = "Lime stick wool",
                InspectionSectionId = Section2.Id,
                IsCommon = false,
                WarshahId = warshahid
                  ,
                Status = true
            };
            var t23 = new InspectionItem
            {

                ItemNameAr = "ماسورة مبرد الجير ",
                ItemNameEn = "Lime cooler pipe",
                InspectionSectionId = Section2.Id,
                IsCommon = false,
                WarshahId = warshahid
                  ,
                Status = true
            };
            var t24 = new InspectionItem
            {

                ItemNameAr = "حالة الجير ",
                ItemNameEn = "Lime case",
                InspectionSectionId = Section2.Id,
                IsCommon = false,
                WarshahId = warshahid
                  ,
                Status = true
            };

            // add items to section 3

            var Section3 = new InspectionSection
            {
                SectionNameAr = "نظام الكهرباء",
                SectionNameEn = " Electricity system ",
                InspectionTemplateId = templateid,
                IsCommon = false,
                WarshahId = warshahid,
                IsActive = true
            };

            _uow.InspectionSectionRepository.Add(Section3);
            _uow.Save();


            var t25 = new InspectionItem
            {

                ItemNameAr = " البطارية ",
                ItemNameEn = "Battery",
                InspectionSectionId = Section3.Id,
                IsCommon = false,
                WarshahId = warshahid
                  ,
                Status = true
            };
            var t26 = new InspectionItem
            {

                ItemNameAr = "الدينمو",
                ItemNameEn = "the dynamo",
                InspectionSectionId = Section3.Id,
                IsCommon = false,
                WarshahId = warshahid
                  ,
                Status = true
            };
            var t27 = new InspectionItem
            {

                ItemNameAr = "السلف ",
                ItemNameEn = "Predecessor",
                InspectionSectionId = Section3.Id,
                IsCommon = false,
                WarshahId = warshahid
                  ,
                Status = true
            };
            var t28 = new InspectionItem
            {

                ItemNameAr = "الانوار ",
                ItemNameEn = "Lights",
                InspectionSectionId = Section3.Id,
                IsCommon = false,
                WarshahId = warshahid
                  ,
                Status = true
            };
            var t29 = new InspectionItem
            {

                ItemNameAr = "لمكيف كمبروسر ",
                ItemNameEn = "For a kambroser air conditioner.  ",
                InspectionSectionId = Section3.Id,
                IsCommon = false,
                WarshahId = warshahid
                 ,
                Status = true
            };
            var t30 = new InspectionItem
            {

                ItemNameAr = "السنتر لوك ",
                ItemNameEn = "Center lock",
                InspectionSectionId = Section3.Id,
                IsCommon = false,
                WarshahId = warshahid
                 ,
                Status = true
            };
            var t31 = new InspectionItem
            {

                ItemNameAr = "المساحات ",
                ItemNameEn = "wiper ",
                InspectionSectionId = Section3.Id,
                IsCommon = false,
                WarshahId = warshahid
                 ,
                Status = true
            };
            var t32 = new InspectionItem
            {

                ItemNameAr = "شاشة المكيف ",
                ItemNameEn = "Air conditioner screen",
                InspectionSectionId = Section3.Id,
                IsCommon = false,
                WarshahId = warshahid
                 ,
                Status = true
            };

            var t33 = new InspectionItem
            {

                ItemNameAr = "كودات الكمبيوتر  ",
                ItemNameEn = "Computer codes",
                InspectionSectionId = Section3.Id,
                IsCommon = false,
                WarshahId = warshahid
                ,
                Status = true
            };

            var t34 = new InspectionItem
            {

                ItemNameAr = "ثلاجة مكيف",
                ItemNameEn = " Air-conditioned refrigerator ",
                InspectionSectionId = Section3.Id,
                IsCommon = false,
                WarshahId = warshahid
                ,
                Status = true
            };

            var t35 = new InspectionItem
            {

                ItemNameAr = "رديتر مكيف",
                ItemNameEn = " Redditor air conditioner ",
                InspectionSectionId = Section3.Id,
                IsCommon = false,
                WarshahId = warshahid
                ,
                Status = true
            };




            // add items to section 4

            var Section4 = new InspectionSection
            {
                SectionNameAr = "السوائل",
                SectionNameEn = " Fluids  ",
                InspectionTemplateId = templateid,
                IsCommon = false,
                WarshahId = warshahid,
                IsActive = true
            };

            _uow.InspectionSectionRepository.Add(Section4);
            _uow.Save();


            var t36 = new InspectionItem
            {

                ItemNameAr = "زيت الماكينة ",
                ItemNameEn = "Machine oil",
                InspectionSectionId = Section4.Id,
                IsCommon = false,
                WarshahId = warshahid
                  ,
                Status = true
            };
            var t37 = new InspectionItem
            {

                ItemNameAr = "زيت الجير ",
                ItemNameEn = "Lime oil",
                InspectionSectionId = Section4.Id,
                IsCommon = false,
                WarshahId = warshahid
                  ,
                Status = true
            };

            var t38 = new InspectionItem
            {

                ItemNameAr = "زيت الفرامل  ",
                ItemNameEn = "Brake oil",
                InspectionSectionId = Section4.Id,
                IsCommon = false,
                WarshahId = warshahid
                  ,
                Status = true
            };

            var t39 = new InspectionItem
            {

                ItemNameAr = "ماء الردياتير ",
                ItemNameEn = "Water of the radyter",
                InspectionSectionId = Section4.Id,
                IsCommon = false,
                WarshahId = warshahid
                  ,
                Status = true
            };
            var t40 = new InspectionItem
            {

                ItemNameAr = "ماء المساحات",
                ItemNameEn = " Water spaces ",
                InspectionSectionId = Section4.Id,
                IsCommon = false,
                WarshahId = warshahid
                  ,
                Status = true
            };

            // template 1     section = 5
            var Section5 = new InspectionSection
            {
                SectionNameAr = "أسفل السيارة",
                SectionNameEn = " Under the Car ",
                InspectionTemplateId = templateid,
                IsCommon = false,
                WarshahId = warshahid,
                IsActive = true
            };

            _uow.InspectionSectionRepository.Add(Section5);
            _uow.Save();


            var t41 = new InspectionItem
            {

                ItemNameAr = "الأذرعة ",
                ItemNameEn = " Arms ",
                InspectionSectionId = Section5.Id,
                IsCommon = false,
                WarshahId = warshahid
                  ,
                Status = true
            };
            var t42 = new InspectionItem
            {

                ItemNameAr = "الركبة اليمنى واليسرى ",
                ItemNameEn = "Right and left knee",
                InspectionSectionId = Section5.Id,
                IsCommon = false,
                WarshahId = warshahid
                  ,
                Status = true
            };
            var t43 = new InspectionItem
            {

                ItemNameAr = "ذراع شاص",
                ItemNameEn = "A chasing arm",
                InspectionSectionId = Section5.Id,
                IsCommon = false,
                WarshahId = warshahid
                  ,
                Status = true
            };
            var t44 = new InspectionItem
            {

                ItemNameAr = "ذراع الدودة ",
                ItemNameEn = "Worm arm",
                InspectionSectionId = Section5.Id,
                IsCommon = false,
                WarshahId = warshahid
                  ,
                Status = true
            };
            var t45 = new InspectionItem
            {

                ItemNameAr = "علبة الدركسيون (طلمبة)",
                ItemNameEn = "Gendarmerie box (Talamba)",
                InspectionSectionId = Section5.Id,
                IsCommon = false,
                WarshahId = warshahid
                  ,
                Status = true
            };
            var t46 = new InspectionItem
            {

                ItemNameAr = "لبات الدركسيون",
                ItemNameEn = "Pat Draconian",
                InspectionSectionId = Section5.Id,
                IsCommon = false,
                WarshahId = warshahid
                  ,
                Status = true
            };
            var t47 = new InspectionItem
            {

                ItemNameAr = "الدودة",
                ItemNameEn = "The worm",
                InspectionSectionId = Section5.Id,
                IsCommon = false,
                WarshahId = warshahid
                  ,
                Status = true
            };
            var t48 = new InspectionItem
            {

                ItemNameAr = "عامود الدركسيون",
                ItemNameEn = "Gendarmerie column",
                InspectionSectionId = Section5.Id,
                IsCommon = false,
                WarshahId = warshahid
                  ,
                Status = true
            };
            var t49 = new InspectionItem
            {

                ItemNameAr = "المساعدات الامامية / كراسي مساعدات",
                ItemNameEn = "Front aid / aid chairs",
                InspectionSectionId = Section5.Id,
                IsCommon = false,
                WarshahId = warshahid
                  ,
                Status = true
            };
            var t50 = new InspectionItem
            {

                ItemNameAr = "المساعدات الخلفية",
                ItemNameEn = "Rear aid",
                InspectionSectionId = Section5.Id,
                IsCommon = false,
                WarshahId = warshahid
                  ,
                Status = true
            };
            var t51 = new InspectionItem
                {

                ItemNameAr = "مسمام عامود التوازن",
                ItemNameEn = "Balance column valve",
                InspectionSectionId = Section5.Id,
                    IsCommon = false,
                    WarshahId = warshahid
                  ,
                    Status = true
                };
            var t52 = new InspectionItem
            {

                ItemNameAr = "صليب عامود الدوران",
                ItemNameEn = "Spin column cross",
                InspectionSectionId = Section5.Id,
                IsCommon = false,
                WarshahId = warshahid
                  ,
                Status = true
            };
            var t53 = new InspectionItem
            {

                ItemNameAr = "جلد المقص العلوي",
                ItemNameEn = "Upper scissor skin",
                InspectionSectionId = Section5.Id,
                IsCommon = false,
                WarshahId = warshahid
                  ,
                Status = true
            };
            var t54 = new InspectionItem
            {

                ItemNameAr = "جلد المقص السفلي",
                ItemNameEn = "Leather lower scissors",
                InspectionSectionId = Section5.Id,
                IsCommon = false,
                WarshahId = warshahid
                  ,
                Status = true
            };
            var t55 = new InspectionItem
            {

                ItemNameAr = "اليايات ",
                ItemNameEn = "The Yayat",
                InspectionSectionId = Section5.Id,
                IsCommon = false,
                WarshahId = warshahid
                  ,
                Status = true
            };

            // template 1 section = 6
            var Section6 = new InspectionSection
            {
                SectionNameAr = "نظام الفرامل",
                SectionNameEn = " Brake system ",
                InspectionTemplateId = templateid,
                IsCommon = false,
                WarshahId = warshahid,
                IsActive = true

            };

            _uow.InspectionSectionRepository.Add(Section6);
            _uow.Save();

            var t56 = new InspectionItem
            {

                ItemNameAr = "رامل (امامية / خلفية",
                ItemNameEn = "Ramel (front/back)",
                InspectionSectionId = Section6.Id,
                IsCommon = false,
                WarshahId = warshahid
                  ,
                Status = true
            };


            var t57 = new InspectionItem
            {

                ItemNameAr = "هوب (امامي / خلفي)  ",
                ItemNameEn = "Hope (front/back) ",
                InspectionSectionId = Section6.Id,
                IsCommon = false,
                WarshahId = warshahid
                ,
                Status = true
            };

            var t58 = new InspectionItem
            {

                ItemNameAr = "فلنجات (امامية / خلفية) ",
                ItemNameEn = "Flangat (front/back)",
                InspectionSectionId = Section6.Id,
                IsCommon = false,
                WarshahId = warshahid
                ,
                Status = true
            };
            var t59 = new InspectionItem
            {

                ItemNameAr = "علبة الفرامل الرئيسية",
                ItemNameEn = "Main brake box",
                InspectionSectionId = Section6.Id,
                IsCommon = false,
                WarshahId = warshahid
                ,
                Status = true
            };
            var t60 = new InspectionItem
            {

                ItemNameAr = "باكم الفرامل ",
                ItemNameEn = "bakem - brakes",
                InspectionSectionId = Section6.Id,
                IsCommon = false,
                WarshahId = warshahid
                ,
                Status = true
            };

            var t61 = new InspectionItem
            {

                ItemNameAr = "سلندر (امامي / خلفي) ",
                ItemNameEn = "Slender (front/back)",
                InspectionSectionId = Section6.Id,
                IsCommon = false,
                WarshahId = warshahid
                ,
                Status = true
            };
            var t62 = new InspectionItem
            {

                ItemNameAr = "سلك فرامل اليد (جلنط) ",
                ItemNameEn = "Hand brake wire (galent)",
                InspectionSectionId = Section6.Id,
                IsCommon = false,
                WarshahId = warshahid
                            ,
                Status = true
            };
            var t63 = new InspectionItem
            {

                ItemNameAr = "ليات فرامل",
                ItemNameEn = "Brake nights",
                InspectionSectionId = Section6.Id,
                IsCommon = false,
                WarshahId = warshahid
                            ,
                Status = true
            };



            // template 1 section = 7
            var Section7 = new InspectionSection
            {
                SectionNameAr = "الدفرنس / الكورونا",
                SectionNameEn = " Aldverse / Corona ",
                InspectionTemplateId = templateid,
                IsCommon = false,
                WarshahId = warshahid,
                IsActive = true

            };

            _uow.InspectionSectionRepository.Add(Section7);
            _uow.Save();

            var t64 = new InspectionItem
            {

                ItemNameAr = "العكوس الامامية ",
                ItemNameEn = "Forward inverse",
                InspectionSectionId = Section7.Id,
                IsCommon = false,
                WarshahId = warshahid
                  ,
                Status = true
            };
            var t65 = new InspectionItem
            {

                ItemNameAr = "العكوس الخلفية",
                ItemNameEn = " Rear inverse",
                InspectionSectionId = Section7.Id,
                IsCommon = false,
                WarshahId = warshahid
                  ,
                Status = true
            };
            var t66 = new InspectionItem
            {

                ItemNameAr = "العكوس الدفرنس",
                ItemNameEn = " French inverse",
                InspectionSectionId = Section7.Id,
                IsCommon = false,
                WarshahId = warshahid
                  ,
                Status = true
            };
            var t67 = new InspectionItem
            {

                ItemNameAr = "دفرنس امام / خلفي",
                ItemNameEn = " Frances in front/back",
                InspectionSectionId = Section7.Id,
                IsCommon = false,
                WarshahId = warshahid
                  ,
                Status = true
            };
            var t68 = new InspectionItem
            {

                ItemNameAr = "صوفة عكس (امامي / خلفي)",
                ItemNameEn = " Reverse wool (front/back)",
                InspectionSectionId = Section7.Id,
                IsCommon = false,
                WarshahId = warshahid
                  ,
                Status = true
            };
           
            // template 1 section = 8
            var Section8 = new InspectionSection
            {
                SectionNameAr = "القسم الداخلي",
                SectionNameEn = " Internal Section ",
                InspectionTemplateId = templateid,
                IsCommon = false,
                WarshahId = warshahid,
                IsActive = true

            };

            _uow.InspectionSectionRepository.Add(Section8);
            _uow.Save();


            var t69 = new InspectionItem
            {

                ItemNameAr = "مقاعد وأحزمة",
                ItemNameEn = " Seats and belts",
                InspectionSectionId = Section8.Id,
                IsCommon = false,
                WarshahId = warshahid
                  ,
                Status = true
            };
            var t70 = new InspectionItem
            {

                ItemNameAr = "الضوابط والمفاتيح الداخلية",
                ItemNameEn = " Internal controls and keys",
                InspectionSectionId = Section8.Id,
                IsCommon = false,
                WarshahId = warshahid
                ,
                Status = true
            };
            var t71 = new InspectionItem
            {

                ItemNameAr = "فتحة السقف والنوافذ ",
                ItemNameEn = " Sunroof and windows ",
                InspectionSectionId = Section8.Id,
                IsCommon = false,
                WarshahId = warshahid
                ,
                Status = true
            };
            var t72 = new InspectionItem
            {

                ItemNameAr = "عداد الوقود ودرجة الحرارة",
                ItemNameEn = " Fuel meter and temperature",
                InspectionSectionId = Section8.Id,
                IsCommon = false,
                WarshahId = warshahid
                ,
                Status = true
            };
            var t73 = new InspectionItem
            {

                ItemNameAr = "لوحة القيادة وأجهزة القياس",
                ItemNameEn = " Dashboard and measuring devices",
                InspectionSectionId = Section8.Id,
                IsCommon = false,
                WarshahId = warshahid
                ,
                Status = true
            };
            var t74 = new InspectionItem
            {

                ItemNameAr = "نظام راديو / موسيقى",
                ItemNameEn = " Radio/Music System",
                InspectionSectionId = Section8.Id,
                IsCommon = false,
                WarshahId = warshahid
                ,
                Status = true
            };

            var t75 = new InspectionItem
            {

                ItemNameAr = "وسائد هوائية",
                ItemNameEn = " Airbags",
                InspectionSectionId = Section8.Id,
                IsCommon = false,
                WarshahId = warshahid
                ,
                Status = true
            };

            var t76 = new InspectionItem
            {
                ItemNameAr = "إمالة / قفل عجلة القيادة",
                ItemNameEn = " Tilt/lock steering wheel",
                InspectionSectionId = Section8.Id,
                IsCommon = false,
                WarshahId = warshahid
       ,
                Status = true
            };
            var t77 = new InspectionItem
            {
                ItemNameAr = "المرايا",
                ItemNameEn = " Mirrors",
                InspectionSectionId = Section8.Id,
                IsCommon = false,
                WarshahId = warshahid
     ,
                Status = true
            };

            // template 1 section = 9
            var Section9 = new InspectionSection
            {
                SectionNameAr = "الهيكل الخارجي",
                SectionNameEn = " Exterior structure ",
                InspectionTemplateId = templateid,
                IsCommon = false,
                WarshahId = warshahid,
                IsActive = true

            };

            _uow.InspectionSectionRepository.Add(Section9);
            _uow.Save();

            var t78 = new InspectionItem
            {

                ItemNameAr = "الجهة الامامية",
                ItemNameEn = " Front",
                InspectionSectionId = Section9.Id,
                IsCommon = false,
                WarshahId = warshahid
                  ,
                Status = true
            };

            var t79 = new InspectionItem
            {

                ItemNameAr = "الجهة الخلفية",
                ItemNameEn = " Back side",
                InspectionSectionId = Section9.Id,
                IsCommon = false,
                WarshahId = warshahid
                ,
                Status = true
            };

            var t80 = new InspectionItem
            {

                ItemNameAr = "الجهة اليمنى",
                ItemNameEn = " Right side",
                InspectionSectionId = Section9.Id,
                IsCommon = false,
                WarshahId = warshahid
                ,
                Status = true
            };

            var t81 = new InspectionItem
            {

                ItemNameAr = "الجهة اليسرى",
                ItemNameEn = " Left side",
                InspectionSectionId = Section9.Id,
                IsCommon = false,
                WarshahId = warshahid
                ,
                Status = true
            };

            var t82 = new InspectionItem
            {

                ItemNameAr = "اعلى السيارة ( التنده)",
                ItemNameEn = " Top of the car ( Ceiling)",
                InspectionSectionId = Section9.Id,
                IsCommon = false,
                WarshahId = warshahid
                ,
                Status = true
            };

            _uow.InspectionItemsRepository.Add(t1);
            _uow.InspectionItemsRepository.Add(t2);
            _uow.InspectionItemsRepository.Add(t3);
            _uow.InspectionItemsRepository.Add(t4);
            _uow.InspectionItemsRepository.Add(t5);
            _uow.InspectionItemsRepository.Add(t6);
            _uow.InspectionItemsRepository.Add(t7);
            _uow.InspectionItemsRepository.Add(t8);
            _uow.InspectionItemsRepository.Add(t9);
            _uow.InspectionItemsRepository.Add(t10);
            _uow.InspectionItemsRepository.Add(t11);
            _uow.InspectionItemsRepository.Add(t12);
            _uow.InspectionItemsRepository.Add(t13);
            _uow.InspectionItemsRepository.Add(t14);
            _uow.InspectionItemsRepository.Add(t15);
            _uow.InspectionItemsRepository.Add(t16);
            _uow.InspectionItemsRepository.Add(t17);
            _uow.InspectionItemsRepository.Add(t18);
            _uow.InspectionItemsRepository.Add(t19);
            _uow.InspectionItemsRepository.Add(t20);
            _uow.InspectionItemsRepository.Add(t21);
            _uow.InspectionItemsRepository.Add(t22);
            _uow.InspectionItemsRepository.Add(t23);
            _uow.InspectionItemsRepository.Add(t24);
            _uow.InspectionItemsRepository.Add(t25);
            _uow.InspectionItemsRepository.Add(t26);
            _uow.InspectionItemsRepository.Add(t27);
            _uow.InspectionItemsRepository.Add(t28);
            _uow.InspectionItemsRepository.Add(t29);
            _uow.InspectionItemsRepository.Add(t30);
            _uow.InspectionItemsRepository.Add(t31);
            _uow.InspectionItemsRepository.Add(t32);
            _uow.InspectionItemsRepository.Add(t33);
            _uow.InspectionItemsRepository.Add(t34);
            _uow.InspectionItemsRepository.Add(t35);
            _uow.InspectionItemsRepository.Add(t36);
            _uow.InspectionItemsRepository.Add(t37);
            _uow.InspectionItemsRepository.Add(t38);
            _uow.InspectionItemsRepository.Add(t39);
            _uow.InspectionItemsRepository.Add(t40);
            _uow.InspectionItemsRepository.Add(t41);
            _uow.InspectionItemsRepository.Add(t42);
            _uow.InspectionItemsRepository.Add(t43);
            _uow.InspectionItemsRepository.Add(t44);
            _uow.InspectionItemsRepository.Add(t45);
            _uow.InspectionItemsRepository.Add(t46);
            _uow.InspectionItemsRepository.Add(t47);
            _uow.InspectionItemsRepository.Add(t48);
            _uow.InspectionItemsRepository.Add(t49);
            _uow.InspectionItemsRepository.Add(t50);
            _uow.InspectionItemsRepository.Add(t51);
            _uow.InspectionItemsRepository.Add(t52);
            _uow.InspectionItemsRepository.Add(t53);
            _uow.InspectionItemsRepository.Add(t54);
            _uow.InspectionItemsRepository.Add(t56);
            _uow.InspectionItemsRepository.Add(t57);
            _uow.InspectionItemsRepository.Add(t55);
            _uow.InspectionItemsRepository.Add(t58);
            _uow.InspectionItemsRepository.Add(t59);
            _uow.InspectionItemsRepository.Add(t60);
            _uow.InspectionItemsRepository.Add(t61);
            _uow.InspectionItemsRepository.Add(t62);
            _uow.InspectionItemsRepository.Add(t63);
            _uow.InspectionItemsRepository.Add(t64);
            _uow.InspectionItemsRepository.Add(t65);
            _uow.InspectionItemsRepository.Add(t66);
            _uow.InspectionItemsRepository.Add(t67);
            _uow.InspectionItemsRepository.Add(t68);
            _uow.InspectionItemsRepository.Add(t69);
            _uow.InspectionItemsRepository.Add(t70);
            _uow.InspectionItemsRepository.Add(t71);
            _uow.InspectionItemsRepository.Add(t72);
            _uow.InspectionItemsRepository.Add(t73);
            _uow.InspectionItemsRepository.Add(t74);
            _uow.InspectionItemsRepository.Add(t75);
            _uow.InspectionItemsRepository.Add(t76);
            _uow.InspectionItemsRepository.Add(t77);
            _uow.InspectionItemsRepository.Add(t78);
            _uow.InspectionItemsRepository.Add(t79);
            _uow.InspectionItemsRepository.Add(t80);
            _uow.InspectionItemsRepository.Add(t81);
            _uow.InspectionItemsRepository.Add(t82);
           

            _uow.Save();


            var Sections = _uow.InspectionSectionRepository.GetMany(t => t.InspectionTemplateId == templateid).ToHashSet();


            var DTOResult = new List<SectionIncludeItems>();

            foreach (var section in Sections)
            {
                var Op = new SectionIncludeItems();
                Op.SectionName = section.SectionNameAr;
                Op.SectionID = section.Id;
                var it = _uow.InspectionItemsRepository.GetMany(t => t.InspectionSectionId == section.Id).ToHashSet();
                Op.Items = it;
                DTOResult.Add(Op);


            }



            return Ok(DTOResult);


        }



        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]

        [HttpGet, Route("GetInspectionSectionIncludeAllItems")]
        public IActionResult GetInspectionSectionIncludeItems(int warshahid)
        {
            var Sections = _uow.InspectionSectionRepository.GetMany(t => t.IsCommon == true || (t.WarshahId == warshahid && t.IsCommon == false)).ToHashSet();


            var DTOResult = new List<SectionIncludeItems>();

            foreach (var section in Sections)
            {
                var Op = new SectionIncludeItems();
                Op.SectionName = section.SectionNameAr;
                Op.SectionID = section.Id;
                var it = _uow.InspectionItemsRepository.GetMany(t => t.WarshahId == warshahid && t.InspectionSectionId == section.Id).ToHashSet();
                Op.Items = it;
                DTOResult.Add(Op);


            }



            return Ok(DTOResult);
        }




        // Get All InspectionSection by Template and warshah

        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]

        [HttpGet, Route("GetSpecialInspectionSectionIncludeItems")]
        public IActionResult GetSpecialInspectionSectionIncludeItems(int? templateid)
        {
            var Sections = _uow.InspectionSectionRepository.GetMany(t=>t.InspectionTemplateId == templateid).ToHashSet();


            var DTOResult = new List<SectionIncludeItems>();

            foreach (var section in Sections)
            {
                var Op = new SectionIncludeItems();
                Op.SectionName = section.SectionNameAr;
                Op.SectionNameEn = section.SectionNameEn;
                Op.IsActive = section.IsActive;
                Op.IsCommon = (bool)section.IsCommon;
                Op.SectionID = section.Id;
                var it = _uow.InspectionItemsRepository.GetMany(t=>t.InspectionSectionId == section.Id).ToHashSet();
                Op.Items = it;
                DTOResult.Add(Op);


            }



            return Ok(DTOResult);
        }


        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Common)]
        [HttpPost, Route("TuenOffOnSection")]
        public IActionResult TuenOffOnSection(int? sectionid , string status )
        {

            var currentsection = _uow.InspectionSectionRepository.GetById(sectionid);

            // status 1 on  , 0 off

            var currentItems = _uow.InspectionItemsRepository.GetMany(s => s.InspectionSectionId == sectionid).ToHashSet();



            if (status == "true")
            {
                
                currentsection.IsActive = true;

                _uow.InspectionSectionRepository.Update(currentsection);

                foreach (var item in currentItems)
                {

                    item.Status = true;

                    _uow.InspectionItemsRepository.Update(item);

                }

            }

            else
            {
                currentsection.IsActive = false;

                _uow.InspectionSectionRepository.Update(currentsection);


                foreach (var item in currentItems)
                {

                    item.Status = false;

                    _uow.InspectionItemsRepository.Update(item);

                }

            }

            _uow.Save();

            var NewItems = _uow.InspectionItemsRepository.GetMany(s => s.InspectionSectionId == sectionid).ToHashSet();
            var NewSection = _uow.InspectionSectionRepository.GetById(sectionid);

            return Ok(new { NewItems = NewItems , Newsection = NewSection });
        }


        // Update InspectionSection
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Admin)]

        [HttpPost, Route("EditInspectionSection")]
        public IActionResult EditInspectionSection(EditInspectionSectionDTO editInspectionSectionDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var Section = _mapper.Map<DL.Entities.InspectionSection>(editInspectionSectionDTO);
                    if (Section.WarshahId != null)
                    {
                        Section.IsCommon = false;
                    }
                    _uow.InspectionSectionRepository.Update(Section);
                    _uow.Save();
                    return Ok(Section);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid Template");


        }


        // Delete InspectionSection
        [ClaimRequirement(ClaimTypes.Role, RoleConstant.Admin)]


        [HttpDelete, Route("DeleteInspectionSection")]
        public IActionResult DeleteInspectionSection(int? Id)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    _uow.InspectionSectionRepository.Delete(Id);
                    _uow.Save();
                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            }

            return BadRequest("Invalid InspectionSection");


        }

        #endregion
    }
}
