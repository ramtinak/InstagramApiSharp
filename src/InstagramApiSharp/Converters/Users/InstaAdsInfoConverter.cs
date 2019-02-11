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

namespace InstagramApiSharp.Converters
{
    internal class InstaAdsInfoConverter : IObjectConverter<InstaAdsInfo, InstaAdsInfoResponse>
    {
        public InstaAdsInfoResponse SourceObject { get; set; }

        public InstaAdsInfo Convert()
        {
            return new InstaAdsInfo()
            {
                AdsUrl = SourceObject.AdsUrl,
                HasAds = SourceObject.HasAds ?? false
            };
        }
    }
}
