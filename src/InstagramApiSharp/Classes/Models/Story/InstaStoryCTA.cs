/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ RamtinJokar@outlook.com ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

namespace InstagramApiSharp.Classes.Models
{
    public class InstaStoryCTA
    {
        public int LinkType { get; set; }

        public string WebUri { get; set; }

        public string AndroidClass { get; set; }

        public string Package { get; set; }

        public string DeeplinkUri { get; set; }

        public string CallToActionTitle { get; set; }

        public object RedirectUri { get; set; }

        public string LeadGenFormId { get; set; }

        public string IgUserId { get; set; }
    }
}
