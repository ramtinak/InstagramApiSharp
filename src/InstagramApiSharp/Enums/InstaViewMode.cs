using System;
using System.Collections.Generic;
using System.Text;

namespace InstagramApiSharp.Enums
{
    public enum InstaViewMode
    {
        /// <summary>
        ///     Only see one time without replay option, and it will be remove
        /// </summary>
        Once,
        /// <summary>
        ///     Only see once with replay option, and it will be remove
        /// </summary>
        Replayable,
        /// <summary>
        ///     Permanent direct, it's like sending photo/video to direct
        /// </summary>
        Permanent
    }
}
