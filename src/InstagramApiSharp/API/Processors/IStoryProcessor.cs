using System;
using System.Threading.Tasks;
using InstagramApiSharp.Classes;
using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Enums;

namespace InstagramApiSharp.API.Processors
{
    /// <summary>
    ///     Story api functions.
    /// </summary>
    public interface IStoryProcessor
    {
        /// <summary>
        ///     Respond to an story question
        /// </summary>
        /// <param name="storyId">Story id (<see cref="InstaStoryItem.Id"/>)</param>
        /// <param name="questionId">Question id (<see cref="InstaStoryQuestionStickerItem.QuestionId"/>)</param>
        /// <param name="responseText">Text to respond</param>
        Task<IResult<bool>> AnswerToStoryQuestionAsync(string storyId, long questionId, string responseText);

        /// <summary>
        ///     Create new highlight
        /// </summary>
        /// <param name="mediaId">Story media id</param>
        /// <param name="title">Highlight title</param>
        /// <param name="cropWidth">Crop width It depends on the aspect ratio/size of device display and the aspect ratio of story uploaded. must be in a range of 0-1, i.e: 0.19545822</param>
        /// <param name="cropHeight">Crop height It depends on the aspect ratio/size of device display and the aspect ratio of story uploaded. must be in a range of 0-1, i.e: 0.8037307</param>
        Task<IResult<InstaHighlightFeed>> CreateHighlightFeedAsync(string mediaId, string title, float cropWidth, float cropHeight);

        /// <summary>
        ///     Delete highlight feed
        /// </summary>
        /// <param name="highlightId">Highlight id</param>
        /// <param name="mediaId">Media id (CoverMedia.MediaId)</param>
        Task<IResult<bool>> DeleteHighlightFeedAsync(string highlightId, string mediaId);

        /// <summary>
        ///     Delete a media story (photo or video)
        /// </summary>
        /// <param name="mediaId">Story media id</param>
        /// <param name="sharingType">The type of the media</param>
        /// <returns>Return true if the story media is deleted</returns>
        Task<IResult<bool>> DeleteStoryAsync(string storyMediaId, InstaSharingType sharingType = InstaSharingType.Video);

        /// <summary>
        ///     Follow countdown story
        /// </summary>
        /// <param name="countdownId">Countdown id (<see cref="InstaStoryCountdownStickerItem.CountdownId"/>)</param>
        Task<IResult<bool>> FollowCountdownStoryAsync(long countdownId);

        /// <summary>
        ///     Get list of users that blocked from seeing your stories
        /// </summary>
        Task<IResult<InstaUserShortList>> GetBlockedUsersFromStoriesAsync();

        /// <summary>
        ///     Get stories countdowns for self accounts
        /// </summary>
        Task<IResult<InstaStoryCountdownList>> GetCountdownsStoriesAsync();

        /// <summary>
        ///     Get user highlight feeds by user id (pk)
        /// </summary>
        /// <param name="userId">User id (pk)</param>
        Task<IResult<InstaHighlightFeeds>> GetHighlightFeedsAsync(long userId);

        /// <summary>
        ///     Get user highlights archive
        ///     <para>Note: Use <see cref="IStoryProcessor.GetHighlightsArchiveMediasAsync(string)"/> to get hightlight medias of an specific day.</para>
        /// </summary>
        Task<IResult<InstaHighlightShortList>> GetHighlightsArchiveAsync();

        /// <summary>
        ///     Get highlights archive medias
        ///     <para>Note: get highlight id from <see cref="IStoryProcessor.GetHighlightsArchiveAsync"/></para>
        /// </summary>
        /// <param name="highlightId">Highlight id (Get it from <see cref="IStoryProcessor.GetHighlightsArchiveAsync"/>)</param>
        Task<IResult<InstaHighlightSingleFeed>> GetHighlightsArchiveMediasAsync(string highlightId);

        /// <summary>
        ///     Get single highlight medias
        ///     <para>Note: get highlight id from <see cref="IStoryProcessor.GetHighlightFeedsAsync(long)"/></para>
        /// </summary>
        /// <param name="highlightId">Highlight id (Get it from <see cref="IStoryProcessor.GetHighlightFeedsAsync(long)"/>)</param>
        Task<IResult<InstaHighlightSingleFeed>> GetHighlightMediasAsync(string highlightId);

        /// <summary>
        ///     Get user story feed (stories from users followed by current user).
        /// </summary>
        Task<IResult<InstaStoryFeed>> GetStoryFeedAsync();
        /// <summary>
        ///     Get story media viewers
        /// </summary>
        /// <param name="storyMediaId">Story media id</param>
        /// <param name="paginationParameters">Pagination parameters</param>
        Task<IResult<InstaReelStoryMediaViewers>> GetStoryMediaViewersAsync(string storyMediaId, PaginationParameters paginationParameters);

        /// <summary>
        ///     Get story poll voters
        /// </summary>
        /// <param name="storyMediaId">Story media id</param>
        /// <param name="pollId">Story poll id</param>
        /// <param name="paginationParameters">Pagination parameters</param>
        Task<IResult<InstaStoryPollVotersList>> GetStoryPollVotersAsync(string storyMediaId, string pollId, PaginationParameters paginationParameters);

        /// <summary>
        ///     Get the story by userId
        /// </summary>
        /// <param name="userId">User Id</param>
        Task<IResult<InstaStory>> GetUserStoryAsync(long userId);
        /// <summary>
        ///     Get user story reel feed. Contains user info last story including all story items.
        /// </summary>
        /// <param name="userId">User identifier (PK)</param>
        Task<IResult<InstaReelFeed>> GetUserStoryFeedAsync(long userId);

        /// <summary>
        ///     Seen story
        /// </summary>
        /// <param name="storyMediaId">Story media identifier</param>
        /// <param name="takenAtUnix">Taken at unix</param>
        Task<IResult<bool>> MarkStoryAsSeenAsync(string storyMediaId, long takenAtUnix);

        /// <summary>
        ///     Seen highlight
        ///     <para>Get media id from <see cref="InstaHighlightFeed.CoverMedia.MediaId"/></para>
        /// </summary>
        /// <param name="mediaId">Media identifier (get it from <see cref="InstaHighlightFeed.CoverMedia.MediaId"/>)</param>
        /// <param name="highlightId">Highlight id</param>
        /// <param name="takenAtUnix">Taken at unix</param>
        Task<IResult<bool>> MarkHighlightAsSeenAsync(string mediaId, string highlightId, long takenAtUnix);

        /// <summary>
        ///     Share an media to story
        ///     <para>
        ///     Note 1: You must draw whatever you want in your image first! 
        ///     Also it's on you to calculate clickable media but mostly is 0.5 for width and height
        ///     </para>
        ///     <para>
        ///     Note 2: Get media pk from <see cref="InstaMedia.Pk"/>
        ///     </para>
        /// </summary>
        /// <param name="image">Photo to upload</param>
        /// <param name="mediaStoryUpload">
        ///     Media options
        ///     <para>
        ///     Note 1: You must draw whatever you want in your image first! 
        ///     Also it's on you to calculate clickable media but mostly is 0.5 for width and height
        ///     </para>
        ///     <para>
        ///     Note 2: Get media pk from <see cref="InstaMedia.Pk"/>
        ///     </para>
        /// </param>
        Task<IResult<InstaStoryMedia>> ShareMediaAsStoryAsync(InstaImage image, InstaMediaStoryUpload mediaStoryUpload);

        /// <summary>
        ///     Share an media to story with progress
        ///     <para>
        ///     Note 1: You must draw whatever you want in your image first! 
        ///     Also it's on you to calculate clickable media but mostly is 0.5 for width and height
        ///     </para>
        ///     <para>
        ///     Note 2: Get media pk from <see cref="InstaMedia.Pk"/>
        ///     </para>
        /// </summary>
        /// <param name="progress">Progress action</param>
        /// <param name="image">Photo to upload</param>
        /// <param name="mediaStoryUpload">
        ///     Media options
        ///     <para>
        ///     Note 1: You must draw whatever you want in your image first! 
        ///     Also it's on you to calculate clickable media but mostly is 0.5 for width and height
        ///     </para>
        ///     <para>
        ///     Note 2: Get media pk from <see cref="InstaMedia.Pk"/>
        ///     </para>
        /// </param>
        Task<IResult<InstaStoryMedia>> ShareMediaAsStoryAsync(Action<InstaUploaderProgress> progress, InstaImage image, 
            InstaMediaStoryUpload mediaStoryUpload);

        /// <summary>
        ///     Share story to someone
        /// </summary>
        /// <param name="reelId">Reel id</param>
        /// <param name="storyMediaId">Story media id</param>
        /// <param name="threadId">Thread id</param>
        /// <param name="sharingType">Sharing type</param>
        Task<IResult<InstaSharing>> ShareStoryAsync(string reelId, string storyMediaId, string threadId, string text, InstaSharingType sharingType = InstaSharingType.Video);

        /// <summary>
        ///     Reply to story
        ///     <para>Note: Get story media id from <see cref="InstaMedia.InstaIdentifier"/></para>
        /// </summary>
        /// <param name="storyMediaId">Media id (get it from <see cref="InstaMedia.InstaIdentifier"/>)</param>
        /// <param name="userId">Story owner user pk (get it from <see cref="InstaMedia.User.Pk"/>)</param>
        /// <param name="text">Text to send</param>
        Task<IResult<bool>> ReplyToStoryAsync(string storyMediaId, long userId, string text);

        /// <summary>
        ///     UnFollow countdown story
        /// </summary>
        /// <param name="countdownId">Countdown id (<see cref="InstaStoryCountdownStickerItem.CountdownId"/>)</param>
        Task<IResult<bool>> UnFollowCountdownStoryAsync(long countdownId);

        /// <summary>
        ///     Upload story photo
        /// </summary>
        /// <param name="image">Photo to upload</param>
        /// <param name="caption">Caption</param>
        /// <param name="uploadOptions">Upload options => Optional</param>
        Task<IResult<InstaStoryMedia>> UploadStoryPhotoAsync(InstaImage image, string caption, InstaStoryUploadOptions uploadOptions = null);
        /// <summary>
        ///     Upload story photo with progress
        /// </summary>
        /// <param name="progress">Progress action</param>
        /// <param name="image">Photo to upload</param>
        /// <param name="caption">Caption</param>
        /// <param name="uploadOptions">Upload options => Optional</param>
        Task<IResult<InstaStoryMedia>> UploadStoryPhotoAsync(Action<InstaUploaderProgress> progress, InstaImage image, string caption,
            InstaStoryUploadOptions uploadOptions = null);
        /// <summary>
        ///     Upload story photo with adding link address
        ///     <para>Note: this function only works with verified account or you have more than 10k followers.</para>
        /// </summary>
        /// <param name="image">Photo to upload</param>
        /// <param name="caption">Caption</param>
        /// <param name="uri">Uri to add</param>
        /// <param name="uploadOptions">Upload options => Optional</param>
        Task<IResult<InstaStoryMedia>> UploadStoryPhotoWithUrlAsync(InstaImage image, string caption, Uri uri,
            InstaStoryUploadOptions uploadOptions = null);
        /// <summary>
        ///     Upload story photo with adding link address (with progress)
        ///     <para>Note: this function only works with verified account or you have more than 10k followers.</para>
        /// </summary>
        /// <param name="progress">Progress action</param>
        /// <param name="image">Photo to upload</param>
        /// <param name="caption">Caption</param>
        /// <param name="uri">Uri to add</param>
        /// <param name="uploadOptions">Upload options => Optional</param>
        Task<IResult<InstaStoryMedia>> UploadStoryPhotoWithUrlAsync(Action<InstaUploaderProgress> progress, InstaImage image,
            string caption, Uri uri, InstaStoryUploadOptions uploadOptions = null);
        /// <summary>
        ///     Upload story video (to self story)
        /// </summary>
        /// <param name="video">Video to upload</param>
        /// <param name="caption">Caption</param>
        Task<IResult<InstaStoryMedia>> UploadStoryVideoAsync(InstaVideoUpload video, string caption, InstaStoryUploadOptions uploadOptions = null);
        /// <summary>
        ///     Upload story video (to self story) with progress
        /// </summary>
        /// <param name="progress">Progress action</param>
        /// <param name="video">Video to upload</param>
        /// <param name="caption">Caption</param>
        Task<IResult<InstaStoryMedia>> UploadStoryVideoAsync(Action<InstaUploaderProgress> progress, InstaVideoUpload video, string caption, InstaStoryUploadOptions uploadOptions = null);
        /// <summary>
        ///     Upload story video [to self story, to direct threads or both(self and direct)]
        /// </summary>
        /// <param name="video">Video to upload</param>
        /// <param name="storyType">Story type</param>
        /// <param name="threadIds">Thread ids</param>
        Task<IResult<bool>> UploadStoryVideoAsync(InstaVideoUpload video,
            InstaStoryType storyType = InstaStoryType.SelfStory, InstaStoryUploadOptions uploadOptions = null, params string[] threadIds);

        /// <summary>
        ///     Upload story video (to self story) with progress
        /// </summary>
        /// <param name="progress">Progress action</param>
        /// <param name="video">Video to upload</param>
        /// <param name="storyType">Story type</param>
        /// <param name="threadIds">Thread ids</param>
        Task<IResult<bool>> UploadStoryVideoAsync(Action<InstaUploaderProgress> progress, InstaVideoUpload video,
            InstaStoryType storyType = InstaStoryType.SelfStory, InstaStoryUploadOptions uploadOptions = null, params string[] threadIds);

        /// <summary>
        ///     Upload story video (to self story) with adding link address
        ///     <para>Note: this function only works with verified account or you have more than 10k followers.</para>
        /// </summary>
        /// <param name="progress">Progress action</param>
        /// <param name="video">Video to upload</param>
        /// <param name="caption">Caption</param>
        /// <param name="uri">Uri to add</param>
        Task<IResult<InstaStoryMedia>> UploadStoryVideoWithUrlAsync(InstaVideoUpload video, string caption, Uri uri, InstaStoryUploadOptions uploadOptions = null);
        /// <summary>
        ///     Upload story video (to self story) with adding link address (with progress)
        ///     <para>Note: this function only works with verified account or you have more than 10k followers.</para>
        /// </summary>
        /// <param name="progress">Progress action</param>
        /// <param name="video">Video to upload</param>
        /// <param name="caption">Caption</param>
        /// <param name="uri">Uri to add</param>
        Task<IResult<InstaStoryMedia>> UploadStoryVideoWithUrlAsync(Action<InstaUploaderProgress> progress,
            InstaVideoUpload video, string caption, Uri uri, InstaStoryUploadOptions uploadOptions = null);
        /// <summary>
        ///     Upload story video [to self story, to direct threads or both(self and direct)] with adding link address
        ///     <para>Note: this function only works with verified account or you have more than 10k followers.</para>
        /// </summary>
        /// <param name="video">Video to upload</param>
        /// <param name="uri">Uri to add</param>
        /// <param name="storyType">Story type</param>
        /// <param name="threadIds">Thread ids</param>
        Task<IResult<bool>> UploadStoryVideoWithUrlAsync(InstaVideoUpload video, Uri uri,
            InstaStoryType storyType = InstaStoryType.SelfStory, InstaStoryUploadOptions uploadOptions = null, params string[] threadIds);
        /// <summary>
        ///     Upload story video (to self story) with adding link address (with progress)
        ///     <para>Note: this function only works with verified account or you have more than 10k followers.</para>
        /// </summary>
        /// <param name="progress">Progress action</param>
        /// <param name="video">Video to upload</param>
        /// <param name="storyType">Story type</param>
        /// <param name="threadIds">Thread ids</param>
        Task<IResult<bool>> UploadStoryVideoWithUrlAsync(Action<InstaUploaderProgress> progress, InstaVideoUpload video, Uri uri,
            InstaStoryType storyType = InstaStoryType.SelfStory, InstaStoryUploadOptions uploadOptions = null, params string[] threadIds);

        /// <summary>
        ///     Vote to an story poll
        /// </summary>
        /// <param name="storyMediaId">Story media id</param>
        /// <param name="pollId">Story poll id</param>
        /// <param name="pollVote">Your poll vote</param>
        Task<IResult<InstaStoryItem>> VoteStoryPollAsync(string storyMediaId, string pollId, InstaStoryPollVoteType pollVote);

        /// <summary>
        ///     Vote to an story slider
        ///     <para>Note: slider vote must be between 0 and 1</para>
        /// </summary>
        /// <param name="storyMediaId">Story media id</param>
        /// <param name="pollId">Story poll id</param>
        /// <param name="sliderVote">Your slider vote (from 0 to 1)</param>
        Task<IResult<InstaStoryItem>> VoteStorySliderAsync(string storyMediaId, string pollId, double sliderVote = 0.5);

    }
}