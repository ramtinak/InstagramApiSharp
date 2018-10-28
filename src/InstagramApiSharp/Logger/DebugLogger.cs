using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

namespace InstagramApiSharp.Logger
{
    public class DebugLogger : IInstaLogger
    {
        private readonly LogLevel _logLevel;

        public DebugLogger(LogLevel loglevel)
        {
            _logLevel = loglevel;
        }

        public void LogRequest(HttpRequestMessage request)
        {
            if (_logLevel < LogLevel.Request) return;
            WriteSeprator();
            Write($"Request: {request.Method} {request.RequestUri}");
            WriteHeaders(request.Headers);
            WriteProperties(request.Properties);
            if (request.Method == HttpMethod.Post)
                WriteRequestContent(request.Content);
        }

        public void LogRequest(Uri uri)
        {
            if (_logLevel < LogLevel.Request) return;
            Write($"Request: {uri}");
        }

        public void LogResponse(HttpResponseMessage response)
        {
            if (_logLevel < LogLevel.Response) return;
            Write($"Response: {response.RequestMessage.Method} {response.RequestMessage.RequestUri} [{response.StatusCode}]");
            WriteContent(response.Content, Formatting.None, 0);
        }

        public void LogException(Exception ex)
        {
            if (_logLevel < LogLevel.Exceptions) return;
#if NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6 || NETSTANDARD2_0 || NETSTANDARD2_1 || NETSTANDARD2_2 || NETSTANDARD2_3
            Console.WriteLine($"Exception: {ex}");
            Console.WriteLine($"Stacktrace: {ex.StackTrace}");
#else
            System.Diagnostics.Debug.WriteLine($"Exception: {ex}");
            System.Diagnostics.Debug.WriteLine($"Stacktrace: {ex.StackTrace}");
#endif
        }

        public void LogInfo(string info)
        {
            if (_logLevel < LogLevel.Info) return;
            Write($"Info:{Environment.NewLine}{info}");
        }

        private void WriteHeaders(HttpHeaders headers)
        {
            if (headers == null) return;
            if (!headers.Any()) return;
            Write("Headers:");
            foreach (var item in headers)
                Write($"{item.Key}:{JsonConvert.SerializeObject(item.Value)}");
        }

        private void WriteProperties(IDictionary<string, object> properties)
        {
            if (properties == null) return;
            if (properties.Count == 0) return;
            Write($"Properties:\n{JsonConvert.SerializeObject(properties, Formatting.Indented)}");
        }

        private async void WriteContent(HttpContent content, Formatting formatting, int maxLength = 0)
        {
            Write("Content:");
            var raw = await content.ReadAsStringAsync();
            if (formatting == Formatting.Indented) raw = FormatJson(raw);
            raw = raw.Contains("<!DOCTYPE html>") ? "got html content!" : raw;
            if ((raw.Length > maxLength) & (maxLength != 0))
                raw = raw.Substring(0, maxLength);
            Write(raw);
        }
        private async void WriteRequestContent(HttpContent content,int maxLength = 0)
        {
            Write("Content:");
            var raw = await content.ReadAsStringAsync();
            if ((raw.Length > maxLength) & (maxLength != 0))
                raw = raw.Substring(0, maxLength);
            Write(WebUtility.UrlDecode(raw));
        }

        private void WriteSeprator()
        {
            var sep = new StringBuilder();
            for (var i = 0; i < 100; i++) sep.Append("-");
            Write(sep.ToString());
        }

        private string FormatJson(string json)
        {
            dynamic parsedJson = JsonConvert.DeserializeObject(json);
            return JsonConvert.SerializeObject(parsedJson, Formatting.Indented);
        }

        private void Write(string message)
        {
#if NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6 || NETSTANDARD2_0 || NETSTANDARD2_1 || NETSTANDARD2_2 || NETSTANDARD2_3
            Console.WriteLine($"{DateTime.Now.ToString()}:\t{message}");
#else
            System.Diagnostics.Debug.WriteLine($"{DateTime.Now.ToString()}:\t{message}");
#endif
        }
    }
}