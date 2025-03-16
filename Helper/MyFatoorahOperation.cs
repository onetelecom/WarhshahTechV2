using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DL.Migrations;
using HELPER;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;

namespace Helper
{
    public static class MyFatoorahOperation
    {

      public static  class Program
        {
            // You can get test token from this page  https://myfatoorah.readme.io/docs/test-token
            static string token = "IihdEOQ7gsQzadNXwZ6jj5nPIPxwSpnUvQMut1-Kush6hu6esG4OmSthZ99WGSExiG-7YUcnEtkCah1VlYIOEFybdARbFncWO1PTZZqlhGL1JyUHjGFJUMKzils88cL1jSBq5u-XVNl2OEQydtpRb7HREt6sRz2cnGVShhDuO7vQAKIJl9oL2Tq0Q3QvP4hcwXhMH43VTjY49ryDG5Okq9XSIS-NbFWtj_mnzla3cXNdHB2ZNpe7f6riuVY7P6N3nL5FfWRENQbqMYHL3hLNHKA42lVhAriVYxwoG4lzBV1No-ZNfy2eY7TqJHEiPcjAQZyLNOggl9U0GJg6qAZqoqB8jInCjaIZzWSY2T4pLzhvRSw-UmwELYnajxzp_P97I5EjpYVtGD-nNK7HFjFarc_OirS2-BJ7mZPOyIGeAIKB-uWEpC7IpYaPOiv_2OOhhBBwJNDeqno1K_T9Oq48u9SCRKVhbB6niBe5MREBK04qks9jPnEXMniHITvOpv-7-QZe3aipTbqDsoJDdTVsDSiFpbeFbRpE18HL50_V4akNqdgRvmSrFGy-Mj0sZVuT30LPF9oem3phOXce2sCNLjGw43KS28HlTM53DQ4m1DpF5THfW26BsEiinRUzaGhDHiudWln7JQ_mzyg7N9y53o24ceQPFiVHZW9Ak7Ue597Itkx9";
            static string baseURL = "https://apitest.myfatoorah.com";
            private static readonly ISMS _SMS;
            public    static async Task Main(string[] args)
            {
                //var response = await SendPayment().ConfigureAwait(false);
                Console.WriteLine("Send Payment Response :");
                //Console.WriteLine(response);

                Console.ReadLine();
            }
            public static async Task<string> SendPayment(string CustomerName , string CustomerMobile , string CustomerEmail , decimal InvoiceValue , int SupplierCode)
            {
                var sendPaymentRequest = new
                {
                    //required fields
                    CustomerName = CustomerName ,
                    NotificationOption = "All",
                    InvoiceValue = InvoiceValue,
                    //optional fields 
                    DisplayCurrencyIso = "SAR",
                    MobileCountryCode = "+966",
                    CustomerMobile =  CustomerMobile.Substring(4),
                    CustomerEmail = CustomerEmail,
                    CallBackUrl = "https://one.warshahtech.sa/api/Payment/Failed",
                    ErrorUrl = "https://one.warshahtech.sa/api/Payment/Success",
                    Language = "En",
                    CustomerReference = "",
                    CustomerCivilId = "",
                    UserDefinedField = "",
                    //mExpiryDate = DateTime.Now.AddYears(1),
                    // add suppliers
                    Suppliers = new[] {
                        new {
                          SupplierCode = SupplierCode, InvoiceShare = InvoiceValue, ProposedShare = 1
                        }
                 }
                };
                var sendPaymentRequestJSON = JsonConvert.SerializeObject(sendPaymentRequest);

                var mobile = sendPaymentRequest.MobileCountryCode + sendPaymentRequest.CustomerMobile;
                

               var response =  PerformRequest(sendPaymentRequestJSON, "SendPayment" ).ConfigureAwait(false);

               
                return response.ToString();
            }
            public static async Task<string> PerformRequest(string requestJSON, string endPoint )
            {
                string url = baseURL + $"/v2/{endPoint}";
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var httpContent = new StringContent(requestJSON, System.Text.Encoding.UTF8, "application/json");
                var responseMessage = await client.PostAsync(url, httpContent).ConfigureAwait(false);
                string response = string.Empty;
                if (!responseMessage.IsSuccessStatusCode)
                {
                    response = JsonConvert.SerializeObject(new
                    {
                        IsSuccess = false,
                        Message = responseMessage.StatusCode.ToString()
                    });
                }
                else
                {
                   
                    response = await responseMessage.Content.ReadAsStringAsync();
                }

                
                
                

                
                  
                
                responseMessage.Content.ReadAsStreamAsync().Wait();
                //_SMS.SmsFatoora(mobile  , );
                return response;
            }

        }

    }

}

