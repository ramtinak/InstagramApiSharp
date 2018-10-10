/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using System;
using System.Collections.Generic;
using System.Text;
using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Classes.Models.Business;
using InstagramApiSharp.Classes.ResponseWrappers;
using InstagramApiSharp.Classes.ResponseWrappers.Business;
using InstagramApiSharp.Helpers;
using InstagramApiSharp.Enums;
namespace InstagramApiSharp.Converters.Business
{
    internal class InstaInsightsDataNodeConverter : IObjectConverter<InstaInsightsDataNode, InstaInsightsDataNodeResponse>
    {
        public InstaInsightsDataNodeResponse SourceObject { get; set; }

        public InstaInsightsDataNode Convert()
        {
            var dataNode = new InstaInsightsDataNode
            {
                Value = SourceObject.Value ?? 0
            };
            try
            {
                var truncatedType = SourceObject.Name.Trim().Replace("_", "");

                if (Enum.TryParse(truncatedType, true, out InstaInsightsNameType type))
                    dataNode.NameType = type;
            }
            catch { }
            return dataNode;
        }
    }
}
