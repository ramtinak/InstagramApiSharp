using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace InstagramApiSharp
{
    public static class ExtensionHelper
    {
        public static string EncodeList(string[] listOfValues)
        {
            return EncodeList(listOfValues.ToList());
        }
        public static string EncodeList(List<string> listOfValues)
        {
            var list = new List<string>();
            foreach (var item in listOfValues)
                list.Add("\"" + item + "\"");
            return string.Join(",", list);
        }
    }
}
