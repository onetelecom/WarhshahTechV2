using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Fatoora
{
   
    public static class HttpOpFatoora
    {

        //public static async Task<string> webPostMethod(string requestJSON, string endPoint)
        //{
        //    string Token = "IihdEOQ7gsQzadNXwZ6jj5nPIPxwSpnUvQMut1-Kush6hu6esG4OmSthZ99WGSExiG-7YUcnEtkCah1VlYIOEFybdARbFncWO1PTZZqlhGL1JyUHjGFJUMKzils88cL1jSBq5u-XVNl2OEQydtpRb7HREt6sRz2cnGVShhDuO7vQAKIJl9oL2Tq0Q3QvP4hcwXhMH43VTjY49ryDG5Okq9XSIS-NbFWtj_mnzla3cXNdHB2ZNpe7f6riuVY7P6N3nL5FfWRENQbqMYHL3hLNHKA42lVhAriVYxwoG4lzBV1No-ZNfy2eY7TqJHEiPcjAQZyLNOggl9U0GJg6qAZqoqB8jInCjaIZzWSY2T4pLzhvRSw-UmwELYnajxzp_P97I5EjpYVtGD-nNK7HFjFarc_OirS2-BJ7mZPOyIGeAIKB-uWEpC7IpYaPOiv_2OOhhBBwJNDeqno1K_T9Oq48u9SCRKVhbB6niBe5MREBK04qks9jPnEXMniHITvOpv-7-QZe3aipTbqDsoJDdTVsDSiFpbeFbRpE18HL50_V4akNqdgRvmSrFGy-Mj0sZVuT30LPF9oem3phOXce2sCNLjGw43KS28HlTM53DQ4m1DpF5THfW26BsEiinRUzaGhDHiudWln7JQ_mzyg7N9y53o24ceQPFiVHZW9Ak7Ue597Itkx9";
        //    string URL = "https://apitest.myfatoorah.com";
        //    string url = URL + $"/v2/{endPoint}";
        //    string responseFromServer = "";
        //    HttpClient client = new HttpClient();
        //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
        //    var httpContent = new StringContent(requestJSON, System.Text.Encoding.UTF8, "application/json");
        //    var responseMessage =  client.PostAsync(url, httpContent).ConfigureAwait(false);
        //    string response = string.Empty;

        //    if (!responseMessage.IsSuccessStatusCode)
        //    {
        //        response = JsonConvert.SerializeObject(new
        //        {
        //            IsSuccess = false,
        //            Message = responseMessage.StatusCode.ToString()
        //        });
        //    }
        //    else
        //    {
        //        response = await responseMessage.Content.ReadAsStringAsync();
        //    }


        //    return response;
        
        //}
    }
}
