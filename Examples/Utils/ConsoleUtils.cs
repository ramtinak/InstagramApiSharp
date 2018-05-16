using System;
using InstagramApiSharp.Classes.Models;

namespace Examples.Utils
{
    public static class ConsoleUtils
    {
        public static void PrintMedia(string header, InstaMedia media, int maxDescriptionLength)
        {
            Console.WriteLine(
                $"{header} [{media.User.UserName}]: {media.Caption?.Text.Truncate(maxDescriptionLength)}, {media.Code}, likes: {media.LikesCount}, multipost: {media.IsMultiPost}");
        }
    }
}