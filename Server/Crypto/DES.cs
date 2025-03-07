﻿using System.Security.Cryptography;
using System.Text;

namespace Server.Crypto
{
    public class DES
    {
        private static string key;
        private static string iv;

        public DES(string k, string i)
        {
            key = k;
            iv = i;
        }

        public string Encrypt(string plainText)
        {
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                des.Key = Encoding.UTF8.GetBytes(key);
                des.IV = Encoding.UTF8.GetBytes(iv);
                des.Mode = CipherMode.CBC;
                des.Padding = PaddingMode.PKCS7;

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        byte[] inputBytes = Encoding.UTF8.GetBytes(plainText);
                        cs.Write(inputBytes, 0, inputBytes.Length);
                        cs.FlushFinalBlock();
                    }
                    byte[] encryptedData = ms.ToArray();
                    string encryptedTextBase64 = Convert.ToBase64String(encryptedData);
                    return encryptedTextBase64;
                }
            }
        }

        public string Decrypt(string cipherTextBase64)
        {
            byte[] cipherText = Convert.FromBase64String(cipherTextBase64);

            if (cipherText.Length % 8 != 0)
            {
                throw new CryptographicException("Cifrovani podaci nisu u pravilnom formatu.");
            }

            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                des.Key = Encoding.UTF8.GetBytes(key);
                des.IV = Encoding.UTF8.GetBytes(iv);
                des.Mode = CipherMode.CBC;
                des.Padding = PaddingMode.PKCS7;

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherText, 0, cipherText.Length);
                        cs.FlushFinalBlock();
                        return Encoding.UTF8.GetString(ms.ToArray());
                    }
                }
            }
        }
        /*
        static void Main()
        {
            string originalText = "Dijana";
            Console.WriteLine("Originalni tekst: " + originalText);

            // DES ključ i IV (moraju biti tačno 8 bajtova)
            byte[] key = Encoding.UTF8.GetBytes("12345678"); // 8-bajtni ključ (64 bita)
            byte[] iv = Encoding.UTF8.GetBytes("87654321");  // 8-bajtni IV (inicijalni vektor)

            // Šifrovanje
            byte[] encryptedData = Encrypt(originalText, key, iv);
            string encryptedTextBase64 = Convert.ToBase64String(encryptedData);
            Console.WriteLine("Šifrovani tekst (Base64): " + encryptedTextBase64);

            // Dešifrovanje
            string decryptedText = Decrypt(encryptedData, key, iv);
            Console.WriteLine("Dešifrovani tekst: " + decryptedText);
        }
        */
    }
}
