using System;
using System.Collections.Generic;
using System.Text;

namespace InstagramApiSharp.Classes.Models
{
    public class InstaBrandedContent
    {
        public bool RequireApproval { get; set; }

        public List<InstaUserShort> WhitelistedUsers { get; set; } = new List<InstaUserShort>();
    }
}
