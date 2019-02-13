﻿using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace InstagramApiSharp.Classes.Models
{
    public class InstaMedia : INotifyPropertyChanged
    {
        public long CommentsCount { get; set; }
        public string InlineComposerDisplayCondition { get; set; }
        public int InlineComposerImpTriggerTime { get; set; }
        public string MainFeedCarouselStartingMediaId { get; set; }
        public bool? MainFeedCarouselHasUnseenCoverMedia { get; set; }
        public bool? ForceOverlay { get; set; }
        public bool? HideNuxText { get; set; }
        public string OverlayText { get; set; }
        public string OverlayTitle { get; set; }
        public string OverlaySubtitle { get; set; }
        public string DominantColor { get; set; }
        public int? FollowerCount { get; set; }
        public int? PostCount { get; set; }
        public string FBPageUrl { get; set; }
        public int? ExpiringAt { get; set; }
        public int? CarouselMediaCount { get; set; }
        public string Link { get; set; }
        public string LinkText { get; set; }
        public string AdAction { get; set; }
        public string LinkHintText { get; set; }
        public object iTunesItem { get; set; }
        public int? AdLinkType { get; set; }
        public int? AdHeaderStyle { get; set; }
        public int? DrAdType { get; set; }
        public string OrganicTrackingToken { get; set; }
        public string Preview { get; set; }
        public string InventorySource { get; set; }
        public bool IsSeen { get; set; }
        public bool IsEof { get; set; }
        public bool? CollapseComments { get; set; }
        public long TakenAtUnix { get; set; }
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

      //  private string _cmcount;
     //   public string CommentsCount { get => _cmcount; set { _cmcount = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CommentsCount")); } }

        public bool IsCommentsDisabled { get; set; }

        public bool PhotoOfYou { get; set; }

        private bool _hasliked { get; set; }
        public bool HasLiked { get { return _hasliked; } set { _hasliked = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("HasLiked")); } }

        public List<InstaUserTag> UserTags { get; set; } = new List<InstaUserTag>();

        public InstaUserShortList Likers { get; set; } = new InstaUserShortList();
        public InstaCarousel Carousel { get; set; }

        public int ViewCount { get; set; }

        public bool HasAudio { get; set; }

        public bool IsMultiPost => Carousel != null;
        public List<InstaComment> PreviewComments { get; set; } = new List<InstaComment>();
        public InstaLocation Location { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private bool _play = false;
        /// <summary>
        /// This property is for developer's personal use. 
        /// </summary>
        public bool Play { get { return _play; } set { _play = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Play")); } }


        public bool CommentLikesEnabled { get; set; }

        public bool CommentThreadingEnabled { get; set; }

        public bool HasMoreComments { get; set; }

        public int MaxNumVisiblePreviewComments { get; set; }

        public bool CanViewMorePreviewComments { get; set; }

        public bool CanViewerReshare { get; set; }

        public bool CaptionIsEdited { get; set; }

        public bool CanViewerSave { get; set; }

        private bool _hasviewersaved;
        public bool HasViewerSaved { get => _hasviewersaved; set { _hasviewersaved = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("HasViewerSaved")); } }

        public string Title { get; set; }

        public string ProductType { get; set; }

        public bool NearlyCompleteCopyrightMatch { get; set; }

        public int NumberOfQualities { get; set; }

        public double VideoDuration { get; set; }

        public List<InstaProductTag> ProductTags { get; set; } = new List<InstaProductTag>();
    }
}