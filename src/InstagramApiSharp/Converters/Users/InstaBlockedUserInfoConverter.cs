using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Classes.ResponseWrappers;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstagramApiSharp.Converters
{
    internal class InstaBlockedUserInfoConverter : IObjectConverter<InstaBlockedUserInfo, InstaBlockedUserInfoResponse>
    {
        public InstaBlockedUserInfoResponse SourceObject { get; set; }

        public InstaBlockedUserInfo Convert()
        {
            return new InstaBlockedUserInfo()
            {
                BlockedAt = SourceObject.BlockedAt,
                FullName = SourceObject.FullName,
                IsPrivate = SourceObject.IsPrivate,
                Pk = SourceObject.Pk,
                ProfilePicture = SourceObject.ProfilePicture,
                UserName = SourceObject.UserName
            };
        }
    }
}
