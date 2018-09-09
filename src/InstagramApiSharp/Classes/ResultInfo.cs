using System;

namespace InstagramApiSharp.Classes
{
    public class ResultInfo
    {
        public ResultInfo(string message)
        {
            Message = message;
            if (message.Contains("task was canceled"))
                Timeout = true;
            if (message.ToLower().Contains("challenge"))
                NeedsChallenge = true;
        }

        public ResultInfo(Exception exception)
        {
            Exception = exception;
            Message = exception?.Message;
            ResponseType = ResponseType.InternalException;
            if (Message.Contains("task was canceled"))
                Timeout = true;
            if (Message.ToLower().Contains("challenge"))
                NeedsChallenge = true;
        }

        public ResultInfo(ResponseType responseType, string errorMessage)
        {
            ResponseType = responseType;
            Message = errorMessage;
            if (errorMessage.Contains("task was canceled"))
                Timeout = true;
            if (errorMessage.ToLower().Contains("challenge"))
                NeedsChallenge = true;
        }

        public Exception Exception { get; }

        public string Message { get; }

        public ResponseType ResponseType { get; }

        public bool Timeout { get; }

        public bool NeedsChallenge { get; }

        public override string ToString()
        {
            return $"{ResponseType.ToString()}: {Message}.";
        }
    }
}