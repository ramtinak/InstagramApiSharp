using System.Collections.Generic;

namespace InstagramApiSharp.Classes.Models
{
    public class InstaFeed : IInstaBaseList
    {
        public int MediaItemsCount => Medias.Count;
        public int StoriesItemsCount => Stories.Count;

        public List<InstaMedia> Medias { get; set; } = new List<InstaMedia>();
        public List<InstaStory> Stories { get; set; } = new List<InstaStory>();
        public string NextMaxId { get; set; }

        public List<InstaSuggestionItem> SuggestedUserItems { get; set; } = new List<InstaSuggestionItem>();
    }
}