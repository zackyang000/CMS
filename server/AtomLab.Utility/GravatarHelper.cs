using System;
using System.Security.Cryptography;
using System.Text;

namespace AtomLab.Utility
{
    public class GravatarHelper
    {
        public static string GetImage(string email, string defaultImage="", int size=80, Rating maxRating=Rating.X)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(email.Trim()))
                return defaultImage;

            var imageUrl = "http://www.gravatar.com/avatar.php?";

            imageUrl += "gravatar_id=" + HashEmailForGravatar(email.ToLower());
            imageUrl += "&rating=" + maxRating;
            imageUrl += "&size=" + size;

            if (!string.IsNullOrEmpty(defaultImage))
                imageUrl += "&default=" + System.Web.HttpUtility.UrlEncode(defaultImage);

            return imageUrl;
        }

        /// NGravatar avatar rating.
        public enum Rating
        {
            G,
            PG,
            R,
            X
        }

        /// Hashes an email with MD5.  Suitable for use with Gravatar profile
        /// image urls
        private static string HashEmailForGravatar(string email)
        {
            if (email == null) return string.Empty;

            // Create a new instance of the MD5CryptoServiceProvider object.  
            MD5 md5Hasher = MD5.Create();

            // Convert the input string to a byte array and compute the hash.  
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(email));

            // Create a new Stringbuilder to collect the bytes  
            // and create a string.  
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data  
            // and format each one as a hexadecimal string.  
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();  // Return the hexadecimal string. 
        }
    }
}
