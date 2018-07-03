# InstagramApiSharp
An complete Private Instagram Api for .NET (C#, VB.NET).

Supports: Create new account, verify account, edit profile, set profile picture and many more...

| Target | Branch | Version | Download link |
| ------ | ------ | ------ | ------ |
| Nuget | master | v1.0.2.3 | [![NuGet](https://img.shields.io/nuget/v/InstagramApiSharp.svg)](https://www.nuget.org/packages/InstagramApiSharp)


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

## Overview
There's a lot of functions and bug fix me and [NGame1](https://github.com/NGame1) added to this library.
I've added an [Examples](https://github.com/ramtinak/InstagramApiSharp/tree/master/Examples) to show you what's new and how it's works, you can see [Samples/Account.cs](https://github.com/ramtinak/InstagramApiSharp/blob/master/Examples/Samples/Account.cs), [Samples/Discover.cs](https://github.com/ramtinak/InstagramApiSharp/blob/master/Examples/Samples/Discover.cs) and [Samples/Live.cs](https://github.com/ramtinak/InstagramApiSharp/blob/master/Examples/Samples/Live.cs) to see how it's works.

## Features
Some of features:

|    |    |    |    |
| ------ | ------ | ------ | ------ |
| Login | Login with Facebook | Logout | Create new account |
| Edit profile | Change/remove profile picture | Story settings | Get user explore feed |
| Get user timeline feed | Get all user media by username | Get media by its id | Get user info by its username |
| Get current user info | Get tag feed by tag value | Get current user media | Get followers list |
| Get followers list for logged in user | Get following list | Get recent following activity | Get user tags by username |
| Get direct mailbox | Get recent recipients | Get ranked recipients | Get inbox thread |
| Get recent activity | Like media | Unlike media | Follow user |
| Unfollow user | Set account private | Set account public | Send comment |
| Delete comment | Upload photo | Upload video | Get followings list |
| Delete media (photo/video) | Upload story (photo) | Change password | Send direct message |
| Search location | Get location feed | Collection create/get by id/get all/add items | Support challenge required |

## Usage
#### Use builder to get Insta API instance:
```c#
var api = new InstaApiBuilder()
                .UseLogger(new SomeLogger())
                .UseHttpClient(new SomeHttpClient())
                .SetUser(new UserCredentials(...You user...))
                .UseHttpClient(httpHandlerWithSomeProxy)
                .Build();
```
##### Note: every API method has synchronous implementation as well.

## Contract, warning, note
Every method returns object IS NOT COMPLETED YET!
For example:
```
Task<IResult<object>>
```

## Version changes
v1.0.2.3
- Facebook login added
- Fix GetPendingFriendRequests response issue

v1.0.2.2
- Share story added.
- Access to StoryProcessor added.

v1.0.2.1
- GetStateDataAsString and LoadStateDataFromString added.

v1.0.2
- Inline comments support(send and get)

v1.0.1
- Fix Challenge required api. Now you can verify your email or phone number with challenge required functions.

## Wiki
Wiki page coming soon...

## License
Do whatever you want to do!

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

