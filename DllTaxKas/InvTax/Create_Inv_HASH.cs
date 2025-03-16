using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text; 
using ZatcaIntegrationSDK.BLL;

namespace KSAEinvoice
{

    public class Create_Inv_HASH : TaxShared
    {


        public string CalculateSHA256HashNew(string xmlData)
        {
            // Your XML data (replace this with your actual XML content) 

            // Encode the XML data as bytes using UTF-8 encoding
            byte[] xmlBytes = Encoding.UTF8.GetBytes(xmlData);
            string calculatedHash = "";
            // Calculate the SHA-256 hash of the XML data
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(xmlBytes);

                // Convert the calculated hash to the desired format
                calculatedHash = ToCustomFormat(hashBytes);

                //Console.WriteLine("Calculated Hash (Custom Format): " + calculatedHash);
            }

            return calculatedHash;
        }

        static string ToCustomFormat(byte[] hashBytes)
        {
            // Convert the byte array to a Base64-encoded string
            string base64Hash = Convert.ToBase64String(hashBytes);

            // Customize the format by removing padding and adding characters
            string customFormat = base64Hash.TrimEnd('=') + "=";

            return customFormat;
        }


        public string Get_HASH(string xmlAsString)
        {



            string Hash1 = CalculateSHA256Hash(xmlAsString);
            var ENHash1 = Encoding.UTF8.GetBytes(Hash1);
            string base64EncodedHash = Convert.ToBase64String(ENHash1);


            //// Convert the Base64 string to bytes
            //byte[] decodedBytes = Convert.FromBase64String(base64EncodedHash);

            //// Convert the byte array to a string (assuming it's a valid string)
            //string sha256Hash = Encoding.UTF8.GetString(decodedBytes);

            //string reversedInput = ReverseSHA256Hash(sha256Hash);

            return base64EncodedHash;

        }

        public static byte[] ExtractR(string digitalSignature)
        {
            byte[] decodedBytes = Convert.FromBase64String(digitalSignature);

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hash = sha256.ComputeHash(decodedBytes);

                // Truncate to the first 32 bytes (256 bits)
                byte[] truncatedHash = new byte[32];
                Array.Copy(hash, truncatedHash, 32);

                return truncatedHash;
            }
        }


        public string CalculateSHA256Hash(string input)
        {
            //input = FormatXml(input);

            //StringBuilder Sb = new StringBuilder();
            //Byte[] result =  ExtractR(input);
            //return BitConverter.ToString(result).Replace("-", "");

            //StringBuilder Sb = new StringBuilder();
            //using (SHA256 hash = SHA256Managed.Create())
            //{
            //    Encoding encode = Encoding.UTF8;
            //    Byte[] result = hash.ComputeHash(encode.GetBytes(input));
            //    foreach (Byte bytes in result)
            //        Sb.Append(bytes.ToString("x2"));
            //}

            //return Sb.ToString(); 


            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder builder = new StringBuilder();

                for (int i = 0; i < hashBytes.Length; i++)
                {
                    builder.Append(hashBytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }

        static string ReverseSHA256Hash(string sha256Hash)
        {
            if (sha256Hash.Length % 2 != 0)
            {
                throw new ArgumentException("Input must have an even number of characters.");
            }

            byte[] hashBytes = new byte[sha256Hash.Length / 2];
            for (int i = 0; i < hashBytes.Length; i++)
            {
                hashBytes[i] = Convert.ToByte(sha256Hash.Substring(i * 2, 2), 16);
            }

            return Encoding.UTF8.GetString(hashBytes);
        }

    }
}
