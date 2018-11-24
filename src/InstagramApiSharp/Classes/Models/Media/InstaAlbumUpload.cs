using System;
using System.Collections.Generic;
using System.Text;

namespace InstagramApiSharp.Classes.Models
{
    /// <summary>
    ///     Only one of these property property!
    /// </summary>
    public class InstaAlbumUpload
    {
        /// <summary>
        ///     If you set <see cref="ImageToUpload"/>, don't set <see cref="VideoToUpload"/>
        /// </summary>
        public InstaImageUpload ImageToUpload { get; set; }
        /// <summary>
        ///     If you set <see cref="VideoToUpload"/>, don't set <see cref="ImageToUpload"/>
        /// </summary>
        public InstaVideoUpload VideoToUpload { get; set; }

        internal bool IsImage => ImageToUpload != null;

        internal bool IsVideo => VideoToUpload != null;

        internal bool IsBoth => ImageToUpload != null && VideoToUpload != null;
    }
}
