using System;
using System.Net.Http;
using System.Threading.Tasks;
using InstagramApiSharp.Classes.Android.DeviceInfo;
using InstagramApiSharp.Logger;

namespace InstagramApiSharp.Classes
{
    internal class HttpRequestProcessor : IHttpRequestProcessor
    {
        private IRequestDelay _delay;
        private readonly IInstaLogger _logger;
        public IRequestDelay Delay { get { return _delay; } set { _delay = value; } }
        public HttpRequestProcessor(IRequestDelay delay, HttpClient httpClient, HttpClientHandler httpHandler,
            ApiRequestMessage requestMessage, IInstaLogger logger)
        {
            _delay = delay;
            Client = httpClient;
            HttpHandler = httpHandler;
            RequestMessage = requestMessage;
            _logger = logger;
        }

        public HttpClientHandler HttpHandler { get; set; }
        public ApiRequestMessage RequestMessage { get; }
        public HttpClient Client { get; set; }
        public void SetHttpClientHandler(HttpClientHandler handler)
        {
            HttpHandler = handler;
            Client = new HttpClient(handler);
        }

        public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage requestMessage)
        {
            LogHttpRequest(requestMessage);
            if (_delay.Exist)
                await Task.Delay(_delay.Value);
            var response = await Client.SendAsync(requestMessage);
            LogHttpResponse(response);
            return response;
        }

        public async Task<HttpResponseMessage> GetAsync(Uri requestUri)
        {
            _logger?.LogRequest(requestUri);
            if (_delay.Exist)
                await Task.Delay(_delay.Value);
            var response = await Client.GetAsync(requestUri);
            LogHttpResponse(response);
            return response;
        }

        public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage requestMessage,
            HttpCompletionOption completionOption)
        {
            LogHttpRequest(requestMessage);
            if (_delay.Exist)
                await Task.Delay(_delay.Value);
            var response = await Client.SendAsync(requestMessage, completionOption);
            LogHttpResponse(response);
            return response;
        }

        public async Task<string> SendAndGetJsonAsync(HttpRequestMessage requestMessage,
            HttpCompletionOption completionOption)
        {
            LogHttpRequest(requestMessage);
            if (_delay.Exist)
                await Task.Delay(_delay.Value);
            var response = await Client.SendAsync(requestMessage, completionOption);
            LogHttpResponse(response);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GeJsonAsync(Uri requestUri)
        {
            _logger?.LogRequest(requestUri);
            if (_delay.Exist)
                await Task.Delay(_delay.Value);
            var response = await Client.GetAsync(requestUri);
            LogHttpResponse(response);
            return await response.Content.ReadAsStringAsync();
        }

        private void LogHttpRequest(HttpRequestMessage request)
        {
            _logger?.LogRequest(request);
        }

        private void LogHttpResponse(HttpResponseMessage request)
        {
            _logger?.LogResponse(request);
        }
    }
}