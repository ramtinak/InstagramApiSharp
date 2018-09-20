# InstagramApiSharp ![InstagramApiSharp](http://s8.picofile.com/file/8336601292/insta50x.png)
An complete Private Instagram Api for .NET (C#, VB.NET).

Supports: Create new account, verify account, edit profile, set profile picture and many more...

| Target | Branch | Version | Download link |
| ------ | ------ | ------ | ------ |
| Nuget | master | v1.1.0.9 | [![NuGet](https://img.shields.io/nuget/v/InstagramApiSharp.svg)](https://www.nuget.org/packages/InstagramApiSharp) |


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
| .NET Core(UWP) | 10.0.14393 |

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
| IG TV support | Share media to direct thread |

## Usage
#### Use builder to get Insta API instance:
```c#
var api = new InstaApiBuilder()
                .UseLogger(new SomeLogger())
                .UseHttpClient(new SomeHttpClient())
                .SetUser(new UserCredentials(...You user...))
                .UseHttpClientHandler(httpHandlerWithSomeProxy)
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
Check [Wiki page](https://github.com/ramtinak/InstagramApiSharp/wiki) for documentation.

## Version changes
v1.1.0.9
- [Rename]  SuggestedSearchesAsync to GetSuggestedSearchesAsync in TVProcessor
- [Bugfix]  for GetFullUserInfoAsync
- [Update]  [Direct messaging wiki](https://github.com/ramtinak/InstagramApiSharp/wiki/Direct-messaging)
- [Add]  [TV wiki](https://github.com/ramtinak/InstagramApiSharp/wiki/TV)

v1.1.0.8
- [Add] CheckUsernameAsync to IInstaApi
- [Add] GetRequestForDownloadAccountDataAsync to AccountProcessor
- [Add] Progress changed action to change profile picture
- [Bugfix] for GetFullUserInfoAsync (thx to https://t.me/rohollah for report)
- [Bugfix] for random android version (thx to [@aspmaker](https://github.com/aspmaker) )
- [Cleanup] some functions

v1.1.0.7
- [Bugfix] for ShareMediaToThreadAsync (thx to [@huseyinkarael](https://github.com/huseyinkarael) for report)
- [Bugfix] for image/video uploader (thx to [@alexrepetskyi](https://github.com/alexrepetskyi) for report)
- [Bugfix] for like/unlike comment (thx to [@aspmaker](https://github.com/aspmaker) for report)
- [Bugfix] for GetMediaCommentsAsync (thx to [@aspmaker](https://github.com/aspmaker) for report)
- [Bugfix] for GetMediaRepliesCommentsAsync (thx to [@aspmaker](https://github.com/aspmaker) for report)
- [Add] Some new property to InstaComment
- [Add] LeaveGroupThreadAsync to MessagingProcessor
- [Add] 1 new device to AndroidDevices

v1.1.0.5
- [Add] Progress changed action to every uploading functions(media, story, direct)
- [Bugfix] for random android version (thx to [@aspmaker](https://github.com/aspmaker) )

v1.1.0.2
- [Add] ShareMediaToThreadAsync to MessagingProcessor
- [Add] GetFullUserInfoAsync to UserProcessor
- [Add] 1 new device to AndroidDevices
- [Cleanup] some classes

v1.1.0.1
- [Change] SendDirectMessageAsync to SendDirectTextAsync
- [Fix] SendDirectTextAsync issue
- [Add] DeleteMultipleCommentsAsync to CommentProcessor
- [Add] Like/Unlike comment to CommentProcessor
- [Update] GetMediaCommentsAsync in CommentProcessor
- [Add] Pagination support to GetMediaRepliesCommentsAsync
- [Wiki Update] for [Direct messaging page](https://github.com/ramtinak/InstagramApiSharp/wiki/Direct-messaging) 

v1.1.0.0
- [Add] UploadVideoAsync to TVProcessor
- [Change] User agent to v61.0.0.19.86 for supporting new apis
- [Add] Support FelixShare (igtv shared video) in direct threads
- [Add] Some new properties to direct threads (new api)
- [Add] Some new properties to InstaUserInfo
- [Update] InstaCurrentUser.Gender to InstaGenderType
- [Add] Some new properties to InstaMedia
- [Add] Some new properties to InstaStory
- [Update] InstaTagFeed class
- [Bugfix] for GetMediaRepliesCommentsAsync in CommentProcessor

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
- By using this api in your app, you agreed [Instagram Policy](https://www.instagram.com/about/legal/terms/api/).
- Please don't use this library for sending spam or massive direct messages.

## Donation
If you in Iran, feel free to donate:
https://zarinp.al/@ramtin

## Contribute
Feel free to contribute and submit pull requests.

## Thanks
Special thanks to [mgp25](https://github.com/mgp25) and his [php wrapper](https://github.com/mgp25/Instagram-API/).

## Legal
This code is in no way affiliated with, authorized, maintained, sponsored or endorsed by Instagram or any of its affiliates or subsidiaries. This is an independent and unofficial API wrapper.


## Developers

| Name | Github | Email | Telegram | Instagram |
| ------ | ------ | ------ | ------ | ------ |
| Ramtin Jokar | [@Ramtinak](https://github.com/ramtinak) | [Ramtinak@live.com](mailto:ramtinak@live.com) | https://t.me/Ramtinak | https://instagram.com/Rmt4006 |
| Ali Noshahi | [@NGame1](https://github.com/NGame1) | [NGame1390@hotmail.com](mailto:ngame1390@hotmail.com) | https://t.me/NGameW | https://instagram.com/alingame |



Iranian developers - (c) 2018
