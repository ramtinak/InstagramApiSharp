using System;
using System.Threading.Tasks;
using InstagramApiSharp.Classes;
using InstagramApiSharp.Classes.Models;

namespace InstagramApiSharp.API.Processors
{
    public interface IMediaProcessor
    {
        Task<IResult<string>> GetMediaIdFromUrlAsync(Uri uri);

        Task<IResult<bool>> DeleteMediaAsync(string mediaId, InstaMediaType mediaType);

        Task<IResult<bool>> EditMediaAsync(string mediaId, string caption);

        Task<IResult<InstaMedia>> UploadVideoAsync(InstaVideo video, InstaImage image, string caption);

        Task<IResult<bool>> UploadVideoThumbnailAsync(InstaImage image, string uploadId);

        Task<IResult<InstaMedia>> ConfigureVideoAsync(InstaVideo video, string uploadId, string caption);

        Task<IResult<InstaMedia>> ExposeVideoAsync(string uploadId);

        Task<IResult<InstaMedia>> UploadPhotoAsync(InstaImage image, string caption);

        Task<IResult<InstaMedia>> UploadPhotosAlbumAsync(InstaImage[] images, string caption);
        Task<IResult<InstaMedia>> UploadAlbumAsync(InstaImage[] images, InstaVideo[] videos, string caption);

        Task<IResult<InstaMedia>> ConfigurePhotoAsync(InstaImage image, string uploadId, string caption);

        Task<IResult<InstaMedia>> ConfigureAlbumAsync(string[] uploadId, string caption);

        Task<IResult<InstaLikersList>> GetMediaLikersAsync(string mediaId);

        Task<IResult<bool>> LikeMediaAsync(string mediaId);

        Task<IResult<bool>> UnLikeMediaAsync(string mediaId);

        Task<IResult<InstaMedia>> GetMediaByIdAsync(string mediaId);

        Task<IResult<Uri>> GetShareLinkFromMediaIdAsync(string mediaId);
    }
}