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
        /// Edit profile.
        /// </summary>
        /// <param name="url">Url</param>
        /// <param name="phone">Phone number</param>
        /// <param name="name">Name</param>
        /// <param name="biography">Biography</param>
        /// <param name="email">Email</param>
        /// <param name="gender">Gender type</param>
        /// <param name="newUsername">New username (optional)</param>
        /// <returns></returns>
        Task<IResult<AccountUserResponse>> EditProfileAsync(string url, string phone, string name, string biography, string email, InstaGenderType gender, string newUsername = null);
        /// <summary>
        /// Get request for edit profile.
        /// </summary>
        /// <returns></returns>
        Task<IResult<AccountUserResponse>> GetRequestForEditProfileAsync();
        /// <summary>
        /// Set name and phone number.
        /// </summary>
        /// <param name="name">Name</param>
        /// <param name="phoneNumber">Phone number</param>
        /// <returns></returns>
        Task<IResult<bool>> SetNameAndPhoneNumberAsync(string name, string phoneNumber = "");
        /// <summary>
        /// Remove profile picture.
        /// </summary>
        /// <returns></returns>
        Task<IResult<AccountUserResponse>> RemoveProfilePictureAsync();
        /// <summary>
        /// Change profile picture(only jpg and jpeg formats).
        /// </summary>
        /// <param name="pictureBytes">Picture(JPG,JPEG) bytes</param>
        /// <returns></returns>
        Task<IResult<AccountUserResponse>> ChangeProfilePictureAsync(byte[] pictureBytes);
        

        // Story settings
        /// <summary>
        /// Get story settings.
        /// </summary>
        /// <returns></returns>
        Task<IResult<AccountSettingsResponse>> GetStorySettingsAsync();
        /// <summary>
        /// Enable Save story to gallery.
        /// </summary>
        /// <returns></returns>
        Task<IResult<bool>> EnableSaveStoryToGalleryAsync();
        /// <summary>
        /// Disable Save story to gallery.
        /// </summary>
        /// <returns></returns>
        Task<IResult<bool>> DisableSaveStoryToGalleryAsync();
        /// <summary>
        /// Enable Save story to archive.
        /// </summary>
        /// <returns></returns>
        Task<IResult<bool>> EnableSaveStoryToArchiveAsync();
        /// <summary>
        /// Disable Save story to archive.
        /// </summary>
        /// <returns></returns>
        Task<IResult<bool>> DisableSaveStoryToArchiveAsync();
        /// <summary>
        /// Allow story sharing.
        /// </summary>
        /// <param name="allow"></param>
        /// <returns></returns>
        Task<IResult<bool>> AllowStorySharingAsync(bool allow = true);
        /// <summary>
        /// Allow story message replies.
        /// </summary>
        /// <param name="repliesType">Reply typo</param>
        /// <returns></returns>
        Task<IResult<bool>> AllowStoryMessageRepliesAsync(InstaMessageRepliesType repliesType);
        /// <summary>
        /// Check username availablity.
        /// </summary>
        /// <param name="desiredUsername">Desired username</param>
        /// <returns></returns>
        Task<IResult<AccountCheckResponse>> CheckUsernameAsync(string desiredUsername);

        // two factor authentication enable/disable
        /// <summary>
        /// Get security settings info & backup codes
        /// </summary>
        /// <returns></returns>
        /// <summary>
        /// Get Security settings (two factor authentication and backup codes).
        /// </summary>
        /// <returns></returns>
        Task<IResult<AccountSecuritySettingsResponse>> GetSecuritySettingsInfoAsync();
        /// <summary>
        /// Disable two factor authentication.
        /// </summary>
        /// <returns></returns>
        Task<IResult<bool>> DisableTwoFactorAuthenticationAsync();
        /// <summary>
        /// Send two factor enable sms.
        /// </summary>
        /// <param name="phoneNumber">Phone number</param>
        /// <returns></returns>
        Task<IResult<AccountTwoFactorSmsResponse>> SendTwoFactorEnableSmsAsync(string phoneNumber);
        /// <summary>
        /// Verify enable two factor.
        /// </summary>
        /// <param name="phoneNumber">Phone number</param>
        /// <param name="verificationCode">Verification code</param>
        /// <returns></returns>
        Task<IResult<AccountTwoFactorResponse>> TwoFactorEnableAsync(string phoneNumber, string verificationCode);
        /// <summary>
        /// Send confirm email.
        /// </summary>
        /// <returns></returns>
        Task<IResult<AccountConfirmEmailResponse>> SendConfirmEmailAsync();
        /// <summary>
        /// Send sms code.
        /// </summary>
        /// <param name="phoneNumber">Phone number</param>
        /// <returns></returns>
        Task<IResult<AccountSendSmsResponse>> SendSmsCodeAsync(string phoneNumber);
        /// <summary>
        /// Verify sms code.
        /// </summary>
        /// <param name="phoneNumber">Phone number (ex: +9891234...)</param>
        /// <param name="verificationCode">Verification code</param>
        /// <returns></returns>
        Task<IResult<AccountVerifySmsResponse>> VerifySmsCodeAsync(string phoneNumber, string verificationCode);

        Task<IResult<TwoFactorRegenBackupCodesResponse>> RegenerateTwoFactorBackupCodesAsync();





        /// <summary>
        /// NOT COMPLETE
        /// </summary>
        /// <returns></returns>
        //Task<IResult<object>> SetBiographyAsync(string bio);
        /// <summary>
        /// NOT COMPLETE dastrasi last activity
        /// </summary>
        /// <returns></returns>
        //Task<IResult<object>> EnablePresenceAsync();
        /// <summary>
        /// NOT COMPLETE dastrasi last activity
        /// </summary>
        /// <returns></returns>
        //Task<IResult<object>> DisablePresenceAsync();
        /// <summary>
        /// NOT COMPLETE dastrasi last activity
        /// </summary>
        /// <returns></returns>
        //Task<IResult<object>> GetCommentFilterAsync();

    }
}
