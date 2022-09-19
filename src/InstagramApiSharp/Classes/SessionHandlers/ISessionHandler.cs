using InstagramApiSharp.API;
using System;
using System.Collections.Generic;
using System.Text;
#if WINDOWS_UWP
using Windows.Storage;
#endif
namespace InstagramApiSharp.Classes.SessionHandlers
{
    public interface ISessionHandler
    {
        IInstaApi InstaApi { get; set; }
#if WINDOWS_UWP
        /// <summary>
        ///     File => Optional
        ///     <para>If you didn't set this, InstagramApiSharp will choose it automatically based on <see cref="InstaApi"/> username!</para>
        /// </summary>
        StorageFile File { get; set; }
#else
        /// <summary>
        ///     Path to file
        /// </summary>
        string FilePath { get; set; }
#endif

        /// <summary>
        ///     Load and Set StateData to InstaApi
        /// </summary>
        /// <param name="useBinaryFormatter">Use BinaryFomatter, Not suggested!!!!
        /// <para> P.S: This is only for backward compatibility</para>
        /// </param>
        void Load(bool useBinaryFormatter = true);

        /// <summary>
        ///     Save current StateData from InstaApi
        /// </summary>
        /// <param name="useBinaryFormatter">Use BinaryFomatter, Not suggested!!!!
        /// <para> P.S: This is only for backward compatibility</para>
        void Save(bool useBinaryFormatter = true);
    }
}
