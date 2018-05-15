using System;

namespace InstagramApiSharp.API.UriCreators
{
    internal class SearchLocationUriCreator : IUriCreator
    {
        private const string SearchLocation = "location_search";

        public Uri GetUri()
        {
            if (!Uri.TryCreate(InstaApiConstants.BaseInstagramUri, SearchLocation, out var instaUri))
                throw new Exception("Can't create URI for searchiing location");
            return instaUri;
        }
    }
}