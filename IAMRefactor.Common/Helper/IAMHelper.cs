using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XSystem.Security.Cryptography;

namespace IAMRefactor.Common.Helper
{
    public static class IAMHelper
    {
        /// <summary>
        /// Gets the current time in seconds from January 1st, 1970 (UTC)
        /// </summary>
        public static long CurrentTimeSeconds
            => (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
  


        public static byte[] GetUTF8SHA256Hash(this string str)
        {
            var y = Encoding.UTF8.GetBytes(str);
            var x = new SHA256Managed().ComputeHash(y);
            return x;
        }

        public static string ToBase64String(this byte[] bytes) =>
          Convert.ToBase64String(bytes);

    }


    public static class IAMLanguages
    {
        public const string English = "en";
        public const string Arabic = "ar";
    }
}
