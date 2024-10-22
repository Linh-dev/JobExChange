using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Business.Utilities
{
    public class EncryptUtil
    {
        public static byte[] DEFAULT_KEY = UTF8Encoding.UTF8.GetBytes("792458ASD!@#uiop");
        public static byte[] DEFAULT_IV = UTF8Encoding.UTF8.GetBytes("sdjk786411vno)(*&%");
        public static string GetSha1(string data)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(data));
                var sb = new StringBuilder(hash.Length * 2);

                foreach (byte b in hash)
                {
                    // can be "x2" if you want lowercase
                    sb.Append(b.ToString("x2"));
                }

                return sb.ToString();
            }
        }

        public static string GetSha512(string data)
        {
            using (SHA512Managed sha1 = new SHA512Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(data));
                var sb = new StringBuilder(hash.Length * 2);

                foreach (byte b in hash)
                {
                    // can be "x2" if you want lowercase
                    sb.Append(b.ToString("x2"));
                }

                return sb.ToString();
            }
        }

        public static string GetSha256(string data)
        {
            using (var sha1 = new SHA256Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(data));
                var sb = new StringBuilder(hash.Length * 2);

                foreach (byte b in hash)
                {
                    // can be "x2" if you want lowercase
                    sb.Append(b.ToString("x2"));
                }
                var res = sb.ToString().Substring(0, 32);
                return res;
            }
        }

        public static string Md5(string data)
        {
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] tmpSource;
                byte[] tmpHash;

                tmpSource = ASCIIEncoding.UTF8.GetBytes(data); // Turn password into byte array
                tmpHash = md5.ComputeHash(tmpSource);

                StringBuilder sOutput = new StringBuilder(tmpHash.Length);
                for (int i = 0; i < tmpHash.Length; i++)
                {
                    sOutput.Append(tmpHash[i].ToString("X2"));  // X2 formats to hexadecimal
                }
                return sOutput.ToString();
            }
        }

        public static string SignData(string pfxPath, string password, string dataStr)
        {
            var collection = new X509Certificate2Collection();
            collection.Import(pfxPath, password, X509KeyStorageFlags.PersistKeySet);
            var certificate = collection[0];
            var privateKey = certificate.PrivateKey as RSACryptoServiceProvider;
            var data = Encoding.UTF8.GetBytes(dataStr);
            var signature = privateKey.SignData(data, "SHA1");
            return Convert.ToBase64String(signature);
        }


        public static string EncryptStringToBytes(string plainText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;
            // Create an RijndaelManaged object
            // with the specified key and IV.
            using (RijndaelManaged rijAlg = new RijndaelManaged())
            {
                rijAlg.Key = Key;
                rijAlg.IV = IV;
                rijAlg.Mode = CipherMode.CBC;
                rijAlg.Padding = PaddingMode.PKCS7;
                // Create an encryptor to perform the stream transform.
                ICryptoTransform encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {

                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }


            // Return the encrypted bytes from the memory stream.
            //return encrypted;
            return Convert.ToBase64String(encrypted);
        }

        public static string DecryptStringFromBytes(string encryptText, byte[] Key, byte[] IV)
        {

            byte[] cipherText = Convert.FromBase64String(encryptText);

            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an RijndaelManaged object
            // with the specified key and IV.
            using (RijndaelManaged rijAlg = new RijndaelManaged())
            {
                rijAlg.Key = Key;
                rijAlg.IV = IV;
                rijAlg.Mode = CipherMode.CBC;
                rijAlg.Padding = PaddingMode.PKCS7;
                // Create a decryptor to perform the stream transform.
                ICryptoTransform decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }

            }

            return plaintext;

        }

        public static string AesEncrypt(string plainText)
        {
            return EncryptStringToBytes(plainText, DEFAULT_KEY, DEFAULT_IV);
        }

        public static string AesDecrypt(string cipherText)
        {
            return DecryptStringFromBytes(cipherText, DEFAULT_KEY, DEFAULT_IV);
        }

        public static string AesEncrypt(string plainText, string key, string iv)
        {
            var arrKey = UTF8Encoding.UTF8.GetBytes(key);
            var arrIv = UTF8Encoding.UTF8.GetBytes(iv);
            return EncryptStringToBytes(plainText, arrKey, arrIv);
        }

        public static string AesDecrypt(string cipherText, string key, string iv)
        {
            var arrKey = UTF8Encoding.UTF8.GetBytes(key);
            var arrIv = UTF8Encoding.UTF8.GetBytes(iv);
            return DecryptStringFromBytes(cipherText, arrKey, arrIv);
        }
    }
}
