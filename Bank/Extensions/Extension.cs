using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Extensions
{
    public static class Extension
    {
        public static bool IsInteger(this string s)
        {
            float output;
            return float.TryParse(s, out output);
        }
    }
}
