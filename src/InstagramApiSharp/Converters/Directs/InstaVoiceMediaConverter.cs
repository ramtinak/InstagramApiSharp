/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using InstagramApiSharp.Enums;
using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Classes.ResponseWrappers;
using System;

namespace InstagramApiSharp.Converters
{
    internal class InstaVoiceMediaConverter : IObjectConverter<InstaVoiceMedia, InstaVoiceMediaResponse>
    {
        public InstaVoiceMediaResponse SourceObject { get; set; }

        public InstaVoiceMedia Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException($"Source object");

            var voiceMedia = new InstaVoiceMedia
            {
                ReplayExpiringAtUs = SourceObject.ReplayExpiringAtUs,
                SeenCount = SourceObject.SeenCount ?? 0
            };

            if (!string.IsNullOrEmpty(SourceObject.ViewMode))
            {
                try
                {
                    voiceMedia.ViewMode = (InstaViewMode)Enum.Parse(typeof(InstaViewMode), SourceObject.ViewMode, true);
                }
                catch { }
            }

            if (SourceObject.SeenUserIds != null && SourceObject.SeenUserIds?.Length > 0)
                foreach (var pk in SourceObject.SeenUserIds)
                    voiceMedia.SeenUserIds.Add(pk);

            if (SourceObject.Media != null)
                voiceMedia.Media = ConvertersFabric.Instance.GetVoiceConverter(SourceObject.Media).Convert();

            return voiceMedia;
        }
    }
}
