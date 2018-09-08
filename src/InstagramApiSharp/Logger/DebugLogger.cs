using System;
using System.Collections.Generic;
using System.Linq;
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
        }

        public void LogRequest(Uri uri)
        {
            if (_logLevel < LogLevel.Request) return;
            Write($"Request: {uri}");
        }

        public void LogResponse(HttpResponseMessage response)
        {
            if (_logLevel < LogLevel.Response) return;
            Write($"Response: {response.RequestMessage.Method} {response.RequestMessage.RequestUri}");
            WriteContent(response.Content, Formatting.None, 0);
        }

        public void LogException(Exception ex)
        {
            if (_logLevel < LogLevel.Exceptions) return;
            Console.WriteLine($"Exception: {ex}");
            Console.WriteLine($"Stacktrace: {ex.StackTrace}");
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
            Console.WriteLine($"{DateTime.Now.ToString()}:\t{message}");
        }
    }
}