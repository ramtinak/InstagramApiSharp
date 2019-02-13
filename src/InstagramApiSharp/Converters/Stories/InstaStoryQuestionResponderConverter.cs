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
using InstagramApiSharp.Helpers;
using System;

namespace InstagramApiSharp.Converters
{
    internal class InstaStoryQuestionResponderConverter : IObjectConverter<InstaStoryQuestionResponder, InstaStoryQuestionResponderResponse>
    {
        public InstaStoryQuestionResponderResponse SourceObject { get; set; }

        public InstaStoryQuestionResponder Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException($"Source object");

            var responder = new InstaStoryQuestionResponder
            {
                HasSharedResponse = SourceObject.HasSharedResponse ?? false,
                Id = SourceObject.Id,
                ResponseText = SourceObject.Response,
                Time = DateTimeHelper.FromUnixTimeSeconds(SourceObject.Ts ?? DateTime.UtcNow.ToUnixTime())
            };

            if (SourceObject.User != null)
                responder.User = ConvertersFabric.Instance.GetUserShortConverter(SourceObject.User).Convert();

            return responder;
        }
    }
}
