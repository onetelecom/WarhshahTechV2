using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Helper
{
    public static class SalesReqConst
    {
        public const int Initial = 1; // حالة البدء

        public const int Offer = 2; // عرض السعر

        public const int WOwnerApproved = 3; //موافقة مالك الورشه 

        public const int WOwnerRejected = 6; //رفض مالك الورشه 

        public const int Shipping = 4; //جاري الشحن

        public const int Shipped = 5;//تم الشحن 



    }
}
