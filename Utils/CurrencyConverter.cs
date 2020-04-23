using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ViewApplication.Utils
{
    public static class CurrencyConverter
    {
        public static string formatAmount(decimal? amount)
        {         
            return string.Format(new CultureInfo("ig-NG", false), "{0:c0}", amount);
        }
        public static decimal currencyToDecimal(string amount)
        {
            decimal value = decimal.Parse(amount, NumberStyles.Any);
            return value;
        }
    }
}
