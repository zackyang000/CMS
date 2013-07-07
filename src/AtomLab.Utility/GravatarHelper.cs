using System;
using System.Text;

namespace AtomLab.Utility
{
    public class GravatarHelper
    {
        public static string GetImageSource(string email, string defaultImage, int size=80, Rating maxRating=Rating.X)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(email.Trim()))
                return defaultImage;

            var imageUrl = "http://www.gravatar.com/avatar.php?";
            var encoder = new UTF8Encoding();
            var md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            var hashedBytes = md5.ComputeHash(encoder.GetBytes(email.ToLower()));
            var sb = new StringBuilder(hashedBytes.Length*2);

            for (var i = 0; i < hashedBytes.Length; i++)
                sb.Append(hashedBytes[i].ToString("X2"));

            imageUrl += "gravatar_id=" + sb.ToString().ToLower();
            imageUrl += "&rating=" + maxRating.ToString();
            imageUrl += "&size=" + size.ToString();

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
    }
}
