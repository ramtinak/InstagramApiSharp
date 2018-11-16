/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using InstagramApiSharp.Classes;
using InstagramApiSharp.Enums;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace InstagramApiSharp.Helpers
{
    /*internal class ProgressableStreamContent : HttpContent
    {
        /// <summary>
        ///     100kb
        /// </summary>
        private const int defaultBufferSize = 25 * 4096;

        private HttpContent content;
        private readonly int _bufferSize;
        public InstaUploaderProgress UploaderProgress { get; internal set; }

        private readonly Action<InstaUploaderProgress> progress;
        public ProgressableStreamContent(HttpContent content, Action<InstaUploaderProgress> progress) : this(content, defaultBufferSize, progress) { }
        public ProgressableStreamContent(HttpContent content, int bufferSize, Action<InstaUploaderProgress> progress)
        {
            if (bufferSize <= 0)
            {
                throw new ArgumentOutOfRangeException("bufferSize");
            }

            this.content = content ?? throw new ArgumentNullException("content");
            if (bufferSize < 5120)
                bufferSize = defaultBufferSize;
            _bufferSize = bufferSize;
            this.progress = progress;

            foreach (var h in content.Headers)
            {
                Headers.Add(h.Key, h.Value);
            }
        }

        protected override Task SerializeToStreamAsync(Stream stream, TransportContext context)
        {
            return Task.Run(async () =>
            {
                var buffer = new byte[_bufferSize];
                TryComputeLength(out var size);
                var uploadedBytes = 0;


                using (var inputStream = await content.ReadAsStreamAsync())
                {
                    while (true)
                    {
                        var length = await inputStream.ReadAsync(buffer, 0, buffer.Length);
                        if (length <= 0) break;
                        uploadedBytes += length;
                        Invoke(uploadedBytes, size);
                        await stream.WriteAsync(buffer, 0, length);
                        await stream.FlushAsync();
                    }
                }
                await stream.FlushAsync();
            });
        }
        void Invoke(long bytes, long size)
        {
            if (UploaderProgress == null)
                UploaderProgress = new InstaUploaderProgress();
            UploaderProgress.FileSize = size;
            UploaderProgress.UploadedBytes = bytes;
            var state = size == bytes ? InstaUploadState.Uploaded : InstaUploadState.Uploading;
            UploaderProgress.UploadState = state;
            progress?.Invoke(UploaderProgress);
        }
        protected override bool TryComputeLength(out long length)
        {
            length = content.Headers.ContentLength.GetValueOrDefault();
            return true;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                content.Dispose();
            }
            base.Dispose(disposing);
        }
    }*/
}
