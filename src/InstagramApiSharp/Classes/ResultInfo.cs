using InstagramApiSharp.Classes.ResponseWrappers;
using System;

namespace InstagramApiSharp.Classes
{
    public class ResultInfo
    {
        public ResultInfo(string message)
        {
            Message = message;
            HandleMessages(message);
        }

        public ResultInfo(Exception exception)
        {
            Exception = exception;
            Message = exception?.Message;
            ResponseType = ResponseType.InternalException;
            HandleMessages(Message);
        }

        public ResultInfo(ResponseType responseType, string errorMessage)
        {
            ResponseType = responseType;
            Message = errorMessage;
            HandleMessages(errorMessage);
        }
        public ResultInfo(ResponseType responseType, BadStatusResponse status)
        {
            Message = status?.Message;
            ResponseType = responseType;
            HandleMessages(Message);
        }
        public void HandleMessages(string errorMessage)
        {
            if (errorMessage.Contains("task was canceled"))
                Timeout = true;
            if (errorMessage.ToLower().Contains("challenge"))
                NeedsChallenge = true;
        }
        public Exception Exception { get; }

        public string Message { get; }

        public ResponseType ResponseType { get; }

        public bool Timeout { get; internal set; }

        public bool NeedsChallenge { get; internal set; }

        public override string ToString()
        {
            return $"{ResponseType.ToString()}: {Message}.";
        }
    }
}