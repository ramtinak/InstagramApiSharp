using System;
using System.Net.Http;
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
                var responseType = ResponseType.UnExpectedResponse;
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

                var resultInfo = new ResultInfo(responseType, status.Message);
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
                var responseType = ResponseType.UnExpectedResponse;
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

                var resultInfo = new ResultInfo(responseType, message);
                return new Result<T>(false, default(T), resultInfo);
            }
        }
    }
}