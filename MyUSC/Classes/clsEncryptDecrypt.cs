using System.Security.Cryptography;
using System.Configuration;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using System;
using MyUSC.Classes;

public class clsEncryptDecrypt
{
    public string strStringToEncrypt { get; set; }
    public string strStringToDecrypt { get; set; }
    public string strEncryptedString { get; set; }
    public string strDecryptedString { get; set; }
    public string GETstrUser { get; set; }

    public bool ExecuteEncryption()
    {
        try
        {
            TripleDESCryptoServiceProvider crp = new TripleDESCryptoServiceProvider();
            UnicodeEncoding uEncode = new UnicodeEncoding();
            ASCIIEncoding aEncode = new ASCIIEncoding();

            //private key
            byte[] bytPlainText = uEncode.GetBytes(strStringToEncrypt);
            MemoryStream stmCipherText = new MemoryStream();
            // Get the AppSettings section.
            NameValueCollection appStgs = ConfigurationManager.AppSettings;
            string strSeed = appStgs["EncryptDecryptSeed"];
            byte[] slt = { 0X0, 0X1, 0X2, 0X3, 0X4, 0X5, 0X6, 0XF1, 0XF0, 0XEE, 0X21, 0X22, 0X45 };
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(strSeed, slt);
            byte[] bytDerivedKey = pdb.GetBytes(24);
            crp.Key = bytDerivedKey;
            crp.IV = pdb.GetBytes(8);
            CryptoStream csEncrypted = new CryptoStream(stmCipherText, crp.CreateEncryptor(), CryptoStreamMode.Write);
            csEncrypted.Write(bytPlainText, 0, bytPlainText.Length);
            csEncrypted.FlushFinalBlock();
            //Return resultcp as a Base64 encoded string
            strEncryptedString = Convert.ToBase64String(stmCipherText.ToArray());
            return true;
        }
        catch (Exception ex)
        {
			EvtLog.WriteException("ExecuteEncryption", ex, 99);
            return false;
        }
        finally
        {

        }

    }

    public bool ExecuteDecryption()
    {
        TripleDESCryptoServiceProvider crp = new TripleDESCryptoServiceProvider();
        UnicodeEncoding uEncode = new UnicodeEncoding();
        ASCIIEncoding aEncode = new ASCIIEncoding();
        byte[] bytCiphertext = Convert.FromBase64String(strStringToDecrypt.Replace(" ", "+"));
        MemoryStream stmPlainText = new MemoryStream();
        MemoryStream stmCipherText = new MemoryStream(bytCiphertext);

        try
        {
            //private key
            NameValueCollection appStgs = ConfigurationManager.AppSettings;
            string strSeed = appStgs["EncryptDecryptSeed"];
            byte[] slt = { 0X0, 0X1, 0X2, 0X3, 0X4, 0X5, 0X6, 0XF1, 0XF0, 0XEE, 0X21, 0X22, 0X45 };
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(strSeed, slt);
            byte[] bytDerivedKey = pdb.GetBytes(24);
            crp.Key = bytDerivedKey;
            crp.IV = pdb.GetBytes(8);
            CryptoStream csDecrypted = new CryptoStream(stmCipherText, crp.CreateDecryptor(), CryptoStreamMode.Read);
            StreamWriter sw = new StreamWriter(stmPlainText);
            StreamReader sr = new StreamReader(csDecrypted);
            sw.Write(sr.ReadToEnd());
            sw.Flush();
            csDecrypted.Clear();
            crp.Clear();
            strDecryptedString = uEncode.GetString(stmPlainText.ToArray());
            return true;
        }
        catch (Exception ex)
        {
			EvtLog.WriteException("ExecuteDecryption", ex, 99);
            return false;
        }
        finally
        {

        }
    }
}
