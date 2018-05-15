using System.Collections.Generic;

namespace InstagramApiSharp.Classes.Models
{
    public class InstaUserShortList : List<InstaUserShort>, IInstaBaseList
    {
        public string NextId { get; set; }
    }
}