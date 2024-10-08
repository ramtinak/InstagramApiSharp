/*
 * This is part of the private version of InstagramApiSharp's library.
 * 
 * This example demonstrate how to draw a text into Image and how to upload a link sticker
 * You need to install SkiSharp's library from nuget (it's a crossplatform library for working with 2d)
 * 
 * 
 * Developer: Ramtin Jokar [ Ramtinak@live.com ]
 * 
 * (c) 2024
 */

using InstagramApiSharp.API;
using InstagramApiSharp.API.Builder;
using InstagramApiSharp.Classes;
using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Classes.SessionHandlers;
using InstagramApiSharp.Enums;
using InstagramApiSharp.Logger;
using SkiaSharp;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ChallengeRequireExample
{
    internal class StoryStickerUploadWithDrawing
    {
        private static IInstaApi InstaApi;
        const string StateFile = "state.bin";


        public static async Task UploadAsync()
        {
            var userSession = new UserSessionData
            {
                UserName = "User",
                Password = "Pass"
            };

            InstaApi = InstaApiBuilder.CreateBuilder()
                .SetUser(userSession)
                .UseLogger(new DebugLogger(LogLevel.All))
                .SetRequestDelay(RequestDelay.FromSeconds(0, 1))
                // Session handler, set a file path to save/load your state/session data
                .SetSessionHandler(new FileSessionHandler { FilePath = StateFile })
                .Build();

            InstaApi.SessionHandler?.Load();

            if (!InstaApi.IsUserAuthenticated)
            {
                // login your account;

                Console.WriteLine("login your account");

                return;
            }

            // drawing and uploading part>
            var inputPath = "D:\\3.jpg"; // your image file
            var outputPath = "D:\\3-2222222.jpg"; // set a output for drawing
            var text = "Check this link"; // our custom text
            var url = "https://microsoft.com"; // url

            // draw our text on image, you can change fontname, font size, foreground color
            // background color, or other things by playing with the DrawText function.

            var drawResult = DrawText(inputPath, outputPath, text);

            // lets calculate X-Y co-ordinates of our text based on Instagram which is between 0 and 1
            var x = drawResult.TextX / drawResult.ImageWidth;
            var y = drawResult.TextY / drawResult.ImageHeight;

            // lets calculate the Width-Height of the drawing part as well
            var width = drawResult.TextWidth / drawResult.ImageWidth;
            var height = drawResult.TextHeight / drawResult.ImageHeight;

            var storyOptions = new InstaStoryUploadOptions();
            // now we have to pass the calculated values to the InstaStoryLinkStickerUpload class
            storyOptions.LinkStickers.Add(new InstaStoryLinkStickerUpload
            {
                X = x,
                Y = y,
                Width = width,
                Height = height,
                Url = url,
                LinkType = InstaStoryLinkType.Web, // MUST BE THIS FOR WEB URLs
                CustomStickerText = text // => Optional, This doesn't appear in InstaMedia respond,---
                                         //// just tells instagram that we used custom sticker text
            });
            // set up InstaImage based on our new Image with text drawing
            var image = new InstaImage 
            { 
                Uri = outputPath,
                Width = (int)width,
                Height = (int)height 
            };

            // Validate url by Instagram
            var validateUrlResult = await InstaApi.StoryProcessor.ValidateUriAsync(new Uri(url));

            if (validateUrlResult.Succeeded)
            {
                var storyTest = await InstaApi.StoryProcessor.UploadStoryPhotoAsync(image, caption:null, storyOptions);

            }
        }
        public static DrawingResult DrawText(string inputPath,
            string outputPath,
            string text,
            string fontName = "Arial",
            float fontSize = 65.0f,
            int quality = 90)
        {
            var result = new DrawingResult();
            var resizeFactor = 1f;
            var bitmap = SKBitmap.Decode(inputPath);
            var canvas = new SKCanvas(bitmap);

            canvas.SetMatrix(SKMatrix.CreateScale(resizeFactor, resizeFactor));
            canvas.DrawBitmap(bitmap, 0, 0);
            canvas.ResetMatrix();

            result.ImageHeight = bitmap.Height;
            result.ImageWidth = bitmap.Width;

            var font = SKTypeface.FromFamilyName(fontName,
                SKFontStyleWeight.Normal,
                SKFontStyleWidth.Normal,
                SKFontStyleSlant.Upright);

            var foregroundBrush = new SKPaint
            {
                Typeface = font,
                TextSize = fontSize,
                Color = SKColors.Black
            };
            var backgroundBrush = new SKPaint
            {
                Typeface = font,
                TextSize = fontSize,
                Color = SKColors.White
            };
            SKRect rect = new SKRect();
            var textWidth = foregroundBrush.MeasureText(text, ref rect);
            var x = (bitmap.Width * resizeFactor / 2.0f) - (textWidth / 2.0f);
            var y = bitmap.Height * resizeFactor / 2.0f;

            var width = rect.Width + 25;
            var height = rect.Height + 25;

            canvas.DrawRoundRect(x - 10, y - rect.Height - 5, width, height, 15.0f, 15.0f, backgroundBrush);
            canvas.DrawText(text, x, y, foregroundBrush);

            canvas.Flush();

            var image = SKImage.FromBitmap(bitmap);
            var data = image.Encode(SKEncodedImageFormat.Jpeg, quality);

            using (var stream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                data.SaveTo(stream);
            }

            data.Dispose();
            image.Dispose();
            canvas.Dispose();
            backgroundBrush.Dispose();
            foregroundBrush.Dispose();
            font.Dispose();
            bitmap.Dispose();

            result.TextWidth = width;
            result.TextHeight = height;
            result.TextX = x;
            result.TextY = y;

            return result;
        }
    }
    public struct DrawingResult
    {
        public float ImageWidth { get; set; }
        public float ImageHeight { get; set; }
        public float TextWidth { get; set; }
        public float TextHeight { get; set; }
        public float TextX { get; set; }
        public float TextY { get; set; }
    }
}
