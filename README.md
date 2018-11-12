# InstagramApiSharp ![InstagramApiSharp](http://s8.picofile.com/file/8336601292/insta50x.png)
An complete Private Instagram Api for .NET (C#, VB.NET).

Supports: Create new account, verify account, edit profile, set profile picture and many more...

| Target | Branch | Version | Download link |
| ------ | ------ | ------ | ------ |
| Nuget | master | v1.1.4.3 | [![NuGet](https://img.shields.io/nuget/v/InstagramApiSharp.svg)](https://www.nuget.org/packages/InstagramApiSharp) |

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
v1.1.4.3
- [Update] PaginationParameters, change NextId to NextMaxId
- [Add] NextMinId to PaginationParameters (for comments)
- [Bugfix] for GetMediaCommentsAsync
- [Bugfix] for GetMediaRepliesCommentsAsync

v1.1.4.2
- [Bugfix] Unique guid for devices (now instagram won't suspicious to you, relogin required)

v1.1.4.1 
- [Minor bugfix] for GetUserAsync in UserProcessor

v1.1.4.0
- [Bugfix] for RavenReplayChainCount in direct thread item
- [Update] tiny update for story seen uri
- [Add] MarkStoryAsSeenAsync to StoryProcessor
- [Add] GetHighlightMediasAsync to StoryProcessor
- [Add] ReplyToStoryAsync to StoryProcessor
- [Bugfix] for Result.GetResponseType

v1.1.3.8
- [Bugfix] For checkpoint issue thx to [@hermaphros](https://github.com/hermaphros) for solution
- [Bugfix] For GetFullUserInfoAsync
- [Add] FriendshipStatus to InstaUserInfo
- [Add] ProfileContextIds to InstaUserInfo

v1.1.3.7
- [Update] SearchUserByLocationAsync
- [Bugfix] for GetStoryFeedAsync (thx to [@iancona](https://github.com/iancona) for report)
- [Update] LoginAsync
- [Add] ActionBlocked to ResponseType
- Some minor improvements

v1.1.3.6
- [Bugfix] For GetExploreFeedAsync pagination
- [Bugfix] For direct reel share
- [Bugfix] For GetMediaCommentLikersAsync
- [Update] InstaComment
- [Update] ShareMediaToThreadAsync
- [Add] GetRankedRecipientsByUsernameAsync to MessagingProcessor
- [Add] ShareMediaToUser to MessagingProcessor

v1.1.3.5
- [Add] Support reel share in direct threads
- [Add] Remove follower
- [Add] Translate biography
- [Add] Translate comments/captions
- [Add] Search places (in facebook)
- [Bugfix] For SetApiVersion

v1.1.3.4
- [Update] Microsoft.NETCore.UniversalWindowsPlatform (uwp)

v1.1.3.3
- [Bugfix] For direct inbox/thread/pending

v1.1.3.2
- [Add] StoryFeedMedia and ShowOneTapTooltip to InstaStoryItem
- [Add] Get user from nametag image
- [Add] Upload nametag image
- [Add] PaginationParameters to GetDirectInboxAsync
- [Add] PaginationParameters to GetDirectInboxThreadAsync
- [Add] PaginationParameters to GetPendingDirectAsync

v1.1.3.1
- [Add] Get branded content approval to BusinessProcessor
- [Add] Search branded users to BusinessProcessor
- [Add] Enable/disable branded approval to BusinessProcessor
- [Add] Remove/add users to branded whitelist to BusinessProcessor
- [Add] Add users to group threads
- [Bugfix] For uploading photo/video/album

v1.1.3.0
- [Add] Suggested users
- [Add] Follow/Unfollow hashtags
- [Add] Get stories of an hashtag
- [Add] Get recent hashtag medias
- [Add] Get ranked hashtag medias
- [Add] Sync phone contact
- [Add] Get direct users presence
- [Add] Get friendship status for multiple ids
- [Add] Get suggested hashtags
- [Add] Get following hashtags information
- [Add] Get suggestion details
- [Add] Get highlights archive
- [Add] Get highlights archive medias
- [Business wiki page added](https://github.com/ramtinak/InstagramApiSharp/wiki/Business-account)

v1.1.2.8
- [Bugfix] for uploading album (photos)

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
