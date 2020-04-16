using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstagramApiSharp.Classes.Models
{
    public class InstaAccountProfilPicUpload
    {
        [JsonProperty("upload_id")]
        public string UploadId { get; set; }

        [JsonProperty("xsharing_nonces")]
        public XsharingNonces XsharingNonces { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }

    public class XsharingNonces
    {
    }
}
