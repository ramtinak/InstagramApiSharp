using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
