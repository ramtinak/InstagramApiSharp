/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ RamtinJokar@outlook.com ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using System;
using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Classes.ResponseWrappers;
using InstagramApiSharp.Enums;
using System.Linq;
using InstagramApiSharp.Classes;
using InstagramApiSharp.Classes.ResponseWrappers.Business;

namespace InstagramApiSharp.Converters
{
    internal class InstaBrandedContentConverter : IObjectConverter<InstaBrandedContent, InstaBrandedContentResponse>
    {
        public InstaBrandedContentResponse SourceObject { get; set; }

        public InstaBrandedContent Convert()
        {
            if (SourceObject == null)
                throw new ArgumentNullException("SourceObject");

            var brandedContent = new InstaBrandedContent
            {
                RequireApproval = SourceObject.RequireApproval
            };
            if (SourceObject.WhitelistedUsers != null && SourceObject.WhitelistedUsers.Any())
            {
                foreach (var item in SourceObject.WhitelistedUsers)
                {
                    try
                    {
                        brandedContent.WhitelistedUsers.Add(ConvertersFabric.Instance.GetUserShortConverter(item).Convert());
                    }
                    catch { }
                }
            }
            return brandedContent;
        }
    }
}
