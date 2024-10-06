/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ RamtinJokar@outlook.com ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Classes.ResponseWrappers;
using InstagramApiSharp.Enums;
using System;

namespace InstagramApiSharp.Converters
{
    internal class InstaStoryLinkStickerItemConverter : IObjectConverter<InstaStoryLinkStickerItem, InstaStoryLinkStickerItemResponse>
    {
        public InstaStoryLinkStickerItemResponse SourceObject { get; set; }

        public InstaStoryLinkStickerItem Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException($"{GetType().FullName} Source object");
            var quiz = new InstaStoryLinkStickerItem
            {
                Height = SourceObject.Height,
                IsHidden = System.Convert.ToBoolean(SourceObject.IsHidden),
                IsPinned = System.Convert.ToBoolean(SourceObject.IsPinned),
                IsSticker = System.Convert.ToBoolean(SourceObject.IsSticker),
                IsFbSticker = System.Convert.ToBoolean(SourceObject.IsFbSticker),
                Rotation = SourceObject.Rotation,
                Width = SourceObject.Width,
                X = SourceObject.X,
                Y = SourceObject.Y,
                Z = SourceObject.Z
            };
            if (SourceObject.StoryLink != null)
            {
                InstaStoryLinkType type = default;
                if (SourceObject.StoryLink.LinkType.IsNotEmpty())
                {
                    type = (InstaStoryLinkType)Enum.Parse(typeof(InstaStoryLinkType),
                     SourceObject.StoryLink.LinkType.Replace(" ", "").Replace("_", ""), true);
                }
                quiz.StoryLink = new InstaStoryLink
                {
                    LinkTitle = SourceObject.StoryLink.LinkTitle,
                    LinkType = type,
                    OriginalLinkType = SourceObject.StoryLink.LinkType,
                    Url = SourceObject.StoryLink.Url
                };
            }
            return quiz;
        }
    }
}
