using System;
using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Classes.ResponseWrappers;
using InstagramApiSharp.Enums;
namespace InstagramApiSharp.Converters
{
    internal class InstaCurrentUserConverter : IObjectConverter<InstaCurrentUser, InstaCurrentUserResponse>
    {
        public InstaCurrentUserResponse SourceObject { get; set; }

        public InstaCurrentUser Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException($"Source object");
            var shortConverter = ConvertersFabric.Instance.GetUserShortConverter(SourceObject);
            var user = new InstaCurrentUser(shortConverter.Convert())
            {
                HasAnonymousProfilePicture = SourceObject.HasAnonymousProfilePicture,
                Biography = SourceObject.Biography,
                Birthday = SourceObject.Birthday,
                CountryCode = SourceObject.CountryCode,
                NationalNumber = SourceObject.NationalNumber,
                Email = SourceObject.Email,
                ExternalUrl = SourceObject.ExternalURL,
                ShowConversionEditEntry = SourceObject.ShowConversationEditEntry,
                Gender = (InstaGenderType)SourceObject.Gender,
                PhoneNumber = SourceObject.PhoneNumber
            };

            if (SourceObject.HDProfilePicVersions != null && SourceObject.HDProfilePicVersions?.Length > 0)
                foreach (var imageResponse in SourceObject.HDProfilePicVersions)
                {
                    var converter = ConvertersFabric.Instance.GetImageConverter(imageResponse);
                    user.HdProfileImages.Add(converter.Convert());
                }

            if (SourceObject.HDProfilePicture != null)
            {
                var converter = ConvertersFabric.Instance.GetImageConverter(SourceObject.HDProfilePicture);
                user.HdProfilePicture = converter.Convert();
            }

            return user;
        }
    }
}