using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Helper
{
    public static class ROStatusConst
    {                                   
        public const int Initial = 1; // حالة البدء
        public const int TechReport = 2;//تقرير الفني 
        public const int ClientApprove = 3; //انتظار موافقة العميل
        public const int ClientApproved =4; // تم الموافقه من العميل 
        public const int WaitTech = 5;//انتظار الفني
        public const int Fixing = 6;//الاصلاح
        public const int Close = 7;//اغلاق
       

        public const int ClientDidNotApproved = 9;//العميل رفض امر الصلاح


        //public const int Ready = 10;//جاهزة للاستلام
        

    }
}
