using System;
using System.Collections.Generic;
using System.Text;

namespace InstagramApiSharp.Classes.Models.Hashtags
{
    public class InstaHashtagMedia
    {
        public List<InstaMedia> Medias { get; set; } = new List<InstaMedia>();

        public bool MoreAvailable { get; set; }

        public string NextMaxId { get; set; }

        public int NextPage { get; set; }

        public List<long> NextMediaIds { get; set; } = new List<long>();

        public bool AutoLoadMoreEnabled { get; set; }
    }

    /*public class InstaHashtagMedia
    {
        public string LayoutType { get; set; }

        public List<InstaMedia> Medias { get; set; } = new List<InstaMedia>();

        public string FeedType { get; set; }

        public InstaHashtagMediaExploreItemInfo ExploreItemInfo { get; set; }
    }
    public class InstaHashtagMediaExploreItemInfo
    {
        public int NumBolumns { get; set; }

        public int TotalNumBolumns { get; set; }

        public int AspectYatio { get; set; }

        public bool Autoplay { get; set; }
    }*/
}
