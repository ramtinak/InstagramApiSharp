using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace InstagramApiSharp.Classes.Models
{
    public class InstaMedia : INotifyPropertyChanged
    {
        public DateTime TakenAt { get; set; }
        public string Pk { get; set; }

        public string InstaIdentifier { get; set; }

        public DateTime DeviceTimeStamp { get; set; }
        public InstaMediaType MediaType { get; set; }

        public string Code { get; set; }

        public string ClientCacheKey { get; set; }
        public string FilterType { get; set; }

        public List<InstaImage> Images { get; set; } = new List<InstaImage>();
        public List<InstaVideo> Videos { get; set; } = new List<InstaVideo>();

        public int Width { get; set; }
        public string Height { get; set; }

        public InstaUser User { get; set; }

        public string TrackingToken { get; set; }

        private int _likecount;
        public int LikesCount { get { return _likecount; } set { _likecount = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("LikesCount")); } }

        public string NextMaxId { get; set; }

        public InstaCaption Caption { get; set; }

        public string CommentsCount { get; set; }

        public bool PhotoOfYou { get; set; }

        private bool _hasliked { get; set; }
        public bool HasLiked { get { return _hasliked; } set { _hasliked = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("HasLiked")); } }

        public List<InstaUserTag> Tags { get; set; } = new List<InstaUserTag>();

        public InstaUserShortList Likers { get; set; } = new InstaUserShortList();
        public InstaCarousel Carousel { get; set; }

        public int ViewCount { get; set; }

        public bool HasAudio { get; set; }

        public bool IsMultiPost => Carousel != null;
        public List<InstaComment> PreviewComments { get; set; } = new List<InstaComment>();
        public InstaLocation Location { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private bool _play = false;
        public bool Play { get { return _play; } set { _play = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Play")); } }
    }
}