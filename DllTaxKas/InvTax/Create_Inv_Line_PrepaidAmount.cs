using System;
using System.Collections.Generic;

namespace KSAEinvoice
{

    public class Create_Inv_Line_PrepaidAmount : TaxShared
    {
        private readonly string Temp_InvoiceLine = @"
                           <cac:InvoiceLine>
                              <cbc:ID>SET_ItemID</cbc:ID>
                              <cbc:InvoicedQuantity unitCode=""PCE"">0.00</cbc:InvoicedQuantity>
                              <cbc:LineExtensionAmount currencyID=""SET_currencyID"">0.00</cbc:LineExtensionAmount>
	                        <cac:DocumentReference> 
                                 <cbc:ID>SET_DocumentRefID</cbc:ID>
                                 <cbc:IssueDate>SET_DocumentRefIssueDate</cbc:IssueDate>
                                 <cbc:IssueTime>SET_DocumentRefIssueTime</cbc:IssueTime>
                                 <cbc:DocumentTypeCode>386</cbc:DocumentTypeCode>
                            </cac:DocumentReference>
                              <cac:TaxTotal>
                                 <cbc:TaxAmount currencyID=""SET_currencyID"">0.00</cbc:TaxAmount>
                                 <cbc:RoundingAmount currencyID=""SET_currencyID"">0.00</cbc:RoundingAmount>
                                    <cac:TaxSubtotal> 
                                        <cbc:TaxableAmount currencyID=""SET_currencyID"">SET_TaxableAmount</cbc:TaxableAmount>
                                        <cbc:TaxAmount currencyID=""SET_currencyID"">SET_TaxAmount</cbc:TaxAmount>
                                        <cac:TaxCategory>
                                        <cbc:ID>SET_IDCategory</cbc:ID>
                                        <cbc:Percent>SET_Percent</cbc:Percent>
                                            <cac:TaxScheme>
                                                <cbc:ID>VAT</cbc:ID>
                                            </cac:TaxScheme>
                                        </cac:TaxCategory>
                                    </cac:TaxSubtotal>
                              </cac:TaxTotal>
                              <cac:Item>
                                 <cbc:Name>Prepayment adjustment</cbc:Name>
                                 <cac:ClassifiedTaxCategory>
                                    <cbc:ID>SET_IDCategory</cbc:ID>
                                    <cbc:Percent>SET_Percent</cbc:Percent>
                                    <cac:TaxScheme>
                                       <cbc:ID>VAT</cbc:ID>
                                    </cac:TaxScheme>
                                 </cac:ClassifiedTaxCategory>
                              </cac:Item> 
                              <cac:Price>
                                 <cbc:PriceAmount currencyID=""SET_currencyID"">0.00</cbc:PriceAmount>  
                              </cac:Price>
                           </cac:InvoiceLine>";


        public string Create_Line_PrepaidAmount(Invoice_xml Inv)
        {

            List<InvoiceLinePrepaid_xml> InvoicePrepaid = Inv.InvoiceLinePrepaid;
            string populated_template = @"";
            //*************************************************************_TaxTotal********************************************
            for (int i = 0; i < InvoicePrepaid.Count; i++)
            {
                string singl_template = Temp_InvoiceLine;
                singl_template = singl_template.Replace("SET_currencyID", SetVal(Inv.TaxCurrencyCode));
                singl_template = singl_template.Replace("SET_ItemID", (i + 1).ToString()); //v


                singl_template = singl_template.Replace("SET_DocumentRefID", SetVal(InvoicePrepaid[i].DocumentRefID));
                singl_template = singl_template.Replace("SET_DocumentRefIssueDate", DateFormat(InvoicePrepaid[i].DocumentRefIssueDate.ToString()));
                singl_template = singl_template.Replace("SET_DocumentRefIssueTime", TimeFormat(InvoicePrepaid[i].DocumentRefIssueTime));
                  
                singl_template = singl_template.Replace("SET_TaxableAmount", SetVal(InvoicePrepaid[i].RefTaxableAmount.ToFixedNoRounding(2)));
                singl_template = singl_template.Replace("SET_TaxAmount", SetVal(InvoicePrepaid[i].RefTaxAmount.ToFixedNoRounding(2)));

                singl_template = singl_template.Replace("SET_IDCategory", SetVal(InvoicePrepaid[i].RefTaxCategoryID));
                singl_template = singl_template.Replace("SET_Percent", SetVal(InvoicePrepaid[i].RefTaxPercent.ToFixedNoRounding(2)));



                populated_template = populated_template + singl_template;
            }


            return populated_template;

        }






    }
}
