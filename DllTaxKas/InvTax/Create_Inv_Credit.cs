using System;
using System.Collections.Generic;

namespace KSAEinvoice
{

    public class Create_Inv_Credit : TaxShared
    {
        private readonly string Temp_Invoice = @"<cac:BillingReference>
                                                    <cac:InvoiceDocumentReference>
                                                        <cbc:ID>SET_BillingReferenceID</cbc:ID>
                                                    </cac:InvoiceDocumentReference>
                                                </cac:BillingReference>";


        public string Create_Credit(Invoice_xml Inv)
        {

            List<Inv_Credit> Inv_Cred = Inv.Inv_Credit;
            string populated_template = @"";
            //*************************************************************_TaxTotal********************************************
            for (int i = 0; i < Inv_Cred.Count; i++)
            {
                string singl_template = Temp_Invoice; 

                singl_template = singl_template.Replace("SET_BillingReferenceID", SetVal(Inv_Cred[i].BillingReferenceID));  


                populated_template = populated_template + singl_template;
            }


            return populated_template;

        }






    }
}
