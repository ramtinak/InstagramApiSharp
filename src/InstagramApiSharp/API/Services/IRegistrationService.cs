/*
*This file is a part of private version of InstagramApiSharp's project
 * 
 * 
 * Developer: Ramtin Jokar [ Ramtinak@live.com ]
 * 
 * 
 * IRANIAN DEVELOPERS (c) 2021
 */

using InstagramApiSharp.Classes;
using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Enums;
using System;
using System.Threading.Tasks;

namespace InstagramApiSharp.API.Services
{
    public interface IRegistrationService
    {
        #region Properties

        /// <summary>
        ///     Waterfall id for registration
        /// </summary>
        string RegistrationWaterfallId { get; set; }
        /// <summary>
        ///     Signup code from Registration via Email
        /// </summary>
        string ForceSignupCode { get; set; }
        /// <summary>
        ///     Birthday for age consent
        /// </summary>
        DateTime Birthday { get; set; }
        /// <summary>
        ///     Check Email Registration response
        /// </summary>
        InstaCheckEmailRegistration InstaCheckEmailRegistration { get; set; }
        /// <summary>
        ///     Sms verification code
        /// </summary>
        string SmsVerificationCode { get; set; }
        /// <summary>
        ///     Registration phone number response
        /// </summary>
        InstaAccountRegistrationPhoneNumberNew AccountRegistrationPhoneNumber { get; set; }

        #endregion Properties

        #region Public functions

        /// <summary>
        ///     Generate random birthday
        /// </summary>
        DateTime GenerateRandomBirthday();

        #endregion

        #region Public Async Functions

        /// <summary>
        ///     Get first contactpoint prefill [ sends before new registration account ]
        /// </summary>
        Task<IResult<bool>> GetFirstContactPointPrefillAsync();

        /// <summary>
        ///     First launcher sync [ sends before new registration account ]
        /// </summary>
        Task<IResult<bool>> FirstLauncherSyncAsync();

        /// <summary>
        ///     First Qe sync [ sends before new registration account ]
        /// </summary>
        Task<IResult<bool>> FirstQeSyncAsync();

        /// <summary>
        ///     Check username availablity
        /// </summary>
        /// <param name="username">Username</param>
        Task<IResult<InstaAccountCheck>> CheckUsernameAsync(string username);

        /// <summary>
        ///     Check email availablity
        /// </summary>
        /// <param name="email">Email</param>
        Task<IResult<InstaCheckEmailRegistration>> CheckEmailAsync(string email);

        /// <summary>
        ///     Get signup consent config
        /// </summary>
        /// <param name="isMainAccount">Is this main account ? always set to to false</param>
        /// <param name="loggedInUserId">Logged in user id (pk) if available</param>
        Task<IResult<InstaSignupConsentConfig>> GetSignupConsentConfigAsync(bool isMainAccount = false, long? loggedInUserId = null);

        /// <summary>
        ///     Send registration verify email
        /// </summary>
        /// <param name="email">Email</param>
        Task<IResult<bool>> SendRegistrationVerifyEmailAsync(string email);

        /// <summary>
        ///     Check registration confirmation code from email
        /// </summary>
        /// <param name="email">Email</param>
        /// <param name="verificationCode">Verification code from email</param>
        Task<IResult<InstaRegistrationConfirmationCode>> CheckRegistrationConfirmationCodeAsync(string email, string verificationCode);

        /// <summary>
        ///     Get si-fetch headers
        /// </summary>
        Task<IResult<bool>> GetSiFetchHeadersAsync();

        /// <summary>
        ///     Get username suggestions
        /// </summary>
        /// <param name="name">Name => will respond with containing provided name</param>
        /// <param name="email">Email => 
        ///         <para>Required for email registration!</para>
        ///         Optional for phone registration!
        /// </param>
        Task<IResult<InstaRegistrationSuggestionResponse>> GetUsernameSuggestionsAsync(string name, string email = null);

        /// <summary>
        ///     Check age eligibility
        /// </summary>
        /// <param name="birthday">Birthday => Passing null, will generate randomly and save it to <see cref="IRegistrationService.Birthday"/></param>
        Task<IResult<InstaCheckAgeEligibility>> CheckAgeEligibilityAsync(DateTime? birthday = null);

        /// <summary>
        ///     Onboarding steps of registration
        /// </summary>
        /// <param name="progressState">Progress state</param>
        /// <param name="registrationMethod">Registration method</param>
        Task<IResult<bool>> GetOnboardingStepsAsync(InstaOnboardingProgressState progressState, InstaRegistrationMethod registrationMethod = InstaRegistrationMethod.Email);

        /// <summary>
        ///     New user flow begins consent
        /// </summary>
        Task<IResult<bool>> NewUserFlowBeginsConsentAsync();

        /// <summary>
        ///     Create new account via email
        /// </summary>
        /// <param name="email">Email</param>
        /// <param name="username">Username</param>
        /// <param name="password">Password</param>
        /// <param name="firstName">First name => Optional</param>
        /// <param name="signUpCode">ForceSignUpCode from <see cref="IRegistrationService.CheckRegistrationConfirmationCodeAsync"/> => Optional</param>
        /// <param name="birthday">Birthday => Optional</param>
        Task<IResult<InstaAccountCreation>> CreateNewAccountWithEmailAsync(string email, string username,
            string password, string firstName = "", string signUpCode = null, DateTime? birthday = null);

        /// <summary>
        ///     Get multiple accounts family
        /// </summary>
        Task<IResult<bool>> GetMultipleAccountsFamilyAsync();

        /// <summary>
        ///     Get zr token result
        /// </summary>
        Task<IResult<bool>> GetZrTokenResultAsync();

        /// <summary>
        ///     Launcher sync [ after registration is done ]
        /// </summary>
        Task<IResult<bool>> LauncherSyncAsync();

        /// <summary>
        ///     Qe sync [ after registration is done ]
        /// </summary>
        Task<IResult<bool>> QeSyncAsync();

        /// <summary>
        ///     Nux new account seen
        /// </summary>
        Task<IResult<bool>> NuxNewAccountSeenAsync();

        /// <summary>
        ///     Get contact point prefill [ after registration is done ]
        /// </summary>
        Task<IResult<bool>> GetContactPointPrefillAsync();

        #region Phone registration

        /// <summary>
        ///     Check phone number
        /// </summary>
        /// <param name="phoneNumber">Phone number</param>
        Task<IResult<bool>> CheckPhoneNumberAsync(string phoneNumber);

        /// <summary>
        ///     Send singup sms code
        /// </summary>
        /// <param name="phoneNumber">Phone number</param>
        Task<IResult<bool>> SendSignUpSmsCodeAsync(string phoneNumber);

        /// <summary>
        ///     Verify signup sms code
        /// </summary>
        /// <param name="phoneNumber">Phone number</param>
        /// <param name="verificationCode">Verification code</param>
        Task<IResult<InstaPhoneNumberRegistration>> VerifySignUpSmsCodeAsync(string phoneNumber, string verificationCode);

        /// <summary>
        ///     Create new account via phone number
        /// </summary>
        /// <param name="phoneNumber">Phone number</param>
        /// <param name="username">Username</param>
        /// <param name="password">Password</param>
        /// <param name="firstName">First name</param>
        /// <param name="verificationCode">Verification code from sms</param>
        /// <param name="birthday">Birthday => Optional</param>
        Task<IResult<InstaAccountCreation>> CreateNewAccountWithPhoneNumberAsync(string phoneNumber,
            string username, string password, string firstName, string verificationCode, DateTime? birthday = null);

        #endregion Phone registration

        #endregion Public Async Functions
    }
}