using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public static class TLVHELPER
    {
        public static string HexToBase64(string strInput)
        {
            try
            {
                var bytes = new byte[strInput.Length / 2];
                for (var i = 0; i < bytes.Length; i++)
                {
                    bytes[i] = Convert.ToByte(strInput.Substring(i * 2, 2), 16);
                }
                return Convert.ToBase64String(bytes);
            }
            catch (Exception)
            {
                return "-1";
            }
        }

        public static string TLV(string tag, string value)
        {
            string decString = tag.ToString();
            string decString3 = value;
            byte[] bytes3 = Encoding.UTF8.GetBytes(decString3);
            string hexString3 = BitConverter.ToString(bytes3);
            hexString3 = hexString3.Replace("-", "");
            int decString2 = bytes3.Length;
            string result = decString2.ToString("X2").ToLower();
            return String.Concat(tag.ToString(), result, hexString3.ToLower());
        }


        //
        private static byte[] GetTLVForValue(int TagNumber, string TagValue)
        {
            byte[] byteValue = Encoding.UTF8.GetBytes(TagValue);
            byte[] val = new byte[2 + byteValue.Length];
            val[0] = (byte)TagNumber;
            val[1] = (byte)byteValue.Length;
            byteValue.CopyTo(val, 2);
            return val;
        }
        public static string GetBase64String(string selllername, string vatnumber, string invoicedatetime, string total, string taxamount)
        {
            var SellerName = GetTLVForValue(1, selllername);
            var vatReg = GetTLVForValue(2, vatnumber);
            var TimeStmp = GetTLVForValue(3, invoicedatetime);
            var InvTotal = GetTLVForValue(4, total);
            var VatTotal = GetTLVForValue(5, taxamount);
            List<byte> TLVBytes = new List<byte>();
            TLVBytes.AddRange(SellerName);
            TLVBytes.AddRange(vatReg);
            TLVBytes.AddRange(TimeStmp);
            TLVBytes.AddRange(InvTotal);
            TLVBytes.AddRange(VatTotal);
            var QrTextValue = Convert.ToBase64String(TLVBytes.ToArray());
            return QrTextValue;
        }

        //

    }

}
