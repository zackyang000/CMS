using System;
using System.IO;
using System.Security.Cryptography;

namespace AtomLab.Utility
{
    /// <summary>
    /// 加密字符串
    /// </summary>
    public static class EncryptStringHelper
    {
        //密钥 remark:改变密钥将改变DES加密/解密结果.
        private static readonly byte[] Key64 = { 254, 72, 48, 66, 12, 123, 55, 46 };
        private static readonly byte[] IV64 = { 25, 251, 244, 15, 13, 88, 168, 33 };

        /// <summary>
        /// SHA1非对称加密.用于密码加密,使用SHA1Managed类产生长度为160位哈希值.
        /// </summary>
        /// <param name="text">传入一个任意长字符串.</param>
        /// <returns>返回长度为28字节的字符串.</returns>
        public static string GetSHA1(string text)
        {
            byte[] byteSHA1 = System.Text.Encoding.UTF8.GetBytes(text);
            var sha1 = new SHA1Managed();
            byte[] result = sha1.ComputeHash(byteSHA1);
            return Convert.ToBase64String(result);
        }

        /// <summary>
        /// DES加密.
        /// </summary>
        /// <param name="text">需加密的文本.</param>
        /// <returns>返回加密后的字符串.</returns>
        public static String Encrypt(String text)
        {
            var desprovider = new DESCryptoServiceProvider();
            var memoryStream = new MemoryStream();
            var cryptoStream = new CryptoStream(memoryStream, desprovider.CreateEncryptor(Key64, IV64), CryptoStreamMode.Write);
            var writerStream = new StreamWriter(cryptoStream);
            writerStream.Write(text);
            writerStream.Flush();
            cryptoStream.FlushFinalBlock();
            memoryStream.Flush();
            return (Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length));
        }

        /// <summary>
        /// DES解密.
        /// </summary>
        /// <param name="text">需解密的文本.</param>
        /// <returns>返回解密后的字符串.</returns>
        public static String Decrypt(String text)
        {
            var desprovider = new DESCryptoServiceProvider();
            byte[] buffer = Convert.FromBase64String(text);
            var memoryStream = new MemoryStream(buffer);
            var cryptoStream = new CryptoStream(memoryStream, desprovider.CreateDecryptor(Key64, IV64), CryptoStreamMode.Read);
            var readerStream = new StreamReader(cryptoStream);
            return readerStream.ReadToEnd();
        }
    }
}