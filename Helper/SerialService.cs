using BL.Infrastructure;
using DL.DTOs.InvoiceDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Helper
{
    public interface ISerialService
    {
        InvoiceSerialDTO GetInvoiceSerial(int WarshahId);
    }
    public class SerialService : ISerialService
    {
        private readonly IUnitOfWork _uow;

        public SerialService(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public InvoiceSerialDTO GetInvoiceSerial(int WarshahId)
        {
            var InvoiceSerialDTO = new InvoiceSerialDTO();
            // Get last invoice number for each warshash
            var invoicenumber = _uow.InvoiceRepository.GetMany(i => i.WarshahId == WarshahId).OrderByDescending(t => t.InvoiceNumber).FirstOrDefault();
            if (invoicenumber == null)
            {
                InvoiceSerialDTO.InvoiceNumber = 1;
            }
            else
            {
                int lastnumber = invoicenumber.InvoiceNumber;
                InvoiceSerialDTO.InvoiceNumber = lastnumber + 1;
            }
            InvoiceSerialDTO.InvoiceSerial = "IN-" + WarshahId + "-" + InvoiceSerialDTO.InvoiceNumber;
            return InvoiceSerialDTO;
        }
    }
}
