using BL.Infrastructure;
using ClosedXML.Excel;
using HELPER;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace WarshahTechV2.Controllers
{
    [ClaimRequirement(ClaimTypes.Role, RoleConstant.Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class BackUpController : ControllerBase
    {
        private readonly IUnitOfWork uow;

        public BackUpController(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        [HttpGet,Route("BackUpUsers")]
        public IActionResult BackUpUsers(int warshahId)
        {
            var users = uow.UserRepository.GetMany(a=>a.WarshahId==warshahId).Include(a=>a.Role).ToHashSet();
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Users");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "Id";
                worksheet.Cell(currentRow, 2).Value = "FirstName";
                worksheet.Cell(currentRow, 3).Value = "LastName";
                worksheet.Cell(currentRow, 4).Value = "Phone";
                worksheet.Cell(currentRow, 5).Value = "Role.Name";
                worksheet.Cell(currentRow, 6).Value = "Email";
                worksheet.Cell(currentRow, 7).Value = "CivilId";
                foreach (var user in users)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = user.Id;
                    worksheet.Cell(currentRow, 2).Value = user.FirstName;
                    worksheet.Cell(currentRow, 3).Value = user.LastName;
                    worksheet.Cell(currentRow, 4).Value = user.Phone;
                    worksheet.Cell(currentRow, 5).Value = user.Role.Name;
                    worksheet.Cell(currentRow, 6).Value = user.Email;
                    worksheet.Cell(currentRow, 7).Value = user.CivilId;
                 }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "users.xlsx");
                }
            }

        }



        [HttpGet, Route("BackUpOrders")]
        public IActionResult BackUpOrders(int warshahId)
        {
            var Orders = uow.RepairOrderRepository.GetMany(a => a.WarshahId == warshahId).Include(a => a.ReciptionOrder.CarOwner).Include(a => a.ReciptionOrder.MotorId.motorMake ).Include(a => a.ReciptionOrder.MotorId.motorModel).ToHashSet();
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Orders");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "Id";
                worksheet.Cell(currentRow, 2).Value = "FixingPrice";
                worksheet.Cell(currentRow, 3).Value = "VatMoney";
                worksheet.Cell(currentRow, 4).Value = "car owner";
                worksheet.Cell(currentRow, 5).Value = "car";
                worksheet.Cell(currentRow, 6).Value = "AfterDiscount";
                worksheet.Cell(currentRow, 7).Value = "BeforeDiscount";
                worksheet.Cell(currentRow, 8).Value = "Deiscount";
                worksheet.Cell(currentRow, 9).Value = "Garuntee";
                worksheet.Cell(currentRow, 10).Value = "KMOut";
                worksheet.Cell(currentRow, 11).Value = "KM_In";
                worksheet.Cell(currentRow, 12).Value = "TechReview";
                worksheet.Cell(currentRow, 13).Value = "CarOwnerDescribtion";
                worksheet.Cell(currentRow, 14).Value = "Total";
                foreach (var Order in Orders)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = Order.Id;
                    worksheet.Cell(currentRow, 2).Value = Order.FixingPrice;
                    worksheet.Cell(currentRow, 3).Value = Order.VatMoney;
                    worksheet.Cell(currentRow, 4).Value = Order.ReciptionOrder.CarOwner.FirstName + "-" + Order.ReciptionOrder.CarOwner.LastName + "-" + Order.ReciptionOrder.CarOwner.Phone;
                    worksheet.Cell(currentRow, 5).Value = Order.ReciptionOrder.MotorId.motorMake.MakeNameAr + "-" + Order.ReciptionOrder.MotorId.motorModel.ModelNameAr + "-" + Order.ReciptionOrder.MotorId.PlateNo;
                    worksheet.Cell(currentRow, 6).Value = Order.AfterDiscount;
                    worksheet.Cell(currentRow, 7).Value = Order.BeforeDiscount;
                    worksheet.Cell(currentRow, 8).Value = Order.Deiscount;
                    worksheet.Cell(currentRow, 9).Value = Order.Garuntee;
                    worksheet.Cell(currentRow, 10).Value = Order.KMOut;
                    worksheet.Cell(currentRow, 11).Value = Order.ReciptionOrder.KM_In;
                    worksheet.Cell(currentRow, 12).Value = Order.TechReview;
                    worksheet.Cell(currentRow, 13).Value = Order.ReciptionOrder.CarOwnerDescribtion;
                    worksheet.Cell(currentRow, 14).Value = Order.Total;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "Orders.xlsx");
                }
            }

        }
        [HttpGet, Route("BackUpInvoices")]
        public IActionResult BackUpInvoices(int warshahId)
        {
            var Invoices = uow.InvoiceRepository.GetMany(a => a.WarshahId == warshahId).ToHashSet();
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Invoices");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "Id";
                worksheet.Cell(currentRow, 2).Value = "AdvancePayment";
                worksheet.Cell(currentRow, 3).Value = "VatMoney";
                worksheet.Cell(currentRow, 4).Value = "AfterDiscount";
                worksheet.Cell(currentRow, 5).Value = "BeforeDiscount";
                worksheet.Cell(currentRow, 6).Value = "CarOwnerName";
                worksheet.Cell(currentRow, 7).Value = "CarType";
                worksheet.Cell(currentRow, 8).Value = "Deiscount";
                worksheet.Cell(currentRow, 9).Value = "CheckPrice";
                worksheet.Cell(currentRow, 10).Value = "FixingPrice";
                worksheet.Cell(currentRow, 11).Value = "InvoiceNumber";
                worksheet.Cell(currentRow, 12).Value = "InvoiceSerial";
                worksheet.Cell(currentRow, 13).Value = "KMIn";
                worksheet.Cell(currentRow, 14).Value = "KMOut";
                worksheet.Cell(currentRow, 15).Value = "RemainAmount";
                worksheet.Cell(currentRow, 16).Value = "TechReview";
                worksheet.Cell(currentRow, 17).Value = "Total";
                worksheet.Cell(currentRow, 18).Value = "CreatedOn";
                foreach (var Invoice in Invoices)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = Invoice.Id;
                    worksheet.Cell(currentRow, 2).Value = Invoice.AdvancePayment;
                    worksheet.Cell(currentRow, 3).Value = Invoice.VatMoney;
                    worksheet.Cell(currentRow, 4).Value = Invoice.AfterDiscount;
                    worksheet.Cell(currentRow, 5).Value = Invoice.BeforeDiscount;
                    worksheet.Cell(currentRow, 6).Value = Invoice.CarOwnerName + "-"+Invoice.CarOwnerPhone;
                    worksheet.Cell(currentRow, 7).Value = Invoice.CarType;
                    worksheet.Cell(currentRow, 8).Value = Invoice.Deiscount;
                    worksheet.Cell(currentRow, 9).Value = Invoice.CheckPrice;
                    worksheet.Cell(currentRow, 10).Value = Invoice.FixingPrice;
                    worksheet.Cell(currentRow, 11).Value = Invoice.InvoiceNumber;
                    worksheet.Cell(currentRow, 12).Value = Invoice.InvoiceSerial;
                    worksheet.Cell(currentRow, 13).Value = Invoice.KMIn;
                    worksheet.Cell(currentRow, 14).Value = Invoice.KMOut;
                    worksheet.Cell(currentRow, 15).Value = Invoice.RemainAmount;
                    worksheet.Cell(currentRow, 16).Value = Invoice.TechReview;
                    worksheet.Cell(currentRow, 17).Value = Invoice.Total;
                    worksheet.Cell(currentRow, 18).Value = Invoice.CreatedOn;

                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "Invoices.xlsx");
                }
            }

        }
    }
}
