/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using System;
using System.Linq;
using InstagramApiSharp.Helpers;
using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Classes.ResponseWrappers;

namespace InstagramApiSharp.Converters
{
    internal class InstaUserPresenceListConverter : IObjectConverter<InstaUserPresenceList, InstaUserPresenceContainerResponse>
    {
        public InstaUserPresenceContainerResponse SourceObject { get; set; }

        public InstaUserPresenceList Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException($"Source object");
            var list = new InstaUserPresenceList();
            if (SourceObject.Items != null && SourceObject.Items.Any())
            {
                foreach (var item in SourceObject.Items)
                {
                    try
                    {
                        list.Add(ConvertersFabric.Instance.GetSingleUserPresenceConverter(item).Convert());
                    }
                    catch { }
                }
            }
            return list;
        }
    }
}
