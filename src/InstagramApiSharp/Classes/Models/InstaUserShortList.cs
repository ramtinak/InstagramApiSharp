using System.Collections.Generic;

namespace InstagramApiSharp.Classes.Models
{
    public class InstaUserShortList : List<InstaUserShort>, IInstaBaseList
    {
        public string NextMaxId { get; set; }
    }
}