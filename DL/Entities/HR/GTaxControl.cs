using Org.BouncyCastle.Asn1.Cms;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.Entities.HR
{
    public class GTaxControl 
    {

        [Key]
        public int Comp_code { get; set; }
     
        public string System { get; set; }
        
        public int UnitID { get; set; }

        public bool ISProduction { get; set; }
        public string remark { get; set; }

        public string unitname { get; set; }

        public string Comp_Name { get; set; }

        public string TIN_NO { get; set; }

        public string VAT_NO { get;}

        public string BusUnit { get; }

        public string Location { get; }

        public string Business { get; }

        public string APPName { get; set; }

        public string AppVersion { get; set; }
        public string AppSerial { get; set; }

        public string CommonName { get; set; }

        public string Country { get; set; }

        public string Invoice_Type { get; set; }

        public string Loginname { get; set; }

        public string Password { get; set; }

        public string Tokenid { get; set; }

        public DateTime GenDate  { get; set; }

        public DateTime ExpDate { get; set; }

        public string CSR { get; set; }

        public string PubKEY { get; set; }

        public string PrivKey { get; set; }   

        public string Certificate { get; set; }

        public string PCCSID { get; set; }

        public string CSID { get; set; }

        public TimeSpan Simpl_UploadTime { get; set; }

        public int  Std_uploadEveryMin { get; set; }

        public bool IsIntegrated { get;}


        public string LastHash { get; set; }

        public string UserName_AIPCall { get; set;}

        public string Password_AIPCall { get; set; }

        public string PathSaveXml { get; set; }


        public string Scheme_Name { get; set; }


        public string Address_Street { get; set; }

        public string Address_Str_Additional { get; set;}

        public string Address_BuildingNo { get; set;}

        public string Address_Build_Additional { get; set;}

        public string Address_City { get; set;}

        public string Address_Postal { get; set;}

        public string Address_Province { get; set;}

        public string Address_District { get; set;}

        public string NationalityCode { get; set;}

        public string CurrencyCode { get; set;}

        public bool IsDebugmode { get; set;}

        public bool IsDirectSimplified { get; set;}


    }
}
