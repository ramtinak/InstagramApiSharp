using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Classes.ResponseWrappers;

namespace InstagramApiSharp.Converters
{
    public interface IConvertersFabric
    {
        IObjectConverter<InstaUserShort, InstaUserShortResponse> GetUserShortConverter(
            InstaUserShortResponse instaresponse);

        IObjectConverter<InstaCurrentUser, InstaCurrentUserResponse> GetCurrentUserConverter(
            InstaCurrentUserResponse instaresponse);

        IObjectConverter<InstaUser, InstaUserResponse> GetUserConverter(InstaUserResponse instaresponse);

        IObjectConverter<InstaMedia, InstaMediaItemResponse> GetSingleMediaConverter(
            InstaMediaItemResponse responseMedia);

        IObjectConverter<InstaFeed, InstaFeedResponse> GetFeedConverter(
            InstaFeedResponse feedResponse);

        IObjectConverter<InstaTagFeed, InstaTagFeedResponse> GetTagFeedConverter(
            InstaTagFeedResponse feedResponse);

        IObjectConverter<InstaMediaList, InstaMediaListResponse> GetMediaListConverter(
            InstaMediaListResponse mediaResponse);

        IObjectConverter<InstaCaption, InstaCaptionResponse> GetCaptionConverter(
            InstaCaptionResponse captionResponse);

        IObjectConverter<InstaFriendshipStatus, InstaFriendshipStatusResponse>
            GetFriendShipStatusConverter(InstaFriendshipStatusResponse friendshipStatusResponse);

        IObjectConverter<InstaStory, InstaStoryResponse> GetSingleStoryConverter(
            InstaStoryResponse storyResponse);

        IObjectConverter<InstaUserTag, InstaUserTagResponse> GetUserTagConverter(InstaUserTagResponse tag);

        IObjectConverter<InstaDirectInboxContainer, InstaDirectInboxContainerResponse>
            GetDirectInboxConverter(InstaDirectInboxContainerResponse inbox);

        IObjectConverter<InstaDirectInboxThread, InstaDirectInboxThreadResponse> GetDirectThreadConverter(
            InstaDirectInboxThreadResponse thread);

        IObjectConverter<InstaDirectInboxItem, InstaDirectInboxItemResponse> GetDirectThreadItemConverter(
            InstaDirectInboxItemResponse threadItem);

        IObjectConverter<InstaDirectInboxSubscription, InstaDirectInboxSubscriptionResponse>
            GetDirectSubscriptionConverter(InstaDirectInboxSubscriptionResponse subscription);

        IObjectConverter<InstaRecentActivityFeed, InstaRecentActivityFeedResponse>
            GetSingleRecentActivityConverter(InstaRecentActivityFeedResponse feedResponse);

        IObjectConverter<InstaRecipients, IInstaRecipientsResponse> GetRecipientsConverter(
            IInstaRecipientsResponse recipients);

        IObjectConverter<InstaComment, InstaCommentResponse> GetCommentConverter(
            InstaCommentResponse comment);

        IObjectConverter<InstaCommentList, InstaCommentListResponse> GetCommentListConverter(
            InstaCommentListResponse commentList);

        IObjectConverter<InstaCarousel, InstaCarouselResponse> GetCarouselConverter(
            InstaCarouselResponse carousel);

        IObjectConverter<InstaCarouselItem, InstaCarouselItemResponse> GetCarouselItemConverter(
            InstaCarouselItemResponse carouselItem);

        IObjectConverter<InstaStoryItem, InstaStoryItemResponse> GetStoryItemConverter(
            InstaStoryItemResponse storyItem);

        IObjectConverter<InstaStory, InstaStoryResponse> GetStoryConverter(InstaStoryResponse storyItem);

        IObjectConverter<InstaStoryTray, InstaStoryTrayResponse> GetStoryTrayConverter(
            InstaStoryTrayResponse storyTray);

        IObjectConverter<InstaStoryMedia, InstaStoryMediaResponse> GetStoryMediaConverter(
            InstaStoryMediaResponse storyMedia);

        IObjectConverter<InstaImage, ImageResponse> GetImageConverter(ImageResponse imageResponse);

        IObjectConverter<InstaExploreFeed, InstaExploreFeedResponse> GetExploreFeedConverter(
            InstaExploreFeedResponse feedResponse);

        IObjectConverter<InstaChannel, InstaChannelResponse> GetChannelConverter(
            InstaChannelResponse response);

        IObjectConverter<InstaTopLive, InstaTopLiveResponse> GetTopLiveConverter(
            InstaTopLiveResponse response);

        IObjectConverter<InstaReelFeed, InstaReelFeedResponse> GetReelFeedConverter(
            InstaReelFeedResponse response);

        IObjectConverter<InstaReelMention, InstaReelMentionResponse> GetMentionConverter(
            InstaReelMentionResponse response);

        IObjectConverter<InstaLocation, InstaLocationResponse> GetLocationConverter(
            InstaLocationResponse response);

        IObjectConverter<InstaHashtagSearch, InstaHashtagSearchResponse> GetHashTagsSearchConverter(
            InstaHashtagSearchResponse response);

        IObjectConverter<InstaHashtag, InstaHashtagResponse> GetHashTagConverter(
            InstaHashtagResponse response);

        IObjectConverter<InstaStoryFeed, InstaStoryFeedResponse> GetStoryFeedConverter(
            InstaStoryFeedResponse response);

        IObjectConverter<InstaCollectionItem, InstaCollectionItemResponse> GetCollectionConverter(
            InstaCollectionItemResponse response);

        IObjectConverter<InstaCollections, InstaCollectionsResponse> GetCollectionsConverter(
            InstaCollectionsResponse response);

        IObjectConverter<InstaCoverMedia, InstaCoverMediaResponse> GetCoverMediaConverter(
            InstaCoverMediaResponse response);
    }
}