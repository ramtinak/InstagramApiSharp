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
using System.Threading.Tasks;


namespace InstagramApiSharp.API.Processors
{
    /// <summary>
    ///     Account api functions.
    ///     <para>Note: this is for self account.</para>
    /// </summary>
    public interface IAccountProcessor
    {
        #region Edit profile
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
        ///     Change profile picture(only jpg and jpeg formats).
        /// </summary>
        /// <param name="pictureBytes">Picture(JPG,JPEG) bytes</param>        
        Task<IResult<InstaUserEdit>> ChangeProfilePictureAsync(byte[] pictureBytes);

        /// <summary>
        ///     Change profile picture(only jpg and jpeg formats).
        /// </summary> 
        /// <param name="progress">Progress action</param>
        /// <param name="pictureBytes">Picture(JPG,JPEG) bytes</param>
        Task<IResult<InstaUserEdit>> ChangeProfilePictureAsync(Action<InstaUploaderProgress> progress, byte[] pictureBytes);

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
        Task<IResult<InstaUserEdit>> EditProfileAsync(string name, string biography, string url, string email, string phone, InstaGenderType? gender, string newUsername = null);

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

        /// <summary>
        /// Get request for edit profile.
        /// </summary>        
        Task<IResult<InstaUserEdit>> GetRequestForEditProfileAsync();

        /// <summary>
        ///     Remove profile picture.
        /// </summary>        
        Task<IResult<InstaUserEdit>> RemoveProfilePictureAsync();

        /// <summary>
        ///     Set current account private
        /// </summary>
        Task<IResult<InstaUserShort>> SetAccountPrivateAsync();
        /// <summary>
        ///     Set current account public
        /// </summary>
        Task<IResult<InstaUserShort>> SetAccountPublicAsync();
        /// <summary>
        ///     Set biography (support hashtags and user mentions)
        /// </summary>
        /// <param name="bio">Biography text, hashtags or user mentions</param>
        Task<IResult<InstaBiography>> SetBiographyAsync(string bio);
        /// <summary>
        ///     Set name and phone number.
        /// </summary>
        /// <param name="name">Name</param>
        /// <param name="phoneNumber">Phone number</param>        
        Task<IResult<bool>> SetNameAndPhoneNumberAsync(string name, string phoneNumber = "");
        /// <summary>
        ///     Upload nametag image
        /// </summary>
        /// <param name="nametagImage">Nametag image</param>
        Task<IResult<InstaMedia>> UploadNametagAsync(InstaImage nametagImage);

        #endregion Edit profile

        #region Story settings
        /// <summary>
        ///     Allow story message replies.
        /// </summary>
        /// <param name="repliesType">Reply typo</param>        
        Task<IResult<bool>> AllowStoryMessageRepliesAsync(InstaMessageRepliesType repliesType);

        /// <summary>
        ///     Allow story sharing.
        /// </summary>
        /// <param name="allow">Allow or disallow story sharing</param>        
        Task<IResult<bool>> AllowStorySharingAsync(bool allow = true);

        /// <summary>
        ///     Check username availablity.
        /// </summary>
        /// <param name="desiredUsername">Desired username</param>        
        Task<IResult<InstaAccountCheck>> CheckUsernameAsync(string desiredUsername);

        /// <summary>
        ///     Disable Save story to archive.
        /// </summary>        
        Task<IResult<bool>> DisableSaveStoryToArchiveAsync();

        /// <summary>
        ///     Disable Save story to gallery.
        /// </summary>        
        Task<IResult<bool>> DisableSaveStoryToGalleryAsync();

        /// <summary>
        ///     Enable Save story to archive.
        /// </summary>        
        Task<IResult<bool>> EnableSaveStoryToArchiveAsync();

        /// <summary>
        ///     Enable Save story to gallery.
        /// </summary>        
        Task<IResult<bool>> EnableSaveStoryToGalleryAsync();

        // Story settings
        /// <summary>
        ///     Get story settings.
        /// </summary>        
        Task<IResult<InstaStorySettings>> GetStorySettingsAsync();
        #endregion Story settings

        #region two factor authentication enable/disable
        /// <summary>
        ///     Disable two factor authentication.
        /// </summary>
        Task<IResult<bool>> DisableTwoFactorAuthenticationAsync();

        // two factor authentication enable/disable
        /// <summary>
        ///     Get Security settings (two factor authentication and backup codes).
        /// </summary>
        Task<IResult<InstaAccountSecuritySettings>> GetSecuritySettingsInfoAsync();
        /// <summary>
        ///     Regenerate two factor backup codes
        /// </summary>
        Task<IResult<TwoFactorRegenBackupCodes>> RegenerateTwoFactorBackupCodesAsync();

        /// <summary>
        ///     Send confirm email.
        /// </summary>
        Task<IResult<InstaAccountConfirmEmail>> SendConfirmEmailAsync();

        /// <summary>
        ///     Send sms code.
        /// </summary>
        /// <param name="phoneNumber">Phone number</param>
        Task<IResult<InstaAccountSendSms>> SendSmsCodeAsync(string phoneNumber);

        /// <summary>
        ///     Verify email by verification url
        /// </summary>
        /// <param name="verificationUri">Verification url</param>
        Task<IResult<bool>> VerifyEmailByVerificationUriAsync(Uri verificationUri);

        /// <summary>
        ///     Send two factor enable sms.
        /// </summary>
        /// <param name="phoneNumber">Phone number</param>
        Task<IResult<InstaAccountTwoFactorSms>> SendTwoFactorEnableSmsAsync(string phoneNumber);
        /// <summary>
        ///     Verify enable two factor.
        /// </summary>
        /// <param name="phoneNumber">Phone number</param>
        /// <param name="verificationCode">Verification code</param>
        Task<IResult<InstaAccountTwoFactor>> TwoFactorEnableAsync(string phoneNumber, string verificationCode);
        /// <summary>
        ///     Verify sms code.
        /// </summary>
        /// <param name="phoneNumber">Phone number (ex: +9891234...)</param>
        /// <param name="verificationCode">Verification code</param>
        Task<IResult<InstaAccountVerifySms>> VerifySmsCodeAsync(string phoneNumber, string verificationCode);
        #endregion two factor authentication enable/disable

        #region Other functions

        /// <summary>
        ///     Enable presence (people can track your activities and you can see their activies too)
        /// </summary>
        Task<IResult<bool>> EnablePresenceAsync();

        /// <summary>
        ///     Disable presence (people can't track your activities and you can't see their activies too)
        /// </summary>
        Task<IResult<bool>> DisablePresenceAsync();

        /// <summary>
        ///     Get presence options (see your presence is disable or not)
        /// </summary>
        Task<IResult<InstaPresence>> GetPresenceOptionsAsync();

        /// <summary>
        ///     Switch to personal account
        /// </summary>
        Task<IResult<InstaUser>> SwitchToPersonalAccountAsync();

        /// <summary>
        ///     Switch to business account
        /// </summary>
        Task<IResult<InstaBusinessUser>> SwitchToBusinessAccountAsync();


        #endregion Other functions

        #region NOT COMPLETE FUNCTIONS
        /// <summary>
        ///     NOT COMPLETE dastrasi last activity
        /// </summary>
        //Task<IResult<object>> GetCommentFilterAsync();
        #endregion NOT COMPLETE FUNCTIONS
    }
}
