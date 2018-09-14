using System;
using System.Collections.Generic;
using System.Text;

namespace InstagramApiSharp.Enums
{
    public enum InstaUploadState
    {
        Preparing,
        Uploading,
        Uploaded,
        UploadingThumbnail,
        ThumbnailUploaded,
        Configuring,
        Configured,
        Completed,
        Error
    }
}
