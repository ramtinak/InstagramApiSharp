namespace InstagramApiSharp.Classes.Models
{
    public class InstaCollectionItem
    {
        public long CollectionId { get; set; }

        public string CollectionName { get; set; }

        public bool HasRelatedMedia { get; set; }

        public InstaMediaList Media { get; set; }

        public InstaCoverMedia CoverMedia { get; set; }

        public string NextMaxId { get; set; }
    }
}