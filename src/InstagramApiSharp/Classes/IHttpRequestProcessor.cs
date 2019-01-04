using System;
using System.Net.Http;
using System.Threading.Tasks;
using InstagramApiSharp.Classes.Android.DeviceInfo;

namespace InstagramApiSharp.Classes
{
    public interface IHttpRequestProcessor
    {
        HttpClientHandler HttpHandler { get; set; }
        ApiRequestMessage RequestMessage { get; }
        HttpClient Client { get; }
        void SetHttpClientHandler(HttpClientHandler handler);
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage requestMessage);
        Task<HttpResponseMessage> GetAsync(Uri requestUri);
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage requestMessage, HttpCompletionOption completionOption);
        Task<string> SendAndGetJsonAsync(HttpRequestMessage requestMessage, HttpCompletionOption completionOption);
        Task<string> GeJsonAsync(Uri requestUri);
        IRequestDelay Delay { get; set; }
    }
}