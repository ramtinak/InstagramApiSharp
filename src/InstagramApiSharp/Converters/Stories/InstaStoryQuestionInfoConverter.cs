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
    internal class InstaStoryQuestionInfoConverter : IObjectConverter<InstaStoryQuestionInfo, InstaStoryQuestionInfoResponse>
    {
        public InstaStoryQuestionInfoResponse SourceObject { get; set; }

        public InstaStoryQuestionInfo Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException($"Source object");

            var questionInfo = new InstaStoryQuestionInfo
            {
                BackgroundColor = SourceObject.BackgroundColor,
                LatestQuestionResponseTime = DateTimeHelper.FromUnixTimeSeconds(SourceObject.LatestQuestionResponseTime ?? 
                DateTime.UtcNow.ToUnixTime()),
                MaxId = SourceObject.MaxId,
                MoreAvailable = SourceObject.MoreAvailable ?? false,
                Question = SourceObject.Question,
                QuestionId = SourceObject.QuestionId,
                QuestionResponseCount = SourceObject.QuestionResponseCount ?? 0,
                QuestionType = SourceObject.QuestionType,
                TextColor = SourceObject.TextColor
            };

            if (SourceObject.Responders?.Count > 0)
                foreach (var responder in SourceObject.Responders)
                    questionInfo.Responders.Add(ConvertersFabric.Instance
                        .GetStoryQuestionResponderConverter(responder).Convert());

            return questionInfo;
        }
    }
}
