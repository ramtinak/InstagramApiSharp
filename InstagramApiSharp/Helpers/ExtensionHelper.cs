using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace InstagramApiSharp
{
    internal static class ExtensionHelper
    {
        public static string EncodeList(this string[] listOfValues, bool appendQuotation = true)
        {
            return EncodeList(listOfValues.ToList(), appendQuotation);
        }
        public static string EncodeList(this List<string> listOfValues, bool appendQuotation = true)
        {
            if (!appendQuotation)
                return string.Join(",", listOfValues);
            var list = new List<string>();
            foreach (var item in listOfValues)
                list.Add("\"" + item + "\"");
            return string.Join(",", list);
        }
    }
}
