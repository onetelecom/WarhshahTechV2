
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZatcaIntegrationSDK.BLL;
//using QRCoder;

namespace KSAEinvoice
{


    public class QRGenerator : TaxShared
    {










        public string QRGenerNew(QrModel Model)
        {
            EInvoiceSigningLogic _IEInvoiceSigningLogic = new EInvoiceSigningLogic();
            // Assuming GetDigitalSignature returns a string
            string GetDigitalSignature = _IEInvoiceSigningLogic.GetDigitalSignature(Model.HashedXml, Model.privateKey).ResultedValue.ToString();
             
            GlobalVariables.CertificateValue = Model.Certificate;

            // Generate TLV for each field
            byte[] _CompName = GetTLVForValue("1", Model.CompName);
            byte[] _VatNo = GetTLVForValue("2", Model.VatNo);
            string Day = Convert.ToDateTime(Model.TrDate).ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'ff'Z'");
            byte[] _Day = GetTLVForValue("3", Day);
            byte[] _invoiceTotal = GetTLVForValue("4", Model.invoiceTotal.ToString());
            byte[] _vatTotal = GetTLVForValue("5", Model.vatTotal.ToString());
            byte[] _HashedXml = GetTLVForValue("6", Model.HashedXml);
            byte[] _public_key = GetTLVForValue("7", Model.public_key);
            byte[] _GetDigitalSignature = GetTLVForValue("8", GetDigitalSignature);

            List<byte[]> tagsBufsArray = new List<byte[]>
                    {
                        _CompName, _VatNo, _Day, _invoiceTotal, _vatTotal, _HashedXml, _public_key, _GetDigitalSignature
                    };

            // Concatenate the TLV-encoded fields
            byte[] qrCodeBytes = ConcatenateTLV(tagsBufsArray);

            // Convert bytes to base64
            string qrCodeB64 = Convert.ToBase64String(qrCodeBytes);

            return qrCodeB64;
        }

        public byte[] GetTLVForValue(string tagNum, string tagValue)
        {
            // Convert tagNum and tagValue to bytes
            byte[] tagNumBytes = Encoding.UTF8.GetBytes(tagNum);
            byte[] tagValueBytes = Encoding.UTF8.GetBytes(tagValue);

            // Create TLV encoding: tag + length + value
            byte[] lengthBytes = new byte[] { (byte)tagValueBytes.Length };

            // Concatenate tag, length, and value bytes
            byte[] tlvBytes = tagNumBytes.Concat(lengthBytes).Concat(tagValueBytes).ToArray();

            return tlvBytes;
        }

        public byte[] ConcatenateTLV(List<byte[]> tagsBufsArray)
        {
            int totalLength = tagsBufsArray.Sum(arr => arr.Length);
            byte[] concatenatedBytes = new byte[totalLength];

            int currentIndex = 0;
            foreach (var tagBuf in tagsBufsArray)
            {
                Buffer.BlockCopy(tagBuf, 0, concatenatedBytes, currentIndex, tagBuf.Length);
                currentIndex += tagBuf.Length;
            }

            return concatenatedBytes;
        }












        //public string Get_QR(QrModel Model)
        //{
        //    // Test Case 
        //    //Model.CompName = "Safe Systems Est.";
        //    //Model.VatNo = "310122393500003";
        //    //Model.Total = 500;
        //    //Model.Vat = Convert.ToDecimal( 75);
        //    //Model.TrDate = Convert.ToDateTime("2022-11-30T14:30:00Z");

        //    //Qr = 010C426F6273205265636F726473020F3331303132323339333530303030330314323032322D30342D32355431363A33303A30305A0407313030302E303005063135302E3030
        //    // QR64 = AQxCb2JzIFJlY29yZHMCDzMxMDEyMjM5MzUwMDAwMwMUMjAyMi0wNC0yNVQxNjozMDowMFoEBzEwMDAuMDAFBjE1MC4wMA==

        //    string Qr = ""; string Qr64;
        //    string st; DateTime d;
        //    List<TLV> TlvList = new List<TLV>();



        //    st = Model.CompName;
        //    TlvList.Add(genartateTLV(1, st));
        //    TlvList.Add(genartateTLV(2, Model.VatNo));

        //    d = Convert.ToDateTime(Model.TrDate);
        //    st = d.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'ff'Z'");
        //    TlvList.Add(genartateTLV(3, st));
        //    st = Convert.ToDecimal(Model.invoiceTotal + Model.vatTotal).ToString("#0.00");

        //    TlvList.Add(genartateTLV(4, st));
        //    st = Convert.ToDecimal(Model.vatTotal).ToString("#0.00");

        //    TlvList.Add(genartateTLV(5, st));




        //    foreach (var item in TlvList)
        //    {
        //        Qr = Qr + item.HexVal;
        //    }
        //    var HexArr = new byte[Qr.Length / 2];
        //    for (var i = 0; i < Qr.Length / 2; i++)
        //    {
        //        HexArr[i] = Convert.ToByte(Qr.Substring(i * 2, 2), 16);
        //    }

        //    Qr64 = System.Convert.ToBase64String(HexArr);

        //    return (Qr64);

        //}


        //public string NewGet_QR(QrModel Model)
        //{
        //    EInvoiceSigningLogic _IEInvoiceSigningLogic = new EInvoiceSigningLogic();

        //    // Assuming GetDigitalSignature returns a string
        //    string GetDigitalSignature = _IEInvoiceSigningLogic.GetDigitalSignature(Model.HashedXml, Model.privateKey).ResultedValue.ToString();

        //    GlobalVariables.SignatureNewValue = GetDigitalSignature;
        //    GlobalVariables.CertificateValue = Model.Certificate;

        //    // Convert publicKey String to byte[]  
        //    byte[] publicKeyBytes = Convert.FromBase64String(Model.public_key);

        //    // Convert certificateContent String to byte[]  
        //    byte[] certificateBytes = Convert.FromBase64String(Model.Certificate);

        //    string Qr = "";
        //    string st;
        //    DateTime d;
        //    List<TLV> TlvList = new List<TLV>();

        //    st = Model.CompName;
        //    TlvList.Add(genartateTLV(1, st));
        //    TlvList.Add(genartateTLV(2, Model.VatNo));

        //    d = Convert.ToDateTime(Model.TrDate);
        //    st = d.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'ff'Z'");
        //    TlvList.Add(genartateTLV(3, st));

        //    // Assuming invoiceTotal and vatTotal are in decimal format
        //    st = Convert.ToDecimal(Model.invoiceTotal + Model.vatTotal).ToString("#0.00");
        //    TlvList.Add(genartateTLV(4, st));

        //    st = Convert.ToDecimal(Model.vatTotal).ToString("#0.00");
        //    TlvList.Add(genartateTLV(5, st));

        //    // Assuming HashedXml is a string
        //    TlvList.Add(genartateTLV(6, Model.HashedXml));

        //    // Convert publicKeyBytes to a string representation
        //    st = Model.public_key;
        //    TlvList.Add(genartateTLV(7, st));

        //    st = GetDigitalSignature;
        //    TlvList.Add(genartateTLV(8, st));

        //    // Convert certificateBytes to a string representation
        //    st = Model.Certificate;
        //    TlvList.Add(genartateTLV(9, st));

        //    foreach (var item in TlvList)
        //    {
        //        Qr = Qr + item.HexVal;
        //    }

        //    var HexArr = new byte[Qr.Length / 2];
        //    for (var i = 0; i < Qr.Length / 2; i++)
        //    {
        //        HexArr[i] = Convert.ToByte(Qr.Substring(i * 2, 2), 16);
        //    }

        //    string Qr64 = System.Convert.ToBase64String(HexArr);
        //    return Qr64;
        //}

        //public string Get_QR_New(QrModel Model)
        //{

        //    QRValidator _IQRValidator = new QRValidator();



        //    DateTime trDate = Convert.ToDateTime(Model.TrDate);
        //    //string day = trDate.ToString("yyyyMMddTHHmmss");
        //    string day = trDate.ToString("yyyy-MM-ddTHH:mm:ssZ"); ;
        //    //string day =  ConvertToUnixTimestampSeconds(d).ToString(); 



        //    EInvoiceSigningLogic _IEInvoiceSigningLogic = new EInvoiceSigningLogic();

        //    string GetDigitalSignature = _IEInvoiceSigningLogic.GetDigitalSignature(Model.HashedXml, Model.privateKey).ResultedValue.ToString();


        //    GlobalVariables.SignatureValue = GetDigitalSignature;
        //    GlobalVariables.CertificateValue = Model.Certificate;
        //    //    // Convert publicKey String to byte[]  
        //    sbyte[] publicKeyBytes = ConvertToSByteArray(Model.public_key);

        //    //    // Convert certificateContent String to sbyte[]  
        //    sbyte[] certificateBytes = ConvertToSByteArray(Model.Certificate);

        //    bool answer = true;
        //    string GenerateQrCode = _IQRValidator.GenerateQrCodeFromValues(Model.CompName, Model.VatNo, day, Model.invoiceTotal.ToString(), Model.vatTotal.ToString(), Model.HashedXml, publicKeyBytes, GetDigitalSignature, answer, certificateBytes);


        //    return (GenerateQrCode);

        //}

        //public string Get_QR_Test(string xmlAsString)
        //{

        //    QRValidator _IQRValidator = new QRValidator();

        //    SaveInvoiceXml(FormatXml(xmlAsString), GlobalVariables.SaveUrlFile + ".xml");

        //    string path = GlobalVariables.SaveUrlFile + ".xml";
        //    var GenerateQrCode = _IQRValidator.GenerateEInvoiceQRCode(path);


        //    return (GenerateQrCode.ResultedValue);

        //}

        //public static long ConvertToUnixTimestampSeconds(DateTime date)
        //{
        //    // Calculate the Unix timestamp in seconds
        //    return (long)(date - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
        //}

        //public static sbyte[] ConvertToSByteArray(string input)
        //{
        //    // convert publicKey String to Sbyte*  
        //    byte[] publicKeybytes = Encoding.ASCII.GetBytes(input);
        //    //convert it to sbyte array
        //    sbyte[] publicKeySbytes = new sbyte[publicKeybytes.Length];
        //    for (int i = 0; i < publicKeybytes.Length; i++)
        //    {
        //        publicKeySbytes[i] = (sbyte)publicKeybytes[i];
        //    }


        //    //// Encode the input string as UTF-8 and convert it to bytes
        //    //byte[] byteArray = Encoding.UTF8.GetBytes(input);

        //    //// Convert each byte to a signed byte (sbyte)
        //    //sbyte[] sbyteArray = new sbyte[byteArray.Length];
        //    //Buffer.BlockCopy(byteArray, 0, sbyteArray, 0, byteArray.Length);

        //    return publicKeySbytes;
        //}

        //public string QrGeneratorStage2(QrModel Model)
        //{
        //    // Test Case 
        //    //Model.CompName = "Safe Systems Est.";
        //    //Model.VatNo = "310122393500003";
        //    //Model.Total = 500;
        //    //Model.Vat = Convert.ToDecimal( 75);
        //    //Model.TrDate = Convert.ToDateTime("2022-11-30T14:30:00Z");

        //    //Qr = 010C426F6273205265636F726473020F3331303132323339333530303030330314323032322D30342D32355431363A33303A30305A0407313030302E303005063135302E3030
        //    // QR64 = AQxCb2JzIFJlY29yZHMCDzMxMDEyMjM5MzUwMDAwMwMUMjAyMi0wNC0yNVQxNjozMDowMFoEBzEwMDAuMDAFBjE1MC4wMA==

        //    string Qr = ""; string Qr64;
        //    string st; DateTime d;
        //    List<TLV> TlvList = new List<TLV>();



        //    st = Model.CompName;
        //    TlvList.Add(genartateTLV(1, st));
        //    TlvList.Add(genartateTLV(2, Model.VatNo));

        //    d = Convert.ToDateTime(Model.TrDate);
        //    st = d.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'ff'Z'");
        //    TlvList.Add(genartateTLV(3, st));
        //    st = Convert.ToDecimal(Model.invoiceTotal + Model.vatTotal).ToString("#0.00");

        //    TlvList.Add(genartateTLV(4, st));
        //    st = Convert.ToDecimal(Model.vatTotal).ToString("#0.00");

        //    TlvList.Add(genartateTLV(5, st));


        //    TlvList.Add(genartateTLV(6, Model.HashedXml));
        //    TlvList.Add(genartateTLV(7, Model.DigitalSignature));
        //    //TlvList.Add(genartateTLV(8, Model.public_key));

        //    foreach (var item in TlvList)
        //    {
        //        Qr = Qr + item.HexVal;
        //    }
        //    var HexArr = new byte[Qr.Length / 2];
        //    for (var i = 0; i < Qr.Length / 2; i++)
        //    {
        //        HexArr[i] = Convert.ToByte(Qr.Substring(i * 2, 2), 16);
        //    }

        //    Qr64 = System.Convert.ToBase64String(HexArr);

        //    return (Qr64);

        //}
        //private static TLV genartateTLV(byte tp, string Val)
        //{
        //    TLV itm = new TLV();
        //    itm.TAG = tp;
        //    itm.Len = Convert.ToByte(Encoding.UTF8.GetBytes(Val).Length);
        //    itm.Value = Val;
        //    itm.HexVal = itm.TAG.ToString("X2") + itm.Len.ToString("X2") + ToHexString(Val);

        //    return (itm);
        //}
        //public static string ToHexString(string str)
        //{
        //    var sb = new StringBuilder();

        //    var bytes = Encoding.UTF8.GetBytes(str);
        //    foreach (var t in bytes)
        //    {
        //        sb.Append(t.ToString("X2"));
        //    }

        //    return sb.ToString();
        //}


        //public BitmapByteQRCode toQrCode(QrModel Model, int width = 250, int height = 250)
        //{
        //    string Qr = QrGenerator(Model);

        //    QRCodeGenerator Gn = new QRCodeGenerator;
        //    BitmapByteQRCode qr = Gn.CreateQrCode(Qr, 2);
        //    {
        //        Format = BarcodeFormat.QR_CODE,
        //        Options = new EncodingOptions
        //        {
        //            Width = width,
        //            Height = height
        //        }
        //    };
        //    Bitmap QrCode = barcodeWriter.Write(this.ToBase64());

        //    return QrCode;
        //}

    }
}