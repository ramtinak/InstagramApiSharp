using System.Threading.Tasks;
using InstagramApiSharp.Classes;
using InstagramApiSharp.Classes.Models;

namespace InstagramApiSharp.API.Processors
{
    public interface IStoryProcessor
    {
        Task<IResult<InstaStoryFeed>> GetStoryFeedAsync();
        Task<IResult<InstaStory>> GetUserStoryAsync(long userId);
        Task<IResult<InstaStoryMedia>> UploadStoryPhotoAsync(InstaImage image, string caption);
        Task<IResult<InstaStoryMedia>> ConfigureStoryPhotoAsync(InstaImage image, string uploadId, string caption);
        Task<IResult<InstaReelFeed>> GetUserStoryFeedAsync(long userId);
        Task<IResult<InstaReelStoryMediaViewers>> GetStoryMediaViewers(string StoryMediaId, PaginationParameters paginationParameters);
        Task<IResult<InstaStorySharing>> ShareStoryAsync(string reelId, string storyMediaId, string threadId);
    }
}