using InstagramApiSharp.Classes;
using InstagramApiSharp.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace InstagramApiSharp.Helpers
{
    internal class ProgressableStreamContent : HttpContent
    {
        /// <summary>
        /// 20kb
        /// </summary>
        private const int defaultBufferSize = 5 * 4096;

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
                var buffer = new Byte[_bufferSize];
                TryComputeLength(out long size);
                var uploadedBytes = 0;


                using (var inputStream = await content.ReadAsStreamAsync())
                {
                    while (true)
                    {
                        var length = inputStream.Read(buffer, 0, buffer.Length);
                        if (length <= 0) break;
                        uploadedBytes += length;
                        Invoke(uploadedBytes, size);
                        stream.Write(buffer, 0, length);
                        stream.Flush();
                    }
                }
                stream.Flush();
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
    }
}
