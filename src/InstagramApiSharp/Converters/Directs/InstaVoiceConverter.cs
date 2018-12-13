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
using System.Linq;

namespace InstagramApiSharp.Converters
{
    internal class InstaVoiceConverter : IObjectConverter<InstaVoice, InstaVoiceResponse>
    {
        public InstaVoiceResponse SourceObject { get; set; }

        public InstaVoice Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException($"Source object");

            var voice = new InstaVoice
            {
               Audio = ConvertersFabric.Instance.GetAudioConverter(SourceObject.Audio).Convert(),
               Id = SourceObject.Id,
               MediaType = SourceObject.MediaType,
               OrganicTrackingToken = SourceObject.OrganicTrackingToken,
               ProductType = SourceObject.ProductType
            };
            if (SourceObject.User != null)
                voice.FriendshipStatus = ConvertersFabric.Instance.GetFriendShipStatusConverter(SourceObject.User.FriendshipStatus).Convert();

            return voice;
        }
    }
}
