/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using InstagramApiSharp.Enums;
namespace InstagramApiSharp.Classes
{
    public class InstaUploaderProgress
    {
        public InstaUploadState UploadState { get; internal set; }
        //public long FileSize { get; internal set; }
        //public long UploadedBytes { get; internal set; }
        public string UploadId { get; internal set; }
        public string Caption { get; internal set; }
        public string Name { get; internal set; } = "Uploading single file";
    }
}
