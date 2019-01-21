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
        private readonly IInstaApi InstaApi;

        public CommentMedia(IInstaApi instaApi)
        {
            InstaApi = instaApi;
        }

        public async Task DoShow()
        {
            var commentResult = await InstaApi.CommentProcessor.CommentMediaAsync("", "Hi there!");
            Console.WriteLine(commentResult.Succeeded
                ? $"Comment created: {commentResult.Value.Pk}, text: {commentResult.Value.Text}"
                : $"Unable to create comment: {commentResult.Info.Message}");
        }
    }
}