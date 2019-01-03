using System;
using System.Collections.Generic;
using System.Text;

namespace InstagramApiSharp.Classes.Models
{
    public class InstaDiscoverTopSearches
    {
        public string RankToken { get; set; }

        public List<InstaDiscoverSearches> TopResults { get; set; } = new List<InstaDiscoverSearches>();
    }
}
