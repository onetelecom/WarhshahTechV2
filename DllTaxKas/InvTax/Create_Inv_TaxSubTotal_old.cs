using System;
using System.Collections.Generic;

namespace KSAEinvoice
{

    public class Create_Inv_TaxSubTotal_Old : TaxShared
    {
        private readonly string TaxTotal = @"
                        <cac:TaxTotal>
                            <cbc:TaxAmount currencyID=""SET_currencyID"">SET_TaxAmount</cbc:TaxAmount>
                        </cac:TaxTotal>
                        <cac:TaxTotal>
                        <cbc:TaxAmount currencyID=""SET_currencyID"">SET_TaxAmount</cbc:TaxAmount>

                              <cac:TaxSubtotal>
                                 <cbc:TaxableAmount currencyID=""SET_currencyID"">SET_TaxableAmount</cbc:TaxableAmount>
                                 <cbc:TaxAmount currencyID=""SET_currencyID"">SET_TaxSubtotal_TaxAmount</cbc:TaxAmount>
                                 <cac:TaxCategory>
                                    <cbc:ID>SET_ID_TaxCategory</cbc:ID>
                                    <cbc:Percent>SET_Percent</cbc:Percent>
                                    <cac:TaxScheme>
                                       <cbc:ID>VAT</cbc:ID>
                                    </cac:TaxScheme>
                                 </cac:TaxCategory>
                              </cac:TaxSubtotal>

                        </cac:TaxTotal> 
                         <cac:LegalMonetaryTotal>
                              <cbc:LineExtensionAmount currencyID=""SET_currencyID"">SET_LineExtensionAmount</cbc:LineExtensionAmount>
                              <cbc:TaxExclusiveAmount currencyID=""SET_currencyID"">SET_TaxExclusiveAmount</cbc:TaxExclusiveAmount>
                              <cbc:TaxInclusiveAmount currencyID=""SET_currencyID"">SET_TaxInclusiveAmount</cbc:TaxInclusiveAmount>
                              <cbc:AllowanceTotalAmount currencyID=""SET_currencyID"">0.00</cbc:AllowanceTotalAmount>
                              <cbc:PrepaidAmount currencyID=""SET_currencyID"">0.00</cbc:PrepaidAmount>
                              <cbc:PayableAmount currencyID=""SET_currencyID"">SET_PayableAmount</cbc:PayableAmount>
                         </cac:LegalMonetaryTotal>";


        public string Creare_TaxTotal(Invoice_xml Inv)
        {

            List<InvoiceLine_xml> _InvoiceLine = Inv.InvoiceLine;
            TaxTotal _TaxTotal = Inv.TaxTotal_Header;
            string populated_template = @"";
            //*************************************************************Sum TaxTotal******************************************** 
            for (int i = 0; i < _InvoiceLine.Count; i++)
            {
                _InvoiceLine[i].TaxTotal.TaxAmount = ((_InvoiceLine[i].LineExtensionAmount * _InvoiceLine[i].TaxTotal.TaxSubtotal.TaxCategory.Percent) / 100);
                _TaxTotal.TaxAmount += _InvoiceLine[i].TaxTotal.TaxAmount;
                //
                // find tax nat and Prc in rax list 
                // If find  add amunt and vat 
                // if not  append vat nat and prc 
                //     add amount and vat 

                _TaxTotal.TaxSubtotal.TaxableAmount += _InvoiceLine[i].LineExtensionAmount; 
                _TaxTotal.TaxSubtotal.TaxCategory.ID = _InvoiceLine[i].TaxTotal.TaxSubtotal.TaxCategory.ID;
                _TaxTotal.TaxSubtotal.TaxCategory.Percent = _InvoiceLine[i].TaxTotal.TaxSubtotal.TaxCategory.Percent;
                _TaxTotal.TaxSubtotal.TaxAmount += _InvoiceLine[i].TaxTotal.TaxAmount;
            }
            
            //*************************************************************_TaxTotal******************************************** 
            string singl_template = TaxTotal;

            singl_template = singl_template.Replace("SET_currencyID", SetVal(Inv.TaxCurrencyCode));
            singl_template = singl_template.Replace("SET_TaxAmount", SetVal(_TaxTotal.TaxAmount.ToFixedNoRounding(2)));




            singl_template = singl_template.Replace("SET_TaxableAmount", SetVal(_TaxTotal.TaxSubtotal.TaxableAmount.ToFixedNoRounding(2)));
            singl_template = singl_template.Replace("SET_TaxSubtotal_TaxAmount", SetVal(_TaxTotal.TaxSubtotal.TaxAmount.ToFixedNoRounding(2))); 

            singl_template = singl_template.Replace("SET_ID_TaxCategory", SetVal(_TaxTotal.TaxSubtotal.TaxCategory.ID));
            singl_template = singl_template.Replace("SET_Percent", SetVal(_TaxTotal.TaxSubtotal.TaxCategory.Percent.ToFixedNoRounding(2)));
 






            singl_template = singl_template.Replace("SET_LineExtensionAmount", SetVal(_TaxTotal.TaxSubtotal.TaxableAmount.ToFixedNoRounding(2)));
            singl_template = singl_template.Replace("SET_TaxExclusiveAmount", SetVal(_TaxTotal.TaxSubtotal.TaxableAmount.ToFixedNoRounding(2)));
            singl_template = singl_template.Replace("SET_TaxInclusiveAmount", SetVal((_TaxTotal.TaxAmount + _TaxTotal.TaxSubtotal.TaxableAmount).ToFixedNoRounding(2)));
            singl_template = singl_template.Replace("SET_PayableAmount", SetVal((_TaxTotal.TaxAmount + _TaxTotal.TaxSubtotal.TaxableAmount).ToFixedNoRounding(2)));


            populated_template = populated_template + singl_template;


            return populated_template;

        }






    }
}
