using java.util;
using net.sf.saxon.expr.instruct;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KSAEinvoice
{

    public class Create_Inv_AllowanceCharge : TaxShared
    {



        string _template = @"<cac:AllowanceCharge>
                    <cbc:ChargeIndicator>SET_ChargeIndicator</cbc:ChargeIndicator>
                    <cbc:AllowanceChargeReasonCode>SET_CodeReason</cbc:AllowanceChargeReasonCode>
                    <cbc:AllowanceChargeReason>SET_Reason</cbc:AllowanceChargeReason>
                    <cbc:Amount currencyID=""SET_currencyID"">SET_Amount</cbc:Amount>
                    <cac:TaxCategory>
                        <cbc:ID>SET_ID_Category</cbc:ID>  
                        <cbc:Percent>SET_Percent</cbc:Percent>
                        <cac:TaxScheme>
                            <cbc:ID>VAT</cbc:ID>
                        </cac:TaxScheme>
                    </cac:TaxCategory>
                </cac:AllowanceCharge>";


        string _Template_Allowance = "";
        string All_Template_Allowance = "";

        List<Allowance_ChargesInv> _Allowance_ChargesInv = new List<Allowance_ChargesInv>();
        Allowance_ChargesInv _Allo_ChargesInv = new Allowance_ChargesInv();
        Allowance_Inv _Allowance_Inv = new Allowance_Inv();

        public Allowance_Inv Creare_Allowance(Invoice_xml Inv)
        {



            //*************************Adv Invoice***********************
            //foreach (var item in Inv.Inv_AdvDed)
            //{
            //    if (item.AdvDedVat > 0 && item.AdvDedAmount > 0)
            //    {
            //        _Allo_ChargesInv = new Allowance_ChargesInv();
            //        _Template_Allowance = _template;
            //        _Template_Allowance = _Template_Allowance.Replace("SET_ChargeIndicator", "false");
            //        _Template_Allowance = _Template_Allowance.Replace("SET_CodeReason", SetVal(item.AdvDedReasonCode));
            //        _Template_Allowance = _Template_Allowance.Replace("SET_Reason", SetVal(item.AdvDedReason));
            //        //_Template_Allowance = _Template_Allowance.Replace("SET_MultiplierFactorNumeric", "<cbc:MultiplierFactorNumeric>" + SetVal(item.AdvDedVatPrc) + "</cbc:MultiplierFactorNumeric>");
            //        _Template_Allowance = _Template_Allowance.Replace("SET_MultiplierFactorNumeric", "");
            //        _Template_Allowance = _Template_Allowance.Replace("SET_Amount", SetVal(item.AdvDedAmount));
            //        //_Template_Allowance = _Template_Allowance.Replace("SET_BaseAmount", @"<cbc:BaseAmount currencyID=""SET_currencyID"">" + SetVal(item.AdvDedAmount) + "</cbc:BaseAmount>");
            //        _Template_Allowance = _Template_Allowance.Replace("SET_BaseAmount", @"");
            //        _Template_Allowance = _Template_Allowance.Replace("SET_ID_Category", SetVal(item.AdvDedVatNat));
            //        _Template_Allowance = _Template_Allowance.Replace("SET_Percent", SetVal(item.AdvDedVatPrc));
            //        _Template_Allowance = _Template_Allowance.Replace("SET_currencyID", SetVal(Inv.TaxCurrencyCode));

            //        //find vat 
            //        // if fiound  - amount , - vat 

            //        _Allo_ChargesInv.ID = item.AdvDedVatNat;
            //        _Allo_ChargesInv.Percent = item.AdvDedVatPrc;
            //        _Allo_ChargesInv.Amount = item.AdvDedAmount;
            //        _Allo_ChargesInv.Vat_Amount = item.AdvDedVat;
            //        _Allo_ChargesInv.ChargeIndicator = false;
            //        _Allowance_ChargesInv.Add(_Allo_ChargesInv);

            //        All_Template_Allowance = All_Template_Allowance + _Template_Allowance;
            //    }
            //}


            //*************************Discount Invoice***********************
            foreach (var item in Inv.Inv_HDDisc)
            {
                if (item.HDDiscAmount > 0)
                {
                    _Allo_ChargesInv = new Allowance_ChargesInv();
                    _Template_Allowance = _template;
                    _Template_Allowance = _Template_Allowance.Replace("SET_ChargeIndicator", "false");
                    _Template_Allowance = _Template_Allowance.Replace("SET_CodeReason", SetVal(item.HDDiscReasonCode));
                    _Template_Allowance = _Template_Allowance.Replace("SET_Reason", SetVal(item.HDDiscReason));
                    _Template_Allowance = _Template_Allowance.Replace("SET_MultiplierFactorNumeric", "");
                    _Template_Allowance = _Template_Allowance.Replace("SET_Amount", SetVal(item.HDDiscAmount));
                    _Template_Allowance = _Template_Allowance.Replace("SET_BaseAmount", @"");
                    _Template_Allowance = _Template_Allowance.Replace("SET_ID_Category", SetVal(item.HDDiscVatNat));
                    _Template_Allowance = _Template_Allowance.Replace("SET_Percent", SetVal(item.HDDiscVatPrc));
                    _Template_Allowance = _Template_Allowance.Replace("SET_currencyID", SetVal(Inv.TaxCurrencyCode));

                    _Allo_ChargesInv.ID = item.HDDiscVatNat;
                    _Allo_ChargesInv.Percent = item.HDDiscVatPrc;
                    _Allo_ChargesInv.Amount = item.HDDiscAmount;
                    _Allo_ChargesInv.Vat_Amount = item.HDDiscVat;
                    _Allo_ChargesInv.ChargeIndicator = false;
                    _Allowance_ChargesInv.Add(_Allo_ChargesInv);


                    All_Template_Allowance = All_Template_Allowance + _Template_Allowance;
                }
            }
            //*************************Allow Invoice*********************** 
            foreach (var item in Inv.Inv_Allow)
            {
                if (item.AllowAmount > 0)
                {
                    _Allo_ChargesInv = new Allowance_ChargesInv();
                    _Template_Allowance = _template;
                    _Template_Allowance = _Template_Allowance.Replace("SET_ChargeIndicator", "true");
                    _Template_Allowance = _Template_Allowance.Replace("SET_CodeReason", SetVal(item.AllowReasonCode));
                    _Template_Allowance = _Template_Allowance.Replace("SET_Reason", SetVal(item.AllowReason));
                    _Template_Allowance = _Template_Allowance.Replace("SET_MultiplierFactorNumeric", "");
                    _Template_Allowance = _Template_Allowance.Replace("SET_Amount", SetVal(item.AllowAmount));
                    _Template_Allowance = _Template_Allowance.Replace("SET_BaseAmount", @"");
                    _Template_Allowance = _Template_Allowance.Replace("SET_ID_Category", SetVal(item.AllowVatNat));
                    _Template_Allowance = _Template_Allowance.Replace("SET_Percent", SetVal(item.AllowVatPrc));
                    _Template_Allowance = _Template_Allowance.Replace("SET_currencyID", SetVal(Inv.TaxCurrencyCode));

                    _Allo_ChargesInv.ID = item.AllowVatNat;
                    _Allo_ChargesInv.Percent = item.AllowVatPrc;
                    _Allo_ChargesInv.Amount = item.AllowAmount;
                    _Allo_ChargesInv.Vat_Amount = item.AllowVat;
                    _Allo_ChargesInv.ChargeIndicator = true;

                    _Allowance_ChargesInv.Add(_Allo_ChargesInv);

                    All_Template_Allowance = All_Template_Allowance + _Template_Allowance;
                }
            }


            ////*************************Round Invoice*********************** 
            //if (Inv.HD_Round > 0)
            //{
            //    _Allo_ChargesInv = new Allowance_ChargesInv();
            //    _Template_Allowance = _template;
            //    _Template_Allowance = _Template_Allowance.Replace("SET_ChargeIndicator", "false");
            //    _Template_Allowance = _Template_Allowance.Replace("SET_CodeReason", "95");
            //    _Template_Allowance = _Template_Allowance.Replace("SET_Reason", "Round");
            //    _Template_Allowance = _Template_Allowance.Replace("SET_MultiplierFactorNumeric", "");
            //    _Template_Allowance = _Template_Allowance.Replace("SET_Amount", SetVal(Inv.HD_Round.ToFixedNoRounding(2)));
            //    _Template_Allowance = _Template_Allowance.Replace("SET_BaseAmount", @"0.00");
            //    _Template_Allowance = _Template_Allowance.Replace("SET_ID_Category", "S");
            //    _Template_Allowance = _Template_Allowance.Replace("SET_Percent", SetVal(15.00));
            //    _Template_Allowance = _Template_Allowance.Replace("SET_currencyID", SetVal(Inv.TaxCurrencyCode));

            //    _Allo_ChargesInv.ID = "S";
            //    _Allo_ChargesInv.Percent = 15;
            //    _Allo_ChargesInv.Amount = Inv.HD_Round;
            //    _Allo_ChargesInv.Vat_Amount = 0;
            //    _Allo_ChargesInv.ChargeIndicator = false;
            //    _Allowance_ChargesInv.Add(_Allo_ChargesInv);


            //    All_Template_Allowance = All_Template_Allowance + _Template_Allowance;
            //}





            _Allowance_Inv.All_Template_Allowance = All_Template_Allowance;
            _Allowance_Inv.Allowance_ChargesInv = _Allowance_ChargesInv;

            return _Allowance_Inv;

        }

    }
}
