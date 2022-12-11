/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ RamtinJokar@outlook.com ]
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
    internal class InstaPrimaryCountryInfoConverter : IObjectConverter<InstaPrimaryCountryInfo, InstaPrimaryCountryInfoResponse>
    {
        public InstaPrimaryCountryInfoResponse SourceObject { get; set; }

        public InstaPrimaryCountryInfo Convert()
        {
            return new InstaPrimaryCountryInfo()
            {
                CountryName = SourceObject.CountryName,
                HasCountry = SourceObject.HasCountry ?? false,
                IsVisible = SourceObject.IsVisible ?? false
            };
        }
    }
}
