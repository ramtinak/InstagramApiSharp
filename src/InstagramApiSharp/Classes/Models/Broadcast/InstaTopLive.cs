using System.Collections.Generic;

namespace InstagramApiSharp.Classes.Models
{
    public class InstaTopLive
    {
        public int RankedPosition { get; set; }

        public List<InstaUserShort> BroadcastOwners { get; set; } = new List<InstaUserShort>();
    }
}