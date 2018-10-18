using System.Collections.Generic;

namespace InstagramApiSharp.Classes.Models
{
    public class InstaHashtagSearch : List<InstaHashtag>
    {
        public bool MoreAvailable { get; set; }

        public string RankToken { get; set; }
    }
}
