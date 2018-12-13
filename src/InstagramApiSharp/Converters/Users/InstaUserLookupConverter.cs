/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using System;
using InstagramApiSharp.Enums;
using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Classes.ResponseWrappers;

namespace InstagramApiSharp.Converters
{
    internal class InstaUserLookupConverter : IObjectConverter<InstaUserLookup, InstaUserLookupResponse>
    {
        public InstaUserLookupResponse SourceObject { get; set; }

        public InstaUserLookup Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException($"Source object");
            var lookup = new InstaUserLookup
            {
                CanEmailReset = SourceObject.CanEmailReset,
                CanSmsReset = SourceObject.CanSmsReset,
                CanWaReset = SourceObject.CanWaReset,
                CorrectedInput = SourceObject.CorrectedInput,
                Email = SourceObject.Email,
                EmailSent = SourceObject.EmailSent,
                HasValidPhone = SourceObject.HasValidPhone,
                MultipleUsersFound = SourceObject.MultipleUsersFound,
                PhoneNumber = SourceObject.PhoneNumber,
                SmsSent = SourceObject.SmsSent
            };
            try
            {
                if (!string.IsNullOrEmpty(SourceObject.LookupSource))
                    lookup.LookupSourceType = (InstaLookupType)Enum.Parse(typeof(InstaLookupType), SourceObject.LookupSource, true);
            }
            catch { }
            try
            {
                if (SourceObject.User != null)
                    lookup.User = ConvertersFabric.Instance.GetUserShortConverter(SourceObject.User).Convert();
            }
            catch { }

            return lookup;
        }
    }
}
