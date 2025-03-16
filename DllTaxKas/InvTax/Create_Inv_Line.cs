using System;
using System.Collections.Generic;

namespace KSAEinvoice
{

    public class Create_Inv_Line : TaxShared
    {
        private readonly string Temp_InvoiceLine = @"
                           <cac:InvoiceLine>
                              <cbc:ID>SET_ItemID</cbc:ID>
                              <cbc:InvoicedQuantity unitCode=""SET_unitCode"">SET_InvoicedQuantity</cbc:InvoicedQuantity>
                              <cbc:LineExtensionAmount currencyID=""SET_currencyID"">SET_LineExtensionAmount</cbc:LineExtensionAmount>
	                        <cac:AllowanceCharge>
                                <cbc:ChargeIndicator>false</cbc:ChargeIndicator>
                                <cbc:AllowanceChargeReason>discount</cbc:AllowanceChargeReason>
                                <cbc:Amount currencyID=""SET_currencyID"">SET_DiscountAmount</cbc:Amount>
                            </cac:AllowanceCharge>
                              <cac:TaxTotal>
                                 <cbc:TaxAmount currencyID=""SET_currencyID"">SET_TaxAmount</cbc:TaxAmount>
                                 <cbc:RoundingAmount currencyID=""SET_currencyID"">SET_RoundingAmount</cbc:RoundingAmount>
                              </cac:TaxTotal> 
                              <cac:Item>
                                 <cbc:Name>SET_Name_Item</cbc:Name>
                                 <cac:ClassifiedTaxCategory>
                                    <cbc:ID>SET_IDTaxCategory</cbc:ID>
                                    <cbc:Percent>SET_Percent</cbc:Percent>
                                    <cac:TaxScheme>
                                       <cbc:ID>VAT</cbc:ID>
                                    </cac:TaxScheme>
                                 </cac:ClassifiedTaxCategory>
                              </cac:Item>
                             
                              <cac:Price>
                                 <cbc:PriceAmount currencyID=""SET_currencyID"">SET_PriceAmount</cbc:PriceAmount>  
                              </cac:Price>
                           </cac:InvoiceLine>";


        public string Creare_InvoiceLine(Invoice_xml Inv)
        {

            List<InvoiceLine_xml> _InvoiceLine = Inv.InvoiceLine;
            string populated_template = @"";
            //*************************************************************_TaxTotal********************************************
            for (int i = 0; i < _InvoiceLine.Count; i++)
            {
                string singl_template = Temp_InvoiceLine;
                //long _ItemID = _InvoiceLine[i].ID;
                int _ItemID =  i + 1;
                singl_template = singl_template.Replace("SET_currencyID", SetVal(Inv.TaxCurrencyCode));

                singl_template = singl_template.Replace("SET_ItemID", _ItemID.ToString()); //v
                singl_template = singl_template.Replace("SET_unitCode", SetVal(_InvoiceLine[i].Item.unitCode)); // الوحده
                singl_template = singl_template.Replace("SET_InvoicedQuantity", SetVal(_InvoiceLine[i].InvoicedQuantity.ToFixedNoRounding(2))); // كمية الصنف  
                singl_template = singl_template.Replace("SET_LineExtensionAmount", SetVal(_InvoiceLine[i].LineExtensionAmount.ToFixedNoRounding(2))); // الاجمالي بعد الخصم 

                //AllowanceCharge      _Discount _Item          
                singl_template = singl_template.Replace("SET_DiscountAmount", SetVal(_InvoiceLine[i].DiscountAmount.ToFixedNoRounding(2))); // مبلغ الخصم


                //TaxTotal
                decimal VatAmount= (_InvoiceLine[i].Item.ClassifiedTaxCategory.Percent * _InvoiceLine[i].LineExtensionAmount) / 100;    
                singl_template = singl_template.Replace("SET_TaxAmount", SetVal(VatAmount.ToFixedNoRounding(2))); // مبلغ الضريبه
                singl_template = singl_template.Replace("SET_RoundingAmount", SetVal((VatAmount + _InvoiceLine[i].LineExtensionAmount).ToFixedNoRounding(2))); // الاجمالي بعد الضريبه


                //Item
                singl_template = singl_template.Replace("SET_Name_Item", SetVal(_InvoiceLine[i].Item.Name));  // اسم الصنف
                singl_template = singl_template.Replace("SET_IDTaxCategory", SetVal(_InvoiceLine[i].Item.ClassifiedTaxCategory.ID)); // فئة الضريبه S or Z
                singl_template = singl_template.Replace("SET_Percent", SetVal(_InvoiceLine[i].Item.ClassifiedTaxCategory.Percent.ToFixedNoRounding(2))); // نسبة الضريبه
                singl_template = singl_template.Replace("SET_PriceAmount", SetVal(_InvoiceLine[i].Price.PriceAmount.ToFixedNoRounding(2))); // سعر القطعه الوحده



                populated_template = populated_template + singl_template;
            }


            return populated_template;

        }






    }
}
