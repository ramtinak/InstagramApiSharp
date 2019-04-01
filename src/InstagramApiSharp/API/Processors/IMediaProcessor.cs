using System;
using System.Threading.Tasks;
using InstagramApiSharp.Classes;
using InstagramApiSharp.Classes.Models;

namespace InstagramApiSharp.API.Processors
{
    /// <summary>
    ///     Media api functions.
    /// </summary>
    public interface IMediaProcessor
    {
        /// <summary>
        ///     Add an post to archive list (this will show the post only for you!)
        /// </summary>
        /// <param name="mediaId">Media id (<see cref="InstaMedia.InstaIdentifier"/>)</param>
        /// <returns>Return true if the media is archived</returns>
        Task<IResult<bool>> ArchiveMediaAsync(string mediaId);

        /// <summary>
        ///     Delete a media (photo, video or album)
        /// </summary>
        /// <param name="mediaId">Media id (<see cref="InstaMedia.InstaIdentifier"/>)</param>
        /// <param name="mediaType">The type of the media</param>
        /// <returns>Return true if the media is deleted</returns>
        Task<IResult<bool>> DeleteMediaAsync(string mediaId, InstaMediaType mediaType);

        /// <summary>
        ///     Edit the caption/location of the media (photo/video/album)
        /// </summary>
        /// <param name="mediaId">The media ID</param>
        /// <param name="caption">The new caption</param>
        /// <param name="location">Location => Optional (get it from <seealso cref="ILocationProcessor.SearchLocationAsync"/></param>
        /// <param name="userTags">User tags => Optional</param>
        /// <returns>Return true if everything is ok</returns>
        Task<IResult<InstaMedia>> EditMediaAsync(string mediaId, string caption, InstaLocationShort location = null, InstaUserTagUpload[] userTags = null);

        /// <summary>
        ///     Get archived medias
        /// </summary>
        /// <param name="paginationParameters">Pagination parameters: next id and max amount of pages to load</param>
        /// <returns>
        ///     <see cref="InstaMediaList" />
        /// </returns>
        Task<IResult<InstaMediaList>> GetArchivedMediaAsync(PaginationParameters paginationParameters);

        /// <summary>
        ///     Get blocked medias
        ///     <para>Note: returns media ids!</para>
        /// </summary>
        Task<IResult<InstaMediaIdList>> GetBlockedMediasAsync();

        /// <summary>
        ///     Get multiple media by its multiple ids asynchronously
        /// </summary>
        /// <param name="mediaIds">Media ids</param>
        /// <returns>
        ///     <see cref="InstaMediaList" />
        /// </returns>
        Task<IResult<InstaMediaList>> GetMediaByIdsAsync(params string[] mediaIds);

        /// <summary>
        ///     Get media by its id asynchronously
        /// </summary>
        /// <param name="mediaId">Media id (<see cref="InstaMedia.InstaIdentifier>"/>)</param>
        /// <returns>
        ///     <see cref="InstaMedia" />
        /// </returns>
        Task<IResult<InstaMedia>> GetMediaByIdAsync(string mediaId);

        /// <summary>
        ///     Get media ID from an url (got from "share link")
        /// </summary>
        /// <param name="uri">Uri to get media ID</param>
        /// <returns>Media ID</returns>
        Task<IResult<string>> GetMediaIdFromUrlAsync(Uri uri);
        /// <summary>
        ///     Get users (short) who liked certain media. Normaly it return around 1000 last users.
        /// </summary>
        /// <param name="mediaId">Media id</param>
        Task<IResult<InstaLikersList>> GetMediaLikersAsync(string mediaId);

        /// <summary>
        ///     Get share link from media Id
        /// </summary>
        /// <param name="mediaId">media ID</param>
        /// <returns>Share link as Uri</returns>
        Task<IResult<Uri>> GetShareLinkFromMediaIdAsync(string mediaId);

        /// <summary>
        ///     Like media (photo or video)
        /// </summary>
        /// <param name="mediaId">Media id</param>
        Task<IResult<bool>> LikeMediaAsync(string mediaId);

        /// <summary>
        ///     Report media
        /// </summary>
        /// <param name="mediaId">Media id</param>
        Task<IResult<bool>> ReportMediaAsync(string mediaId);
        
        /// <summary>
        ///     Save media
        /// </summary>
        /// <param name="mediaId">Media id</param>
        Task<IResult<bool>> SaveMediaAsync(string mediaId);

        /// <summary>
        /// <summary>
        ///     Remove an post from archive list (this will show the post for everyone!)
        /// </summary>
        /// <param name="mediaId">Media id (<see cref="InstaMedia.InstaIdentifier"/>)</param>
        /// <returns>Return true if the media is unarchived</returns>
        Task<IResult<bool>> UnArchiveMediaAsync(string mediaId);

        /// <summary>
        ///     Remove like from media (photo or video)
        /// </summary>
        /// <param name="mediaId">Media id</param>
        Task<IResult<bool>> UnLikeMediaAsync(string mediaId);

        /// <summary>
        ///     Unsave media
        /// </summary>
        /// <param name="mediaId">Media id</param>
        Task<IResult<bool>> UnSaveMediaAsync(string mediaId);

        /// <summary>
        ///     Upload album (videos and photos)
        /// </summary>
        /// <param name="images">Array of photos to upload</param>
        /// <param name="videos">Array of videos to upload</param>
        /// <param name="caption">Caption</param>
        /// <param name="location">Location => Optional (get it from <seealso cref="ILocationProcessor.SearchLocationAsync"/></param>
        Task<IResult<InstaMedia>> UploadAlbumAsync(InstaImageUpload[] images, InstaVideoUpload[] videos, string caption, InstaLocationShort location = null);

        /// <summary>
        ///     Upload album (videos and photos) with progress
        /// </summary>
        /// <param name="progress">Progress action</param>
        /// <param name="images">Array of photos to upload</param>
        /// <param name="videos">Array of videos to upload</param>
        /// <param name="caption">Caption</param>
        /// <param name="location">Location => Optional (get it from <seealso cref="ILocationProcessor.SearchLocationAsync"/></param>
        Task<IResult<InstaMedia>> UploadAlbumAsync(Action<InstaUploaderProgress> progress, InstaImageUpload[] images, InstaVideoUpload[] videos, string caption, InstaLocationShort location = null);

        /// <summary>
        ///     Upload album (videos and photos)
        /// </summary>
        /// <param name="album">Array of photos or videos to upload</param>
        /// <param name="caption">Caption</param>
        /// <param name="location">Location => Optional (get it from <seealso cref="ILocationProcessor.SearchLocationAsync"/></param>
        Task<IResult<InstaMedia>> UploadAlbumAsync(InstaAlbumUpload[] album, string caption, InstaLocationShort location = null);

        /// <summary>
        ///     Upload album (videos and photos) with progress
        /// </summary>
        /// <param name="progress">Progress action</param>
        /// <param name="album">Array of photos or videos to upload</param>
        /// <param name="caption">Caption</param>
        /// <param name="location">Location => Optional (get it from <seealso cref="ILocationProcessor.SearchLocationAsync"/></param>
        Task<IResult<InstaMedia>> UploadAlbumAsync(Action<InstaUploaderProgress> progress, InstaAlbumUpload[] album, string caption, InstaLocationShort location = null);

        /// <summary>
        ///     Upload photo [Supports user tags]
        /// </summary>
        /// <param name="image">Photo to upload</param>
        /// <param name="caption">Caption</param>
        /// <param name="location">Location => Optional (get it from <seealso cref="ILocationProcessor.SearchLocationAsync"/></param>
        /// <param name="userTags">User tags => Optional</param>
        Task<IResult<InstaMedia>> UploadPhotoAsync(InstaImageUpload image, string caption, InstaLocationShort location = null);

        /// <summary>
        ///     Upload photo with progress [Supports user tags]
        /// </summary>
        /// <param name="progress">Progress action</param>
        /// <param name="image">Photo to upload</param>
        /// <param name="caption">Caption</param>
        /// <param name="location">Location => Optional (get it from <seealso cref="ILocationProcessor.SearchLocationAsync"/></param>
        /// <param name="userTags">User tags => Optional</param>
        Task<IResult<InstaMedia>> UploadPhotoAsync(Action<InstaUploaderProgress> progress, InstaImageUpload image, string caption, InstaLocationShort location = null);

        /// <summary>
        ///     Upload video [Supports user tags]
        /// </summary>
        /// <param name="video">Video and thumbnail to upload</param>
        /// <param name="caption">Caption</param>
        /// <param name="location">Location => Optional (get it from <seealso cref="ILocationProcessor.SearchLocationAsync"/></param>
        Task<IResult<InstaMedia>> UploadVideoAsync(InstaVideoUpload video, string caption, InstaLocationShort location = null);
        /// <summary>
        ///     Upload video with progress [Supports user tags]
        /// </summary>
        /// <param name="progress">Progress action</param>
        /// <param name="video">Video and thumbnail to upload</param>
        /// <param name="caption">Caption</param>
        /// <param name="location">Location => Optional (get it from <seealso cref="ILocationProcessor.SearchLocationAsync"/></param>
        Task<IResult<InstaMedia>> UploadVideoAsync(Action<InstaUploaderProgress> progress, InstaVideoUpload video, string caption, InstaLocationShort location = null);
    }
}