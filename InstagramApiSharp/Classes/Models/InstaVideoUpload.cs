using System;
using System.Collections.Generic;
using System.Text;

namespace InstagramApiSharp.Classes.Models
{
    public class InstaVideoUpload
    {
        public InstaVideoUpload() { }
        public InstaVideoUpload(InstaVideo video, InstaImage videoThumbnail)
        {
            Video = video;
            VideoThumbnail = videoThumbnail;
        }
        public InstaVideo Video { get; /*private*/ set; }
        public InstaImage VideoThumbnail { get; /*private*/ set; }
    }
}
