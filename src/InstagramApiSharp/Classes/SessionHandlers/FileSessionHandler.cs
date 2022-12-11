using InstagramApiSharp.API;
using InstagramApiSharp.API.Builder;
using InstagramApiSharp.Helpers;
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
        /// <param name="useBinaryFormatter">Use BinaryFomatter, Not suggested!!!!
        /// <para> P.S: This is only for backward compatibility</para>
        /// </param>
        public string FilePath { get; set; }
#endif

        /// <summary>
        ///     Load and Set StateData to InstaApi
        /// </summary>
        public
#if WINDOWS_UWP
            async
#endif
            void Load(bool useBinaryFormatter = true)
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
                if (useBinaryFormatter)
                {
                    using (var fs = File.OpenRead(FilePath))
                    {
                        InstaApi.LoadStateDataFromStream(fs);
                    }
                }
                else
                {
                    var state = File.ReadAllText(FilePath);
                    var decoded = CryptoHelper.Base64Decode(state);
                    InstaApi.LoadStateDataFromString(decoded);
                }
            }
#endif
        }

        /// <summary>
        ///     Save current StateData from InstaApi
        /// </summary>
        /// <param name="useBinaryFormatter">Use BinaryFomatter, Not suggested!!!!
        /// <para> P.S: This is only for backward compatibility</para>
        /// </param>
        public
#if WINDOWS_UWP
            async
#endif
            void Save(bool useBinaryFormatter = true)
        {
#if WINDOWS_UWP
            if (File == null)
                File = await LocalFolder.CreateFileAsync($"{InstaApi.GetLoggedUser().UserName.ToLower()}.bin", CreationCollisionOption.ReplaceExisting);

            var json = InstaApi.GetStateDataAsString();
            await FileIO.WriteTextAsync(File, json, Windows.Storage.Streams.UnicodeEncoding.Utf8);
#else

            if (useBinaryFormatter)
            {
                SaveMe(InstaApi.GetStateDataAsStream());
            }
            else
            {
                var b64 = CryptoHelper.Base64Encode(InstaApi.GetStateDataAsString());
                var data = Encoding.UTF8.GetBytes(b64);
                SaveMe(new MemoryStream(data));
            }
            void SaveMe(Stream stream)
            {
                using (var fileStream = File.Create(FilePath))
                {
                    stream.Seek(0, SeekOrigin.Begin);
                    stream.CopyTo(fileStream);
                }
            }
#endif
        }
    }
}
