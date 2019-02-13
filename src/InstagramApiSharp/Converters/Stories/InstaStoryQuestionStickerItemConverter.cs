using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Classes.ResponseWrappers;
using System;

namespace InstagramApiSharp.Converters
{
    internal class InstaStoryQuestionStickerItemConverter : IObjectConverter<InstaStoryQuestionStickerItem, InstaStoryQuestionStickerItemResponse>
    {
        public InstaStoryQuestionStickerItemResponse SourceObject { get; set; }

        public InstaStoryQuestionStickerItem Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException($"Source object");
            return new InstaStoryQuestionStickerItem
            {
                BackgroundColor = SourceObject.BackgroundColor,
                ProfilePicUrl = SourceObject.ProfilePicUrl,
                Question = SourceObject.Question,
                QuestionId = SourceObject.QuestionId,
                QuestionType = SourceObject.QuestionType,
                TextColor = SourceObject.TextColor,
                ViewerCanInteract = SourceObject.ViewerCanInteract
            };

        }
    }
}
