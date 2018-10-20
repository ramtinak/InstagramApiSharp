/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using System;
using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Classes.ResponseWrappers;

namespace InstagramApiSharp.Converters
{
    internal class InstaTranslateConverter : IObjectConverter<InstaTranslate, InstaTranslateResponse>
    {
        public InstaTranslateResponse SourceObject { get; set; }

        public InstaTranslate Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException("SourceObject");

            var translate = new InstaTranslate
            {
                Id = SourceObject.Id,
                Translation = SourceObject.Translation
            };
            return translate;
        }
    }
}
