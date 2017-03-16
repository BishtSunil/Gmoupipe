using System;
using System.Collections.Generic;
using System.Web;
using System.Security.Cryptography;
using System.Security;
using System.Text;

/// <summary>
/// Summary description for Encrypt
/// </summary>
public class Encrypt
{
	public Encrypt()
	{
	}

    public static string encryptStr(string en)
    {
            byte[] b = System.Text.ASCIIEncoding.ASCII.GetBytes(en);
            string encryptedConnectionString = Convert.ToBase64String(b);
        return encryptedConnectionString;
    }

    public static string decryptStr(string de)
    {
        byte[] b = Convert.FromBase64String(de);
        string decryptedConnectionString = System.Text.ASCIIEncoding.ASCII.GetString(b);
        return decryptedConnectionString;
    }

    private static byte[] Key = { 221, 218, 213, 111, 122, 23, 134, 145, 123, 134, 231, 222, 11, 23, 222, 12, 122, 11, 200, 104, 23, 13, 22, 116 };
    private static byte[] IV = { 241, 203, 179, 187, 97, 78, 211, 222 };
    public static string encryptMethod(string inputString)
    {
        if (!string.IsNullOrEmpty(inputString))
        {
            byte[] buffer = Encoding.ASCII.GetBytes(inputString);

            TripleDESCryptoServiceProvider tripleDes = new TripleDESCryptoServiceProvider()
            {
                Key = Key,
                IV = IV
            };
            ICryptoTransform ITransform = tripleDes.CreateEncryptor();
            string id = Convert.ToBase64String(ITransform.TransformFinalBlock(buffer, 0, buffer.Length));
            //plus=>slu  ,  minus=>minda  ,  equal=>kukur    ,  forwardSlash=>fuddasalua   ,  backSlash=>bukkaku
            id = id.Replace("+", "slua").Replace("-", "minda").Replace("=", "kukur").Replace("/", "fuddasalua").Replace(@"\", "bukkaku");
            return id;
        }
        else
            return "";
    }
    public static string decryptMethod(string inputString)
    {
        try
        {
            if (!string.IsNullOrEmpty(inputString))
            {
                inputString = inputString.Replace("slua", "+").Replace("minda", "-").Replace("kukur", "=").Replace("fuddasalua", "/").Replace("bukkaku", @"\"); ;
                byte[] buffer = Convert.FromBase64String(inputString);
                TripleDESCryptoServiceProvider tripleDes = new TripleDESCryptoServiceProvider()
                {
                    Key = Key,
                    IV = IV
                };
                ICryptoTransform ITransform = tripleDes.CreateDecryptor();
                return Encoding.ASCII.GetString(ITransform.TransformFinalBlock(buffer, 0, buffer.Length));
            }
            else return "";
        }
        catch (Exception ex)
        {
          //return "0";
            return inputString;
           //   return inputString;
        }
    }

}
