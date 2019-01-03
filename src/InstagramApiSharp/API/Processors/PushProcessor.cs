using InstagramApiSharp.Classes;
using InstagramApiSharp.Classes.Android.DeviceInfo;
using InstagramApiSharp.Helpers;
using InstagramApiSharp.Logger;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace InstagramApiSharp.API.Processors
{
    //internal class PushProcessor : IPushProcessor
    //{
    //    private readonly AndroidDevice _deviceInfo;
    //    private readonly HttpHelper _httpHelper;
    //    private readonly IHttpRequestProcessor _httpRequestProcessor;
    //    private readonly InstaApi _instaApi;
    //    private readonly IInstaLogger _logger;
    //    private readonly UserSessionData _user;
    //    private readonly UserAuthValidate _userAuthValidate;
    //    public PushProcessor(AndroidDevice deviceInfo, UserSessionData user,
    //        IHttpRequestProcessor httpRequestProcessor, IInstaLogger logger,
    //        UserAuthValidate userAuthValidate, InstaApi instaApi, HttpHelper httpHelper)
    //    {
    //        _deviceInfo = deviceInfo;
    //        _user = user;
    //        _httpRequestProcessor = httpRequestProcessor;
    //        _logger = logger;
    //        _userAuthValidate = userAuthValidate;
    //        _instaApi = instaApi;
    //        _httpHelper = httpHelper;
    //    }

    //    public async Task<bool> RegisterPush()
    //    {
    //        try
    //        {
    //            var instaUri = UriCreator.GetPushRegisterUri();

    //            var data = new Dictionary<string, string>
    //            {
    //                {"product_version", "beta" },
    //                {"wp_token_type","wns" },
    //                {"_csrftoken", _user.CsrfToken},
    //                {"device_token", "https://db5p.notify.windows.com/?token=AwYAAABg4Ep0deAFuOd%2f5%2b9aSMJGsnkDXTEnbu6L7L2haknP2QqxwDHLwvIuzhNmjzN8zFGLgwc7Ni8KUn73HwM9HCMNgbS0FjJOZTLctbDZ03ID6kfBvvDp5N548I8u0i772O%2b1JYPkdvl3AFi060gA2cN7"},
    //                {"_uuid", _deviceInfo.DeviceGuid.ToString()},
    //                {"device_id", _deviceInfo.DeviceId },
    //                {"device_typr","windows_wns" },
    //                {"wp_notif_type","toasttile" },
    //                {"users", _user.LoggedInUser.Pk.ToString() }
    //            };
    //            var request = _httpHelper.GetDefaultRequest(HttpMethod.Post, instaUri, _deviceInfo, data);
    //            var response = await _httpRequestProcessor.SendAsync(request);
    //            var json = await response.Content.ReadAsStringAsync();

    //            if (response.StatusCode != HttpStatusCode.OK)
    //                return Result.UnExpectedResponse<InstaDirectInboxThread>(response, json);
    //            var threadResponse = JsonConvert.DeserializeObject<InstaDirectInboxThreadResponse>(json,
    //                         new InstaThreadDataConverter());

    //            //Reverse for Chat Order
    //            threadResponse.Items.Reverse();
    //            var converter = ConvertersFabric.Instance.GetDirectThreadConverter(threadResponse);


    //            return Result.Success(converter.Convert());
    //        }
    //        catch (Exception exception)
    //        {
    //            _logger?.LogException(exception);
    //            return Result.Fail<InstaDirectInboxThread>(exception);
    //        }
    //    }
    //}
}
