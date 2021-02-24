using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSCSC
{
     public static class CapitalizeString
    {
        public static string FirstCharToUpper(this string str)
        {
            if (String.IsNullOrEmpty(str))
            {
                throw new ArgumentException("Input string is Empty!");
            }

            return str.First().ToString().ToUpper() + str.Substring(1);
        }
    }
}
