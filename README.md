# Exaroton API for C#
## NOTICE: This package is not officially endorsed or affilaited with exaroton
`Exaroton` is a C# class that provides a convenient interface for [exaroton](https://exaroton.com/), a Minecraft server hosting service.

With this package, it is now as easy as creating an `Exaroton` object. The object will provide methods that makes it easy to interact and manage your exaroton-hosted Minecraft servers!

## How to Use
Simply create an `Exaroton` object and input your API key, which can be found [here](https://exaroton.com/account/), as a parameter.:

`Exaroton apiInstance = new Exaroton('YOUR_API_KEY_GOES_HERE')`

From there, you can interface with the Exaroton API from a single instance! For example, if you wanted to retrieve a list of servers, here's how you would do it:

`var servers = api.GetServers().Result.Data`

From there, you can look at the different servers associated with your exaroton account.

This is just a brief example. I plan to use readthedocs.org for better documentation in the future!
