
# OAuth 2 Authorization Code Grant



Documentation for accessing and setting credentials for OAuthACG.

## Auth Credentials

| Name | Type | Description | Setter | Getter |
|  --- | --- | --- | --- | --- |
| OAuthClientId | `string` | OAuth 2 Client ID | `OAuthClientId` | `OAuthClientId` |
| OAuthClientSecret | `string` | OAuth 2 Client Secret | `OAuthClientSecret` | `OAuthClientSecret` |
| OAuthRedirectUri | `string` | OAuth 2 Redirection endpoint or Callback Uri | `OAuthRedirectUri` | `OAuthRedirectUri` |
| OAuthToken | `Models.OAuthToken` | Object for storing information about the OAuth token | `OAuthToken` | `OAuthToken` |
| OAuthScopes | `List<Models.OAuthScopeOAuthACGEnum>` | List of scopes that apply to the OAuth token | `OAuthScopes` | `OAuthScopes` |



**Note:** Auth credentials can be set using `OAuthACGCredentials` in the client builder and accessed through `OAuthACGCredentials` method in the client instance.

## Usage Example

### 1\. Client Initialization

You must initialize the client with *OAuth 2.0 Authorization Code Grant* credentials as shown in the following code snippet.

```csharp
MultiAuthSample.Standard.MultiAuthSampleClient client = new MultiAuthSample.Standard.MultiAuthSampleClient.Builder()
    .OAuthACGCredentials(
        new OAuthACGModel.Builder(
            "OAuthClientId",
            "OAuthClientSecret",
            "OAuthRedirectUri"
        )
        .OAuthScopes(
            new List<OAuthScopeOAuthACGEnum>
            {
                OAuthScopeOAuthACGEnum.ReadScope,
            })
        .Build())
    .Build();
```



Your application must obtain user authorization before it can execute an endpoint call incase this SDK chooses to use *OAuth 2.0 Authorization Code Grant*. This authorization includes the following steps

### 2\. Obtain user consent

To obtain user's consent, you must redirect the user to the authorization page.The `BuildAuthorizationUrl()` method creates the URL to the authorization page. You must have initialized the client with scopes for which you need permission to access.

```csharp
string authUrl = client.OAuthACGCredentials.BuildAuthorizationUrl();
```

### 3\. Handle the OAuth server response

Once the user responds to the consent request, the OAuth 2.0 server responds to your application's access request by redirecting the user to the redirect URI specified set in `Configuration`.

If the user approves the request, the authorization code will be sent as the `code` query string:

```
https://example.com/oauth/callback?code=XXXXXXXXXXXXXXXXXXXXXXXXX
```

If the user does not approve the request, the response contains an `error` query string:

```
https://example.com/oauth/callback?error=access_denied
```

### 4\. Authorize the client using the code

After the server receives the code, it can exchange this for an *access token*. The access token is an object containing information for authorizing client requests and refreshing the token itself.

```csharp
var authManager = client.OAuthACG;

try
{
    OAuthToken token = authManager.FetchToken();
    // re-instantiate the client with OAuth token
    client = client.ToBuilder()
        .OAuthACGCredentials(
            client.OAuthACGModel.ToBuilder()
                .OAuthToken(token)
                .Build())
        .Build();
}
catch (ApiException e)
{
    // TODO Handle exception
}
```

### Scopes

Scopes enable your application to only request access to the resources it needs while enabling users to control the amount of access they grant to your application. Available scopes are defined in the [`OAuthScopeOAuthACGEnum`](../../doc/models/o-auth-scope-o-auth-acg-enum.md) enumeration.

| Scope Name | Description |
|  --- | --- |
| `READ_SCOPE` | Read request for files |

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
            .OAuthACGCredentials(
                client.OAuthACGModel.ToBuilder()
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
SaveTokenToDatabase(client.OAuthACGCredentials.OAuthToken);
```

### Creating a client from a stored token

To authorize a client from a stored access token, just set the access token in Configuration along with the other configuration parameters before creating the client:

```csharp
// load token later
OAuthToken token = LoadTokenFromDatabase();

// re-instantiate the client with OAuth token
MultiAuthSampleClient client = client.ToBuilder()
    .OAuthACGCredentials(
        client.OAuthACGModel.ToBuilder()
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
                .OAuthACGCredentials(
                    new OAuthACGModel.Builder(
                        "OAuthClientId",
                        "OAuthClientSecret",
                        "OAuthRedirectUri"
                    )
                    .OAuthScopes(
                        new List<OAuthScopeOAuthACGEnum>
                        {
                            OAuthScopeOAuthACGEnum.ReadScope,
                        })
                    .Build())
                .Environment(MultiAuthSample.Standard.Environment.Testing)
                .Build();
            try
            {
                OAuthToken token = LoadTokenFromDatabase();

                // Set the token if it is not set before
                if (token == null)
                {
                    var authManager = client.OAuthACGCredentials;
                    string authUrl = authManager.BuildAuthorizationUrl();
                    string authorizationCode = GetAuthorizationCode(authUrl);
                    token = authManager.FetchToken(authorizationCode);
                }

                SaveTokenToDatabase(token);
                // re-instantiate the client with OAuth token
                client = client.ToBuilder()
                    .OAuthACGCredentials(
                        client.OAuthACGModel.ToBuilder()
                            .OAuthToken(token)
                            .Build())
                    .Build();
            }
            catch (OAuthProviderException e)
            {
                // TODO Handle exception
            }
        }

        private static string GetAuthorizationCode(string authUrl)
        {
            // TODO Open the given auth URL, give access and return authorization code from redirect URL
            return string.Empty;
        }

        private static void SaveTokenToDatabase(OAuthToken token)
        {
            //Save token here
        }

        private static OAuthToken LoadTokenFromDatabase()
        {
            OAuthToken token = null;
            //token = Get token here
            return token;
        }
    }
}

// the client is now authorized and you can use controllers to make endpoint calls
```


