using jdk.@internal.util.xml.impl;
using KSAEinvoice;
using Newtonsoft.Json;
//using Org.BouncyCastle.Ocsp;
using SignXMLDocument;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;


public partial class TaxShared
{


    public string SetVal(object value)
    {
        string result = (value == null) ? "" : value.ToString();
        result = result.Replace("\r\n", "");
        return result;
    }


    public static string DateFormat(string dateForm)
    {
        try
        {
            System.DateTime date = System.DateTime.Now;
            string myDate = "";

            if (dateForm.Contains("Date("))
            {
                myDate = dateForm.Split('(')[1].Split(')')[0];
                date = new System.DateTime(1970, 1, 1).AddMilliseconds(double.Parse(myDate));
            }
            else
            {
                date = System.DateTime.Parse(dateForm);
            }

            int yy = date.Year;
            int mm = date.Month;
            int dd = date.Day;

            string year = yy.ToString();
            string month = mm < 10 ? "0" + mm.ToString() : mm.ToString();
            string day = dd < 10 ? "0" + dd.ToString() : dd.ToString();

            string startDate = year + "-" + month + "-" + day;
            string form_date = startDate;
            return form_date;
        }
        catch
        {
            return DateFormat(System.DateTime.Now.ToString());
        }
    }

    public static string TimeFormat(System.DateTime _TimeFormat)
    {
        try
        {
            string formattedTime = _TimeFormat.ToString("HH:mm:ss");
            return formattedTime;
        }
        catch
        {
            return TimeFormat(System.DateTime.Now);
        }
    }
    public   DateTime AddTimeIndata(DateTime dateTime, string stringTime)
    {
         
        string[] values = stringTime.Split(':');

        return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, Convert.ToInt16(values[0]), Convert.ToInt16(values[1]), 0);
    }


    public DateTime MargeTime_in_Date(DateTime dateTime, DateTime Time)
    {

        return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, Time.Hour, Time.Minute, Time.Second);
    }

    public string FormatTrNo(DateTime dateTime, long? TrNo)
    {
      string Str_TrNo =  dateTime.Year.ToString() + "/" + dateTime.Month.ToString() + "/" + dateTime.Day.ToString() + "/" + TrNo.ToString(); 
        return Str_TrNo;
    }


    public List<InvoiceLine_xml> GetCleanModelInvoiceLine(List<InvoiceLine_xml> listModel)
    {
        //return listModel;
        return JsonConvert.DeserializeObject<List<InvoiceLine_xml>>(JsonConvert.SerializeObject(listModel));
    }


    public string Encrypt(string sourceData)
    {
        byte[] key = new byte[] { 90, 20, 30, 40, 50, 55, 170, 128 };
        byte[] iv = new byte[] { 190, 2, 3, 4, 5, 6, 220, 8 };
        try
        {
            byte[] sourceDataBytes = ASCIIEncoding.UTF8.GetBytes(sourceData);
            MemoryStream tempStream = new MemoryStream();
            DESCryptoServiceProvider encryptor = new DESCryptoServiceProvider();
            CryptoStream encryptionStream = new CryptoStream(tempStream, encryptor.CreateEncryptor(key, iv), CryptoStreamMode.Write);
            encryptionStream.Write(sourceDataBytes, 0, sourceDataBytes.Length);
            encryptionStream.FlushFinalBlock();
            byte[] encryptedDataBytes = tempStream.GetBuffer();
            return Convert.ToBase64String(encryptedDataBytes, 0, (int)tempStream.Length);
        }
        catch
        {
            throw new Exception("Unable to encrypt data");
        }
    }

    public   string Decrypt(string sourceData)
    {
        byte[] key = new byte[] { 90, 20, 30, 40, 50, 55, 170, 128 };
        byte[] iv = new byte[] { 190, 2, 3, 4, 5, 6, 220, 8 };
        try
        {
            byte[] encryptedDataBytes = Convert.FromBase64String(sourceData);
            MemoryStream tempStream = new MemoryStream(encryptedDataBytes, 0, encryptedDataBytes.Length);
            DESCryptoServiceProvider decryptor = new DESCryptoServiceProvider();
            CryptoStream decryptionStream = new CryptoStream(tempStream, decryptor.CreateDecryptor(key, iv), CryptoStreamMode.Read);
            StreamReader allDataReader = new StreamReader(decryptionStream);
            return allDataReader.ReadToEnd();
        }
        catch
        {
            throw new Exception("Unable to decrypt data");
        }

    }

    public XmlDocument SaveInvoiceXml(Invoice_xml props , string xmlString , string filePath)
    {
        XmlDocument invoiceXml = new XmlDocument();
        invoiceXml.PreserveWhitespace = true;

        try
        {
            string formattedString = xmlString ;

            invoiceXml.LoadXml(formattedString);
             
            // Save the XML content to a file
            //string filePath = "C:\\Users\\Bse04\\Desktop\\XML_Test\\output.xml"; // Replace with the desired file path 
            invoiceXml.Save(filePath);


            TaxShared taxShared = new TaxShared();
            taxShared._InsertLogDebug(props.InvoiceID, props.CompCode, props.System, props.UUID, "IsSuccess", "Save Invoice Xml in path", "Url : " + filePath);

        }
        catch (System.Exception Ex)
        {
            string Mess =  Ex.Message;



            TaxShared taxShared = new TaxShared();
            taxShared._InsertLogDebug(props.InvoiceID, props.CompCode, props.System, props.UUID, Ex.Message, "Save Invoice Xml in path", "Url : " + filePath);


            // Handle any exceptions that might occur while loading the XML
            // For example, you can log the exception or handle it in a way that makes sense for your application
        }

        return invoiceXml;
    }
     public XmlDocument SaveInvoiceXmlReturnTax(  string xmlString , string filePath)
    {
        XmlDocument invoiceXml = new XmlDocument();
        invoiceXml.PreserveWhitespace = true;

        try
        {
            string formattedString = xmlString ;

            invoiceXml.LoadXml(formattedString);
             
            // Save the XML content to a file
            //string filePath = "C:\\Users\\Bse04\\Desktop\\XML_Test\\output.xml"; // Replace with the desired file path 
            invoiceXml.Save(filePath);

 
        }
        catch (System.Exception Ex)
        {
            string Mess =  Ex.Message;


 
            // Handle any exceptions that might occur while loading the XML
            // For example, you can log the exception or handle it in a way that makes sense for your application
        }

        return invoiceXml;
    }



    public string FormatXml(string xml)
    {
        // Load the input string as XML
        XElement xmlElement = XElement.Parse(xml);

        // Serialize the XML with indentation
    string Xml = @"<?xml version=""1.0"" encoding=""UTF-8""?>" +"\n"+ xmlElement.ToString();
        return Xml;
    }
    public Invoice_xml GetModelInvoice()
    {
        Invoice_xml props = new Invoice_xml();
        UBLExtensions _UBLExtensions = new UBLExtensions();
        UBLExtension _UBLExtension = new UBLExtension();
        ExtensionContent _ExtensionContent = new ExtensionContent();
        UBLDocumentSignatures _UBLDocumentSignatures = new UBLDocumentSignatures();
        SignatureInformation _SignatureInformation = new SignatureInformation();

        AdditionalDocumentReference _SinglAdditionalDocumentReference = new AdditionalDocumentReference();
        List<AdditionalDocumentReference> _AdditionalDocumentReference = new List<AdditionalDocumentReference>();


        Signature _Signature = new Signature();
        SignedInfo _SignedInfo = new SignedInfo();
        List<Reference> ListReference = new List<Reference>();
        Reference _Reference = new Reference();
        Transforms _Transforms = new Transforms();

        KeyInfo _KeyInfo = new KeyInfo();
        X509Data _X509Data = new X509Data();

        KSAEinvoice.Object _Object = new KSAEinvoice.Object();
        QualifyingProperties _QualifyingProperties = new QualifyingProperties();
        SignedProperties _SignedProperties = new SignedProperties();
        SignedSignatureProperties _SignedSignatureProperties = new SignedSignatureProperties();
        SigningCertificate _SigningCertificate = new SigningCertificate();
        Cert _Cert = new Cert();
        CertDigest _CertDigest = new CertDigest();
        IssuerSerial _IssuerSerial = new IssuerSerial();

        AccountingSupplierParty _AccountingSupplierParty = new AccountingSupplierParty();
        AccountingCustomerParty _AccountingCustomerParty = new AccountingCustomerParty();
        Party _Party = new Party();
        Party _Party_Cust = new Party();
        PartyIdentification _PartyIdentification = new PartyIdentification();
        PartyIdentification _PartyIdentification_Cust = new PartyIdentification();
        PostalAddress _PostalAddress = new PostalAddress();
        PostalAddress _PostalAddress_Cust = new PostalAddress();
        PartyTaxScheme _PartyTaxScheme = new PartyTaxScheme();
        PartyTaxScheme _PartyTaxScheme_Cust= new PartyTaxScheme();
        PartyLegalEntity _PartyLegalEntity = new PartyLegalEntity();
        PartyLegalEntity _PartyLegalEntity_Cust = new PartyLegalEntity();

        Country _Country = new Country();
        Country _Country_Cust = new Country(); 

        Delivery _Delivery = new Delivery();
        PaymentMeans _PaymentMeans = new PaymentMeans();

        TaxSubtotal _TaxSubtotal = new TaxSubtotal();
        TaxCategory _TaxCategory = new TaxCategory();


        TaxTotal _SinglTaxTotal = new TaxTotal();
        List<TaxTotal> _TaxTotal = new List<TaxTotal>();

        InvoiceLine_xml _SinglInvoiceLine = new InvoiceLine_xml();
        List<InvoiceLine_xml> _InvoiceLine = new List<InvoiceLine_xml>();

        Item _Item = new Item();
        ClassifiedTaxCategory _ClassifiedTaxCategory = new ClassifiedTaxCategory();

        Price _Price = new Price();

        LegalMonetaryTotal _LegalMonetaryTotal = new LegalMonetaryTotal();
        QrModel _QrModel = new QrModel();

        props.UBLExtensions = _UBLExtensions;
        props.UBLExtensions.UBLExtension = _UBLExtension;
        props.UBLExtensions.UBLExtension.ExtensionContent = _ExtensionContent;
        props.UBLExtensions.UBLExtension.ExtensionContent.UBLDocumentSignatures = _UBLDocumentSignatures;
        props.UBLExtensions.UBLExtension.ExtensionContent.UBLDocumentSignatures.SignatureInformation = _SignatureInformation;
        props.UBLExtensions.UBLExtension.ExtensionContent.UBLDocumentSignatures.SignatureInformation.Signature = _Signature;
        props.UBLExtensions.UBLExtension.ExtensionContent.UBLDocumentSignatures.SignatureInformation.Signature.SignedInfo = _SignedInfo;
        _Reference.Transforms = _Transforms;
        props.UBLExtensions.UBLExtension.ExtensionContent.UBLDocumentSignatures.SignatureInformation.Signature.SignedInfo.Reference = _Reference;
        //props.UBLExtensions.UBLExtension.ExtensionContent.UBLDocumentSignatures.SignatureInformation.Signature.SignedInfo.Reference.Add(_Reference);
        props.UBLExtensions.UBLExtension.ExtensionContent.UBLDocumentSignatures.SignatureInformation.Signature.KeyInfo = _KeyInfo;
        props.UBLExtensions.UBLExtension.ExtensionContent.UBLDocumentSignatures.SignatureInformation.Signature.KeyInfo.X509Data = _X509Data;

        props.UBLExtensions.UBLExtension.ExtensionContent.UBLDocumentSignatures.SignatureInformation.Signature.Object = _Object;
        props.UBLExtensions.UBLExtension.ExtensionContent.UBLDocumentSignatures.SignatureInformation.Signature.Object.QualifyingProperties = _QualifyingProperties;
        props.UBLExtensions.UBLExtension.ExtensionContent.UBLDocumentSignatures.SignatureInformation.Signature.Object.QualifyingProperties.SignedProperties = _SignedProperties;
        props.UBLExtensions.UBLExtension.ExtensionContent.UBLDocumentSignatures.SignatureInformation.Signature.Object.QualifyingProperties.SignedProperties.SignedSignatureProperties = _SignedSignatureProperties;
        props.UBLExtensions.UBLExtension.ExtensionContent.UBLDocumentSignatures.SignatureInformation.Signature.Object.QualifyingProperties.SignedProperties.SignedSignatureProperties.SigningCertificate = _SigningCertificate;
        props.UBLExtensions.UBLExtension.ExtensionContent.UBLDocumentSignatures.SignatureInformation.Signature.Object.QualifyingProperties.SignedProperties.SignedSignatureProperties.SigningCertificate.Cert = _Cert;
        props.UBLExtensions.UBLExtension.ExtensionContent.UBLDocumentSignatures.SignatureInformation.Signature.Object.QualifyingProperties.SignedProperties.SignedSignatureProperties.SigningCertificate.Cert.CertDigest = _CertDigest;
        props.UBLExtensions.UBLExtension.ExtensionContent.UBLDocumentSignatures.SignatureInformation.Signature.Object.QualifyingProperties.SignedProperties.SignedSignatureProperties.SigningCertificate.Cert.IssuerSerial = _IssuerSerial;


        //props.AdditionalDocumentReference = _AdditionalDocumentReference;
        props.AdditionalDocumentReference = _SinglAdditionalDocumentReference;
        //props.AdditionalDocumentReference.Add(_SinglAdditionalDocumentReference);
        props.Signature = _Signature;

        props.AccountingSupplierParty = _AccountingSupplierParty;
        props.AccountingSupplierParty.Party = _Party;
        props.AccountingSupplierParty.Party.PartyIdentification = _PartyIdentification;
        props.AccountingSupplierParty.Party.PartyLegalEntity = _PartyLegalEntity;
        props.AccountingSupplierParty.Party.PartyTaxScheme = _PartyTaxScheme;
        props.AccountingSupplierParty.Party.PostalAddress = _PostalAddress;
        props.AccountingSupplierParty.Party.PostalAddress.Country = _Country; 

        props.AccountingCustomerParty = _AccountingCustomerParty;
        props.AccountingCustomerParty.Party = _Party_Cust;
        props.AccountingCustomerParty.Party.PartyIdentification = _PartyIdentification_Cust;
        props.AccountingCustomerParty.Party.PartyLegalEntity = _PartyLegalEntity_Cust;
        props.AccountingCustomerParty.Party.PartyTaxScheme = _PartyTaxScheme_Cust;
        props.AccountingCustomerParty.Party.PostalAddress = _PostalAddress_Cust;
        props.AccountingCustomerParty.Party.PostalAddress.Country = _Country_Cust; 

        props.Delivery = _Delivery;
        props.PaymentMeans = _PaymentMeans;

        //props.TaxTotal = _TaxTotal;
        _SinglTaxTotal.TaxSubtotal = _TaxSubtotal;
        _SinglTaxTotal.TaxSubtotal.TaxCategory = _TaxCategory; 
        props.TaxTotal_Header = _SinglTaxTotal;


        props.LegalMonetaryTotal = _LegalMonetaryTotal;

        props.InvoiceLine = _InvoiceLine;
        _SinglInvoiceLine.TaxTotal = _SinglTaxTotal;
        _SinglInvoiceLine.Item = _Item;
        _SinglInvoiceLine.Item.ClassifiedTaxCategory = _ClassifiedTaxCategory; 
        _SinglInvoiceLine.Price = _Price;


        props.QrModel = _QrModel;
        props.InvoiceLine.Add(_SinglInvoiceLine);

        return props;
    }



    public string CreateHash(string xmlContent)
    {
        // Convert the XML content to bytes
        byte[] xmlBytes = Encoding.UTF8.GetBytes(xmlContent);

        // Convert the bytes to a Base64 string
        string base64String = Convert.ToBase64String(xmlBytes);

        return base64String;
    }



    public ZATCASimplifiedInvoiceProps GetNewRoot_Old()
    {
        ZATCASimplifiedInvoiceProps props = new ZATCASimplifiedInvoiceProps();
        EGSUnitInfo _EGSUnitInfo = new EGSUnitInfo();
        EGSUnitLocation _EGSUnitLocation = new EGSUnitLocation();

        ZATCASimplifiedInvoiceLineItem _lineItem = new ZATCASimplifiedInvoiceLineItem();
        ZATCASimplifiedInvoiceLineItem[] _lineItemArray = new ZATCASimplifiedInvoiceLineItem[1];

        ZATCASimplifiedInvoiceLineItemDiscount _Discounts = new ZATCASimplifiedInvoiceLineItemDiscount();
        ZATCASimplifiedInvoiceLineItemDiscount[] _DiscountsArray = new ZATCASimplifiedInvoiceLineItemDiscount[1];

        ZATCASimplifiedInvoiceLineItemTax _OtherTaxes = new ZATCASimplifiedInvoiceLineItemTax();
        ZATCASimplifiedInvoiceLineItemTax[] _OtherTaxesArray = new ZATCASimplifiedInvoiceLineItemTax[1];

        ZATCASimplifiedInvoicCancelation cancelation = new ZATCASimplifiedInvoicCancelation();

        _EGSUnitInfo.location = _EGSUnitLocation;
        props.egs_info = _EGSUnitInfo;
        props.line_items = _lineItemArray;
        props.line_items[0] = (_lineItem);

        props.line_items[0].Discounts = _DiscountsArray;
        props.line_items[0].Discounts[0] = _Discounts;

        props.line_items[0].OtherTaxes = _OtherTaxesArray;
        props.line_items[0].OtherTaxes[0] = _OtherTaxes;

        props.cancelation = cancelation;

        return props;
    }


  
    public TaxResponse _InsertLogDebug(long TRID, int Comp, string System, string UUID, string result, string Op_Descr,  string Remarks)
    {
        TaxResponse response = new TaxResponse();

        try
        {

            if (GTaxControl.IsDebugmode != true)
            { 
                response.IsSuccess = true;
                return response;
            }

             
            string URL = GlobalVariables.Url_Api;

            // Your SQL update query Tax_Log 
            DateTime currentDateAndTime = DateTime.Now;
            string Log_Query = @"INSERT INTO [dbo].[Log_Debug]
           ([System]
           , [TimeStamp]
           , [Comp]
           , [TRID]
           , [UUID]
           , [URL]
           , [Result]
           , [Op_Descr]
           , [Remarks])" +
                "VALUES(N'" + System + "',N'" + currentDateAndTime + "'," + Comp + "," + TRID + ",N'" + UUID + "',N'" + URL + "',N'" + result + "',N'" + Op_Descr + "',N'" + Remarks + "')";

            using (SqlConnection connection = new SqlConnection(GlobalVariables.connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(Log_Query, connection))
                {
                    // Execute the update query
                    int rowsAffected = command.ExecuteNonQuery();
                }
            }

            response.IsSuccess = true;
            return response;
        }
        catch (Exception ex)
        {
            response.ErrorMessage = " Error Insert LogDebug : " + ex.Message;
            response.IsSuccess = false;
            return response;
        }


    }


}



public static class NumberExtensions
{
    public static string ToFixedNoRounding(this double number, int n)
    {
        string str = number.ToString();
        System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex("^-?\\d+(?:\\.\\d{0," + n + "})?");
        System.Text.RegularExpressions.Match match = reg.Match(str);
        if (match.Success)
        {
            string a = match.Value;
            int dot = a.IndexOf(".");
            if (dot == -1)
            {
                return a + "." + new string('0', n);
            }
            int b = n - (a.Length - dot) + 1;
            return b > 0 ? (a + new string('0', b)) : a;
        }
        return "0.00";
    }
}

public static class DecimalExtensions
{
    public static string ToFixedNoRounding(this decimal number, int n)
    {
        decimal originalValue = number;
        int decimalPlaces = n;

        decimal roundedValue = Math.Round(originalValue, decimalPlaces, MidpointRounding.AwayFromZero);

        return roundedValue.ToString();
         
    }

    public static string ToFixedNoRounding_Old(this decimal number, int n)
    {
        string str = number.ToString();
        System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex("^-?\\d+(?:\\.\\d{0," + n + "})?");
        System.Text.RegularExpressions.Match match = reg.Match(str);
        if (match.Success)
        {
            string a = match.Value;
            int dot = a.IndexOf(".");
            if (dot == -1)
            {
                return a + "." + new string('0', n);
            }
            int b = n - (a.Length - dot) + 1;
            return b > 0 ? (a + new string('0', b)) : a;
        }
        return "0.00";
    }
}