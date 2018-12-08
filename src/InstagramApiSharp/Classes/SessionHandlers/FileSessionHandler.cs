using InstagramApiSharp.API;
using InstagramApiSharp.API.Builder;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
#if WINDOWS_UWP
using Windows.Storage;
#endif
namespace InstagramApiSharp.Classes.SessionHandlers
{
    public class FileSessionHandler : ISessionHandler
    {

        public IInstaApi InstaApi { get; set; }
#if WINDOWS_UWP
        /// <summary>
        ///     File => Optional
        ///     <para>If you didn't set this, InstagramApiSharp will choose it automatically based on <see cref="InstaApi"/> username!</para>
        /// </summary>
        public StorageFile File { get; set; }

        private readonly StorageFolder LocalFolder = ApplicationData.Current.LocalFolder;
#else
        /// <summary>
        ///     Path to file
        /// </summary>
        public string FilePath { get; set; }
#endif

        /// <summary>
        ///     Load and Set StateData to InstaApi
        /// </summary>
        public
#if WINDOWS_UWP
            async
#endif
            void Load()
        {
#if WINDOWS_UWP
            if (File == null)
                throw new ArgumentNullException("File cannot be null for Load session\r\nPlease set a file and try again.");
            try
            {
                var json = await FileIO.ReadTextAsync(File);
                if (string.IsNullOrEmpty(json))
                    return;

                if(InstaApi == null)
                    InstaApi = InstaApiBuilder.CreateBuilder()
                        .SetUser(UserSessionData.Empty)
                        .Build();

                InstaApi.LoadStateDataFromString(json);
            }
            catch { }
#else
            if (File.Exists(FilePath))
            {
                using (var fs = File.OpenRead(FilePath))
                {
                    InstaApi.LoadStateDataFromStream(fs);
                }
            }
#endif
        }

        /// <summary>
        ///     Save current StateData from InstaApi
        /// </summary>
        public
#if WINDOWS_UWP
            async
#endif
            void Save()
        {
#if WINDOWS_UWP
            if (File == null)
                File = await LocalFolder.CreateFileAsync($"{InstaApi.GetLoggedUser().UserName.ToLower()}.bin", CreationCollisionOption.ReplaceExisting);

            var json = InstaApi.GetStateDataAsString();
            await FileIO.WriteTextAsync(File, json, Windows.Storage.Streams.UnicodeEncoding.Utf8);
#else
            using (var state = InstaApi.GetStateDataAsStream())
            {
                using (var fileStream = File.Create(FilePath))
                {
                    state.Seek(0, SeekOrigin.Begin);
                    state.CopyTo(fileStream);
                }
            }
#endif
        }
    }
}
