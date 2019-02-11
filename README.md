# InstagramApiSharp ![InstagramApiSharp](http://s8.picofile.com/file/8336601292/insta50x.png)
An complete Private Instagram Api for .NET (C#, VB.NET).

Supports almost every features that Instagram app has!

| Target | Branch | Version | Download link |
| ------ | ------ | ------ | ------ |
| Nuget | master | v1.3.3.3 | [![NuGet](https://img.shields.io/nuget/v/InstagramApiSharp.svg)](https://www.nuget.org/packages/InstagramApiSharp) |

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

Buy me a coffee if you like this project: https://buymeacoff.ee/rXdVTHQkh

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
v1.3.3.3
- [Bugfix] for GetUserFollowersAsync
- [Bugfix] for GetUserFollowingAsync
- [Rename] GetLocationFeedAsync to GetLocationStoriesAsync
- [Rename] InstaHashtagMediaList to InstaSectionMediaList
- [Add] some new properties to PaginationParameters
- [Add] GetTopLocationFeedsAsync to LocationProcessor 
- [Add] GetRecentLocationFeedsAsync to LocationProcessor
- [Add] GetAccountDetailsAsync to BusinessProcessor
- [Update] GetRecentHashtagMediaListAsync pagination 
- [Update] GetUserTimelineFeedAsync pull refresh

v1.3.3.2
- [Add] InstaStoryFriendshipStatus class
- [Update] GetStoryFeedAsync
- [Update] GetUserStoryAsync
- [Update] GetFullUserInfoAsync
- [Update] GetFriendshipStatusAsync
- [Update] BlockUserAsync/UnBlockUserAsync
- [Update] IgnoreFriendshipRequestAsync
- [Update] HideMyStoryFromUserAsync/UnHideMyStoryFromUserAsync
- [Update] MuteFriendStoryAsync/UnMuteFriendStoryAsync
- [Update] MuteUserMediaAsync/UnMuteUserMediaAsync
- [Update] FollowUserAsync/UnFollowUserAsync
- [Update] InstaStory.FriendshipStatus (support muting)
- [Update] InstaFullUserInfo.UserDetail.FriendshipStatus
- [Update] InstaStoryFeed.Broadcasts
- [Update] InstaStoryFeed.PostLives
- [Update] InstaReelFeed.FriendshipStatus

v1.3.3.1
- [Add] ShareMediaAsStoryAsync to StoryProcessor
- [Add] HasViewerSaved property to InstaMedia
- [Add] converters for all live broadcast classes
- [Add] pagination to GetDiscoverTopLiveAsync
- [Add] LoginWithFacebookAsync to IInstaApi

v1.3.3.0
- [Bugfix] for followers and followings get functions
- [Bugfix] for SendGetRequestAsync
- [Bugfix] for GetTopHashtagMediaListAsync (thx to [@huseyinkarael](https://github.com/huseyinkarael) for report)
- [Bugfix] for GetRecentHashtagMediaListAsync (thx to [@huseyinkarael](https://github.com/huseyinkarael) for report)
- [Bugfix] for GetArchivedMediaAsync pagination
- [Bugfix] for GetUserTagsAsync (thx to [@hamidrh](https://github.com/hamidrh) for bugfix)
- [Rename] GetLikeFeedAsync to GetLikedFeedAsync
- [Rename] SendLikeAsync to SendDirectLikeAsync
- [Add] GetUserFollowersByIdAsync to UserProcessor
- [Add] GetUserFollowingByIdAsync to UserProcessor 
- [Add] SaveMediaAsync to MediaProcessor
- [Add] UnSaveMediaAsync to MediaProcessor
- [Add] GetSavedFeedAsync to FeedProcessor
- [Add] support story question in InstaStoryItem
- [Add] GetUserShoppableMediaByIdAsync to ShoppingProcessor
- [Add] GetUserMediaByIdAsync to UserProcessor
- [Add] GetBestFriendsAsync to UserProcessor
- [Add] GetBestFriendsSuggestionsAsync to UserProcessor
- [Add] AddBestFriendsAsync to UserProcessor
- [Add] DeleteBestFriendsAsync to UserProcessor
- [Update] GetUserTagsAsync pagination
- [Update] GetTagFeedAsync pagination
- [Update] GetUserTimelineFeedAsync pagination
- [Update] GetSavedFeedAsync
- [Update] GetLikedFeedAsync pagination
- [Update] GetRecentHashtagMediaListAsync pagination
- [Update] GetTopHashtagMediaListAsync pagination
- [Update] ApproveDirectPendingRequestAsync
- [Update] GetUserShoppableMediaAsync pagination
- [Update] GetUserShoppableMediaAsync pagination

[Version changes](https://github.com/ramtinak/InstagramApiSharp/wiki/Version-changes) page

## Known Issues
Nothing!!!!

## [InstaPost](https://github.com/ramtinak/InstaPost/) app.
You can download source code or app from [InstaPost](https://github.com/ramtinak/InstaPost/) github page.
![InstaPost](http://s9.picofile.com/file/8335529176/sc1.PNG)
![InstaPost](http://s8.picofile.com/file/8335529250/sc5.PNG)

## Language
You can ask questions or report issues in Persian or English language.
I can't answer to other languages, because I don't understand them.

## License
Do whatever you want to do! Except changing library name!!!!

## Terms and conditions
- Use this Api at your own risk.

## Contribute
Feel free to contribute and submit pull requests.

## Thanks
Special thanks to [all contributors](https://github.com/ramtinak/InstagramApiSharp/graphs/contributors).

Special thanks to [mgp25](https://github.com/mgp25) and his [php wrapper](https://github.com/mgp25/Instagram-API/).

## Legal
This code is in no way affiliated with, authorized, maintained, sponsored or endorsed by Instagram or any of its affiliates or subsidiaries. This is an independent and unofficial API wrapper.

## Developers

| Name | Github | Email | Telegram | Instagram |
| ------ | ------ | ------ | ------ | ------ |
| Ramtin Jokar | [@Ramtinak](https://github.com/ramtinak) | [Ramtinak@live.com](mailto:ramtinak@live.com) | - | - |
| Ali Noshahi | [@NGame1](https://github.com/NGame1) | [NGame1390@hotmail.com](mailto:ngame1390@hotmail.com) | https://t.me/NGameW | https://instagram.com/alingame |



Iranian developers - (c) 2019 | Tabestan - Zemestan 1397.
