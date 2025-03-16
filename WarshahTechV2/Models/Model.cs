using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KSAEinvoice
{

    public class TaxResponse
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public string Reuslt { get; set; }
        public int StatusCode { get; set; }
        public object Response { get; set; }
        public string InvoiceXml { get; set; }
        public long InvoiceID { get; set; }
        public long InvoiceTrNo { get; set; }
        public string InvoiceUUIDNew { get; set; }
        public string InvoiceQr_CodeNew { get; set; }
        public string InvoiceHashNew { get; set; }
        public string PrevInvoiceHash { get; set; }
        public int TaxStatus { get; set; }

    }


    public class ApiResponse
    {
        [JsonProperty("IsSuccess")]
        public bool IsSuccess { get; set; }

        [JsonProperty("ErrorMessage")]
        public string ErrorMessage { get; set; }

        [JsonProperty("StatusCode")]
        public int StatusCode { get; set; }

        [JsonProperty("Response")]
        public TaxResponse Response { get; set; }
    }

    //********************************Model**********************************************

    public class IQ_KSATaxInvHeader
    {
        public int? CompCode { get; set; }
        public int? InvoiceID { get; set; }
        public int? TrNo { get; set; }
        public DateTime? TrDate { get; set; }
        public TimeSpan? TrTime { get; set; }
        public int? invoiceStatus { get; set; }
        public int? InvoiceTypeCode { get; set; }
        public int? InvoiceTransCode { get; set; }
        public int? TaxStatus { get; set; }
        public string DocUUID { get; set; }
        public string DocNo { get; set; }
        public int? GlobalInvoiceCounter { get; set; }
        public string PrevInvoiceHash { get; set; }
        public string RefNO { get; set; }
        public string InstructionNote { get; set; }
        public string QRCode { get; set; }
        public string SalesOrderRef { get; set; }
        public string SalesOrderDescr { get; set; }
        public DateTime? DeliverydateFrom { get; set; }
        public DateTime? DeliverydateTo { get; set; }
        public int? PaymentMeanCode { get; set; }
        public string purchaseorderDesc { get; set; }
        public string perofrmainvoiceno { get; set; }
        public string Cus_Name { get; set; }
        public string Cus_VatNo { get; set; }
        public string Cus_BuildingNumber { get; set; }
        public string Cus_CityName { get; set; }
        public string Cus_PostalZone { get; set; }
        public string Cus_StreetName { get; set; }
        public string Cus_governate { get; set; }
        public string Address_District { get; set; }
        public string Address_Build_Additional { get; set; }
        public string Address_Str_Additional { get; set; }
        public bool? ISPersonal { get; set; }
        public int? AdvDedAmount { get; set; }
        public int? AdvDedVat { get; set; }
        public decimal? AdvDedVatPrc { get; set; }
        public string AdvDedVatNat { get; set; }
        public string AdvDedReason { get; set; }
        public string AdvDedReasonCode { get; set; }
        public decimal? HDDiscAmount { get; set; }
        public decimal? HDDiscVat { get; set; }
        public decimal? HDDiscVatPrc { get; set; }
        public string HDDiscVatNat { get; set; }
        public string HDDiscReason { get; set; }
        public string HDDiscReasonCode { get; set; }
        public decimal? AllowAmount { get; set; }
        public decimal? AllowVat { get; set; }
        public decimal? AllowVatPrc { get; set; }
        public string AllowVatNat { get; set; }
        public string AllowReason { get; set; }
        public string AllowReasonCode { get; set; }
        public decimal? hd_NetAmount { get; set; }
        public decimal? Hd_TaxableAmount { get; set; }
        public decimal? Hd_NetWithTax { get; set; }
        public decimal? Hd_NetAdditions { get; set; }
        public decimal? Hd_NetDeduction { get; set; }
        public decimal? Hd_PaidAmount { get; set; }
        public decimal? Hd_DueAmount { get; set; }
        public decimal? Hd_NetTax { get; set; }
        public decimal? hd_netTaxCaluated { get; set; }
        public decimal? HD_Round { get; set; }

    }



    public class IQ_KSATaxInvItems
    {
        public int? TaxInvoiceID { get; set; }
        public int TaxInvoiceDetailID { get; set; }
        public int? TaxItemSerial { get; set; }
        public string TaxItemCode { get; set; }
        public string TaxItemDescr { get; set; }
        public string TaxItemUnit { get; set; }
        public decimal? TaxItemQty { get; set; }
        public decimal? TaxItemTotal { get; set; }
        public decimal? TaxItemUnitPrice { get; set; }
        public decimal? TaxItemNetTotal { get; set; }
        public decimal? TaxItemDiscPrc { get; set; }
        public decimal? TaxItemDiscAmt { get; set; }
        public decimal? TaxItemVatPrc { get; set; }
        public decimal? TaxItemVatAmt { get; set; }
        public string VatNatureCode { get; set; }
    }


    public class IQ_KSATaxInvHeader_PerPaid
    {
        public int BillPrePaidID { get; set; }
        public int? BillId { get; set; }
        public int? PrePaidBillId { get; set; }
        public int? TrNo { get; set; }
        public DateTime? TrDate { get; set; }
        public TimeSpan? TrTime { get; set; }
        public string DocNo { get; set; }
        public int? GlobalInvoiceCounter { get; set; }
        public decimal? AdvRemainAmount { get; set; }
        public decimal? AdvDeduction { get; set; }
        public decimal? AdvVatAmount { get; set; }
        public decimal? VatPrc { get; set; }
        public decimal? Advamount { get; set; }
        public string VatNat { get; set; }
    }


}


