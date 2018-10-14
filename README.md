# InstagramApiSharp ![InstagramApiSharp](http://s8.picofile.com/file/8336601292/insta50x.png)
An complete Private Instagram Api for .NET (C#, VB.NET).

Supports: Create new account, verify account, edit profile, set profile picture and many more...

| Target | Branch | Version | Download link |
| ------ | ------ | ------ | ------ |
| Nuget | master | v1.1.3.1 | [![NuGet](https://img.shields.io/nuget/v/InstagramApiSharp.svg)](https://www.nuget.org/packages/InstagramApiSharp) |


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
Check [Wiki pages](https://github.com/ramtinak/InstagramApiSharp/wiki) for documentation.

## Version changes
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

v1.1.2.7
- [Support] DebugLogger for uwp

v1.1.2.6
- [Add] SetRequestDelay to IInstaApi
- [Wiki update] for [FAQ page](https://github.com/ramtinak/InstagramApiSharp/wiki/FAQ)

v1.1.2.5
- [Bugfix] for uploading video album (thx to [@rasaradin](https://github.com/rasaradin) for report)

v1.1.2.4
- [Add] ValidateUrlAsync to BusinessProcessor
- [Add] GetBusinessPartnersButtonsAsync to BusinessProcessor
- [Add] AddOrChangeBusinessButtonAsync to BusinessProcessor
- [Add] RemoveBusinessButtonAsync to BusinessProcessor
- [Add] GetSuggestedCategoriesAsync to BusinessProcessor
- [Add] GetCategoriesAsync to BusinessProcessor
- [Add] GetSubCategoriesAsync to BusinessProcessor
- [Add] SearchCityLocationAsync to BusinessProcessor
- [Add] ChangeBusinessCategoryAsync to BusinessProcessor
- [Add] GetBusinessAccountInformationAsync to BusinessProcessor
- [Add] RemoveBusinessLocationAsync to BusinessProcessor
- [Add] UpdateBusinessInfoAsync to BusinessProcessor

v1.1.2.3
- [Bugfix] for [#58](https://github.com/ramtinak/InstagramApiSharp/issues/58) (thx to [@mstrifonov](https://github.com/mstrifonov) and [@murdock477](https://github.com/murdock477) for report and tests)

v1.1.2.2
- [Change] minimun target platform to 10240(uwp)

v1.1.2.1
- [Update] edit media function (location support)
- [Bugfix] for caption in upload photo [large photo]
- [Bugfix] for caption in upload video [large video]
- [Bugfix] for caption in upload album [large album]

v1.1.2.0
- [Bugfix] for [#55](https://github.com/ramtinak/InstagramApiSharp/issues/55) and add some properties to InstaFeed
- [Bugfix] for [#53](https://github.com/ramtinak/InstagramApiSharp/issues/53) and add some properties to InstaRecentActivityFeed
- [Add] Set accept language to InstaApi (thx to [@Lorymi](https://github.com/Lorymi) )
- [Cleanup] and code refactoring some classes
- [Add] Report media to MediaProcessor
- [Add] Report user to UserProcessor
- [Add] Business support to IInstaApi.BusinessProcessor

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
