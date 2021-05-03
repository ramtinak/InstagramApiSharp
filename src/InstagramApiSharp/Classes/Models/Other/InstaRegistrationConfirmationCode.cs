/*
 * This file is a part of private version of InstagramApiSharp's project
 * 
 * 
 * Developer: Ramtin Jokar [ Ramtinak@live.com ]
 * 
 * 
 * IRANIAN DEVELOPERS (c) 2021
 */

using InstagramApiSharp.Classes.Models;
using Newtonsoft.Json;

namespace InstagramApiSharp.Classes
{
    public class InstaRegistrationConfirmationCode : InstaDefaultResponse
    {
        [JsonProperty("signup_code")]
        public string SignupCode { get; set; }
    }
}