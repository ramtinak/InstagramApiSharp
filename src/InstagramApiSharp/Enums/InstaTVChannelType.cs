using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
namespace InstagramApiSharp.Enums
{
    public enum InstaTVChannelType
    {
        /// <summary>
        ///     chrono_following
        /// </summary>
        ChronoFollowing,
        /// <summary>
        ///     popular
        /// </summary>
        Popular,
        /// <summary>
        ///     continue_watching
        /// </summary>
        ContinueWatching,
        /// <summary>
        ///     user => self channel
        /// </summary>
        User,
        /// <summary>
        ///     for_you => suggested
        /// </summary>
        ForYou
    }
}
