using System;
using System.Threading.Tasks;
using InstagramApiSharp.Classes;
using InstagramApiSharp.Classes.Models;

namespace InstagramApiSharp.API.Processors
{
    public interface IMediaProcessor
    {
        /// <summary>
        ///     Get media ID from an url (got from "share link")
        /// </summary>
        /// <param name="uri">Uri to get media ID</param>
        /// <returns>Media ID</returns>
        Task<IResult<string>> GetMediaIdFromUrlAsync(Uri uri);
        /// <summary>
        ///     Delete a media (photo or video)
        /// </summary>
        /// <param name="mediaId">The media ID</param>
        /// <param name="mediaType">The type of the media</param>
        /// <returns>Return true if the media is deleted</returns>
        Task<IResult<bool>> DeleteMediaAsync(string mediaId, InstaMediaType mediaType);
        /// <summary>
        ///     Edit the caption of the media (photo/video)
        /// </summary>
        /// <param name="mediaId">The media ID</param>
        /// <param name="caption">The new caption</param>
        /// <returns>Return true if everything is ok</returns>
        Task<IResult<bool>> EditMediaAsync(string mediaId, string caption);
        /// <summary>
        ///     Upload video
        /// </summary>
        /// <param name="video">Video and thumbnail to upload</param>
        /// <param name="caption">Caption</param>
        Task<IResult<InstaMedia>> UploadVideoAsync(InstaVideoUpload video, string caption);

        //Task<IResult<bool>> UploadVideoThumbnailAsync(InstaImage image, string uploadId);
        //Task<IResult<InstaMedia>> ConfigureVideoAsync(InstaVideo video, string uploadId, string caption);
        //Task<IResult<InstaMedia>> ExposeVideoAsync(string uploadId);

        /// <summary>
        ///     Upload photo
        /// </summary>
        /// <param name="image">Photo to upload</param>
        /// <param name="caption">Caption</param>
        Task<IResult<InstaMedia>> UploadPhotoAsync(InstaImage image, string caption);
        [Obsolete("UploadPhotosAlbumAsync is deprecated, please use UploadAlbumAsync instead.\r\nThis will be deleted in the next update.",false)]
        /// <summary>
        ///     Upload photo
        /// </summary>
        /// <param name="images">Array of photos to upload</param>
        /// <param name="caption">Caption</param>
        Task<IResult<InstaMedia>> UploadPhotosAlbumAsync(InstaImage[] images, string caption);
        /// <summary>
        ///     Upload album (videos and photos)
        /// </summary>
        /// <param name="images">Array of photos to upload</param>
        /// <param name="videos">Array of videos to upload</param>
        /// <param name="caption">Caption</param>
        Task<IResult<InstaMedia>> UploadAlbumAsync(InstaImage[] images, InstaVideoUpload[] videos, string caption);
        //Task<IResult<InstaMedia>> ConfigurePhotoAsync(InstaImage image, string uploadId, string caption);
        //Task<IResult<InstaMedia>> ConfigureAlbumAsync(string[] uploadId, string caption);

        /// <summary>
        ///     Get users (short) who liked certain media. Normaly it return around 1000 last users.
        /// </summary>
        /// <param name="mediaId">Media id</param>
        Task<IResult<InstaLikersList>> GetMediaLikersAsync(string mediaId);
        /// <summary>
        ///     Like media (photo or video)
        /// </summary>
        /// <param name="mediaId">Media id</param>
        Task<IResult<bool>> LikeMediaAsync(string mediaId);
        /// <summary>
        ///     Remove like from media (photo or video)
        /// </summary>
        /// <param name="mediaId">Media id</param>
        Task<IResult<bool>> UnLikeMediaAsync(string mediaId);
        /// <summary>
        ///     Get media by its id asynchronously
        /// </summary>
        /// <param name="mediaId">Maximum count of pages to retrieve</param>
        /// <returns>
        ///     <see cref="InstaMedia" />
        /// </returns>
        Task<IResult<InstaMedia>> GetMediaByIdAsync(string mediaId);
        /// <summary>
        ///     Get share link from media Id
        /// </summary>
        /// <param name="mediaId">media ID</param>
        /// <returns>Share link as Uri</returns>
        Task<IResult<Uri>> GetShareLinkFromMediaIdAsync(string mediaId);
    }
}