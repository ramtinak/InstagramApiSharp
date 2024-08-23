/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ RamtinJokar@outlook.com ]
 * 
 * IRANIAN DEVELOPERS
 */

using InstagramApiSharp.Classes.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace InstagramApiSharp.Classes.ResponseWrappers
{
    public class InstaReelsMediaListResponse : InstaDefaultResponse
    {
        [JsonProperty("items")]
        public List<InstaMediaAlbumResponse> Medias { get; set; } = new List<InstaMediaAlbumResponse>();
        [JsonProperty("paging_info")]
        public InstaPagingInfoResponse PagingInfo { get; set; }
    }
}
