using System.Collections.Generic;

namespace InstagramApiSharp.Classes.Models
{
    public class InstaMediaList : List<InstaMedia>, IInstaBaseList
    {
        public int Pages { get; set; } = 0;
        public int PageSize { get; set; } = 0;
        public string NextMaxId { get; set; }
    }
}