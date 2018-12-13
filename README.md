# InstagramApiSharp ![InstagramApiSharp](http://s8.picofile.com/file/8336601292/insta50x.png)
An complete Private Instagram Api for .NET (C#, VB.NET).

Supports: Create new account, verify account, edit profile, set profile picture and many more...

| Target | Branch | Version | Download link |
| ------ | ------ | ------ | ------ |
| Nuget | master | v1.2.0.1 | [![NuGet](https://img.shields.io/nuget/v/InstagramApiSharp.svg)](https://www.nuget.org/packages/InstagramApiSharp) |

## IMPORTANT NOTE:
`PaginationParameters` updated, you must use `NextMaxId` instead of using old `NextId`!!!!!

## Note
This library is based on [InstaSharper](https://github.com/a-legotin/InstaSharper) with more functions and flexibility.

## Install
Use this library as dll, reference from [NuGet](https://www.nuget.org/packages/InstagramApiSharp/).

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

## Overview
There's a lot of functions and bug fix me and [NGame1](https://github.com/NGame1) added to this library.
Check [sample projects](https://github.com/ramtinak/InstagramApiSharp/tree/master/samples) and [wiki pages](https://github.com/ramtinak/InstagramApiSharp/wiki) to see how it's works.

## Features
Some of features:

|    |    |    |    |
| ------ | ------ | ------ | ------ |
| Login | Login with Facebook | Logout | Create new account email/phone number |
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
| Upload album (videos/photo) | Highlight support | Share story | Send direct photo/video/ stories/profile/ link/location |
| IG TV support | Share media to direct thread | Business account support |

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

## Contract, warning, note
Every method returns object IS NOT COMPLETED YET!
For example:
```
Task<IResult<object>>
```

## Wiki
Check [Wiki pages](https://github.com/ramtinak/InstagramApiSharp/wiki) for documentation.

## Version changes
v1.2.0.1
- [Bugfix] for recent activities return following activities

v1.2.0
- [Add] ISessionHandler for save/load session to IInstaApiBuilder (thx to [@estgold](https://github.com/estgold) for PR)
- [Bugfix] for GetSecuritySettingsInfoAsync
- [Add] GetBlockedUsersAsync to UserProcessor
- [Add] SwitchToPersonalAccountAsync to AccountProcessor
- [Add] SetHttpRequestProccesor in IInstaApiBuilder (thx to [@estgold](https://github.com/estgold) for PR)
- [Add] GetRecoveryOptionsAsync to IInstaApi
- [Bugfix] for VerifyCodeForChallengeRequireAsync
- [Rename] GetBlockedStorySharingUsersStory to GetBlockedUsersFromStoriesAsync
- [Add] voice message support to direct thread item (Api version v74 or newer is required => `InstaApi.SetApiVersion(InstaApiVersionType.Version74)`)
- [Add] animated image (gif) message support to direct thread item (Api version v74 or newer is required => `InstaApi.SetApiVersion(InstaApiVersionType.Version74)`)

v1.1.6.2
- [Update] A change in builder to made it IoC/DI compatible

v1.1.6.1
- [Bugfix] for GetFollowingRecentActivityFeedAsync pagination [now works correctly]
- [Add] new UploadAlbumAsync (check [#95 issue](https://github.com/ramtinak/InstagramApiSharp/issues/95))
- [Add] GetBlockedMediasAsync to MediaProcessor
- [Add] GetMediaByIdsAsync to MediaProcessor for getting multiple medias

v1.1.6.0
- [Bugfix] for GetFollowingRecentActivityFeedAsync pagination
- [Add] InstaImageUpload class (use this for uploading photo/album from now)
- [Add] User tags support to UploadAlbumAsync (see [album wiki page](https://github.com/ramtinak/InstagramApiSharp/wiki/Upload-album))
- [Update] UploadPhotoAsync (you should use InstaImageUpload instead! see [photo wiki page](https://github.com/ramtinak/InstagramApiSharp/wiki/Upload-photo))

v1.1.5.5
- [Update] Result with more error support
- [Add] MutualFirst flag added to GetUserFollowersAsync

v1.1.5.2
- [Add] ability to request verification code again (thx to [@sh2ezo](https://github.com/sh2ezo) for PR)
- [Support] NetStandard 1.3
- [Update] InstaRecentActivityConverter
- [Add] PaginationParameters support to GetCollectionsAsync
- [Rename] GetCollectionAsync to GetSingleCollectionAsync
- [Add] PaginationParameters support to GetSingleCollectionAsync
- [Add] GetLocationInfoAdd GetLocationInfo to LocationProcessor to LocationProcessor
- [Add] EditCollectionAsync to CollectionProcessor

v1.1.5.1
- [Add] to VerifyEmailByVerificationUriAsync to AccountProcessor
- [Add] DeleteDirectThreadAsync to MessagingProcessor
- [Add] DeleteSelfMessageAsync to MessagingProcessor

v1.1.5.0
- [Bugfix] for GetUserAsync
- [Add] user tags support to UploadPhotoAsync
- [Add] user tags edit to EditMediaAsync
- [Add] Product tags to InstaMedia
- [Add] user tag support to InstaCarouselItem
- [Add] ShoppingProcessor
- [Add] GetUserShoppableMediaAsync to ShoppingProcessor and UserProcessor
- [Add] GetProductInfoAsync to ShoppingProcessor
- [Add] MarkUserAsOverageAsync to UserProcessor
- [Add] FavoriteUserAsync and UnFavoriteUserAsync to UserProcessor
- [Add] FavoriteUserStoriesAsync and UnFavoriteUserStoriesAsync to UserProcessor
- [Add] MuteUserMediaAsync and UnMuteUserMediaAsync to UserProcessor
- [Add] HideMyStoryFromUserAsync and UnHideMyStoryFromUserAsync to UserProcessor
- [Add] MuteFriendStoryAsync and UnMuteFriendStoryAsync to UserProcessor
- [Add] GetBlockedStorySharingUsersStory to StoryProcessor

v1.1.4.4
- [Bugfix] for phone number/email login (now you can login with phone/email to as well) (spectial thanks to [@learn-itnow](https://github.com/learn-itnow) for his help)

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

## Donation
No need to donate.

## Contribute
Feel free to contribute and submit pull requests.

## Thanks
Special thanks to [mgp25](https://github.com/mgp25) and his [php wrapper](https://github.com/mgp25/Instagram-API/).

## Legal
This code is in no way affiliated with, authorized, maintained, sponsored or endorsed by Instagram or any of its affiliates or subsidiaries. This is an independent and unofficial API wrapper.

## Developers

| Name | Github | Email | Telegram | Instagram |
| ------ | ------ | ------ | ------ | ------ |
| Ramtin Jokar | [@Ramtinak](https://github.com/ramtinak) | [Ramtinak@live.com](mailto:ramtinak@live.com) | - | - |
| Ali Noshahi | [@NGame1](https://github.com/NGame1) | [NGame1390@hotmail.com](mailto:ngame1390@hotmail.com) | https://t.me/NGameW | https://instagram.com/alingame |



Iranian developers - (c) 2018 | Tabestan & Paeez 1397.
