using System;

namespace InstagramApiSharp.API.UriCreators
{
    internal class GetLocationFeedUriCreator : IUriCreatorNextId
    {
        private const string LocationFeed = "feed/location/{0}/";

        public Uri GetUri(long id, string nextId)
        {
            if (!Uri.TryCreate(InstaApiConstants.BaseInstagramUri, string.Format(LocationFeed, id), out var instaUri))
                throw new Exception("Can't create URI for getting location feed");
            var query = string.Empty;
            if (!string.IsNullOrEmpty(nextId)) query += $"max_id={nextId}";
            var uriBuilder = new UriBuilder(instaUri) {Query = query};
            return uriBuilder.Uri;
        }
    }
}