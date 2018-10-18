/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Classes.ResponseWrappers;
using System;

namespace InstagramApiSharp.Converters
{
    internal class InstaStoryCTAConverter : IObjectConverter<InstaStoryCTA, InstaStoryCTAResponse>
    {
        public InstaStoryCTAResponse SourceObject { get; set; }

        public InstaStoryCTA Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException($"Source object");

            var storyCta = new InstaStoryCTA
            {
                AndroidClass = SourceObject.AndroidClass,
                CallToActionTitle = SourceObject.CallToActionTitle,
                DeeplinkUri = SourceObject.DeeplinkUri,
                IgUserId = SourceObject.IgUserId,
                LeadGenFormId = SourceObject.LeadGenFormId,
                LinkType = SourceObject.LinkType,
                Package = SourceObject.Package,
                RedirectUri = SourceObject.RedirectUri,
                WebUri = SourceObject.WebUri
            };

            return storyCta;
        }
    }
}
