using System;
using System.Net.Http;
using InstagramApiSharp.Classes.ResponseWrappers;
using InstagramApiSharp.Helpers;

namespace InstagramApiSharp.Classes
{
    public class Result<T> : IResult<T>
    {
        public Result(bool succeeded, T value, ResultInfo info)
        {
            Succeeded = succeeded;
            Value = value;
            Info = info;
        }

        public Result(bool succeeded, ResultInfo info)
        {
            Succeeded = succeeded;
            Info = info;
        }

        public Result(bool succeeded, T value)
        {
            Succeeded = succeeded;
            Value = value;
        }

        public bool Succeeded { get; }
        public T Value { get; }
        public ResultInfo Info { get; } = new ResultInfo("");
    }

    public static class Result
    {
        public static IResult<T> Success<T>(T resValue)
        {
            return new Result<T>(true, resValue, new ResultInfo(ResponseType.OK, "No errors detected"));
        }

        public static IResult<T> Success<T>(string successMsg, T resValue)
        {
            return new Result<T>(true, resValue, new ResultInfo(ResponseType.OK, successMsg));
        }

        public static IResult<T> Fail<T>(Exception exception)
        {
            return new Result<T>(false, default(T), new ResultInfo(exception));
        }

        public static IResult<T> Fail<T>(string errMsg)
        {
            return new Result<T>(false, default(T), new ResultInfo(errMsg));
        }

        public static IResult<T> Fail<T>(string errMsg, T resValue)
        {
            return new Result<T>(false, resValue, new ResultInfo(errMsg));
        }

        public static IResult<T> Fail<T>(Exception exception, T resValue)
        {
            return new Result<T>(false, resValue, new ResultInfo(exception));
        }

        public static IResult<T> Fail<T>(Exception exception, T resValue, ResponseType responseType)
        {
            return new Result<T>(false, resValue, new ResultInfo(exception, responseType));
        }

        public static IResult<T> Fail<T>(ResultInfo info, T resValue)
        {
            return new Result<T>(false, resValue, info);
        }

        public static IResult<T> Fail<T>(string errMsg, ResponseType responseType, T resValue)
        {
            return new Result<T>(false, resValue, new ResultInfo(responseType, errMsg));
        }

        public static IResult<T> UnExpectedResponse<T>(HttpResponseMessage response, string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                var resultInfo = new ResultInfo(ResponseType.UnExpectedResponse,
                    $"Unexpected response status: {response.StatusCode}");
                return new Result<T>(false, default(T), resultInfo);
            }
            else
            {
                var status = ErrorHandlingHelper.GetBadStatusFromJsonString(json);
                var responseType = GetResponseType(status);

                var resultInfo = new ResultInfo(responseType, status)
                {
                    Challenge = status.Challenge
                };
                return new Result<T>(false, default(T), resultInfo);
            }
        }

        public static IResult<T> UnExpectedResponse<T>(HttpResponseMessage response, string message, string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                var resultInfo = new ResultInfo(ResponseType.UnExpectedResponse,
                    $"{message}\r\nUnexpected response status: {response.StatusCode}");
                return new Result<T>(false, default(T), resultInfo);
            }
            else
            {
                var status = ErrorHandlingHelper.GetBadStatusFromJsonString(json);
                var responseType = GetResponseType(status);

                var resultInfo = new ResultInfo(responseType, message)
                {
                    Challenge = status.Challenge
                };

                return new Result<T>(false, default(T), resultInfo);
            }
        }
        static ResponseType GetResponseType(BadStatusResponse status)
        {
            var responseType = ResponseType.UnExpectedResponse;
            if(!string.IsNullOrWhiteSpace(status.ErrorType))
                switch (status.ErrorType)
                {
                    case "checkpoint_logged_out":
                        responseType = ResponseType.CheckPointRequired;
                        break;
                    case "login_required":
                        responseType = ResponseType.LoginRequired;
                        break;
                    case "Sorry, too many requests.Please try again later":
                        responseType = ResponseType.RequestsLimit;
                        break;
                    case "sentry_block":
                        responseType = ResponseType.SentryBlock;
                        break;
                    case "inactive user":
                    case "inactive_user":
                        responseType = ResponseType.InactiveUser;
                        break;
                    case "checkpoint_challenge_required":
                        responseType = ResponseType.ChallengeRequired;
                        break;
                }

            if (!status.IsOk() && status.Message.Contains("wait a few minutes"))
                responseType = ResponseType.RequestsLimit;

            if (!string.IsNullOrEmpty(status.Message) && status.Message.Contains("consent_required"))
                responseType = ResponseType.ConsentRequired;

            if (!string.IsNullOrEmpty(status.FeedbackTitle) && status.FeedbackTitle.ToLower().Contains("action blocked"))
                responseType = ResponseType.ActionBlocked;

            if (!string.IsNullOrEmpty(status.Message) && status.Message.Contains("login_required"))
                responseType = ResponseType.LoginRequired;

            if (!string.IsNullOrEmpty(status.Message) && status.Message.ToLower().Contains("media not found or unavailable"))
                responseType = ResponseType.MediaNotFound;

            if (!string.IsNullOrEmpty(status.FeedbackTitle) && status.FeedbackTitle.ToLower().Contains("commenting is Off"))
                responseType = ResponseType.CommentingIsDisabled;

            if (!string.IsNullOrEmpty(status.Message) && status.Message.ToLower().Contains("already liked"))
                responseType = ResponseType.AlreadyLiked;

            if (!string.IsNullOrEmpty(status.FeedbackMessage) && status.FeedbackMessage.ToLower().Contains("post you were viewing has been deleted"))
                responseType = ResponseType.DeletedPost;

            if (!string.IsNullOrEmpty(status.Message) && status.Message.ToLower().Contains("you cannot like this"))
                responseType = ResponseType.CantLike;

            if (status.Payload != null)
                if (!string.IsNullOrEmpty(status.Payload.Message) && status.Payload.Message.ToLower().Contains("media is not accessible"))
                    responseType = ResponseType.DeletedPost;

            if (status.Spam)
                responseType = ResponseType.Spam;

            if (status?.Message?.IndexOf("challenge_required") != -1)
                responseType = ResponseType.ChallengeRequired;
            return responseType;
        }
    }
}