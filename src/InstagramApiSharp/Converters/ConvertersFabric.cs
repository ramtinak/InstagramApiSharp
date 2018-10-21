using System;
using InstagramApiSharp.Classes;
using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Classes.Models.Business;
using InstagramApiSharp.Classes.Models.Hashtags;
using InstagramApiSharp.Classes.ResponseWrappers;
using InstagramApiSharp.Classes.ResponseWrappers.Business;
using InstagramApiSharp.Converters.Business;
using InstagramApiSharp.Converters.Hashtags;
using InstagramApiSharp.Converters.Users;

namespace InstagramApiSharp.Converters
{
    internal class ConvertersFabric
    {
        private static readonly Lazy<ConvertersFabric> LazyInstance =
            new Lazy<ConvertersFabric>(() => new ConvertersFabric());

        public static ConvertersFabric Instance => LazyInstance.Value;

        public IObjectConverter<InstaUserShort, InstaUserShortResponse> GetUserShortConverter(
            InstaUserShortResponse instaresponse)
        {
            return new InstaUserShortConverter {SourceObject = instaresponse};
        }

        public IObjectConverter<InstaCurrentUser, InstaCurrentUserResponse> GetCurrentUserConverter(
            InstaCurrentUserResponse instaresponse)
        {
            return new InstaCurrentUserConverter {SourceObject = instaresponse};
        }

        public IObjectConverter<InstaUser, InstaUserResponse> GetUserConverter(InstaUserResponse instaresponse)
        {
            return new InstaUserConverter {SourceObject = instaresponse};
        }

        public IObjectConverter<InstaMedia, InstaMediaItemResponse> GetSingleMediaConverter(
            InstaMediaItemResponse responseMedia)
        {
            return new InstaMediaConverter {SourceObject = responseMedia};
        }
        public IObjectConverter<InstaMedia, InstaMediaAlbumResponse> GetSingleMediaFromAlbumConverter(
            InstaMediaAlbumResponse responseMedia)
        {
            return new InstaMediaAlbumConverter { SourceObject = responseMedia };
        }
        public IObjectConverter<InstaFeed, InstaFeedResponse> GetFeedConverter(
            InstaFeedResponse feedResponse)
        {
            return new InstaFeedConverter {SourceObject = feedResponse};
        }

        public IObjectConverter<InstaTagFeed, InstaTagFeedResponse> GetTagFeedConverter(
            InstaTagFeedResponse feedResponse)
        {
            return new InstaTagFeedConverter {SourceObject = feedResponse};
        }

        public IObjectConverter<InstaMediaList, InstaMediaListResponse> GetMediaListConverter(
            InstaMediaListResponse mediaResponse)
        {
            return new InstaMediaListConverter {SourceObject = mediaResponse};
        }

        public IObjectConverter<InstaCaption, InstaCaptionResponse> GetCaptionConverter(
            InstaCaptionResponse captionResponse)
        {
            return new InstaCaptionConverter {SourceObject = captionResponse};
        }

        public IObjectConverter<InstaFriendshipStatus, InstaFriendshipStatusResponse>
            GetFriendShipStatusConverter(InstaFriendshipStatusResponse friendshipStatusResponse)
        {
            return new InstaFriendshipStatusConverter {SourceObject = friendshipStatusResponse};
        }

        public IObjectConverter<InstaStory, InstaStoryResponse> GetSingleStoryConverter(
            InstaStoryResponse storyResponse)
        {
            return new InstaStoryConverter {SourceObject = storyResponse};
        }

        public IObjectConverter<InstaUserTag, InstaUserTagResponse> GetUserTagConverter(InstaUserTagResponse tag)
        {
            return new InstaUserTagConverter {SourceObject = tag};
        }

        public IObjectConverter<InstaDirectInboxContainer, InstaDirectInboxContainerResponse>
            GetDirectInboxConverter(InstaDirectInboxContainerResponse inbox)
        {
            return new InstaDirectInboxConverter {SourceObject = inbox};
        }

        public IObjectConverter<InstaDirectInboxThread, InstaDirectInboxThreadResponse> GetDirectThreadConverter(
            InstaDirectInboxThreadResponse thread)
        {
            return new InstaDirectThreadConverter {SourceObject = thread};
        }

        public IObjectConverter<InstaDirectInboxItem, InstaDirectInboxItemResponse> GetDirectThreadItemConverter(
            InstaDirectInboxItemResponse threadItem)
        {
            return new InstaDirectThreadItemConverter {SourceObject = threadItem};
        }

        public IObjectConverter<InstaDirectInboxSubscription, InstaDirectInboxSubscriptionResponse>
            GetDirectSubscriptionConverter(InstaDirectInboxSubscriptionResponse subscription)
        {
            return new InstaDirectInboxSubscriptionConverter {SourceObject = subscription};
        }

        public IObjectConverter<InstaRecentActivityFeed, InstaRecentActivityFeedResponse>
            GetSingleRecentActivityConverter(InstaRecentActivityFeedResponse feedResponse)
        {
            return new InstaRecentActivityConverter {SourceObject = feedResponse};
        }

        public IObjectConverter<InstaRecipients, IInstaRecipientsResponse> GetRecipientsConverter(
            IInstaRecipientsResponse recipients)
        {
            return new InstaRecipientsConverter {SourceObject = recipients};
        }

        public IObjectConverter<InstaComment, InstaCommentResponse> GetCommentConverter(
            InstaCommentResponse comment)
        {
            return new InstaCommentConverter {SourceObject = comment};
        }

        public IObjectConverter<InstaCommentList, InstaCommentListResponse> GetCommentListConverter(
            InstaCommentListResponse commentList)
        {
            return new InstaCommentListConverter {SourceObject = commentList};
        }

        public IObjectConverter<InstaCarousel, InstaCarouselResponse> GetCarouselConverter(
            InstaCarouselResponse carousel)
        {
            return new InstaCarouselConverter {SourceObject = carousel};
        }

        public IObjectConverter<InstaCarouselItem, InstaCarouselItemResponse> GetCarouselItemConverter(
            InstaCarouselItemResponse carouselItem)
        {
            return new InstaCarouselItemConverter {SourceObject = carouselItem};
        }

        public IObjectConverter<InstaStoryItem, InstaStoryItemResponse> GetStoryItemConverter(
            InstaStoryItemResponse storyItem)
        {
            return new InstaStoryItemConverter {SourceObject = storyItem};
        }

        public IObjectConverter<InstaStoryFeedMedia, InstaStoryFeedMediaResponse> GetStoryFeedMediaConverter(
            InstaStoryFeedMediaResponse storyItem)
        {
            return new InstaStoryFeedMediaConverter { SourceObject = storyItem };
        }

        public IObjectConverter<InstaStory, InstaStoryResponse> GetStoryConverter(InstaStoryResponse storyItem)
        {
            return new InstaStoryConverter {SourceObject = storyItem};
        }

        public IObjectConverter<InstaStoryTray, InstaStoryTrayResponse> GetStoryTrayConverter(
            InstaStoryTrayResponse storyTray)
        {
            return new InstaStoryTrayConverter {SourceObject = storyTray};
        }

        public IObjectConverter<InstaStoryMedia, InstaStoryMediaResponse> GetStoryMediaConverter(
            InstaStoryMediaResponse storyMedia)
        {
            return new InstaStoryMediaConverter {SourceObject = storyMedia};
        }

        public IObjectConverter<InstaStoryCTA, InstaStoryCTAResponse> GetStoryCtaConverter(
            InstaStoryCTAResponse storyCta)
        {
            return new InstaStoryCTAConverter { SourceObject = storyCta };
        }

        public IObjectConverter<InstaImage, ImageResponse> GetImageConverter(ImageResponse imageResponse)
        {
            return new InstaMediaImageConverter {SourceObject = imageResponse};
        }

        public IObjectConverter<InstaExploreFeed, InstaExploreFeedResponse> GetExploreFeedConverter(
            InstaExploreFeedResponse feedResponse)
        {
            return new InstaExploreFeedConverter {SourceObject = feedResponse};
        }

        public IObjectConverter<InstaChannel, InstaChannelResponse> GetChannelConverter(
            InstaChannelResponse response)
        {
            return new InstaChannelConverter {SourceObject = response};
        }

        public IObjectConverter<InstaTopLive, InstaTopLiveResponse> GetTopLiveConverter(
            InstaTopLiveResponse response)
        {
            return new InstaTopLiveConverter {SourceObject = response};
        }

        public IObjectConverter<InstaReelFeed, InstaReelFeedResponse> GetReelFeedConverter(
            InstaReelFeedResponse response)
        {
            return new InstaReelFeedConverter {SourceObject = response};
        }

        public IObjectConverter<InstaReelShare, InstaReelShareResponse> GetReelShareConverter(
            InstaReelShareResponse response)
        {
            return new InstaReelShareConverter { SourceObject = response };
        }

        public IObjectConverter<InstaReelMention, InstaReelMentionResponse> GetMentionConverter(
            InstaReelMentionResponse response)
        {
            return new InstaReelMentionConverter {SourceObject = response};
        }

        public IObjectConverter<InstaLocation, InstaLocationResponse> GetLocationConverter(
            InstaLocationResponse response)
        {
            return new InstaLocationConverter {SourceObject = response};
        }

        public IObjectConverter<InstaHashtagSearch, InstaHashtagSearchResponse> GetHashTagsSearchConverter(
            InstaHashtagSearchResponse response)
        {
            return new InstaHashtagSearchConverter {SourceObject = response};
        }

        public IObjectConverter<InstaHashtag, InstaHashtagResponse> GetHashTagConverter(
            InstaHashtagResponse response)
        {
            return new InstaHashtagConverter {SourceObject = response};
        }

        public IObjectConverter<InstaStoryFeed, InstaStoryFeedResponse> GetStoryFeedConverter(
            InstaStoryFeedResponse response)
        {
            return new InstaStoryFeedConverter {SourceObject = response};
        }
        public IObjectConverter<InstaHighlightFeeds, InstaHighlightFeedsResponse> GetHighlightFeedsConverter(
            InstaHighlightFeedsResponse response)
        {
            return new InstaHighlightConverter { SourceObject = response };
        }
        public IObjectConverter<InstaHighlightSingleFeed, InstaHighlightReelResponse> GetHighlightReelConverter(
            InstaHighlightReelResponse response)
        {
            return new InstaHighlightReelConverter { SourceObject = response };
        }
        public IObjectConverter<InstaCollectionItem, InstaCollectionItemResponse> GetCollectionConverter(
            InstaCollectionItemResponse response)
        {
            return new InstaCollectionConverter {SourceObject = response};
        }

        public IObjectConverter<InstaCollections, InstaCollectionsResponse> GetCollectionsConverter(
            InstaCollectionsResponse response)
        {
            return new InstaCollectionsConverter {SourceObject = response};
        }

        public IObjectConverter<InstaCoverMedia, InstaCoverMediaResponse> GetCoverMediaConverter(
            InstaCoverMediaResponse response)
        {
            return new InstaCoverMediaConverter {SourceObject = response};
        }

        public IObjectConverter<InstaInboxMedia, InstaInboxMediaResponse> GetInboxMediaConverter(
            InstaInboxMediaResponse response)
        {
            return new InstaInboxMediaConverter {SourceObject = response};
        }

        public IObjectConverter<InstaLocationShortList, InstaLocationSearchResponse> GetLocationsSearchConverter(
            InstaLocationSearchResponse response)
        {
            return new InstaLocationSearchConverter {SourceObject = response};
        }

        public IObjectConverter<InstaLocationShort, InstaLocationShortResponse> GetLocationShortConverter(
            InstaLocationShortResponse response)
        {
            return new InstaLocationShortConverter {SourceObject = response};
        }

        public IObjectConverter<InstaLocationFeed, InstaLocationFeedResponse> GetLocationFeedConverter(
            InstaLocationFeedResponse response)
        {
            return new InstaLocationFeedConverter {SourceObject = response};
        }

        public IObjectConverter<InstaUserInfo, InstaUserInfoContainerResponse> GetUserInfoConverter(
            InstaUserInfoContainerResponse response)
        {
            return new InstaUserInfoConverter {SourceObject = response};
        }

        public IObjectConverter<InstaInlineCommentList, InstaInlineCommentListResponse> GetInlineCommentsConverter(
            InstaInlineCommentListResponse response)
        {
            return new InstaInlineCommentListConverter { SourceObject = response };
        }

        public IObjectConverter<InstaFullUserInfo, InstaFullUserInfoResponse> GetFullUserInfoConverter(
            InstaFullUserInfoResponse response)
        {
            return new InstaFullUserInfoConverter { SourceObject = response };
        }
        public IObjectConverter<InstaCommentShort, InstaCommentShortResponse> GetCommentShortConverter(
            InstaCommentShortResponse response)
        {
            return new InstaCommentShortConverter { SourceObject = response };
        }

        public IObjectConverter<InstaSuggestionItem, InstaSuggestionItemResponse> GetSuggestionItemConverter(
            InstaSuggestionItemResponse response)
        {
            return new InstaSuggestionItemConverter { SourceObject = response };
        }

        public IObjectConverter<InstaSuggestionItemList, InstaSuggestionItemListResponse> GetSuggestionItemListConverter(
            InstaSuggestionItemListResponse response)
        {
            return new InstaSuggestionItemListConverter { SourceObject = response };
        }

        public IObjectConverter<InstaSuggestions, InstaSuggestionUserContainerResponse> GetSuggestionsConverter(
            InstaSuggestionUserContainerResponse response)
        {
            return new InstaSuggestionsConverter { SourceObject = response };
        }

        public IObjectConverter<InstaStatistics, InstaStatisticsRootResponse> GetStatisticsConverter(
            InstaStatisticsRootResponse response)
        {
            return new InstaStatisticsConverter { SourceObject = response };
        }

        public IObjectConverter<InstaMediaShort, InstaMediaShortResponse> GetMediaShortConverter(
            InstaMediaShortResponse response)
        {
            return new InstaMediaShortConverter { SourceObject = response };
        }

        public IObjectConverter<InstaStatisticsDataPointItem, InstaStatisticsDataPointItemResponse> GetStatisticsDataPointConverter(
            InstaStatisticsDataPointItemResponse response)
        {
            return new InstaStatisticsDataPointConverter { SourceObject = response };
        }

        public IObjectConverter<InstaInsightsDataNode, InstaInsightsDataNodeResponse> GetInsightsDataNodeConverter(
            InstaInsightsDataNodeResponse response)
        {
            return new InstaInsightsDataNodeConverter { SourceObject = response };
        }

        public IObjectConverter<InstaFullMediaInsights, InstaFullMediaInsightsResponse> GetFullMediaInsightsConverter(
            InstaFullMediaInsightsResponse response)
        {
            return new InstaFullMediaInsightsConverter { SourceObject = response };
        }

        public IObjectConverter<InstaBusinessUser, InstaBusinessUserContainerResponse> GetBusinessUserConverter(
            InstaBusinessUserContainerResponse response)
        {
            return new InstaBusinessUserConverter { SourceObject = response };
        }

        public IObjectConverter<InstaHashtagOwner, InstaHashtagOwnerResponse> GetHashtagOwnerConverter(
            InstaHashtagOwnerResponse response)
        {
            return new InstaHashtagOwnerConverter { SourceObject = response };
        }

        public IObjectConverter<InstaHashtagStory, InstaHashtagStoryContainerResponse> GetHashtagStoryConverter(
            InstaHashtagStoryContainerResponse response)
        {
            return new InstaHashtagStoryConverter { SourceObject = response };
        }

        public IObjectConverter<InstaHashtagMedia, InstaHashtagMediaListResponse> GetHashtagMediaListConverter(
            InstaHashtagMediaListResponse response)
        {
            return new InstaHashtagMediaConverter { SourceObject = response };
        }

        public IObjectConverter<InstaUserContact, InstaUserContactResponse> GetSingleUserContactConverter(
            InstaUserContactResponse response)
        {
            return new InstaUserContactConverter { SourceObject = response };
        }

        public IObjectConverter<InstaContactUserList, InstaContactUserListResponse> GetUserContactListConverter(
            InstaContactUserListResponse response)
        {
            return new InstaUserContactListConverter { SourceObject = response };
        }

        public IObjectConverter<InstaUserPresence, InstaUserPresenceResponse> GetSingleUserPresenceConverter(
            InstaUserPresenceResponse response)
        {
            return new InstaSingleUserPresenceConverter { SourceObject = response };
        }

        public IObjectConverter<InstaUserPresenceList, InstaUserPresenceContainerResponse> GetUserPresenceListConverter(
            InstaUserPresenceContainerResponse response)
        {
            return new InstaUserPresenceListConverter { SourceObject = response };
        }

        public IObjectConverter<InstaDiscoverSearches, InstaDiscoverSearchesResponse> GetDiscoverSearchesConverter(
            InstaDiscoverSearchesResponse response)
        {
            return new InstaDiscoverSearchesConverter { SourceObject = response };
        }

        public IObjectConverter<InstaDiscoverRecentSearches, InstaDiscoverRecentSearchesResponse> GetDiscoverRecentSearchesConverter(
            InstaDiscoverRecentSearchesResponse response)
        {
            return new InstaDiscoverRecentSearchesConverter { SourceObject = response };
        }

        public IObjectConverter<InstaDiscoverSuggestedSearches, InstaDiscoverSuggestedSearchesResponse> GetDiscoverSuggestedSearchesConverter(
            InstaDiscoverSuggestedSearchesResponse response)
        {
            return new InstaDiscoverSuggestedSearchesConverter { SourceObject = response };
        }

        public IObjectConverter<InstaDiscoverSearchResult, InstaDiscoverSearchResultResponse> GetDiscoverSearchResultConverter(
            InstaDiscoverSearchResultResponse response)
        {
            return new InstaDiscoverSearchResultConverter { SourceObject = response };
        }

        public IObjectConverter<InstaFriendshipShortStatusList, InstaFriendshipShortStatusListResponse> GetFriendshipShortStatusListConverter(
            InstaFriendshipShortStatusListResponse response)
        {
            return new InstaFriendshipShortStatusListConverter { SourceObject = response };
        }

        public IObjectConverter<InstaUserChaining, InstaUserChainingResponse> GetSingleUserChainingConverter(
            InstaUserChainingResponse response)
        {
            return new InstaUserChainingConverter { SourceObject = response };
        }

        public IObjectConverter<InstaUserChainingList, InstaUserChainingContainerResponse> GetUserChainingListConverter(
            InstaUserChainingContainerResponse response)
        {
            return new InstaUserChainingListConverter { SourceObject = response };
        }

        public IObjectConverter<InstaHighlightShort, InstaHighlightShortResponse> GetSingleHighlightShortConverter(
            InstaHighlightShortResponse response)
        {
            return new InstaHighlightShortConverter { SourceObject = response };
        }

        public IObjectConverter<InstaHighlightShortList, InstaHighlightShortListResponse> GetHighlightShortListConverter(
            InstaHighlightShortListResponse response)
        {
            return new InstaHighlightShortListConverter { SourceObject = response };
        }

        public IObjectConverter<InstaBrandedContent, InstaBrandedContentResponse> GetBrandedContentConverter(
            InstaBrandedContentResponse response)
        {
            return new InstaBrandedContentConverter { SourceObject = response };
        }

        public IObjectConverter<InstaTranslate, InstaTranslateResponse> GetSingleTranslateConverter(
            InstaTranslateResponse response)
        {
            return new InstaTranslateConverter { SourceObject = response };
        }

        public IObjectConverter<InstaTranslateList, InstaTranslateContainerResponse> GetTranslateContainerConverter(
            InstaTranslateContainerResponse response)
        {
            return new InstaTranslateContainerConverter { SourceObject = response };
        }

        public IObjectConverter<InstaPlace, InstaPlaceResponse> GetPlaceConverter(
            InstaPlaceResponse response)
        {
            return new InstaPlaceConverter { SourceObject = response };
        }
    }
}