using System;
using System.Collections.Generic;
using System.Text;

namespace InstagramApiSharp.Logger
{
    /// <summary>
    /// Instagram Debug Logger
    /// </summary>
    /// <param name="message">Debug message</param>
    public delegate void InstaDebugLogger(string message);

    public class InstaDebug
    {
        /// <summary>
        /// Instagram log changed delegate
        /// </summary>
        public static event InstaDebugLogger onInstaDebugLoggerChanged;

        public static void Write(string message)
        {
            onInstaDebugLoggerChanged(message);
        }
    }
}
