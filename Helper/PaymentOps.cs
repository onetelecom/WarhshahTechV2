using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Helper
{
    public static class PaymentOperation
    {
        public static string PaymentOperationDo(string CardId, string CallBackUrl, string ReturnUrl, double total, string PaymentType) 
        {

       
            if(CallBackUrl != "https://one.warshahtech.sa/api/Payment/FailedSMS")
            {
                CallBackUrl = "https://one.warshahtech.sa/api/Payment/Failed";
                ReturnUrl = "https://one.warshahtech.sa/api/Payment/Success";
            }


          

            //CallBackUrl = "https://api.warshahtech.net/api/Payment/Failed";
            //ReturnUrl = "https://api.warshahtech.net/api/Payment/Success";

            
            PaymentModel paymentRequest = new PaymentModel();
            #region Production 
            //paymentRequest.profile_id = 60052;
            //var Token = "authorization:SDJN9MD6GB-JBGD2LWJ6B-M9KRNZBH9W";
            #endregion
            #region Test 
            paymentRequest.profile_id = 58583;
            var Token = "authorization:SDJN9MD6RM-J6NZMMZR6M-HRLGBJM2JM";
            #endregion
            paymentRequest.cart_id = CardId;
            paymentRequest.callback = CallBackUrl;
            paymentRequest.Return = ReturnUrl;
            paymentRequest.cart_amount = total;
            paymentRequest.cart_currency = "SAR";
            paymentRequest.cart_description = "Payment For " + PaymentType;
            paymentRequest.tran_class = "ecom";
            paymentRequest.tran_type = "Sale";
            var json = JsonConvert.SerializeObject(paymentRequest);
        

            var rsponse = HttpOp.webPostMethod(json, "https://secure.paytabs.sa/payment/request", Token);


            JObject jObject = JObject.Parse(rsponse);
            string Link = jObject["redirect_url"].Value<string>();
            return Link;
        }
    }
}
