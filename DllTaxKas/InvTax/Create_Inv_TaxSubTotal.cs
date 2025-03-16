using System;
using System.Collections.Generic;
using System.Linq;
using ZatcaIntegrationSDK;

namespace KSAEinvoice
{

    public class Create_Inv_TaxSubTotal : TaxShared
    {



        private readonly string TaxTotal = @"
                        <cac:TaxTotal>
                            <cbc:TaxAmount currencyID=""SET_currencyID"">SET_TotalTaxAmount</cbc:TaxAmount>
                        </cac:TaxTotal>
                        <cac:TaxTotal>
                        <cbc:TaxAmount currencyID=""SET_currencyID"">SET_TotalTaxSubtotalAmount</cbc:TaxAmount>

                              
                                            SET_TaxSubtotal


                        </cac:TaxTotal> 
                         <cac:LegalMonetaryTotal>
                              <cbc:LineExtensionAmount currencyID=""SET_currencyID"">SET_LineExtensionAmount</cbc:LineExtensionAmount>
                              <cbc:TaxExclusiveAmount currencyID=""SET_currencyID"">SET_TaxExclusiveAmount</cbc:TaxExclusiveAmount>
                              <cbc:TaxInclusiveAmount currencyID=""SET_currencyID"">SET_TaxInclusiveAmount</cbc:TaxInclusiveAmount>
                              <cbc:AllowanceTotalAmount currencyID=""SET_currencyID"">SET_AllowanceTotalAmount</cbc:AllowanceTotalAmount>
                              <cbc:ChargeTotalAmount currencyID=""SET_currencyID"">SET_ChargeTotalAmount</cbc:ChargeTotalAmount>
                              <cbc:PrepaidAmount currencyID=""SET_currencyID"">SET_PrepaidAmount</cbc:PrepaidAmount>
                              <cbc:PayableRoundingAmount currencyID=""SET_currencyID"">SET_PayableRoundingAmount</cbc:PayableRoundingAmount>
                              <cbc:PayableAmount currencyID=""SET_currencyID"">SET_PayableAmount</cbc:PayableAmount> 
                         </cac:LegalMonetaryTotal>";


        private readonly string TaxSubtotal = @"
                             <cac:TaxSubtotal>
                                 <cbc:TaxableAmount currencyID=""SET_currencyID"">SET_TaxableAmount</cbc:TaxableAmount>
                                 <cbc:TaxAmount currencyID=""SET_currencyID"">SET_TaxSubtotal_TaxAmount</cbc:TaxAmount>
                                     <cac:TaxCategory>
                                        <cbc:ID>SET_ID_TaxCategory</cbc:ID>
                                        <cbc:Percent>SET_Percent</cbc:Percent>
                                      SET_TaxExemptionReasonCode 
                                       SET_TaxExemptionReason
                                        <cac:TaxScheme>
                                           <cbc:ID>VAT</cbc:ID>
                                        </cac:TaxScheme>
                                     </cac:TaxCategory>
                              </cac:TaxSubtotal>";

        public string Creare_TaxTotal(Invoice_xml Inv)
        {

            List<InvoiceLine_xml> _InvoiceLine = Inv.InvoiceLine;
            TaxTotal _TaxTotal = Inv.TaxTotal_Header;
            string populated_template = @"";
            //*************************************************************Sum TaxTotal******************************************** 
            decimal TaxAmount = 0;
            decimal TotalVatAmount = 0;
            List<TaxSubtotal_Item> List_TaxSubtotal = new List<TaxSubtotal_Item>();

            for (int i = 0; i < _InvoiceLine.Count; i++) // find tax
            {
                TaxAmount = ((_InvoiceLine[i].LineExtensionAmount * _InvoiceLine[i].TaxTotal.TaxSubtotal.TaxCategory.Percent) / 100);
                TotalVatAmount += TaxAmount;

                // find tax nat and Prc in rax list 
                // If find  add amunt and vat 
                // if not  append vat nat and prc 
                //     add amount and vat 

                TaxSubtotal_Item Subtotal_Item = new TaxSubtotal_Item();

                Subtotal_Item.ID = _InvoiceLine[i].TaxTotal.TaxSubtotal.TaxCategory.ID;
                Subtotal_Item.Percent = _InvoiceLine[i].TaxTotal.TaxSubtotal.TaxCategory.Percent;
                Subtotal_Item.TaxAmount = TaxAmount;
                Subtotal_Item.TaxableAmount = _InvoiceLine[i].LineExtensionAmount;

                List_TaxSubtotal.Add(Subtotal_Item);
            }


            // Group by ID and Percent, and calculate sums
            var List_TaxSubtotalSum = List_TaxSubtotal
                .GroupBy(item => new { item.ID, item.Percent })
                .Select(group => new
                {
                    ID = group.Key.ID,
                    Percent = group.Key.Percent,
                    SumTaxAmount = group.Sum(item => item.TaxAmount),
                    SumTaxableAmount = group.Sum(item => item.TaxableAmount)
                }).ToList();



            var GroupAllowance_Charges = Inv.Allowance_ChargesInv
                      .GroupBy(item => new { item.ID, item.Percent, item.ChargeIndicator })
                    .Select(group => new
                    {
                        ID = group.Key.ID,
                        Percent = group.Key.Percent,
                        ChargeIndicator = group.Key.ChargeIndicator,
                        SumAmount = group.Sum(item => item.Amount),
                        SumVat_Amount = group.Sum(item => item.Vat_Amount)
                    }).ToList();

            //*************************************************************_TaxTotal******************************************** 
            string singl_template = TaxTotal;

            //*************************************************************Total VatAmount Items Invoice******************************************** 

            singl_template = singl_template.Replace("SET_currencyID", SetVal(Inv.TaxCurrencyCode));

            //*************************************************************Taxable********************************************  

            string _AllTaxSubtotal = "";
            decimal _TotalTaxAmount = 0;
            decimal _TotalTaxSubtotalAmount = 0;
            foreach (var group in List_TaxSubtotalSum)
            {

                decimal TaxableAmount = group.SumTaxableAmount;
                decimal _TaxAmount = 0;
                var _Allowance = GroupAllowance_Charges.Where(x => x.ID == group.ID && x.Percent == group.Percent).ToList();
                foreach (var _Allow in _Allowance)
                {
                    if (_Allow != null)
                    {
                        if (_Allow.ChargeIndicator == false)
                        {
                            TaxableAmount -= (_Allow.SumAmount);
                            //_TaxAmount += (TaxableAmount * group.Percent) / 100;
                            //_TaxAmount += group.SumTaxAmount - _Allow.Vat_Amount;
                        }
                        else
                        {
                            TaxableAmount += (_Allow.SumAmount);
                            //_TaxAmount += (TaxableAmount * group.Percent) / 100;
                            //_TaxAmount += group.SumTaxAmount + _Allow.Vat_Amount;
                        }
                    }

                }

                _TotalTaxAmount = _TotalTaxAmount + group.SumTaxAmount;


                _TaxAmount = (TaxableAmount * group.Percent) / 100;
                //_TaxAmount = Inv.Hd_NetTax;
                //_TaxAmount = Inv.hd_netTaxCaluated;

                string _TaxSubtotal = TaxSubtotal;
                _TaxSubtotal = _TaxSubtotal.Replace("SET_TaxableAmount", SetVal(TaxableAmount.ToFixedNoRounding(2)));
                _TaxSubtotal = _TaxSubtotal.Replace("SET_TaxSubtotal_TaxAmount", SetVal(_TaxAmount.ToFixedNoRounding(2)));
                _TaxSubtotal = _TaxSubtotal.Replace("SET_ID_TaxCategory", SetVal(group.ID));
                _TaxSubtotal = _TaxSubtotal.Replace("SET_Percent", SetVal(group.Percent.ToFixedNoRounding(2)));
                _TaxSubtotal = _TaxSubtotal.Replace("SET_currencyID", SetVal(Inv.TaxCurrencyCode));

                string _TaxExemptionReasonCode = "";
                string _TaxExemptionReason = "";
                if (group.ID == "E")
                {
                    _TaxExemptionReasonCode = @"  <cbc:TaxExemptionReasonCode>VATEX-SA-30</cbc:TaxExemptionReasonCode>";
                    _TaxExemptionReason = @" <cbc:TaxExemptionReason>Realestate</cbc:TaxExemptionReason>";

                    _TaxSubtotal = _TaxSubtotal.Replace("SET_TaxExemptionReasonCode", _TaxExemptionReasonCode);
                    _TaxSubtotal = _TaxSubtotal.Replace("SET_TaxExemptionReason", _TaxExemptionReason);
                }
                if (group.ID == "Z")
                {
                    _TaxExemptionReasonCode = @"  <cbc:TaxExemptionReasonCode>VATEX-SA-32</cbc:TaxExemptionReasonCode>";
                    _TaxExemptionReason = @" <cbc:TaxExemptionReason>Export of goods</cbc:TaxExemptionReason>";

                    _TaxSubtotal = _TaxSubtotal.Replace("SET_TaxExemptionReasonCode", _TaxExemptionReasonCode);
                    _TaxSubtotal = _TaxSubtotal.Replace("SET_TaxExemptionReason", _TaxExemptionReason);
                }
                else
                {
                    _TaxSubtotal = _TaxSubtotal.Replace("SET_TaxExemptionReasonCode", _TaxExemptionReasonCode);
                    _TaxSubtotal = _TaxSubtotal.Replace("SET_TaxExemptionReason", _TaxExemptionReason);
                }



                _TotalTaxSubtotalAmount = _TotalTaxSubtotalAmount + _TaxAmount;



                _AllTaxSubtotal = _AllTaxSubtotal + "\n" + _TaxSubtotal;
            }

            singl_template = singl_template.Replace("SET_TotalTaxAmount", SetVal(_TotalTaxAmount.ToFixedNoRounding(2)));
            singl_template = singl_template.Replace("SET_TotalTaxSubtotalAmount", SetVal(_TotalTaxSubtotalAmount.ToFixedNoRounding(2)));


            singl_template = singl_template.Replace("SET_TaxSubtotal", _AllTaxSubtotal);



            //*************
            decimal TaxInclusiveAmount = Convert.ToDecimal(_TotalTaxSubtotalAmount.ToFixedNoRounding(2)) + Inv.Hd_TaxableAmount;
            decimal PayableAmount = TaxInclusiveAmount - Inv.Hd_PaidAmount;

            //****************************************************************Total Invoice*****************************
            singl_template = singl_template.Replace("SET_LineExtensionAmount", SetVal(Inv.hd_NetAmount.ToFixedNoRounding(2)));
            singl_template = singl_template.Replace("SET_TaxExclusiveAmount", SetVal(Inv.Hd_TaxableAmount.ToFixedNoRounding(2)));
            //singl_template = singl_template.Replace("SET_TaxInclusiveAmount", SetVal(Inv.Hd_NetWithTax.ToFixedNoRounding(2)));
            singl_template = singl_template.Replace("SET_TaxInclusiveAmount", SetVal(TaxInclusiveAmount.ToFixedNoRounding(2)));
            singl_template = singl_template.Replace("SET_AllowanceTotalAmount", SetVal(Inv.Hd_NetDeduction.ToFixedNoRounding(2)));
            singl_template = singl_template.Replace("SET_ChargeTotalAmount", SetVal(Inv.Hd_NetAdditions.ToFixedNoRounding(2)));
            singl_template = singl_template.Replace("SET_PrepaidAmount", SetVal(Inv.Hd_PaidAmount.ToFixedNoRounding(2)));
            singl_template = singl_template.Replace("SET_PayableRoundingAmount", SetVal(Inv.HD_Round.ToFixedNoRounding(2)));
            //singl_template = singl_template.Replace("SET_PayableRoundingAmount", SetVal((0.00).ToFixedNoRounding(2)));
            //singl_template = singl_template.Replace("SET_PayableAmount", SetVal((Inv.Hd_DueAmount).ToFixedNoRounding(2))); 
            singl_template = singl_template.Replace("SET_PayableAmount", SetVal((PayableAmount).ToFixedNoRounding(2)));



            populated_template = populated_template + singl_template;


            return populated_template;

        }






    }
}
