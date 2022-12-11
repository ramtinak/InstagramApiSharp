/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ RamtinJokar@outlook.com ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstagramApiSharp.API;
using InstagramApiSharp.Classes;
using InstagramApiSharp.Enums;
/////////////////////////////////////////////////////////////////////
////////////////////// IMPORTANT NOTE ///////////////////////////////
// Please check wiki pages for more information:
// https://github.com/ramtinak/InstagramApiSharp/wiki
////////////////////// IMPORTANT NOTE ///////////////////////////////
/////////////////////////////////////////////////////////////////////
namespace Examples.Samples
{
    internal class Account : IDemoSample
    {
        private readonly IInstaApi InstaApi;

        public Account(IInstaApi instaApi)
        {
            InstaApi = instaApi;
        }

        public async Task DoShow()
        {   
            // get currently logged in user
            var currentUser = await InstaApi.GetCurrentUserAsync();
            Console.WriteLine(
                $"Logged in: username - {currentUser.Value.UserName}, full name - {currentUser.Value.FullName}");

            Console.WriteLine("See Samples/Account.cs to see how it's works");
            Console.WriteLine("Accounts functions: ");
            Console.WriteLine(@"EditProfileAsync
GetRequestForEditProfileAsync
SetNameAndPhoneNumberAsync
RemoveProfilePictureAsync
ChangeProfilePictureAsync
GetStorySettingsAsync
EnableSaveStoryToGalleryAsync
DisableSaveStoryToGalleryAsync
EnableSaveStoryToArchiveAsync
DisableSaveStoryToArchiveAsync
AllowStorySharingAsync
AllowStoryMessageRepliesAsync
CheckUsernameAsync
GetSecuritySettingsInfoAsync
DisableTwoFactorAuthenticationAsync
SendTwoFactorEnableSmsAsync
TwoFactorEnableAsync
SendConfirmEmailAsync
SendSmsCodeAsync
VerifySmsCodeAsync");
        }

        public async void EditProfile()
        {
            string name = "Ramtin Jokar"; // leave null if you don't want to change it
            InstaGenderType? gender = InstaGenderType.Male; // leave null if you don't want to change it
            string email = "Ramtinak@live.com"; // leave null if you don't want to change it
            string url = ""; // leave empty if you have no site/blog | leave null if you don't want to change it
            string phone = "+989171234567"; // leave null if you don't want to change it
            string biography = "C# Programmer\n\nIRaN/FARS/KaZeRouN"; // leave null if you don't want to change it
            string newUsername = ""; // leave empty if you don't want to change your username

            var result = await InstaApi.AccountProcessor.EditProfileAsync(name, biography, url, email, phone, gender, newUsername);

            if (result.Succeeded)
            {
                Console.WriteLine("Profile changed");
                Console.WriteLine("Username: " + result.Value.Username);
                Console.WriteLine("FullName: " + result.Value.FullName);
                Console.WriteLine("Biography: " + result.Value.Biography);
                Console.WriteLine("Email: " + result.Value.Email);
                Console.WriteLine("PhoneNumber: " + result.Value.PhoneNumber);
                Console.WriteLine("Url: " + result.Value.ExternalUrl);
                Console.WriteLine("Gender: " + result.Value.Gender);
                Console.WriteLine();
            }
            else
                Console.WriteLine("Error while editing profile: " + result.Info.Message);

        }

        public async void ChangeProfilePicture()
        {
            var picturePath = @"c:\someawesomepicture.jpg";
            // note: only JPG and JPEG format will accept it in instagram!
            var pictureBytes = File.ReadAllBytes(picturePath);

            var result = await InstaApi.AccountProcessor.ChangeProfilePictureAsync(pictureBytes);
            if(result.Succeeded)
            {
                Console.WriteLine("New profile picture: " + result.Value.ProfilePicUrl);
            }
            else
                Console.WriteLine("Error while changing profile picture: " + result.Info.Message);

        }

        public async void RemoveProfilePicture()
        {
            var result = await InstaApi.AccountProcessor.RemoveProfilePictureAsync();
            if (result.Succeeded)
            {
                Console.WriteLine("Profile picture removed.");
            }
            else
                Console.WriteLine("Error while removing profile picture: " + result.Info.Message);

        }

        public async void SetNameAndPhoneNumber()
        {
            string name = "Ramtin Jokar";
            string phone = "+989171234567";
            var result = await InstaApi.AccountProcessor.SetNameAndPhoneNumberAsync(name, phone);
            if (result.Succeeded)
            {
                Console.WriteLine("Name and phone number changed");
            }
            else
                Console.WriteLine("Error while changing name and phone number: " + result.Info.Message);
        }

        public async void StorySettings()
        {
            var storySettings = await InstaApi.AccountProcessor.GetStorySettingsAsync();
            if (storySettings.Succeeded)
            {
                Console.WriteLine("Story settings");
                Console.WriteLine("Save story to gallery(camera roll): " + storySettings.Value.SaveToCameraRoll);
                Console.WriteLine("Save story to archive: " + storySettings.Value.ReelAutoArchive);
                Console.WriteLine("Allow message replies: " + storySettings.Value.MessagePrefsType);
                Console.WriteLine("Allow sharing story: " + storySettings.Value.AllowStoryReshare);

                // enable/disable save story to gallery(camera roll)
                await InstaApi.AccountProcessor.EnableSaveStoryToGalleryAsync();
                await InstaApi.AccountProcessor.DisableSaveStoryToGalleryAsync();

                // enable/disable save story to archive
                await InstaApi.AccountProcessor.EnableSaveStoryToArchiveAsync();
                await InstaApi.AccountProcessor.DisableSaveStoryToArchiveAsync();

                // allow/disallow sharing stories
                await InstaApi.AccountProcessor.AllowStorySharingAsync(true);
                // await _instaApi.AccountProcessor.AllowStorySharingAsync(false);
                
                // allow story message replies
                await InstaApi.AccountProcessor.AllowStoryMessageRepliesAsync(InstaMessageRepliesType.Everyone);
                // await _instaApi.AccountProcessor.AllowStoryMessageRepliesAsync(InstaMessageRepliesType.Following);
                // await _instaApi.AccountProcessor.AllowStoryMessageRepliesAsync(InstaMessageRepliesType.Off);
            }
        }

        public async void CheckUsernameAvailable()
        {
            var username = "rmt4006";

            var result = await InstaApi.AccountProcessor.CheckUsernameAsync(username);
            if (result.Succeeded)
            {
                if(result.Value.Available)
                    Console.WriteLine($"'{username}' available.");
                else
                    Console.WriteLine($"'{username}' taken.");
            }
            else
                Console.WriteLine("Error while checking username available: " + result.Info.Message);
        }

        public async void SecuritySettingsAndTwoFactor()
        {
            var result = await InstaApi.AccountProcessor.GetSecuritySettingsInfoAsync();
            if (result.Succeeded)
            {
                Console.WriteLine("Security settings information");
                Console.WriteLine("PhoneNumber: " + result.Value.PhoneNumber);
                Console.WriteLine("NationalNumber: " + result.Value.NationalNumber);
                Console.WriteLine("CountryCode: " + result.Value.CountryCode);
                Console.WriteLine("IsTwoFactorEnabled: " + result.Value.IsTwoFactorEnabled);
                Console.WriteLine("IsPhoneConfirmed: " + result.Value.IsPhoneConfirmed);
                Console.WriteLine("BackupCodes: " + string.Join("\t", result.Value.BackupCodes));
                
                // disable two factor authentication
                await InstaApi.AccountProcessor.DisableTwoFactorAuthenticationAsync();



                var phoneNumber = result.Value.PhoneNumber; // "+989171234567"
                // send enable two factor sms authentication 
                await InstaApi.AccountProcessor.SendTwoFactorEnableSmsAsync(phoneNumber);

                // enable(verify) two factor authentication
                var verificationCode = "40061373";
                await InstaApi.AccountProcessor.TwoFactorEnableAsync(phoneNumber, verificationCode);




                // send sms code to verify account with sms
                await InstaApi.AccountProcessor.SendSmsCodeAsync(phoneNumber);

                // verify sms code for verify account with sms
                await InstaApi.AccountProcessor.VerifySmsCodeAsync(phoneNumber, "13734006");




                // send confirm email for verify account with email
                await InstaApi.AccountProcessor.SendConfirmEmailAsync();


            }
        }
    }
}
