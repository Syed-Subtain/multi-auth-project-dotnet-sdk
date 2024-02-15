
# OAuth 2 Client Credentials Grant



Documentation for accessing and setting credentials for OAuthCCG.

## Auth Credentials

| Name | Type | Description | Setter | Getter |
|  --- | --- | --- | --- | --- |
| OAuthClientId | `string` | OAuth 2 Client ID | `OAuthClientId` | `OAuthClientId` |
| OAuthClientSecret | `string` | OAuth 2 Client Secret | `OAuthClientSecret` | `OAuthClientSecret` |
| OAuthToken | `Models.OAuthToken` | Object for storing information about the OAuth token | `OAuthToken` | `OAuthToken` |



**Note:** Auth credentials can be set using `OAuthCCGCredentials` in the client builder and accessed through `OAuthCCGCredentials` method in the client instance.

## Usage Example

### Client Initialization

You must initialize the client with *OAuth 2.0 Client Credentials Grant* credentials as shown in the following code snippet.

```csharp
MultiAuthSample.Standard.MultiAuthSampleClient client = new MultiAuthSample.Standard.MultiAuthSampleClient.Builder()
    .OAuthCCGCredentials(
        new OAuthCCGModel.Builder(
            "OAuthClientId",
            "OAuthClientSecret"
        )
        .Build())
    .Build();
```



Your application must obtain user authorization before it can execute an endpoint call in case this SDK chooses to use *OAuth 2.0 Client Credentials Grant*. This authorization includes the following steps

The `FetchToken()` method will exchange the OAuth client credentials for an *access token*. The access token is an object containing information for authorizing client requests and refreshing the token itself.

```csharp
var authManager = client.OAuthCCG;

try
{
    OAuthToken token = authManager.FetchToken();
    // re-instantiate the client with OAuth token
    client = client.ToBuilder()
        .OAuthCCGCredentials(
            client.OAuthCCGModel.ToBuilder()
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

### Storing an access token for reuse

It is recommended that you store the access token for reuse.

```csharp
// store token
SaveTokenToDatabase(client.OAuthCCGCredentials.OAuthToken);
```

### Creating a client from a stored token

To authorize a client from a stored access token, just set the access token in Configuration along with the other configuration parameters before creating the client:

```csharp
// load token later
OAuthToken token = LoadTokenFromDatabase();

// re-instantiate the client with OAuth token
MultiAuthSampleClient client = client.ToBuilder()
    .OAuthCCGCredentials(
        client.OAuthCCGModel.ToBuilder()
            .OAuthToken(token)
            .Build())
    .Build();
```

### Complete example



```csharp
using MultiAuthSample.Standard;
using MultiAuthSample.Standard.Models;
using MultiAuthSample.Standard.Exceptions;
using MultiAuthSample.Standard.Authentication;
using System.Collections.Generic;
namespace OAuthTestApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            MultiAuthSample.Standard.MultiAuthSampleClient client = new MultiAuthSample.Standard.MultiAuthSampleClient.Builder()
                .OAuthCCGCredentials(
                    new OAuthCCGModel.Builder(
                        "OAuthClientId",
                        "OAuthClientSecret"
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
                    var authManager = client.OAuthCCGCredentials;
                    token = authManager.FetchToken();
                }

                SaveTokenToDatabase(token);
                // re-instantiate the client with OAuth token
                client = client.ToBuilder()
                    .OAuthCCGCredentials(
                        client.OAuthCCGModel.ToBuilder()
                            .OAuthToken(token)
                            .Build())
                    .Build();
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


