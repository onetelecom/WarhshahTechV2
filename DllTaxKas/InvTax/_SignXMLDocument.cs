using java.util;
using javax.xml.transform;
using net.sf.saxon.expr.parser;
using net.sf.saxon.functions;
using Newtonsoft.Json;
using org.apache.xerces.xni;
using SignXMLDocument;
using sun.net.www.http;
using System;
using System.Data.SqlTypes;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection.Emit;
using System.Security.Cryptography; 
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using ZatcaIntegrationSDK;
using ZatcaIntegrationSDK.APIHelper;
using ZatcaIntegrationSDK.BLL;
using ZatcaIntegrationSDK.HelperContracts;
using Result = ZatcaIntegrationSDK.Result;

namespace KSAEinvoice
{

    public partial class _SignXMLDocument
    {
        private string All_Xml = @"";
        public TaxShared _Shared = new TaxShared();
        private Mode mode { get; set; }

        public TaxResponse GetXmlStandard(Invoice_xml props)
        {
            TaxResponse response = new TaxResponse();

            Create_Xml_Standard Xml_Inv = new Create_Xml_Standard();
            All_Xml = Xml_Inv._Creare_Xml_Standard(props);

            _Shared.SaveInvoiceXml(props, All_Xml, GlobalVariables.SaveUrlFile + ".xml");

            response = ValidAndSignDocument(props, GlobalVariables.SaveUrlFile + ".xml");


            return response;
        }

        public TaxResponse GetXmlSimplified(Invoice_xml props)
        {
            TaxResponse response = new TaxResponse();

            try
            {
                Create_Xml_Simplified Xml_Inv = new Create_Xml_Simplified();
                All_Xml = Xml_Inv._Create_Xml_Simplified(props);

                _Shared.SaveInvoiceXml(props, All_Xml, GlobalVariables.SaveUrlFile + ".xml");

                response = ValidAndSignDocument(props, GlobalVariables.SaveUrlFile + ".xml");

                return response;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Response = ex.Message.ToString();
                return response;
            }

        }

        public TaxResponse ValidAndSignDocument(Invoice_xml props, string xmlFilePath)
        {

            xmlFilePath = xmlFilePath.Replace(@"\\\\\", @"\");
            xmlFilePath = xmlFilePath.Replace(@"\\\\", @"\");
            xmlFilePath = xmlFilePath.Replace(@"\\\", @"\");
            xmlFilePath = xmlFilePath.Replace(@"\\", @"\");
            TaxResponse response = new TaxResponse();
            try
            {

                EInvoiceSigningLogic logic = new EInvoiceSigningLogic();
                Result result = new Result();
                TaxShared _Shared = new TaxShared();


                result = logic.SignDocument(xmlFilePath, GTaxControl.Certificate, GTaxControl.PrivKey); //سحب التوقيع
                var doc1 = new XmlDocument();

                if (result.IsValid)
                {

                    doc1.PreserveWhitespace = true;
                    doc1.LoadXml(result.ResultedValue);
                    doc1.Save(xmlFilePath);
                    result.PIH = Utility.GetNodeInnerText(doc1, SettingsParams.PIH_XPATH);

                    //set the language of error message the default is arabic with code AR and English with code EN
                    EInvoiceValidator vali = new EInvoiceValidator(); //: "EN"
                    Result validationresult = new Result();
                    validationresult = vali.ValidateEInvoice(xmlFilePath, GTaxControl.Certificate.ToString(), result.PIH); //عمل اتشيك علي الفاتوره
                    if (!validationresult.IsValid)
                    {
                        if (!string.IsNullOrEmpty(validationresult.ErrorMessage))
                        {
                            result.ErrorMessage += validationresult.ErrorMessage + "\n";
                        }
                        if (validationresult.lstSteps != null)
                        {
                            foreach (Result r in validationresult.lstSteps)
                            {
                                if (r.IsValid == false)
                                {
                                    result.ErrorMessage += r.ErrorMessage;
                                }
                            }
                        }


                        TaxShared taxShared = new TaxShared();
                        taxShared._InsertLogDebug(props.InvoiceID, props.CompCode, props.System, props.UUID, result.ErrorMessage.ToString(), "Valid Invoice Xml in Tax", "");

                        response.IsSuccess = false;
                        response.ErrorMessage = result.ErrorMessage;
                        return response;
                    }
                    else
                    {

                        string updatedXmlString = doc1.OuterXml;


                        TaxShared taxShared = new TaxShared();
                        taxShared._InsertLogDebug(props.InvoiceID, props.CompCode, props.System, props.UUID, "IsSuccess", "Valid Invoice Xml in Tax", "");


                        response.IsSuccess = true;
                        response.Response = updatedXmlString;
                        return response;
                    }
                }
                else
                {

                    TaxShared taxShared = new TaxShared();
                    taxShared._InsertLogDebug(props.InvoiceID, props.CompCode, props.System, props.UUID, result.ErrorMessage.ToString(), "Valid Invoice Xml in Tax", "");


                    response.IsSuccess = false;
                    response.ErrorMessage = result.ErrorMessage.ToString();
                    return response;
                }
            }
            catch (Exception ex)
            {
                TaxShared taxShared = new TaxShared();
                taxShared._InsertLogDebug(props.InvoiceID, props.CompCode, props.System, props.UUID, ex.Message, "Valid Invoice Xml in Tax", "");


                response.IsSuccess = false;
                response.ErrorMessage = ex.Message.ToString();
                return response;
            }

        }

        public async Task<TaxResponse> SendInvTax(long InvoiceID, int CompCode, string System, string UUID, bool IS_Standard, string xmlFilePath)
        {


            TaxResponse response = new TaxResponse();

            try
            {
                XmlDocument doc = new XmlDocument();
                doc.PreserveWhitespace = true;
                try
                {
                    doc.Load(xmlFilePath);
                }
                catch
                {
                    response.IsSuccess = false;
                    response.ErrorMessage = "Can not load XML file";
                    return response;
                }
                string EncodedInvoice = Utility.ToBase64Encode(doc.OuterXml);
                string uuid = Utility.GetNodeInnerText(doc, SettingsParams.UUID_XPATH);
                string InvoiceHash = Utility.GetNodeInnerText(doc, SettingsParams.Hash_XPATH);
                string API_URL = "";
                //GTaxControl.IsIntegrated

                if (!IS_Standard) // Simplified
                {
                    if (GTaxControl.ISProduction == true) //فعلي
                    {
                        API_URL = @"https://gw-fatoora.zatca.gov.sa/e-invoicing/core/invoices/reporting/single/";// فعلي  Customer
                    }
                    else //تجربة 
                    {
                        API_URL = @"https://gw-fatoora.zatca.gov.sa/e-invoicing/simulation/invoices/reporting/single";// تجربة  Customer  
                    }

                    //API_URL = @"https://gw-fatoora.zatca.gov.sa/e-invoicing/developer-portal/invoices/reporting/single"; // تجربة  developer  
                }
                else
                { //Standard 
                    if (GTaxControl.ISProduction == true) //فعلي
                    {
                        API_URL = @"https://gw-fatoora.zatca.gov.sa/e-invoicing/core/invoices/clearance/single/";// فعلي  Customer
                    }
                    else //تجربة 
                    {
                        API_URL = @"https://gw-fatoora.zatca.gov.sa/e-invoicing/simulation/invoices/clearance/single";// تجربة  Customer  
                    }

                    //API_URL = @"https://gw-fatoora.zatca.gov.sa/e-invoicing/developer-portal/invoices/clearance/single"; // تجربة  developer  
                }

                InvoiceReportingRequest invrequestbody = new InvoiceReportingRequest();
                invrequestbody.invoiceHash = InvoiceHash;
                invrequestbody.uuid = uuid;
                invrequestbody.invoice = EncodedInvoice;



                //response = await SendXmlToApi(IS_Standard, GTaxControl.UserName_AIPCall, GTaxControl.Password_AIPCall, invrequestbody);
                response = await SendDataToApi(InvoiceID, CompCode, System, UUID, API_URL, GTaxControl.UserName_AIPCall, GTaxControl.Password_AIPCall, invrequestbody);


                if (IS_Standard)
                {
                    if (response.IsSuccess)
                    {
                        string JsonData = response.Response.ToString();
                        ResultStandard _Result = JsonConvert.DeserializeObject<ResultStandard>(JsonData);
                        string base64Invoice = _Result.clearedInvoice.ToString();
                        byte[] decodedBytes = Convert.FromBase64String(base64Invoice);
                        string decodedInvoice = Encoding.UTF8.GetString(decodedBytes);

                        _Shared.SaveInvoiceXmlReturnTax(decodedInvoice, GlobalVariables.SaveUrlFile + ".xml");

                    }

                }

                GlobalVariables.TaxStatus = 2;

                return response;


            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = ex.Message.ToString();
                return response;

            }
        }

        public async Task<TaxResponse> SendDataToApi(long InvoiceID, int CompCode, string System, string UUID, string apiUrl, string userName, string password, InvoiceReportingRequest inv)
        {
            TaxResponse response = new TaxResponse();
            TaxShared taxShared = new TaxShared();


            try
            {
                

                // Set Authorization header using Basic Authentication
                string base64Credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{userName}:{password}"));
                string authorizationHeader = $"Basic {base64Credentials}";


                taxShared._InsertLogDebug(InvoiceID, CompCode, System, UUID, "en", "Send Header Accept-Language", "");
                taxShared._InsertLogDebug(InvoiceID, CompCode, System, UUID, "V2", "Send Header Accept-Version", "");
                taxShared._InsertLogDebug(InvoiceID, CompCode, System, UUID, authorizationHeader, "Send Header Authorization", "");

                // Set TLS version to ensure secure communication
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                using (System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient())
                {
                    string jsonData = JsonConvert.SerializeObject(inv);

                    HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");


                    LogInTxtFile(InvoiceID, CompCode, System, UUID, jsonData);

                    //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", GTaxControl.PCCSID.Trim());
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    httpClient.DefaultRequestHeaders.Add("Accept-Language", "en");
                    httpClient.DefaultRequestHeaders.Add("Accept-Version", "V2");
                    httpClient.DefaultRequestHeaders.Add("Authorization", authorizationHeader);

                    //httpClient.Timeout = TimeSpan.FromSeconds(120);
                    //httpClient.DefaultRequestHeaders.Add("Content-Type", "application/json; charset=utf-8");

                    taxShared._InsertLogDebug(InvoiceID, CompCode, System, UUID, apiUrl, " Url Tax Send Data Invoice", "");


                    HttpResponseMessage _response = await httpClient.PostAsync(apiUrl, content);

                    response.IsSuccess = _response.IsSuccessStatusCode;
                    response.Response = await _response.Content.ReadAsStringAsync();
                    response.ErrorMessage = !response.IsSuccess ? $"HTTP {Convert.ToInt32(_response.StatusCode)} - {_response.ReasonPhrase}" : null;

                    // Deserialize the response if needed
                    // response.Results = JsonConvert.DeserializeObject<ResultsTax>(response.Response);
                }
            }
            catch (HttpRequestException e)
            {
                response.IsSuccess = false;
                response.ErrorMessage = "HTTP request failed: " + e.Message;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = "An unexpected error occurred: " + ex.Message;
            }
            return response;
        }

        public async void CreatebinarySecurityTokenAndSecret(string privateKeyXml)
        {


            // API URL for retrieving CSIDs after compliance/invoices
            string apiUrl = "https://gw-fatoora.zatca.gov.sa/e-invoicing/simulation/production/csids";
            // Assuming you have a request ID
            string requestId = "3101745747";

            try
            {
                // Create HttpClient instance
                using (System.Net.Http.HttpClient client = new System.Net.Http.HttpClient())
                {
                    // Build the request URL with the request ID
                    string requestUrl = $"{apiUrl}?request_id={requestId}";

                    // Send GET request to the API URL with the request ID
                    HttpResponseMessage response = await client.GetAsync(requestUrl);

                    // Check if the response is successful
                    if (response.IsSuccessStatusCode)
                    {
                        // Read the response content as string
                        string result = await response.Content.ReadAsStringAsync();

                        // Display the result
                        Console.WriteLine("Response from API:");
                        Console.WriteLine(result);
                    }
                    else
                    {
                        // Display error message if request was not successful
                        Console.WriteLine($"Failed to fetch CSIDs. Status code: {response.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                // Display any exception that occurred during the request
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        static string ConvertToPem(byte[] derBytes)
        {
            // Add PEM header and footer
            string pemString = "-----BEGIN PUBLIC KEY-----\n";
            pemString += Convert.ToBase64String(derBytes, Base64FormattingOptions.InsertLineBreaks) + "\n";
            pemString += "-----END PUBLIC KEY-----";

            return pemString;
        }

        public static byte[] GenerateSecret(int length)
        {
            byte[] secret = new byte[length];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(secret);
            }
            return secret;
        }
        // Helper method to extract element value from XML
   

        public void LogInTxtFile(long InvoiceID, int CompCode, string System, string UUID, string jsonData)
        {
            TaxShared taxShared = new TaxShared();
            string rootPath = AppDomain.CurrentDomain.BaseDirectory; // Root path of your application

            string filePath = Path.Combine(rootPath, "Send_in_Body_jsonData_Last_Invoice.txt");

            // Create the directory if it doesn't exist
            Directory.CreateDirectory(rootPath);

            try
            {
                File.WriteAllText(filePath, jsonData);
                taxShared._InsertLogDebug(InvoiceID, CompCode, System, UUID, "IsSuccess", "Save File txt in Body jsonData", "");
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                Console.WriteLine($"An error occurred: {ex.Message}");
                taxShared._InsertLogDebug(InvoiceID, CompCode, System, UUID, ex.Message, "Save File txt in Body jsonData", "");
            }
        }

        public async Task<TaxResponse> SendXmlToApi(bool IS_Standard, string userName, string password, InvoiceReportingRequest inv)
        {
            TaxResponse response = new TaxResponse();

            try
            {

                this.mode = GTaxControl.ISProduction == true ? Mode.Production : Mode.Simulation;

                ApiRequestLogic apireqlogic = new ApiRequestLogic(this.mode);


                if (IS_Standard)
                {
                    InvoiceClearanceResponse invoicereportingmodel = apireqlogic.CallClearanceAPI(userName, password, inv);
                    if (invoicereportingmodel.IsSuccess)
                    {
                        response.IsSuccess = true;
                        return response;
                    }
                    else
                    {
                        string err = "";
                        err = invoicereportingmodel.ErrorMessage;
                        if (invoicereportingmodel.validationResults != null)
                        {
                            foreach (ErrorModel error in invoicereportingmodel.validationResults.ErrorMessages)
                            {
                                err += error.Message + "\n";
                            }
                        }

                        response.IsSuccess = false;
                        response.ErrorMessage = err;
                        return response;
                    }
                }
                else
                {
                    InvoiceReportingResponse invoicereportingmodel = apireqlogic.CallReportingAPI(userName, password, inv);
                    if (invoicereportingmodel.IsSuccess)
                    {
                        response.IsSuccess = true;
                        return response;
                    }
                    else
                    {
                        string err = "";
                        err = invoicereportingmodel.ErrorMessage;
                        if (invoicereportingmodel.validationResults != null)
                        {
                            foreach (ErrorModel error in invoicereportingmodel.validationResults.ErrorMessages)
                            {
                                err += error.Message + "\n";
                            }
                        }

                        response.IsSuccess = false;
                        response.ErrorMessage = err;
                        return response;
                    }
                }

            }
            catch (HttpRequestException e)
            {
                response.IsSuccess = false;
                response.ErrorMessage = "HTTP request failed: " + e.Message;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = "An unexpected error occurred: " + ex.Message;
            }

            return response;
        }



    }
}
