
using Newtonsoft.Json;
using SignXMLDocument;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using ZatcaIntegrationSDK;
using ZatcaIntegrationSDK.APIHelper;
using ZatcaIntegrationSDK.BLL;
using ZatcaIntegrationSDK.HelperContracts;
using System.Xml.Linq;
using System.IO; 
using System.Reflection.Emit;
using javax.xml.transform;
using java.util;

namespace KSAEinvoice
{

    public partial class _InvTax
    {
        public enum Mode_Send
        {
            developer,
            Simulation,
            Production
        }

        public enum Type
        {
            IS_Standard = 1,
            Simplified = 0
        }

        public _SignXMLDocument tax_KSA = new _SignXMLDocument();
        private TypeInv _TypeInv = new TypeInv();
        private Type_Code _Type_Code = new Type_Code();

        string NameXml = "";

        public TaxResponse GetInvoiceType(bool IS_Standard, Invoice_xml props)
        {
            TaxResponse response = new TaxResponse();

            try
            {

                props.ProfileID = "reporting:1.0";
                //***************************************************************************************
                if (IS_Standard)
                {
                    props.InvoiceType = _TypeInv.Standard;
                    NameXml = "Standard";
                }
                else
                {
                    props.InvoiceType = _TypeInv.Simplified;
                    NameXml = "Simplified";
                }

                //if (props.TrType == 0)
                //{
                //    if (props.TypeCode != "386")
                //    {
                //        props.TypeCode = _Type_Code.INV;
                //        NameXml = NameXml + "_Inv";
                //    } 
                //}
                //else if (props.TrType == 1)
                //{
                //    props.TypeCode = _Type_Code.Credit;
                //    NameXml = NameXml + "_Credit";
                //}
             




                props.DocumentCurrencyCode = GTaxControl.CurrencyCode; // Example currency code
                props.TaxCurrencyCode = GTaxControl.CurrencyCode; // Example tax currency code 
                props.AdditionalDocumentReference.ID = "ICV"; // DocumentReference ID


                //********************************************************Supplier***********************************

                props.AccountingCustomerParty.Party.PartyIdentification.Scheme_Name = GTaxControl.Scheme_Name;

                props.AccountingSupplierParty.Party.PartyIdentification.Scheme_Name = GTaxControl.Scheme_Name;
                props.AccountingSupplierParty.Party.PartyIdentification.ID = GTaxControl.VAT_NO;
                props.AccountingSupplierParty.Party.PostalAddress.BuildingNumber = GTaxControl.Address_BuildingNo;
                props.AccountingSupplierParty.Party.PostalAddress.CityName = GTaxControl.Address_City;
                props.AccountingSupplierParty.Party.PostalAddress.CitySubdivisionName = GTaxControl.Address_City;
                props.AccountingSupplierParty.Party.PostalAddress.CountrySubentity = GTaxControl.Address_City;
                props.AccountingSupplierParty.Party.PostalAddress.PlotIdentification = GTaxControl.VAT_NO;//InvHeader.Sub_PlotIdentification;
                props.AccountingSupplierParty.Party.PostalAddress.PostalZone = GTaxControl.Address_Postal;
                props.AccountingSupplierParty.Party.PostalAddress.StreetName = GTaxControl.Address_Street;
                props.AccountingSupplierParty.Party.PostalAddress.Country.IdentificationCode = GTaxControl.NationalityCode;
                props.AccountingSupplierParty.Party.PartyLegalEntity.RegistrationName = GTaxControl.Comp_Name;// "ARABIAN BUILDING SUPPORT";
                props.AccountingSupplierParty.Party.PartyTaxScheme.CompanyID = GTaxControl.VAT_NO;

                //**************************************************UBLExtensions******************************************************  

                props.UBLExtensions.UBLExtension.ExtensionContent.UBLDocumentSignatures.SignatureInformation.Signature.KeyInfo.X509Data.X509Certificate = GTaxControl.Certificate;


                ////**************************************************QrModel******************************************************
                //props.QrModel.CompName = _Shared.Decrypt(InvHeader.sub_Name);
                //props.QrModel.HashedXml = GTaxControl.LastHash;
                //props.QrModel.privateKey = GTaxControl.PrivKey;
                //props.QrModel.public_key = GTaxControl.PubKEY;
                //props.QrModel.Certificate = GTaxControl.Certificate;
                //props.QrModel.invoiceTotal = invoiceTotal;
                //props.QrModel.vatTotal = vatTotal;
                //props.QrModel.VatNo = GTaxControl.VAT_NO;
                //props.QrModel.TrDate = _trDate;



                GlobalVariables.InvoiceID = props.InvoiceID;
                GlobalVariables.InvoiceTrNo = props.AdditionalDocumentReference.UUID;

                //string rootPath = AppDomain.CurrentDomain.BaseDirectory;
                string rootPath = GTaxControl.PathSaveXml;

                if (rootPath == null)
                {
                    rootPath = AppDomain.CurrentDomain.BaseDirectory;
                }

                int currentYear = DateTime.Now.Year;

                //GlobalVariables.SaveUrlFile = rootPath + @"\\Comp" + props.CompCode + "\\" + currentYear;
                GlobalVariables.SaveUrlFile = rootPath + "\\" + currentYear + @"\\Comp" + props.CompCode ;

                Directory.CreateDirectory(GlobalVariables.SaveUrlFile);

                GlobalVariables.SaveUrlFile = GlobalVariables.SaveUrlFile + "\\" + props.UUID.ToString();

                response.IsSuccess = true;
                response.Response = props;
                return response;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = ex.Message;
                return response;
            }



        }

        public TaxResponse CreateRoot_Xml(Invoice_xml props, bool IS_Standard)
        {
            TaxResponse response = new TaxResponse();

            try
            {
                if (!string.IsNullOrEmpty(props.PrevInvoiceHash) && props.PrevInvoiceHash.Trim() != "")
                {
                    GTaxControl.LastHash = props.PrevInvoiceHash;
                }
       
                //if (!string.IsNullOrEmpty(props.PrevInvoiceHash))
                //{
                //    if (props.PrevInvoiceHash.Trim() != "")
                //    {
                //        GTaxControl.LastHash = props.PrevInvoiceHash;
                //    }
                //}




                if (IS_Standard)
                {
                    response = tax_KSA.GetXmlStandard(props);
                }
                else
                {
                    response = tax_KSA.GetXmlSimplified(props);

                }

                GlobalVariables.TaxStatus = 1;

                return response;
            }
            catch (Exception ex)
            {

                response.IsSuccess = false;
                response.ErrorMessage = ex.Message;
                return response;
            }

        }

        public async Task<TaxResponse> CreateXml(Invoice_xml props)
        {
            TaxResponse response = new TaxResponse();
            MainSettings.GetControlTax(props.CompCode, props.UnitID, props.System);


            if (GTaxControl.Comp_code == 0)
            {
                response.IsSuccess = false;
                response.ErrorMessage = "Not Available";
                return response;
            }

            response = GetInvoiceType(props.IS_Standard, props);

            if (!response.IsSuccess)
            {
                response.StatusCode = 400;
                return response;
            }

            Invoice_xml invoice = response.Response as Invoice_xml;
            response = CreateRoot_Xml(invoice, props.IS_Standard);

            if (!response.IsSuccess)
            {
                response.StatusCode = 400;
                return response;
            }


            response = GetfieldValue(GlobalVariables.SaveUrlFile + ".xml");

           

            return response;
        }

        public async Task<TaxResponse> SendInv(bool IS_Standard, string UUID, long InvoiceID, long InvoiceTrNo, int CompCode, int UnitID , string System)
        {
            TaxResponse response = new TaxResponse();
            MainSettings.GetControlTax(CompCode, UnitID, System);
            int currentYear = DateTime.Now.Year;
            string rootPath = GTaxControl.PathSaveXml;// AppDomain.CurrentDomain.BaseDirectory;

            response = await tax_KSA.SendInvTax(InvoiceID, CompCode, System, UUID, IS_Standard, rootPath + "\\" + currentYear + "\\Comp" + CompCode + "\\" + UUID + ".xml"); 

            if (response.IsSuccess)
            { 
                TaxShared taxShared = new TaxShared();
                taxShared._InsertLogDebug(InvoiceID, CompCode, System, UUID, "IsSuccess", "Send Invoice Xml to Tax", "");
            }
            else
            {
                TaxShared taxShared = new TaxShared();
                taxShared._InsertLogDebug(InvoiceID, CompCode, System, UUID, response.ErrorMessage, "Send Invoice Xml to Tax", "");
            }
            



            if (!response.IsSuccess)
                return response;

            GlobalVariables.InvoiceID = InvoiceID;
            GlobalVariables.InvoiceTrNo = InvoiceTrNo;


            response = GetfieldValue(rootPath + "\\" + currentYear + "\\Comp" + CompCode + "\\" + UUID + ".xml");

            return response;

        }

        public async Task<TaxResponse> CreateXml_And_SendInv(Invoice_xml props)
        {
            TaxResponse response = new TaxResponse();

            MainSettings.GetControlTax(props.CompCode, props.UnitID, props.System);

            if (GTaxControl.Comp_code == 0)
            {
                response.IsSuccess = false;
                response.ErrorMessage = "Not Available";
                return response;
            }


            response = GetInvoiceType(props.IS_Standard, props);

            if (!response.IsSuccess)
            {
                response.StatusCode = 400;
                return response;
            }

            Invoice_xml invoice = response.Response as Invoice_xml;
            response = CreateRoot_Xml(invoice, props.IS_Standard);

            if (!response.IsSuccess)
            {
                response.StatusCode = 400;
                return response;
            }

            if (GTaxControl.IsDirectSimplified == false && props.IS_Standard == false)
            {
               

                response = GetfieldValue(GlobalVariables.SaveUrlFile + ".xml");
                response.StatusCode = 200;
                return response;
            }

            response = await tax_KSA.SendInvTax(props.InvoiceID, props.CompCode, props.System, props.UUID, props.IS_Standard, GlobalVariables.SaveUrlFile + ".xml");

            if (response.IsSuccess)
            {
                 
                TaxShared taxShared = new TaxShared();
                taxShared._InsertLogDebug(props.InvoiceID, props.CompCode, props.System, props.UUID, "IsSuccess", "Send Invoice Xml to Tax", "");
            }
            else
            {
                TaxShared taxShared = new TaxShared();
                taxShared._InsertLogDebug(props.InvoiceID, props.CompCode, props.System, props.UUID, response.ErrorMessage, "Send Invoice Xml to Tax", "");
            }



            if (!response.IsSuccess)
                return response;

            response = GetfieldValue(GlobalVariables.SaveUrlFile + ".xml");


            response.StatusCode = 200; 
            return response;

        }


        public TaxResponse GetfieldValue(string xmlFilePath)
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

                //**********************************Get Value*********************************
                GlobalVariables.InvoiceUUIDNew = Utility.GetNodeInnerText(doc, SettingsParams.UUID_XPATH);
                GlobalVariables.InvoiceHashNew = Utility.GetNodeInnerText(doc, SettingsParams.SIGNED_Properities_DIGEST_VALUE_XPATH);
                GlobalVariables.InvoiceQr_CodeNew = Utility.GetNodeInnerText(doc, SettingsParams.QR_CODE_XPATH);
                string PrevInvoiceHash = Utility.GetNodeInnerText(doc, SettingsParams.PIH_XPATH);
                //string PrevInvoiceHash = Utility.GetNodeInnerText(doc, SettingsParams.Hash_XPATH);


                response.InvoiceXml = doc.OuterXml;
                response.InvoiceID = GlobalVariables.InvoiceID;
                response.InvoiceTrNo = GlobalVariables.InvoiceTrNo;
                response.InvoiceUUIDNew = GlobalVariables.InvoiceUUIDNew;
                response.InvoiceHashNew = GlobalVariables.InvoiceHashNew;
                response.InvoiceQr_CodeNew = GlobalVariables.InvoiceQr_CodeNew;
                response.PrevInvoiceHash = PrevInvoiceHash;
                response.TaxStatus = GlobalVariables.TaxStatus;

                response.IsSuccess = true;

                return response;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = ex.Message;
                return response;
            }



        }

 
    }


}
