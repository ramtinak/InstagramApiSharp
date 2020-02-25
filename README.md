# InstagramApiSharp ![InstagramApiSharp](http://s8.picofile.com/file/8336601292/insta50x.png)
A complete Private Instagram API for .NET (C#, VB.NET).

Supports almost every features that Instagram app has!

IRANIAN DEVELOPERS
-----
InstagramApiSharp's private version - Under developing. Check [#243](https://github.com/ramtinak/InstagramApiSharp/issues/243)

| Target | Branch | Version | Changelog |
| ------ | ------ | ------ | ------ |
| Github | master | v2.1.2 | [v2.1.2](https://github.com/ramtinak/InstagramApiSharp/issues/244#issuecomment-568762999) |


-----
InstagramApiSharp's public version - Update date: February 24, 2020 - End Of Support(from April 5, 2019)!!!

| Target | Branch | Version | Download link | Total downloads |
| ------ | ------ | ------ | ------ | ------ |
| Nuget | master | v1.5.0.0 | [![NuGet](https://img.shields.io/nuget/v/InstagramApiSharp.svg)](https://www.nuget.org/packages/InstagramApiSharp) | [![NuGet downloads](https://img.shields.io/nuget/dt/InstagramApiSharp.svg)](https://www.nuget.org/packages/InstagramApiSharp) |
| Release | master | v1.5.0.0 | [![Release](http://s9.picofile.com/file/8353468992/releases.PNG)](https://github.com/ramtinak/InstagramApiSharp/releases/latest) | |

## WARNING!!!
Use [InstagramApiSharp](https://github.com/ramtinak/InstagramApiSharp/)'s free versions at your own risk! I'm not responsible for free versions anymore!!!!

## IMPORTANT NOTE:
`PaginationParameters` updated, you must use `NextMaxId` instead of using old `NextId`!!!!!

## Note
This library is based on [InstaSharper](https://github.com/a-legotin/InstaSharper) with more features and new cool things.

## Install
Use this library as dll (download from [release page](https://github.com/ramtinak/InstagramApiSharp/releases)) or reference it from [NuGet](https://www.nuget.org/packages/InstagramApiSharp/).

Nuget package manager command:
```
PM> Install-Package InstagramApiSharp
```

Note: this library uses [Json.NET v10.0.3 and above](https://www.nuget.org/packages/Newtonsoft.Json/) for serialize and deserialize json.

## Cross Platform
| Platform | Supported Version |
| ------ | ------ |
| .NET Framework | 4.5.2 |
| .NET Standard | 1.3 |
| .NET Standard | 2.0 |
| .NET Core(UWP) | 10.0.10240 |

## Donation
Paypal: [Donate](https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=9FVAXHZH2MAKW&source=url)

## Overview
There are a lot of features and bug fix me and [NGame1](https://github.com/NGame1) and [other contributors](https://github.com/ramtinak/InstagramApiSharp/graphs/contributors) added to this library.
Check [sample projects](https://github.com/ramtinak/InstagramApiSharp/tree/master/samples) and [wiki pages](https://github.com/ramtinak/InstagramApiSharp/wiki) to see how it's works.

## Features
Some of features:

|    |    |    |    |
| ------ | ------ | ------ | ------ |
| Login | Login with cookies | Logout | Create new account email/phone number |
| Edit profile | Change/remove profile picture | Story settings | Get user explore feed |
| Get user timeline feed | Get all user media by username | Get media by its id | Get user info by its username |
| Get current user info | Get tag feed by tag value | Get current user media | Get followers list |
| Get followers list for logged in user | Get following list | Get recent following activity | Get user tags by username |
| Get direct mailbox | Get recent recipients | Get ranked recipients | Get inbox thread |
| Get recent activity | Like media | Unlike media | Follow user |
| Unfollow user | Set account private | Set account public | Send comment |
| Delete comment | Upload photo | Upload video | Get followings list |
| Delete media (photo/video/album) | Upload story (photo/video/album) | Change password | Send direct message |
| Search location | Get location feed | Collection create/get by id/get all/add items | Support challenge required |
| Upload album (videos/photo) | Highlight support | Share story | Send direct photo/video/ stories/profile/ link/location like/live |
| IG TV support | Share media to direct thread | Business account support | Share media as story |

## Usage
#### Use builder to get Insta API instance:
```c#
var api = InstaApiBuilder.CreateBuilder()
                // required
                .SetUser(new UserSessionData(...Your user...))
                // optional
                .UseLogger(new SomeLogger())
                // optional
                .UseHttpClient(new SomeHttpClient())
                // optional
                .UseHttpClientHandler(httpHandlerWithSomeProxy)
                // optional
                .SetRequestDelay(new SomeRequestDelay())
                // optional
                .SetApiVersion(SomeApiVersion)
                .Build();
```
##### Note: every API method has synchronous implementation as well.

## Important note about me (@ramtinak)
I don't use Instagram at all and I don't like it either, so when you across to an issue or wanna new feature, describe it as much as you can!

## Contract, warning, note
Every method returns object IS NOT COMPLETED YET!
For example:
```
Task<IResult<object>>
```

## Wiki
Check [Wiki pages](https://github.com/ramtinak/InstagramApiSharp/wiki) for documentation.

## Version changes
v1.5.0.1
- [Bugfix] for adding header

v1.5.0.0
- [Update] api version and signature key to v126.0.0.25.121
- [Update] Headers and constants
- [Update] GetUserTimelineFeedAsync, Constants and some others
- [Update] a lot of things related to login and other things
- [Add] SendRequestsBeforeLoginAsync to InstaApi
- [Add] SendRequestsAfterLoginAsync to InstaApi

v1.4.0.1
- Change license to MIT

v1.4.0.0
- [Add] GetTopicalExploreFeedAsync to FeedProcessor (new explore page)
	Supports explore categories, ig tv videos

v1.3.5.0
- [Add] UserTag support in InstaVideoUpload (video upload with user tags)
- [Add] video usertags support for uploading album

v1.3.4.6
- [Bugfix] for slider emoji in story upload
- [Add] CropWidth and CropHeight as parameter for CreateHighlightFeedAsync (thx to [@szokeptr](https://github.com/szokeptr))
- [Add] LastPermanentItem property to InstaDirectInboxThread by [@estgold](https://github.com/estgold)
- [Add] RelatedHashtags to GetTopHashtagMediaListAsync 
- [Update] GetTopHashtagMediaListAsync
- [Update] GetRecentHashtagMediaListAsync
- [Update] api version to 86.0.0.24.87 (it's default now)

v1.3.4.5
- [Add] GetLoggedInChallengeDataInfoAsync to IInstaApi
- [Add] AcceptChallengeAsync to IInstaApi [THIS IS ME feature]  (check [#204](https://github.com/ramtinak/InstagramApiSharp/issues/#204))
- [Bugfix] for highlights media converter
- [Bugfix] for challenge response for logged in user

v1.3.4.4
- [Bugfix] for login with 2FA enabled (thx to [@vivan-snapretail](https://github.com/vivan-snapretail) for report)
- [Add] Dashmanifest property for postlive
- [Add] Support Uploading Story Countdown in InstaStoryUploadOptions
- [Add] GetCountdownsStoriesAsync to StoryProcessor
- [Add] FollowCountdownStoryAsync to StoryProcessor
- [Add] UnFollowCountdownStoryAsync to StoryProcessor

v1.3.4.3
- [Bugfix] for BusinessProcessor.GetStatisticsAsync [ by [@estgold](https://github.com/estgold) ]
- [Bugfix] for GetStoryMediaViewersAsync (thx to [@tommasoceschia](https://github.com/tommasoceschia) for report)
- [Add] HttpRequestProcessor to IInstaApi
- [Add] GetApiVersionInfo to IInstaApi
- [Add] GetUserAgent to IInstaApi

v1.3.4.2
- [Bugfix] for InstaRecentActivityConverter timestamp for different cultures [ by [@estgold](https://github.com/estgold) ]
- [Bugfix] for media products (thx to [@tommasoceschia](https://github.com/tommasoceschia) for report)

v1.3.4.1
- [Bugfix] for UploadStoryPhotoWithUrlAsync
- [Bugfix] for ProductTags (thx to [@tommasoceschia](https://github.com/tommasoceschia) for report)
- [Add] some new properties to InstaProduct class
- [Add] support for Questions in InstaStoryUploadOptions
- [Update] facebook login function
- [Update] story slider

[Version changes](https://github.com/ramtinak/InstagramApiSharp/wiki/Version-changes) page

## Known Issues
Nothing!!!!

## [InstaPost](https://github.com/ramtinak/InstaPost/) app.
You can download source code or app from [InstaPost](https://github.com/ramtinak/InstaPost/) github page.
![InstaPost](http://s9.picofile.com/file/8335529176/sc1.PNG)
![InstaPost](http://s8.picofile.com/file/8335529250/sc5.PNG)

Note: [InstaPost](https://github.com/ramtinak/InstaPost/) app is just an old example that uses [InstagramApiSharp v1.1.5.2](https://www.nuget.org/packages/InstagramApiSharp/1.1.5.2) which is very old version!!! 
Some of codes may not working well or even deprecated in new [InstagramApiSharp](https://github.com/ramtinak/InstagramApiSharp) versions.
I suggest you to update library to the latest [nuget version](https://www.nuget.org/packages/InstagramApiSharp) or use latest [release package](https://github.com/ramtinak/InstagramApiSharp/releases) and update [InstaPost](https://github.com/ramtinak/InstaPost/) codes that is not working or deprecated

## Language
You can ask questions or report issues in Persian or English language.
I can't answer to other languages, because I don't understand them.

## License
MIT.

## Terms and conditions
- Use this Api at your own risk.

## Contribute
Feel free to contribute and submit pull requests.

## Thanks

[estgold](https://github.com/estgold) for contribution

[ADeltaX](https://github.com/ADeltaX) for contribution

[vitalragaz](https://github.com/vitalragaz) for contribution

[n0ise9914](https://github.com/n0ise9914) for contribution

Special thanks to [all other contributors](https://github.com/ramtinak/InstagramApiSharp/graphs/contributors).

Special thanks to [all other InstaSharper contributors](https://github.com/a-legotin/InstaSharper/graphs/contributors).

Special thanks to [mgp25](https://github.com/mgp25) and his [php wrapper](https://github.com/mgp25/Instagram-API/).

## Legal
This code is in no way affiliated with, authorized, maintained, sponsored or endorsed by Instagram or any of its affiliates or subsidiaries. This is an independent and unofficial API wrapper.

## Developers
Private version

| Name | Github | Email | Telegram |
| ------ | ------ | ------ | ------ |
| Ramtin Jokar | [@Ramtinak](https://github.com/ramtinak) | [Ramtinak@live.com](mailto:ramtinak@live.com) | https://t.me/ramtinak |

Public version - End Of Support

| Name | Github | Email | Telegram | Instagram |
| ------ | ------ | ------ | ------ | ------ |
| Ramtin Jokar | [@Ramtinak](https://github.com/ramtinak) | [Ramtinak@live.com](mailto:ramtinak@live.com) | - | - |
| Ali Noshahi | [@NGame1](https://github.com/NGame1) | [NGame1390@hotmail.com](mailto:ngame1390@hotmail.com) | https://t.me/NGameW | https://instagram.com/alingame |



Iranian developers - (c) 2020 | Zemestan 1398.
