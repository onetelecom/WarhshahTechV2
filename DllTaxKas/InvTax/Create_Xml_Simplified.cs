using SignXMLDocument;

namespace KSAEinvoice
{

    public class Create_Xml_Simplified : TaxShared
    {


        private string template = @"<?xml version=""1.0"" encoding=""UTF-8""?*>
                                     <Invoice xmlns=""urn:oasis:names:specification:ubl:schema:xsd:Invoice-2""
                                     xmlns:cac=""urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2""
                                     xmlns:cbc=""urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2""
                                     xmlns:ext=""urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2""> 

                    <cbc:ProfileID>SET_ProfileID</cbc:ProfileID>
                    <cbc:ID>SET_INVOICE_ID</cbc:ID>
                    <cbc:UUID>SET_TERMINAL_UUID</cbc:UUID>
                    <cbc:IssueDate>SET_ISSUE_DATE</cbc:IssueDate>
                    <cbc:IssueTime>SET_ISSUE_TIME</cbc:IssueTime>                    
                    <cbc:InvoiceTypeCode name=""SET_InvoiceType"">SET_In_voiceTypeCode</cbc:InvoiceTypeCode>
                    <cbc:DocumentCurrencyCode>SET_DocumentCurrencyCode</cbc:DocumentCurrencyCode>
                    <cbc:TaxCurrencyCode>SET_TaxCurrencyCode</cbc:TaxCurrencyCode> 
                    SET_BillingReference
                    <cac:AdditionalDocumentReference>
                        <cbc:ID>SET_ID_AdditionalDocumentReference</cbc:ID>
                        <cbc:UUID>SET_UUID_AdditionalDocumentReference</cbc:UUID>
                    </cac:AdditionalDocumentReference>
                    <cac:AdditionalDocumentReference>
                       <cbc:ID>PIH</cbc:ID>
                          <cac:Attachment>
                          <cbc:EmbeddedDocumentBinaryObject mimeCode=""text/plain"">SET_HASH_Last</cbc:EmbeddedDocumentBinaryObject>
                       </cac:Attachment>
                    </cac:AdditionalDocumentReference>
                       
<cac:Signature>
      <cbc:ID>urn:oasis:names:specification:ubl:signature:Invoice</cbc:ID>
      <cbc:SignatureMethod>urn:oasis:names:specification:ubl:dsig:enveloped:xades</cbc:SignatureMethod>
</cac:Signature>
                    <cac:AccountingSupplierParty>
                        <cac:Party>
                            <cac:PartyIdentification>
                                <cbc:ID schemeID=""SET_schemeIDSupplier_Name"">SET_COMMERCIAL_REGISTRATION_NUMBER</cbc:ID>
                            </cac:PartyIdentification>
                            <cac:PostalAddress>
                                <cbc:StreetName>SET_STREET_NAME</cbc:StreetName>
                                <cbc:BuildingNumber>SET_BUILDING_NUMBER</cbc:BuildingNumber>
                                <cbc:PlotIdentification>SET_PLOT_IDENTIFICATION</cbc:PlotIdentification>
                                <cbc:CitySubdivisionName>SET_CITY_SUBDIVISION</cbc:CitySubdivisionName>
                                <cbc:CityName>SET_CITY</cbc:CityName>
                                <cbc:PostalZone>SET_POSTAL_NUMBER</cbc:PostalZone>
                                <cac:Country>
                                    <cbc:IdentificationCode>SA</cbc:IdentificationCode>
                                </cac:Country>
                            </cac:PostalAddress>
                            <cac:PartyTaxScheme> 
                            <cbc:CompanyID>SET_CompanyID</cbc:CompanyID>
                                <cac:TaxScheme>
                                    <cbc:ID>VAT</cbc:ID>
                                </cac:TaxScheme>
                            </cac:PartyTaxScheme>
                            <cac:PartyLegalEntity>
                                <cbc:RegistrationName>SET_RegistrationName</cbc:RegistrationName>
                            </cac:PartyLegalEntity>
                        </cac:Party>
                    </cac:AccountingSupplierParty>
                    <cac:AccountingCustomerParty>
                      <cac:Party> 
                             <cac:PostalAddress> 
                                <cac:Country>
                                   <cbc:IdentificationCode>Cust_IdentificationCode</cbc:IdentificationCode>
                                </cac:Country>
                             </cac:PostalAddress>
                                SET_PartyTaxScheme 
                             <cac:PartyLegalEntity>
                                <cbc:RegistrationName>Cust_RegistrationName</cbc:RegistrationName>
                             </cac:PartyLegalEntity>
                          </cac:Party>
                    </cac:AccountingCustomerParty>
                    SET_ActualDeliveryDate 
                    <cac:PaymentMeans>
                       <cbc:PaymentMeansCode>SET_PaymentMeansCode</cbc:PaymentMeansCode>
                            SET_InstructionNote                        
                    </cac:PaymentMeans>
                      SET_AllowanceCharge
                      SET_TaxTotal 
                      SET_InvoicePrepaidAmount
                      SET_InvoiceLine
                </Invoice>";
         
        public string _Create_Xml_Simplified(Invoice_xml props)
        {
            string populated_template = template;

            // if canceled (BR-KSA-56) set reference number to canceled invoice





            populated_template = populated_template.Replace("SET_PartyTaxScheme", @"<cac:PartyTaxScheme><cac:TaxScheme><cbc:ID>VAT</cbc:ID></cac:TaxScheme></cac:PartyTaxScheme>");




            if (props.TypeCode == "388" || props.TypeCode == "386") // INV
            {
                populated_template = populated_template.Replace("SET_LineCountNumeric", " <cbc:LineCountNumeric>" + SetVal(props.LineCountNumeric) + "</cbc:LineCountNumeric>");
                populated_template = populated_template.Replace("SET_BillingReference", "");
                populated_template = populated_template.Replace("SET_InstructionNote", "");
                 

            }
            else   // Debit or Credit
            {
                populated_template = populated_template.Replace("SET_LineCountNumeric", "");
                Create_Inv_Credit _Create_Inv = new Create_Inv_Credit();
                string BillingReference = SetVal(_Create_Inv.Create_Credit(props));
                populated_template = populated_template.Replace("SET_BillingReference", BillingReference);
                populated_template = populated_template.Replace("SET_InstructionNote", "<cbc:InstructionNote>" + SetVal(props.InstructionNote) + @"</cbc:InstructionNote>");
                

            }






            //************************************************Xml_Header_Inv**************************************
            populated_template = populated_template.Replace("SET_ProfileID", SetVal(props.ProfileID));
            populated_template = populated_template.Replace("SET_INVOICE_ID", SetVal(props.ID));
            populated_template = populated_template.Replace("SET_TERMINAL_UUID", SetVal(props.UUID));
            populated_template = populated_template.Replace("SET_ISSUE_DATE", DateFormat(props.IssueDate.ToString()));
            populated_template = populated_template.Replace("SET_ISSUE_TIME", TimeFormat(props.IssueTime));

            populated_template = populated_template.Replace("SET_InvoiceType", SetVal(props.InvoiceType));
            populated_template = populated_template.Replace("SET_In_voiceTypeCode", SetVal(props.TypeCode));

            populated_template = populated_template.Replace("SET_DocumentCurrencyCode", SetVal(props.DocumentCurrencyCode));
            populated_template = populated_template.Replace("SET_TaxCurrencyCode", SetVal(props.TaxCurrencyCode));
            populated_template = populated_template.Replace("SET_ID_AdditionalDocumentReference", SetVal(props.AdditionalDocumentReference.ID)); //ICV
            populated_template = populated_template.Replace("SET_UUID_AdditionalDocumentReference", SetVal(props.AdditionalDocumentReference.UUID)); // 46531

            //****************************************************Signature************************************************* 
            props.Signature.ID = "urn:oasis:names:specification:ubl:signature:Invoice";
            props.Signature.SignatureMethod = "urn:oasis:names:specification:ubl:dsig:enveloped:xades";

            populated_template = populated_template.Replace("SET_ID_Signature", SetVal(props.Signature.ID)); // urn:oasis:names:specification:ubl:signature:Invoice
            populated_template = populated_template.Replace("SET_SignatureMethod_Signature", SetVal(props.Signature.SignatureMethod)); // urn:oasis:names:specification:ubl:dsig:enveloped:xades

            //****************************************************SupplierParty*************************************************
            populated_template = populated_template.Replace("SET_schemeIDSupplier_Name", SetVal(props.AccountingSupplierParty.Party.PartyIdentification.Scheme_Name));
            populated_template = populated_template.Replace("SET_COMMERCIAL_REGISTRATION_NUMBER", SetVal(props.AccountingSupplierParty.Party.PartyIdentification.ID));
            populated_template = populated_template.Replace("SET_STREET_NAME", SetVal(props.AccountingSupplierParty.Party.PostalAddress.StreetName));
            populated_template = populated_template.Replace("SET_BUILDING_NUMBER", SetVal(props.AccountingSupplierParty.Party.PostalAddress.BuildingNumber));
            populated_template = populated_template.Replace("SET_PLOT_IDENTIFICATION", SetVal(props.AccountingSupplierParty.Party.PostalAddress.PlotIdentification));
            populated_template = populated_template.Replace("SET_CITY_SUBDIVISION", SetVal(props.AccountingSupplierParty.Party.PostalAddress.CitySubdivisionName));
            populated_template = populated_template.Replace("SET_CITY", SetVal(props.AccountingSupplierParty.Party.PostalAddress.CityName));
            populated_template = populated_template.Replace("SET_POSTAL_NUMBER", SetVal(props.AccountingSupplierParty.Party.PostalAddress.PostalZone));
            populated_template = populated_template.Replace("SET_CompanyID", SetVal(props.AccountingSupplierParty.Party.PartyTaxScheme.CompanyID));
            populated_template = populated_template.Replace("SET_RegistrationName", SetVal(props.AccountingSupplierParty.Party.PartyLegalEntity.RegistrationName));

            //****************************************************CustomerParty*************************************************

            populated_template = populated_template.Replace("SET_schemeIDCustomer_Name", SetVal(props.AccountingCustomerParty.Party.PartyIdentification.Scheme_Name));
            populated_template = populated_template.Replace("Cust_schemeID", SetVal(props.AccountingCustomerParty.Party.PartyIdentification.ID));
            populated_template = populated_template.Replace("Cust_StreetName", SetVal(props.AccountingCustomerParty.Party.PostalAddress.StreetName));
            populated_template = populated_template.Replace("Cust_BuildingNumber", SetVal(props.AccountingCustomerParty.Party.PostalAddress.BuildingNumber));
            populated_template = populated_template.Replace("Cust_PlotIdentification", SetVal(props.AccountingCustomerParty.Party.PostalAddress.PlotIdentification));
            populated_template = populated_template.Replace("Cust_CitySubdivisionName", SetVal(props.AccountingCustomerParty.Party.PostalAddress.CitySubdivisionName));
            populated_template = populated_template.Replace("Cust_CityName", SetVal(props.AccountingCustomerParty.Party.PostalAddress.CityName));
            populated_template = populated_template.Replace("Cust_PostalZone", SetVal(props.AccountingCustomerParty.Party.PostalAddress.PostalZone));
            populated_template = populated_template.Replace("Cust_CountrySubentity", SetVal(props.AccountingCustomerParty.Party.PostalAddress.CountrySubentity));
            populated_template = populated_template.Replace("Cust_IdentificationCode", SetVal(props.AccountingCustomerParty.Party.PostalAddress.Country.IdentificationCode));
            populated_template = populated_template.Replace("Cust_RegistrationName", SetVal(props.AccountingCustomerParty.Party.PartyLegalEntity.RegistrationName));

            //*************************************************************Delivery********************************************
            string Del = @"<cac:Delivery>
                        <cbc:ActualDeliveryDate>" + DateFormat(props.Delivery.ActualDeliveryDate.ToString()) + @"</cbc:ActualDeliveryDate>
                    </cac:Delivery>";
            populated_template = populated_template.Replace("SET_ActualDeliveryDate", Del);

            //*************************************************************PaymentMeans********************************************
            populated_template = populated_template.Replace("SET_PaymentMeansCode", SetVal(props.PaymentMeans.PaymentMeansCode));
            
            //*************************************************************AllowanceCharge********************************************
            Create_Inv_AllowanceCharge _AllowanceCharge = new Create_Inv_AllowanceCharge();
            Allowance_Inv _Allowance_Inv = _AllowanceCharge.Creare_Allowance(props);
            props.Allowance_ChargesInv = _Allowance_Inv.Allowance_ChargesInv;
            string Allowance = SetVal(_Allowance_Inv.All_Template_Allowance);
            populated_template = populated_template.Replace("SET_AllowanceCharge", Allowance);

            //*************************************************************TaxTotal********************************************
            Create_Inv_TaxSubTotal _TaxSubTotal = new Create_Inv_TaxSubTotal();
            string TaxTotal = SetVal(_TaxSubTotal.Creare_TaxTotal(props));
            populated_template = populated_template.Replace("SET_TaxTotal", TaxTotal);

            //*************************************************************InvoiceLine******************************************** 
            Create_Inv_Line _Inv_Line = new Create_Inv_Line();
            string InvoiceLine = SetVal(_Inv_Line.Creare_InvoiceLine(props));
            populated_template = populated_template.Replace("SET_InvoiceLine", InvoiceLine);

            //***************************************************INVOICE_HASH**********************************************************
            populated_template = populated_template.Replace("SET_HASH_Last", GTaxControl.LastHash);

            //*************************************************************InvoiceLinePrepaidAmount******************************************** 
            Create_Inv_Line_PrepaidAmount _Inv_LinePrepaidAmount = new Create_Inv_Line_PrepaidAmount();
            string InvoicePrepaidAmount = SetVal(_Inv_LinePrepaidAmount.Create_Line_PrepaidAmount(props));
            populated_template = populated_template.Replace("SET_InvoicePrepaidAmount", InvoicePrepaidAmount);



            template = populated_template;

            try
            {
                string xml = FormatXml(template);

                TaxShared taxShared = new TaxShared();
                if (props.ID.Trim() == "")
                {
                    taxShared._InsertLogDebug(props.InvoiceID, props.CompCode, props.System, props.UUID, "IsSuccess", "Create Xml Standar in Vaild Authrize", "");
                }
                else
                {
                    taxShared._InsertLogDebug(props.InvoiceID, props.CompCode, props.System, props.UUID, "IsSuccess", "Create Xml Standar", "");
                }


                return xml;

            }
            catch (System.Exception ex)
            {
                TaxShared taxShared = new TaxShared();

                if (props.ID.Trim() == "")
                {
                    taxShared._InsertLogDebug(props.InvoiceID, props.CompCode, props.System, props.UUID, ex.Message, "Create Xml Standar in Vaild Authrize", "");
                }
                else
                {
                    taxShared._InsertLogDebug(props.InvoiceID, props.CompCode, props.System, props.UUID, ex.Message, "Create Xml Standar", "");
                }


                return "";
            }

        }

         

    }
}
