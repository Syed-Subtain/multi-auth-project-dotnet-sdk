
# OAuth 2 Bearer token



Documentation for accessing and setting credentials for OAuthBearerToken.

## Auth Credentials

| Name | Type | Description | Setter | Getter |
|  --- | --- | --- | --- | --- |
| AccessToken | `string` | The OAuth 2.0 Access Token to use for API requests. | `AccessToken` | `AccessToken` |



**Note:** Auth credentials can be set using `OAuthBearerTokenCredentials` in the client builder and accessed through `OAuthBearerTokenCredentials` method in the client instance.

## Usage Example

### Client Initialization

You must provide credentials in the client as shown in the following code snippet.

```csharp
MultiAuthSample.Standard.MultiAuthSampleClient client = new MultiAuthSample.Standard.MultiAuthSampleClient.Builder()
    .OAuthBearerTokenCredentials(
        new OAuthBearerTokenModel.Builder(
            "AccessToken"
        )
        .Build())
    .Build();
```


