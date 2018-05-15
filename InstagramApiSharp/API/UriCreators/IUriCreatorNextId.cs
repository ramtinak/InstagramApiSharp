using System;

namespace InstagramApiSharp.API.UriCreators
{
    public interface IUriCreatorNextId
    {
        Uri GetUri(long id, string nextId);
    }
}