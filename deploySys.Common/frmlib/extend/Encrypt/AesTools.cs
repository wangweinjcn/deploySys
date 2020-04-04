using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrmLib.Encrypt
{
    /***针对java的加密代码，配套c#的代码
     * public static String decrypt(String data, String key)
{
if (StringUtils.isEmpty(data))
{
return null;
}

try
{
Key k = getKey(key);
Cipher cipher = Cipher.getInstance("AES/ECB/PKCS5Padding");
cipher.init(2, k);

byte[] keyBytes = Base64.decodeBase64(data);
if (null == keyBytes)
{
throw new IllegalArgumentException("Failed to decode key by Base64.");
}
return new String(cipher.doFinal(keyBytes));
}
catch (Exception e)
{
throw new IllegalArgumentException(e);
}
}

     * 
     * 
     * *
     */
   public class AesTools
    {
        
         /// <summary>
        ///  AES 加密
        /// </summary>
        /// <param name="str"></param>
        /// <param name="key">"1234567890abcDEF"</param>
        /// <returns></returns>
       public static string AesEncrypt(string str, string key = "1234567890abcDEF")
        {
            if (string.IsNullOrEmpty(str)) return null;
            Byte[] toEncryptArray = Encoding.UTF8.GetBytes(str);

            System.Security.Cryptography.RijndaelManaged rm = new System.Security.Cryptography.RijndaelManaged
            {
                Key = Encoding.UTF8.GetBytes(key),
                Mode = System.Security.Cryptography.CipherMode.ECB,
                Padding = System.Security.Cryptography.PaddingMode.PKCS7
            };

            System.Security.Cryptography.ICryptoTransform cTransform = rm.CreateEncryptor();
            Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        /// <summary>
        ///  AES 解密
        /// </summary>
        /// <param name="str"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string AesDecrypt(string str, string key = "1234567890abcDEF")
        {
            
            if (string.IsNullOrEmpty(str)) return null;
            Byte[] toEncryptArray = Convert.FromBase64String(str);

            System.Security.Cryptography.RijndaelManaged rm = new System.Security.Cryptography.RijndaelManaged
            {
                Key = Encoding.UTF8.GetBytes(key),
                Mode = System.Security.Cryptography.CipherMode.ECB,
                Padding = System.Security.Cryptography.PaddingMode.PKCS7
            };

            System.Security.Cryptography.ICryptoTransform cTransform = rm.CreateDecryptor();
            Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Encoding.UTF8.GetString(resultArray);
        }
    
    }
}
