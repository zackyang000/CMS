using System;
using System.IO;
using System.Security.Cryptography;

namespace AtomLab.Utility
{
    public static class EncryptFileHelper
    {        
        /// <summary>
        /// 计算文件MD5值.
        /// </summary>
        /// <param name="path">传入文件绝对路径</param>
        /// <returns>返回文件的MD5值</returns>
        public static string GetMD5(string path)
        {
            var fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            var md5 = new MD5CryptoServiceProvider();//实例化一个md5对像
            byte[] byteMD5 = md5.ComputeHash(fs);
            fs.Close();
            return BitConverter.ToString(byteMD5).Replace("-", "");

            /*以下算法也可得到MD5,但与WinMD5所计算结果不同(计算结果不含0?) */
            /*例如:同一文件,上面结果为:046BAE2D0F8416E19FC59FFB44CBF1E0  */
            /*             下面结果为:46bae2df8416e19fc59ffb44cbf1e0    */
            /*若要使用以下算法,只用删去上面return一行即可                 */
            //string strMD5 = "";
            //foreach (byte b in byteMD5)
            //{
            //    strMD5 += Convert.ToString(b, 16);
            //}
            //return strMD5;
        }
    }
}
