using InstagramApiSharp.API;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstagramApiSharp.Classes.SessionHandlers
{
    public interface ISessionHandler
    {
        IInstaApi InstaApi { get; set; }
        /// <summary>
        ///     Load and Set StateData to InstaApi
        /// </summary>
        void Load();

        /// <summary>
        ///     Save current StateData from InstaApi
        /// </summary>
        void Save();
    }
}
