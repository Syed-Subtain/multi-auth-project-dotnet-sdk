
# OAuth 2 Resource Owner Credentials Grant



Documentation for accessing and setting credentials for OAuthROPCG.

## Auth Credentials

| Name | Type | Description | Setter | Getter |
|  --- | --- | --- | --- | --- |
| OAuthClientId | `string` | OAuth 2 Client ID | `OAuthClientId` | `OAuthClientId` |
| OAuthClientSecret | `string` | OAuth 2 Client Secret | `OAuthClientSecret` | `OAuthClientSecret` |
| OAuthUsername | `string` | OAuth 2 Resource Owner Username | `OAuthUsername` | `OAuthUsername` |
| OAuthPassword | `string` | OAuth 2 Resource Owner Password | `OAuthPassword` | `OAuthPassword` |
| OAuthToken | `Models.OAuthToken` | Object for storing information about the OAuth token | `OAuthToken` | `OAuthToken` |



**Note:** Auth credentials can be set using `OAuthROPCGCredentials` in the client builder and accessed through `OAuthROPCGCredentials` method in the client instance.

## Usage Example

### Client Initialization

You must initialize the client with *OAuth 2.0 Resource Owner Password Credentials Grant* credentials as shown in the following code snippet.

```csharp
MultiAuthSample.Standard.MultiAuthSampleClient client = new MultiAuthSample.Standard.MultiAuthSampleClient.Builder()
    .OAuthROPCGCredentials(
        new OAuthROPCGModel.Builder(
            "OAuthClientId",
            "OAuthClientSecret",
            "OAuthUsername",
            "OAuthPassword"
        )
        .Build())
    .Build();
```



Your application must obtain user authorization before it can execute an endpoint call incase this SDK chooses to use *OAuth 2.0 Resource Owner Password Credentials Grant*. This authorization includes the following steps

The `FetchToken()` method will exchange the user's credentials for an *access token*. The access token is an object containing information for authorizing client requests and refreshing the token itself.

```csharp
var authManager = client.OAuthROPCG;

try
{
    OAuthToken token = authManager.FetchToken();
    // re-instantiate the client with OAuth token
    client = client.ToBuilder()
        .OAuthROPCGCredentials(
            client.OAuthROPCGModel.ToBuilder()
                .OAuthToken(token)
                .Build())
        .Build();
}
catch (ApiException e)
{
    // TODO Handle exception
}
```

The client can now make authorized endpoint calls.

### Refreshing the token

An access token may expire after sometime. To extend its lifetime, you must refresh the token.

```csharp
if (authManager.IsTokenExpired())
{
    try
    {
        OAuthToken token = authManager.RefreshToken();
        // re-instantiate the client with OAuth token
        client = client.ToBuilder()
            .OAuthROPCGCredentials(
                client.OAuthROPCGModel.ToBuilder()
                    .OAuthToken(token)
                    .Build())
            .Build();
    }
    catch (ApiException e)
    {
        // TODO Handle exception
    }
}
```

If a token expires, an exception will be thrown before the next endpoint call requiring authentication.

### Storing an access token for reuse

It is recommended that you store the access token for reuse.

```csharp
// store token
SaveTokenToDatabase(client.OAuthROPCGCredentials.OAuthToken);
```

### Creating a client from a stored token

To authorize a client from a stored access token, just set the access token in Configuration along with the other configuration parameters before creating the client:

```csharp
// load token later
OAuthToken token = LoadTokenFromDatabase();

// re-instantiate the client with OAuth token
MultiAuthSampleClient client = client.ToBuilder()
    .OAuthROPCGCredentials(
        client.OAuthROPCGModel.ToBuilder()
            .OAuthToken(token)
            .Build())
    .Build();
```

### Complete example



```csharp
using MultiAuthSample.Standard;
using MultiAuthSample.Standard.Models;
using MultiAuthSample.Standard.Exceptions;
using MultiAuthSample.Standard.Utilities;
using MultiAuthSample.Standard.Authentication;
using System.Collections.Generic;

namespace OAuthTestApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            MultiAuthSample.Standard.MultiAuthSampleClient client = new MultiAuthSample.Standard.MultiAuthSampleClient.Builder()
                .OAuthROPCGCredentials(
                    new OAuthROPCGModel.Builder(
                        "OAuthClientId",
                        "OAuthClientSecret",
                        "OAuthUsername",
                        "OAuthPassword"
                    )
                    .Build())
                .Environment(MultiAuthSample.Standard.Environment.Testing)
                .Build();
            try
            {
                OAuthToken token = LoadTokenFromDatabase();

                // Set the token if it is not set before
                if (token == null)
                {
                    token = client.OAuthROPCGCredentials.FetchToken();
                }

                // re-instantiate the client with OAuth token
                client = client.ToBuilder()
                    .OAuthROPCGCredentials(
                        client.OAuthROPCGModel.ToBuilder()
                            .OAuthToken(token)
                            .Build())
                    .Build();

                // Refresh the token if it is expired
                if (client.OAuthROPCGCredentials.IsTokenExpired())
                {
                    token = client.OAuthROPCGCredentials.RefreshToken();
                    // re-instantiate the client with OAuth token
                    client = client.ToBuilder()
                        .OAuthROPCGCredentials(
                            client.OAuthROPCGModel.ToBuilder()
                                .OAuthToken(token)
                                .Build())
                        .Build();
                }

                SaveTokenToDatabase(token);
            }
            catch (OAuthProviderException e)
            {
                // TODO Handle exception
            }
        }

        static void SaveTokenToDatabase(OAuthToken token)
        {
            //Save token here
        }

        static OAuthToken LoadTokenFromDatabase()
        {
            OAuthToken token = null;
            //token = Get token here
            return token;
        }
    }
}

// the client is now authorized and you can use controllers to make endpoint calls
```


