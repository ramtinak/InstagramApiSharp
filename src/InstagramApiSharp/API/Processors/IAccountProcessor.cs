/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */
using InstagramApiSharp.Classes;
using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Classes.ResponseWrappers;
using InstagramApiSharp.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace InstagramApiSharp.API.Processors
{
    public interface IAccountProcessor
    {
        #region Edit profile
        /// <summary>
        ///     Set current account private
        /// </summary>
        Task<IResult<InstaUserShort>> SetAccountPrivateAsync();
        /// <summary>
        ///     Set current account public
        /// </summary>
        Task<IResult<InstaUserShort>> SetAccountPublicAsync();
        /// <summary>
        ///     Change password
        /// </summary>
        /// <param name="oldPassword">The old password</param>
        /// <param name="newPassword">
        ///     The new password (shouldn't be the same old password, and should be a password you never used
        ///     here)
        /// </param>
        /// <returns>Return true if the password is changed</returns>
        Task<IResult<bool>> ChangePasswordAsync(string oldPassword, string newPassword);
        /// <summary>
        ///     Edit profile
        /// </summary>
        /// <param name="name">Name (leave null if you don't want to change it)</param>
        /// <param name="biography">Biography (leave null if you don't want to change it)</param>
        /// <param name="url">Url (leave null if you don't want to change it)</param>
        /// <param name="email">Email (leave null if you don't want to change it)</param>
        /// <param name="phone">Phone number (leave null if you don't want to change it)</param>
        /// <param name="gender">Gender type (leave null if you don't want to change it)</param>
        /// <param name="newUsername">New username (optional) (leave null if you don't want to change it)</param>
        Task<IResult<InstaAccountUserResponse>> EditProfileAsync(string name, string biography, string url, string email, string phone, InstaGenderType? gender, string newUsername = null);
        /// <summary>
        ///     Set biography (support hashtags and user mentions)
        /// </summary>
        /// <param name="bio">Biography text, hashtags or user mentions</param>
        Task<IResult<InstaBiography>> SetBiographyAsync(string bio);
        /// <summary>
        /// Get request for edit profile.
        /// </summary>        
        Task<IResult<InstaAccountUserResponse>> GetRequestForEditProfileAsync();
        /// <summary>
        ///     Set name and phone number.
        /// </summary>
        /// <param name="name">Name</param>
        /// <param name="phoneNumber">Phone number</param>        
        Task<IResult<bool>> SetNameAndPhoneNumberAsync(string name, string phoneNumber = "");
        /// <summary>
        ///     Remove profile picture.
        /// </summary>        
        Task<IResult<InstaAccountUserResponse>> RemoveProfilePictureAsync();
        /// <summary>
        ///     Change profile picture(only jpg and jpeg formats).
        /// </summary>
        /// <param name="pictureBytes">Picture(JPG,JPEG) bytes</param>        
        Task<IResult<InstaAccountUserResponse>> ChangeProfilePictureAsync(byte[] pictureBytes);
        /// <summary>
        ///     Change profile picture(only jpg and jpeg formats).
        /// </summary> 
        /// <param name="progress">Progress action</param>
        /// <param name="pictureBytes">Picture(JPG,JPEG) bytes</param>
        Task<IResult<InstaAccountUserResponse>> ChangeProfilePictureAsync(Action<InstaUploaderProgress> progress, byte[] pictureBytes);
        /// <summary>
        ///     Get request for download backup account data.
        /// </summary>
        /// <param name="email">Email</param>
        Task<IResult<InstaRequestDownloadData>> GetRequestForDownloadAccountDataAsync(string email);
        /// <summary>
        ///     Get request for download backup account data.
        /// </summary>
        /// <param name="email">Email</param>
        /// <param name="password">Password (only for facebook logins)</param>
        Task<IResult<InstaRequestDownloadData>> GetRequestForDownloadAccountDataAsync(string email, string password);
        #endregion Edit profile

        #region Story settings
        // Story settings
        /// <summary>
        ///     Get story settings.
        /// </summary>        
        Task<IResult<InstaStorySettings>> GetStorySettingsAsync();
        /// <summary>
        ///     Enable Save story to gallery.
        /// </summary>        
        Task<IResult<bool>> EnableSaveStoryToGalleryAsync();
        /// <summary>
        ///     Disable Save story to gallery.
        /// </summary>        
        Task<IResult<bool>> DisableSaveStoryToGalleryAsync();
        /// <summary>
        ///     Enable Save story to archive.
        /// </summary>        
        Task<IResult<bool>> EnableSaveStoryToArchiveAsync();
        /// <summary>
        ///     Disable Save story to archive.
        /// </summary>        
        Task<IResult<bool>> DisableSaveStoryToArchiveAsync();
        /// <summary>
        ///     Allow story sharing.
        /// </summary>
        /// <param name="allow">Allow or disallow story sharing</param>        
        Task<IResult<bool>> AllowStorySharingAsync(bool allow = true);
        /// <summary>
        ///     Allow story message replies.
        /// </summary>
        /// <param name="repliesType">Reply typo</param>        
        Task<IResult<bool>> AllowStoryMessageRepliesAsync(InstaMessageRepliesType repliesType);
        /// <summary>
        ///     Check username availablity.
        /// </summary>
        /// <param name="desiredUsername">Desired username</param>        
        Task<IResult<AccountCheckResponse>> CheckUsernameAsync(string desiredUsername);
        #endregion Story settings

        #region two factor authentication enable/disable
        // two factor authentication enable/disable
        /// <summary>
        ///     Get Security settings (two factor authentication and backup codes).
        /// </summary>
        Task<IResult<AccountSecuritySettingsResponse>> GetSecuritySettingsInfoAsync();
        /// <summary>
        ///     Disable two factor authentication.
        /// </summary>
        Task<IResult<bool>> DisableTwoFactorAuthenticationAsync();
        /// <summary>
        ///     Send two factor enable sms.
        /// </summary>
        /// <param name="phoneNumber">Phone number</param>
        Task<IResult<AccountTwoFactorSmsResponse>> SendTwoFactorEnableSmsAsync(string phoneNumber);
        /// <summary>
        ///     Verify enable two factor.
        /// </summary>
        /// <param name="phoneNumber">Phone number</param>
        /// <param name="verificationCode">Verification code</param>
        Task<IResult<AccountTwoFactorResponse>> TwoFactorEnableAsync(string phoneNumber, string verificationCode);
        /// <summary>
        ///     Send confirm email.
        /// </summary>
        Task<IResult<AccountConfirmEmailResponse>> SendConfirmEmailAsync();
        /// <summary>
        ///     Send sms code.
        /// </summary>
        /// <param name="phoneNumber">Phone number</param>
        Task<IResult<AccountSendSmsResponse>> SendSmsCodeAsync(string phoneNumber);
        /// <summary>
        ///     Verify sms code.
        /// </summary>
        /// <param name="phoneNumber">Phone number (ex: +9891234...)</param>
        /// <param name="verificationCode">Verification code</param>
        Task<IResult<AccountVerifySmsResponse>> VerifySmsCodeAsync(string phoneNumber, string verificationCode);
        /// <summary>
        ///     Regenerate two factor backup codes
        /// </summary>
        Task<IResult<TwoFactorRegenBackupCodesResponse>> RegenerateTwoFactorBackupCodesAsync();
        #endregion two factor authentication enable/disable




        #region NOT COMPLETE FUNCTIONS
        /// <summary>
        ///     NOT COMPLETE dastrasi last activity
        /// </summary>
        //Task<IResult<object>> EnablePresenceAsync();
        /// <summary>
        ///     NOT COMPLETE dastrasi last activity
        /// </summary>
        //Task<IResult<object>> DisablePresenceAsync();
        /// <summary>
        ///     NOT COMPLETE dastrasi last activity
        /// </summary>
        //Task<IResult<object>> GetCommentFilterAsync();
        #endregion NOT COMPLETE FUNCTIONS
    }
}
