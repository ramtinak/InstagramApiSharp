using InstagramApiSharp.API;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace InstagramApiSharp.Classes.SessionHandlers
{
    public class FileSessionHandler : ISessionHandler
    {

        public IInstaApi InstaApi { get; set; }

        /// <summary>
        ///     Path to file
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        ///     Load and Set StateData to InstaApi
        /// </summary>
        public void Load()
        {
            if (File.Exists(FilePath))
            {
                using (var fs = File.OpenRead(FilePath))
                {
                    InstaApi.LoadStateDataFromStream(fs);
                }
            }
        }

        /// <summary>
        ///     Save current StateData from InstaApi
        /// </summary>
        public void Save()
        {
            using (var state = InstaApi.GetStateDataAsStream())
            {
                using (var fileStream = File.Create(FilePath))
                {
                    state.Seek(0, SeekOrigin.Begin);
                    state.CopyTo(fileStream);
                }
            }
        }
    }
}
