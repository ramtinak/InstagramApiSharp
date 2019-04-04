/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */


using System.Collections.Generic;

namespace InstagramApiSharp.Classes.Models
{
    public class InstaTopicalExploreFeed
    {
        public List<InstaTopicalExploreCluster> Clusters { get; set; } = new List<InstaTopicalExploreCluster>();

        public InstaMediaList Medias { get; set; } = new InstaMediaList();

        public string NextMaxId { get; set; }

        public List<InstaTVChannel> TVChannels { get; set; } = new List<InstaTVChannel>();

        public InstaChannel Channel { get; set; } = new InstaChannel();

        public string MaxId { get; set; }

        public string RankToken { get; set; }

        public bool MoreAvailable { get; set; }

        public int ResultsCount { get; set; }

        public bool AutoLoadMoreEnabled { get; set; }

        public bool HasShoppingChannelContent { get; set; }
    }
}
