/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ RamtinJokar@outlook.com ]
 * 
 * IRANIAN DEVELOPERS
 */

using System.Collections.Generic;

namespace InstagramApiSharp.Classes.Models
{
    public class InstaReelsMediaList : IInstaBaseList
    {
        public List<InstaMedia> Medias { get; set; } = new List<InstaMedia>();
        public bool MoreAvailable { get; set; }
        public string NextMaxId { get; set; }
    }
}
