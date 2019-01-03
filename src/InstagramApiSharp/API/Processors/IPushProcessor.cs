using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InstagramApiSharp.API.Processors
{
    public interface IPushProcessor
    {
        /// <summary>
        /// Registers application for push notifications
        /// </summary>
        /// <returns></returns>
        Task<bool> RegisterPush();
    }
}
