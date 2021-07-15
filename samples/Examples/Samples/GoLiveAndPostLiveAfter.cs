using InstagramApiSharp.Classes.Models;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Examples.Samples;
using InstagramApiSharp.API;

namespace Examples.Samples
{
    internal class StartLive : IDemoSample
    {
        private readonly IInstaApi InstaApi;

        public StartLive(IInstaApi instaApi)
        {
            InstaApi = instaApi;
        }

        private string key;
        public async Task DoShow()
        {
            try
            {
                var during = await InstaApi.LiveProcessor.GetSuggestedBroadcastsAsync();
                if (during.Succeeded)
                {
                    Console.WriteLine("Live now ID:  " + during.Value);
                    await InstaApi.LiveProcessor.EndAsync(during.Value.ToString());
                }
                else
                {
                    var myVar = await InstaApi.LiveProcessor.CreateAsync(broadcastMessage: "Hello there :D");
                    string BID = "";
                    if (myVar.Succeeded)
                    {
                        BID = myVar.Value.BroadcastId.ToString();
                        Console.WriteLine("BroadcastID:  " + BID);
                        Console.WriteLine("UploadUrl:  " + myVar.Value.UploadUrl);
                        Console.WriteLine("Go to OBS and give the pass, when y/Y the stream will start");
                        key = Console.ReadLine();
                        if (key == "y")
                        {
                            var stream = await InstaApi.LiveProcessor.StartAsync(BID, true);
                            await InstaApi.LiveProcessor.EnableCommentsAsync(BID);
                            while (1 == 1)
                            {
                                Console.WriteLine("f for finish, y for live count, c for chearing them");
                                key = Console.ReadLine();
                                if (key == "y")
                                {
                                    var viewers = await InstaApi.LiveProcessor.GetViewerListAsync(BID);
                                    if (viewers.Succeeded)
                                    {
                                        var count = viewers.Value.Count;
                                        Console.WriteLine("Count is: " + count);
                                        viewers.Value.ForEach(x => Console.WriteLine(x.FullName));
                                    }
                                }
                                if (key == "f")
                                {
                                    Console.WriteLine("Are you sure ? y/n");
                                    key = Console.ReadLine();
                                    if (key == "y")
                                    {
                                        var endLive = await InstaApi.LiveProcessor.EndAsync(BID);
                                        if (endLive.Succeeded)
                                        {
                                            break;
                                        }
                                    }
                                }
                            }
                            Console.WriteLine("Post the live to IGTV? y/n");
                            key = Console.ReadLine();
                            if (key == "y")
                            {
                                var cr = await InstaApi.TVProcessor.GetCreationTools();
                                if (cr.Succeeded)
                                {
                                    var s = await InstaApi.LiveProcessor.GetPostLiveThumbnails(BID);
                                    var thumbnailcount = s.Value.Thumbnails.Count;
                                    Random rnd = new Random();

                                    if (s.Succeeded)
                                    {
                                        while (1 == 1)
                                        {
                                            var thumbnailNumber = rnd.Next(1, thumbnailcount);
                                            using (WebClient client = new WebClient())
                                            {
                                                client.DownloadFile(new Uri(s.Value.Thumbnails[thumbnailNumber]), @"c:\temp\image.jpg");
                                            }
                                            Console.WriteLine($"Accept the Thumbnail {thumbnailNumber} out of {thumbnailcount} in 'c->temp->image.jpg' for IGTV live ? y/n");
                                            key = Console.ReadLine();
                                            if (key == "y")
                                            {
                                                Console.WriteLine($"Thumbnail {thumbnailNumber} will be used.");
                                                break;
                                            }
                                        }

                         //               using (Image image = Image.Load(@"c:\temp\image.jpg")) 
                         //               {
                         //                   image.Mutate(x => x
                         //                        .Resize(1080, 1920));
                         //                   image.Save(@"c:\temp\fb.jpg"); // Automatic encoder selected based on extension.
                         //               }

                                        var pic = new InstaImageUpload
                                        {
                                            ImageBytes = File.ReadAllBytes(@"c:\temp\fb.jpg"), // here use the downloaded image or use another picture and give the path
                                            Height = 1920,
                                            Width = 1080,
                                        };

                                        string title = "Test tittle ";
                                        string caption = "Test caption";
                                        Console.WriteLine("title: " + title +  "Caption: " + caption);
                                        Console.WriteLine("wait...");
                                        var vv2 = await InstaApi.LiveProcessor.AddToPostLiveAsync(pic, title, caption, BID);
                                        while (1 == 1)
                                        {
                                            if (vv2.Succeeded)
                                            {
                                                Console.WriteLine("Succesfully published");
                                                break;
                                            }
                                            Console.WriteLine("IDK WHY ?!?!? Press F9 and bring the curser to line 125 -> AddToPostLiveAsync");
                                        }

                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
