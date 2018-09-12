using System;
using System.Threading.Tasks;
using InstagramApiSharp.API;
/////////////////////////////////////////////////////////////////////
////////////////////// IMPORTANT NOTE ///////////////////////////////
// Please check wiki pages for more information:
// https://github.com/ramtinak/InstagramApiSharp/wiki
////////////////////// IMPORTANT NOTE ///////////////////////////////
/////////////////////////////////////////////////////////////////////
namespace Examples.Samples
{
    internal class CommentMedia : IDemoSample
    {
        private readonly IInstaApi _instaApi;

        public CommentMedia(IInstaApi instaApi)
        {
            _instaApi = instaApi;
        }

        public async Task DoShow()
        {
            var commentResult = await _instaApi.CommentProcessor.CommentMediaAsync("", "Hi there!");
            Console.WriteLine(commentResult.Succeeded
                ? $"Comment created: {commentResult.Value.Pk}, text: {commentResult.Value.Text}"
                : $"Unable to create comment: {commentResult.Info.Message}");
        }
    }
}