using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer
{
    public class EncryptAndDrcrypy
    {
        public static string key = "asrsp@@asrsp@";
        public static string ConvertToEncrypt(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return "";
            }
            else
            {
                password += key;
                var passwordByte = Encoding.UTF8.GetBytes(password);
                return Convert.ToBase64String(passwordByte);
            }
            
        }
        public static string ConvertToDecrypt(string base64EncodeData)
        {
            if (string.IsNullOrEmpty(base64EncodeData))
            {
                return "";
            }
            else
            {
                var base64EncodeBytes = Convert.FromBase64String(base64EncodeData);
                var result = Encoding.UTF8.GetString(base64EncodeBytes);
                result.Substring(0,result.Length - key.Length);
                return result;
            }
        }
    }
}
