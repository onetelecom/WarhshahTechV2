using BL.Infrastructure;
using DL.MailModels;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Model.ApiModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Taqnyat.JSONBody;

namespace HELPER
{
    public interface ISMS
    {
        public string SendSMS(string MobileNum, string Message,int? warshahId, bool marketing);

        public string GetSenders(string Bearer);
        public string SendSMS1(string Bearer, string Recipients, string Sender, string Body, string scheduledDatetime = "", string smsId = "");
        public string SmsFatoora(string Mobile, string LinkInvoic);
    }
    public class SMS:ISMS
    {

        private readonly MailSettings _mailSettings;
        private readonly IUnitOfWork _unitOfWork;


        public SMS(IOptions<MailSettings> mailSettings, IUnitOfWork unitOfWork)
        {
            _mailSettings = mailSettings.Value;
            _unitOfWork= unitOfWork;



        }


        public string SendSMS1(string Bearer, string Recipients, string Sender, string Body, string scheduledDatetime = "", string smsId = "")
        {
            string text;
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                ServicePointManager.Expect100Continue = true;
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("https://api.taqnyat.sa/v1/messages");
                httpWebRequest.Method = "POST";
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Headers.Add("Authorization", "Bearer " + Bearer);
                SendClass sendClass = new SendClass();
                sendClass.body = Body;
                sendClass.recipients = Recipients;
                sendClass.sender = Sender;
                sendClass.scheduledDatetime = scheduledDatetime;
                if (Operators.CompareString(Strings.Trim(smsId), "", TextCompare: false) != 0)
                {
                    sendClass.smsId = smsId;
                }

                byte[] bytes = Encoding.UTF8.GetBytes(JSONSerialize(sendClass));
                httpWebRequest.ContentLength = bytes.Length;
                Stream requestStream = httpWebRequest.GetRequestStream();
                requestStream.Write(bytes, 0, bytes.Length);
                requestStream.Close();
                StreamReader streamReader = new StreamReader(httpWebRequest.GetResponse().GetResponseStream());
                text = streamReader.ReadToEnd();
                streamReader.Close();
                return text;
            }
            catch (WebException ex)
            {
                ProjectData.SetProjectError(ex);
                WebException ex2 = ex;
                using WebResponse webResponse = ex2.Response;
                HttpWebResponse httpWebResponse = (HttpWebResponse)webResponse;
                try
                {
                    using (Stream stream = webResponse.GetResponseStream())
                    {
                        object obj = new StreamReader(stream);
                        try
                        {
                            text = Conversions.ToString(NewLateBinding.LateGet(obj, null, "ReadToEnd", new object[0], null, null, null));
                        }
                        finally
                        {
                            if (obj != null)
                            {
                                ((IDisposable)obj).Dispose();
                            }
                        }
                    }

                    string result = text;
                    ProjectData.ClearProjectError();
                    return result;
                }
                catch (Exception ex3)
                {
                    ProjectData.SetProjectError(ex3);
                    Exception ex4 = ex3;
                    throw ex2;
                }
            }
            catch (Exception ex5)
            {
                ProjectData.SetProjectError(ex5);
                Exception ex6 = ex5;
                throw ex6;
            }
            finally
            {
            }
        }

        public string GetSenders(string Bearer)
        {
            string text2;
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                ServicePointManager.Expect100Continue = true;
                string text = "";
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("https://api.taqnyat.sa/v1/messages/senders");
                httpWebRequest.Method = "GET";
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Headers.Add("Authorization", "Bearer " + Bearer);
                StreamReader streamReader = new StreamReader(httpWebRequest.GetResponse().GetResponseStream());
                text2 = streamReader.ReadToEnd();
                streamReader.Close();
                return text2;
            }
            catch (WebException ex)
            {
                ProjectData.SetProjectError(ex);
                WebException ex2 = ex;
                using WebResponse webResponse = ex2.Response;
                HttpWebResponse httpWebResponse = (HttpWebResponse)webResponse;
                try
                {
                    using (Stream stream = webResponse.GetResponseStream())
                    {
                        object obj = new StreamReader(stream);
                        try
                        {
                            text2 = Conversions.ToString(NewLateBinding.LateGet(obj, null, "ReadToEnd", new object[0], null, null, null));
                        }
                        finally
                        {
                            if (obj != null)
                            {
                                ((IDisposable)obj).Dispose();
                            }
                        }
                    }

                    string result = text2;
                    ProjectData.ClearProjectError();
                    return result;
                }
                catch (Exception ex3)
                {
                    ProjectData.SetProjectError(ex3);
                    Exception ex4 = ex3;
                    throw ex2;
                }
            }
            catch (Exception ex5)
            {
                ProjectData.SetProjectError(ex5);
                Exception ex6 = ex5;
                throw ex6;
            }
        }




        public string SendSMS(string MobileNum, string Message, int? warshahId,bool marketing)
        {
            try
            {
                var MessageCount = _unitOfWork.SMSCountRepository.GetMany(a => a.WarshahId == warshahId).FirstOrDefault();
                if (MessageCount != null&&marketing==true)
                {
                    if (MessageCount.MessageRemain>0)
                    {
                    
                        WebClient client1 = new WebClient();
                       //string baseurl1 = "http://mshastra.com/sendurlcomma.aspx?user=" + _mailSettings.ProfileId + "&pwd=" + _mailSettings.Pass + "&senderid=" + _mailSettings.SenderId + "&CountryCode=ALL&mobileno=" + MobileNum + "&msgtext=" + Message;
                        string baseurl1 = "http://mshastra.com/sendurlcomma.aspx?user=" + _mailSettings.ProfileId + "&pwd=" + _mailSettings.Pass + "&senderid=" + _mailSettings.SenderId + "&CountryCode=ALL&mobileno=" + MobileNum + "&msgtext=" + Message;

                        Stream data1 = client1.OpenRead(baseurl1);
                        StreamReader reader1 = new StreamReader(data1);
                        string s1 = reader1.ReadToEnd();
                        data1.Close();
                        reader1.Close();
                        return s1.ToString();
                    }
                    return "No Message Remaining";
                }
                if (marketing==false)
                {
                
                    WebClient client1 = new WebClient();
                    string baseurl1 = "http://mshastra.com/sendurlcomma.aspx?user=" + _mailSettings.ProfileId + "&pwd=" + _mailSettings.Pass + "&senderid=" + _mailSettings.SenderId + "&CountryCode=ALL&mobileno=" + MobileNum + "&msgtext=" + Message;
                    //string baseurl1 = "https://mshastra.com/sendurl.aspx?user=" + _mailSettings.ProfileId + "&pwd=" + _mailSettings.Pass + "&senderid=" + _mailSettings.SenderId + "&CountryCode=ALL&mobileno=" + MobileNum + "&msgtext=" + Message;

                    Stream data1 = client1.OpenRead(baseurl1);
                    StreamReader reader1 = new StreamReader(data1);
                    string s1 = reader1.ReadToEnd();
                    data1.Close();
                    reader1.Close();
                    return s1.ToString();
                }
                WebClient client = new WebClient();
            
                string baseurl = "http://mshastra.com/sendurlcomma.aspx?user=" + _mailSettings.ProfileId + "&pwd=" + _mailSettings.Pass + "&senderid=" + _mailSettings.SenderId + "&CountryCode=ALL&mobileno=" + MobileNum + "&msgtext=" + Message;
                //string baseurl = "https://mshastra.com/sendurl.aspx?user=" + _mailSettings.ProfileId + "&pwd=" + _mailSettings.Pass + "&senderid=" + _mailSettings.SenderId + "&CountryCode=ALL&mobileno=" + MobileNum + "&msgtext=" + Message;

                Stream data = client.OpenRead(baseurl);
                StreamReader reader = new StreamReader(data);
                string s = reader.ReadToEnd();
                data.Close();
                reader.Close();
                return s.ToString();


            }
            catch (Exception ex)
            {

                return "";
            }
        

          
        }

        public string SmsFatoora(string Mobile, string LinkInvoic)
        {
            WebClient client1 = new WebClient();
            string baseurl1 = "http://mshastra.com/sendurlcomma.aspx?user=" + _mailSettings.ProfileId + "&pwd=" + _mailSettings.Pass + "&senderid=" + _mailSettings.SenderId + "&CountryCode=ALL&mobileno=" + Mobile + "&msgtext=" + LinkInvoic;
            Stream data1 = client1.OpenRead(baseurl1);
            StreamReader reader1 = new StreamReader(data1);
            string s1 = reader1.ReadToEnd();
            data1.Close();
            reader1.Close();
            return s1.ToString();

        }

        private string JSONSerialize(SendClass objMsg)
        {
            MemoryStream memoryStream = new MemoryStream();
            DataContractJsonSerializer dataContractJsonSerializer = new DataContractJsonSerializer(typeof(SendClass));
            dataContractJsonSerializer.WriteObject((Stream)memoryStream, (object?)objMsg);
            memoryStream.Position = 0L;
            StreamReader streamReader = new StreamReader(memoryStream);
            return streamReader.ReadToEnd();
        }
    }
}
