using SignXMLDocument;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using ZatcaIntegrationSDK.BLL;

namespace KSAEinvoice
{

    public class Create_Inv_UBLExtensions : TaxShared
    {
        string UBLExtensions = @" <ext:UBLExtensions> 
                                    <ext:UBLExtension>
                                         <ext:ExtensionURI>urn:oasis:names:specification:ubl:dsig:enveloped:xades</ext:ExtensionURI>
                                         <ext:ExtensionContent>
                                            <sig:UBLDocumentSignatures xmlns:sac=""urn:oasis:names:specification:ubl:schema:xsd:SignatureAggregateComponents-2""
                                                                       xmlns:sbc=""urn:oasis:names:specification:ubl:schema:xsd:SignatureBasicComponents-2""
                                                                       xmlns:sig=""urn:oasis:names:specification:ubl:schema:xsd:CommonSignatureComponents-2"">
                                               <sac:SignatureInformation>
                                                  <cbc:ID>urn:oasis:names:specification:ubl:signature:1</cbc:ID>
                                                  <sbc:ReferencedSignatureID>urn:oasis:names:specification:ubl:signature:Invoicesadas</sbc:ReferencedSignatureID>
                                                  <ds:Signature xmlns:ds=""http://www.w3.org/2000/09/xmldsig#"" Id=""signature"" >
                                                     <ds:SignedInfo>
                                                        <ds:CanonicalizationMethod Algorithm =""http://www.w3.org/2006/12/xml-c14n11""/>
                                                        <ds:SignatureMethod Algorithm=""http://www.w3.org/2001/04/xmldsig-more#rsa-sha256""/> 
                                                         <ds:Reference Id=""invoiceSignedData"" URI="""">
                                                           <ds:Transforms>
                                                              <ds:Transform Algorithm=""http://www.w3.org/TR/1999/REC-xpath-19991116"">
                                                                 <ds:XPath>not(//ancestor-or-self::ext:UBLExtensions)</ds:XPath>
                                                              </ds:Transform>
                                                              <ds:Transform Algorithm=""http://www.w3.org/TR/1999/REC-xpath-19991116""> 
                                                                  <ds:XPath>not(//ancestor-or-self::cac:Signature)</ds:XPath>
                                                              </ds:Transform>
                                                              <ds:Transform Algorithm=""http://www.w3.org/TR/1999/REC-xpath-19991116""> 
                                                                  <ds:XPath>not(//ancestor-or-self::cac:AdditionalDocumentReference[cbc:ID='QR'])</ds:XPath>
                                                              </ds:Transform>
                                                              <ds:Transform Algorithm=""http://www.w3.org/2006/12/xml-c14n11""/> 
                                                            </ds:Transforms>
                                                              <ds:DigestMethod Algorithm=""http://www.w3.org/2001/04/xmlenc#sha256""/>
                                                               <ds:DigestValue>SET_Transforms_DigestValue</ds:DigestValue>
                                                         </ds:Reference>
                                                        <ds:Reference Type=""http://www.w3.org/2000/09/xmldsig#SignatureProperties"" URI=""#xadesSignedProperties""> 
                                                               <ds:DigestMethod Algorithm=""http://www.w3.org/2001/04/xmlenc#sha256""/>
                                                            <ds:DigestValue>SET_Reference_DigestValue</ds:DigestValue>
                                                         </ds:Reference>
                                                     </ds:SignedInfo>
                                                     <ds:SignatureValue>SET_SignatureValue</ds:SignatureValue>
                                                     <ds:KeyInfo>
                                                        <ds:X509Data>
                                                           <ds:X509Certificate>SET_X509Certificate</ds:X509Certificate>
                                                        </ds:X509Data>
                                                     </ds:KeyInfo>
                                                     <ds:Object>
                                                        <xades:QualifyingProperties Target=""signature"" xmlns:xades= ""http://uri.etsi.org/01903/v1.3.2#"" > 
                                                            <xades:SignedProperties Id=""xadesSignedProperties""> 
                                                               <xades:SignedSignatureProperties>
                                                                 <xades:SigningTime>SET_SigningTime</xades:SigningTime>
                                                                  <xades:SigningCertificate>
                                                                    <xades:Cert>
                                                                       <xades:CertDigest>
                                                                          <ds:DigestMethod Algorithm=""http://www.w3.org/2001/04/xmlenc#sha256""/> 
                                                                           <ds:DigestValue>SET_CertDigest_DigestValue</ds:DigestValue>
                                                                        </xades:CertDigest>
                                                                       <xades:IssuerSerial>
                                                                          <ds:X509IssuerName>SET_X509IssuerName</ds:X509IssuerName>
                                                                          <ds:X509SerialNumber>SET_X509SerialNumber</ds:X509SerialNumber>
                                                                       </xades:IssuerSerial>
                                                                    </xades:Cert>
                                                                 </xades:SigningCertificate>
                                                              </xades:SignedSignatureProperties>
                                                           </xades:SignedProperties>
                                                        </xades:QualifyingProperties>
                                                     </ds:Object>
                                                  </ds:Signature>
                                               </sac:SignatureInformation>
                                            </sig:UBLDocumentSignatures>
                                         </ext:ExtensionContent>
                                      </ext:UBLExtension>
                                </ext:UBLExtensions>";


        public string Creare_UBLExtensions(UBLExtensions UBLEX )
        {
            string singl_template = UBLExtensions;


            singl_template = singl_template.Replace("SET_Transforms_DigestValue", SetVal(UBLEX.UBLExtension.ExtensionContent.UBLDocumentSignatures.SignatureInformation.Signature.SignedInfo.Reference.Transforms.DigestValue));
            singl_template = singl_template.Replace("SET_Reference_DigestValue", SetVal(UBLEX.UBLExtension.ExtensionContent.UBLDocumentSignatures.SignatureInformation.Signature.SignedInfo.Reference.DigestValue));

            //singl_template = singl_template.Replace("SET_SignatureValue", SetVal(UBLEX.UBLExtension.ExtensionContent.UBLDocumentSignatures.SignatureInformation.Signature.SignatureValue));
            singl_template = singl_template.Replace("SET_X509Certificate", SetVal(UBLEX.UBLExtension.ExtensionContent.UBLDocumentSignatures.SignatureInformation.Signature.KeyInfo.X509Data.X509Certificate));

            EInvoiceSigningLogic _IEInvoiceSigningLogic = new EInvoiceSigningLogic();
            string GetDigitalSignature = _IEInvoiceSigningLogic.GetDigitalSignature(GTaxControl.LastHash, GTaxControl.PrivKey).ResultedValue.ToString();

             
            GlobalVariables.CertificateValue = GTaxControl.Certificate;

            singl_template = singl_template.Replace("SET_SignatureValue", SetVal(GetDigitalSignature));
            singl_template = singl_template.Replace("SET_X509Certificate", SetVal(GlobalVariables.CertificateValue));




            singl_template = singl_template.Replace("SET_SigningTime", SetVal(UBLEX.UBLExtension.ExtensionContent.UBLDocumentSignatures.SignatureInformation.Signature.Object.QualifyingProperties.SignedProperties.SignedSignatureProperties.SigningTime.ToString("yyyy-MM-ddTHH:mm:ssZ")));
            singl_template = singl_template.Replace("SET_CertDigest_DigestValue", SetVal(UBLEX.UBLExtension.ExtensionContent.UBLDocumentSignatures.SignatureInformation.Signature.Object.QualifyingProperties.SignedProperties.SignedSignatureProperties.SigningCertificate.Cert.CertDigest.DigestValue));
      

            singl_template = singl_template.Replace("SET_X509IssuerName", SetVal(UBLEX.UBLExtension.ExtensionContent.UBLDocumentSignatures.SignatureInformation.Signature.Object.QualifyingProperties.SignedProperties.SignedSignatureProperties.SigningCertificate.Cert.IssuerSerial.X509IssuerName));

              
            singl_template = singl_template.Replace("SET_X509SerialNumber", SetVal(UBLEX.UBLExtension.ExtensionContent.UBLDocumentSignatures.SignatureInformation.Signature.Object.QualifyingProperties.SignedProperties.SignedSignatureProperties.SigningCertificate.Cert.IssuerSerial.X509SerialNumber));
            
             
            return singl_template;

        }


        // Generate a unique serial number (positive integer)
        private static uint GenerateUniqueSerialNumber()
        {
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                byte[] bytes = new byte[4];
                rng.GetBytes(bytes);
                return BitConverter.ToUInt32(bytes, 0);
            }
        }

        // Remove leading zero bytes to comply with X.509 encoding
        private static byte[] RemoveLeadingZeroBytes(byte[] bytes)
        {
            int i = 0;
            while (i < bytes.Length && bytes[i] == 0)
                i++;

            if (i == bytes.Length)
                return new byte[] { 0x00 };  // Special case for zero serial number

            byte[] result = new byte[bytes.Length - i];
            Array.Copy(bytes, i, result, 0, result.Length);
            return result;
        }




    }
}
