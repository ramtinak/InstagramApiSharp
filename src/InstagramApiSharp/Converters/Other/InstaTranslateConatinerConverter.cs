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
using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Classes.ResponseWrappers;

namespace InstagramApiSharp.Converters
{
    internal class InstaTranslateContainerConverter : IObjectConverter<InstaTranslateList, InstaTranslateContainerResponse>
    {
        public InstaTranslateContainerResponse SourceObject { get; set; }

        public InstaTranslateList Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException("SourceObject");

            var list = new InstaTranslateList();
            if (SourceObject.Translations != null && SourceObject.Translations.Any())
                foreach (var item in SourceObject.Translations)
                    list.Add(ConvertersFabric.Instance.GetSingleTranslateConverter(item).Convert());

            return list;
        }
    }
}
