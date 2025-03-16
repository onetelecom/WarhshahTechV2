using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Text; 
using System.Xml.Serialization;
using System.Configuration;
using KSAEinvoice;
using Newtonsoft.Json; 
using System.Reflection.Emit;
using System.Security.Cryptography;

namespace SignXMLDocument
{
    public   class MainSettings
    {

        public static string connectionString = ""; 
  


        public static string MapVirtualPathToPhysicalPath(string rootPath, string virtualPath)
        {
            if (string.IsNullOrEmpty(rootPath))
                throw new ArgumentNullException(nameof(rootPath));

            if (string.IsNullOrEmpty(virtualPath))
                throw new ArgumentNullException(nameof(virtualPath));

            // Combine the root path and the virtual path to get the physical path
            string physicalPath = Path.Combine(rootPath, virtualPath.TrimStart('/').Replace('/', '\\'));

            return physicalPath;
        }

        public static bool GetWebConfig()
        {
            string jsonData = "";

            WebConfig config = new WebConfig();

            try
            {
                string rootPath = AppDomain.CurrentDomain.BaseDirectory; // Root path of your application
           
                 

                string filePath = Path.Combine(rootPath, "Data_Config.txt"); 

                if (File.Exists(filePath))
                {
                    string WebCon = File.ReadAllText(filePath);

                    WebCon =  WebCon.Replace(@"\", "\\");
                    config = JsonConvert.DeserializeObject<WebConfig>(WebCon);
                     
                    // Your code using jsonData 
                    connectionString = "Data Source=" + config.Name_Server + ";Initial Catalog=" + config.Name_Data + ";User ID=" + config.User_ID + ";Password=" + config.Password + "";

                    GlobalVariables.connectionString = connectionString;    
                }
                else
                {
                    // Handle the case when the file doesn't exist
                }
            }
            catch (Exception)
            {
                config = new WebConfig();

            }


            // Check if any property is an empty string
            if (string.IsNullOrEmpty(config.Name_Server) ||
                string.IsNullOrEmpty(config.Name_Data) ||
                string.IsNullOrEmpty(config.User_ID) ||
                string.IsNullOrEmpty(config.Password) ||
                string.IsNullOrEmpty(config.CompCode) ||
                string.IsNullOrEmpty(config.UnitID))
            {
                return false;
            }

            // All properties have values
            return true;



        }

        public static void Reset()
        {
            GTaxControl.Comp_code = 0;
            GTaxControl.UnitID = 0;
            GTaxControl.Comp_Name = "";
            GTaxControl.System = "";
            GTaxControl.TIN_NO = "";
            GTaxControl.VAT_NO = "";
        }

        public static void GetControlTax(int CompCode , int UnitID , string System)
        {
            
            if (!GetWebConfig())
            {
                return;
            } 
            try
            {
                


                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM GTaxControl WHERE Comp_code = @CompCode and UnitID = @UnitID and System = @System", con))
                    {
                        cmd.Parameters.AddWithValue("@CompCode", CompCode);
                        cmd.Parameters.AddWithValue("@UnitID", UnitID);
                        cmd.Parameters.AddWithValue("@System", System);

                        Reset();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                
                                    GTaxControl.Comp_code = Convert.IsDBNull(reader["Comp_code"]) ? 0 : Convert.ToInt32(reader["Comp_code"]);
                                    GTaxControl.UnitID = Convert.IsDBNull(reader["UnitID"]) ? 0 : Convert.ToInt16(reader["UnitID"]);
                                    GTaxControl.Comp_Name = Convert.IsDBNull(reader["Comp_Name"]) ? string.Empty : reader["Comp_Name"].ToString();
                                    GTaxControl.System = Convert.IsDBNull(reader["System"]) ? string.Empty : reader["System"].ToString();
                                    GTaxControl.TIN_NO = Convert.IsDBNull(reader["TIN_NO"]) ? string.Empty : reader["TIN_NO"].ToString();
                                    GTaxControl.VAT_NO = Convert.IsDBNull(reader["VAT_NO"]) ? string.Empty : reader["VAT_NO"].ToString();
                                    GTaxControl.BusUnit = Convert.IsDBNull(reader["BusUnit"]) ? string.Empty : reader["BusUnit"].ToString();
                                    GTaxControl.Location = Convert.IsDBNull(reader["Location"]) ? string.Empty : reader["Location"].ToString();
                                    GTaxControl.Business = Convert.IsDBNull(reader["Business"]) ? string.Empty : reader["Business"].ToString();
                                    GTaxControl.APPName = Convert.IsDBNull(reader["APPName"]) ? string.Empty : reader["APPName"].ToString();
                                    GTaxControl.AppVersion = Convert.IsDBNull(reader["AppVersion"]) ? string.Empty : reader["AppVersion"].ToString();
                                    GTaxControl.AppSerial = Convert.IsDBNull(reader["AppSerial"]) ? string.Empty : reader["AppSerial"].ToString();
                                    GTaxControl.CommonName = Convert.IsDBNull(reader["CommonName"]) ? string.Empty : reader["CommonName"].ToString();
                                    GTaxControl.Country = Convert.IsDBNull(reader["Country"]) ? string.Empty : reader["Country"].ToString();
                                    GTaxControl.Invoice_Type = Convert.IsDBNull(reader["Invoice_Type"]) ? string.Empty : reader["Invoice_Type"].ToString();
                                    GTaxControl.Loginname = Convert.IsDBNull(reader["Loginname"]) ? string.Empty : reader["Loginname"].ToString();
                                    GTaxControl.Password = Convert.IsDBNull(reader["Password"]) ? string.Empty : reader["Password"].ToString();
                                    GTaxControl.Tokenid = Convert.IsDBNull(reader["Tokenid"]) ? string.Empty : reader["Tokenid"].ToString();
                                    GTaxControl.GenDate = Convert.IsDBNull(reader["GenDate"]) ? string.Empty : reader["GenDate"].ToString();
                                    GTaxControl.ExpDate = Convert.IsDBNull(reader["ExpDate"]) ? string.Empty : reader["ExpDate"].ToString();
                                    GTaxControl.CSR = Convert.IsDBNull(reader["CSR"]) ? string.Empty : reader["CSR"].ToString();
                                    GTaxControl.PubKEY = Convert.IsDBNull(reader["PubKEY"]) ? string.Empty : reader["PubKEY"].ToString();
                                    GTaxControl.PrivKey = Convert.IsDBNull(reader["PrivKey"]) ? string.Empty : reader["PrivKey"].ToString();
                                    GTaxControl.Certificate = Convert.IsDBNull(reader["Certificate"]) ? string.Empty : reader["Certificate"].ToString();
                                    GTaxControl.PCCSID = Convert.IsDBNull(reader["PCCSID"]) ? string.Empty : reader["PCCSID"].ToString();
                                    GTaxControl.CSID = Convert.IsDBNull(reader["CSID"]) ? string.Empty : reader["CSID"].ToString();                                  
                                    GTaxControl.Simpl_UploadTime = Convert.IsDBNull(reader["Simpl_UploadTime"]) ? string.Empty : reader["Simpl_UploadTime"].ToString();
                                    GTaxControl.Std_uploadEveryMin = Convert.IsDBNull(reader["Std_uploadEveryMin"]) ? 0 : Convert.ToInt16(reader["Std_uploadEveryMin"]);
                                    GTaxControl.IsIntegrated = Convert.IsDBNull(reader["IsIntegrated"]) ? false : Convert.ToBoolean(reader["IsIntegrated"]);
                                    GTaxControl.ISProduction = Convert.IsDBNull(reader["ISProduction"]) ? false : Convert.ToBoolean(reader["ISProduction"]);
                                    GTaxControl.LastHash = Convert.IsDBNull(reader["LastHash"]) ? string.Empty : reader["LastHash"].ToString();
                                    
                                    GTaxControl.IsDebugmode = Convert.IsDBNull(reader["IsDebugmode"]) ? false : Convert.ToBoolean(reader["IsDebugmode"]);
                                    GTaxControl.IsDirectSimplified = Convert.IsDBNull(reader["IsDirectSimplified"]) ? false : Convert.ToBoolean(reader["IsDirectSimplified"]);

                                    GTaxControl.UserName_AIPCall = Convert.IsDBNull(reader["UserName_AIPCall"]) ? string.Empty : reader["UserName_AIPCall"].ToString();
                                    GTaxControl.Password_AIPCall = Convert.IsDBNull(reader["Password_AIPCall"]) ? string.Empty : reader["Password_AIPCall"].ToString();
                                    GTaxControl.PathSaveXml = Convert.IsDBNull(reader["PathSaveXml"]) ? string.Empty : reader["PathSaveXml"].ToString();
                                    GTaxControl.CurrencyCode = Convert.IsDBNull(reader["CurrencyCode"]) ? string.Empty : reader["CurrencyCode"].ToString();
                                    GTaxControl.NationalityCode = Convert.IsDBNull(reader["NationalityCode"]) ? string.Empty : reader["NationalityCode"].ToString();
                                    GTaxControl.Scheme_Name = Convert.IsDBNull(reader["Scheme_Name"]) ? string.Empty : reader["Scheme_Name"].ToString();


                                    GTaxControl.Address_Street = Convert.IsDBNull(reader["Address_Street"]) ? string.Empty : reader["Address_Street"].ToString();
                                    GTaxControl.Address_Str_Additional = Convert.IsDBNull(reader["Address_Str_Additional"]) ? string.Empty : reader["Address_Str_Additional"].ToString();
                                    GTaxControl.Address_BuildingNo = Convert.IsDBNull(reader["Address_BuildingNo"]) ? string.Empty : reader["Address_BuildingNo"].ToString();
                                    GTaxControl.Address_Build_Additional = Convert.IsDBNull(reader["Address_Build_Additional"]) ? string.Empty : reader["Address_Build_Additional"].ToString();
                                    GTaxControl.Address_City = Convert.IsDBNull(reader["Address_City"]) ? string.Empty : reader["Address_City"].ToString();
                                    GTaxControl.Address_Postal = Convert.IsDBNull(reader["Address_Postal"]) ? string.Empty : reader["Address_Postal"].ToString();
                                    GTaxControl.Address_Province = Convert.IsDBNull(reader["Address_Province"]) ? string.Empty : reader["Address_Province"].ToString();
                                    GTaxControl.Address_District = Convert.IsDBNull(reader["Address_District"]) ? string.Empty : reader["Address_District"].ToString();
                                 
    }
                        }
                    }
                } 

            }
            catch (Exception EX)
            {
                return;
            }
        }

        //public static GTaxControl GetCSIDInfo()
        //{
        //    GTaxControl info = new GTaxControl();
        //    try
        //    {
             
        //            info.SecretKey = @"";
        //            info.HashedLast = @"NWZlY2ViNjZmZmM4NmYzOGQ5NTI3ODZjNmQ2OTZjNzljMmRiYzIzOWRkNGU5MWI0NjcyOWQ3M2EyN2ZiNTdlOQ==";
        //            info.PrivateKey = @"MHQCAQEEIJEi+fsODPAMBQBEAKBsACB6yChgChBHKEChl9YAfavJoAcGBSuBBAAKoUQDQgAECt0DeJ2dq4IvyLVDWJNPon2poHZ1CQ4aJXA7DyemGsJPRMoeBEsRzH62CLLmdt6EcC9b9GvJ4cIdToGeQz4HUA==";
        //            info.PublicKey = @"MFYwEAYHKoZIzj0CAQYFK4EEAAoDQgAECt0DeJ2dq4IvyLVDWJNPon2poHZ1CQ4aJXA7DyemGsJPRMoeBEsRzH62CLLmdt6EcC9b9GvJ4cIdToGeQz4HUA==";
        //            info.Certificate = @"MIICNDCCAdqgAwIBAgIGAYrdcXyWMAoGCCqGSM49BAMCMBUxEzARBgNVBAMMCmVJbnZvaWNpbmcwHhcNMjMwOTI4MjAxODMxWhcNMjgwOTI3MjEwMDAwWjB4MQswCQYDVQQGEwJTQTETMBEGA1UECwwKMzAwMDQ5MjMyODE8MDoGA1UECgwzQVJBQklBTiBCVUlMRElORyBTVVBQT1JUIEFORCBSRUhBQklMSVRBVElPTiBDTy5MVEQuMRYwFAYDVQQDDA1BQlNBUl9QTVNfUHJlMFYwEAYHKoZIzj0CAQYFK4EEAAoDQgAECt0DeJ2dq4IvyLVDWJNPon2poHZ1CQ4aJXA7DyemGsJPRMoeBEsRzH62CLLmdt6EcC9b9GvJ4cIdToGeQz4HUKOBtTCBsjAMBgNVHRMBAf8EAjAAMIGhBgNVHREEgZkwgZakgZMwgZAxJDAiBgNVBAQMGzEtU2FmZSBTeXN0ZW1zfDItUE1TfDMtMjAyMzEfMB0GCgmSJomT8ixkAQEMDzMwMDA0OTIzMjgwMDAwMzENMAsGA1UEDAwEMTEwMDEiMCAGA1UEGgwZUml5YWRoLUtpbmcgYWJ1bGF6aXogcm9hZDEUMBIGA1UEDwwLQ29uc3R1Y3Rpb24wCgYIKoZIzj0EAwIDSAAwRQIgMI5Gjr8eqhjQ/0+PxtpDFDQFvwkdUpD1I023UuCLkHYCIQDyZPdKvT90YVWvnKruaTZSlZra8/R6neJarBS5kHJnqw==";
                 
        //    }
        //    catch 
        //    {
        //        return null;
        //    }

        //    return info;
        //}

    }
}
