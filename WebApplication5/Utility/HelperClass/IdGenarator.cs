using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication5.Utility.HelperClass
{
    public class IdGenarator
    {

        public static string Id()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[11];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];

            }

            var finalString = new String(stringChars);
            return finalString;
        }

    }
}