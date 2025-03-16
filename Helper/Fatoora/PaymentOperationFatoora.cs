using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;

namespace Helper.Fatoora
{
  
     public static class PaymentOperationFatoora
    {
        //public static async Task<string> PaymentOperationDoAsync()
        //{

            
        //    var sendPaymentRequest = new
        //    {
        //        //required fields
        //        CustomerName = "Salah Ahmed",
        //        NotificationOption = "LNK ",
        //        InvoiceValue = 100,
        //        //optional fields 
        //        DisplayCurrencyIso = "SAR",
        //        MobileCountryCode = "+966",
        //        CustomerMobile = "582240552",
        //        CustomerEmail = "sahmed@onetelecom.me",
        //        CallBackUrl = "https://one.warshahtech.sa/api/Payment/Failed",
        //        ErrorUrl = "https://one.warshahtech.sa/api/Payment/Success",
        //        Language = "En",
        //        CustomerReference = "",
        //        CustomerCivilId = "",
        //        UserDefinedField = "",
        //        ExpiryDate = DateTime.Now.AddYears(1),
        //        // add suppliers
        //        Suppliers = new[] {
        //                new {
        //                  SupplierCode = 1, InvoiceShare = 100, ProposedShare = 70
        //                }
        //         }
        //    };
        //    var sendPaymentRequestJSON = JsonConvert.SerializeObject(sendPaymentRequest);
        //    //string response = await HttpOpFatoora.webPostMethod(sendPaymentRequestJSON, "SendPayment");
        //    JObject jObject = JObject.Parse(response);
        //    string Link = jObject["InvoiceLink"].Value<string>();
        //    return Link;
        //}
    }
}
