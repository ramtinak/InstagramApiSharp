/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ RamtinJokar@outlook.com ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/////////////////////////////////////////////////////////////////////
////////////////////// IMPORTANT NOTE ///////////////////////////////
// Please check wiki pages for more information:
// https://github.com/ramtinak/InstagramApiSharp/wiki
////////////////////// IMPORTANT NOTE ///////////////////////////////
/////////////////////////////////////////////////////////////////////
namespace FacebookLoginExample
{
    static class Helper
    {
        public static string PrintException(this Exception ex, string name = "")
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{name} exception thrown: ");
            sb.AppendLine($"Source: {ex.Source}");
            sb.AppendLine($"Stack trace: {ex.StackTrace}");
            sb.AppendLine();
            return sb.Output();
        }

        public static string Output(this object source, string start = "")
        {
            string content = $"{start} {Convert.ToString(source)}";
            Debug.WriteLine(content);
            return content;
        }
    }
}
