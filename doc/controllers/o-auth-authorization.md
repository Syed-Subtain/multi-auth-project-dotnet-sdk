# O Auth Authorization

```csharp
OAuthAuthorizationController oAuthAuthorizationController = client.OAuthAuthorizationController;
```

## Class Name

`OAuthAuthorizationController`

## Methods

* [Request Token O Auth CCG](../../doc/controllers/o-auth-authorization.md#request-token-o-auth-ccg)
* [Request Token O Auth ACG](../../doc/controllers/o-auth-authorization.md#request-token-o-auth-acg)
* [Refresh Token O Auth ACG](../../doc/controllers/o-auth-authorization.md#refresh-token-o-auth-acg)
* [Request Token O Auth ROPCG](../../doc/controllers/o-auth-authorization.md#request-token-o-auth-ropcg)
* [Refresh Token O Auth ROPCG](../../doc/controllers/o-auth-authorization.md#refresh-token-o-auth-ropcg)


# Request Token O Auth CCG

Create a new OAuth 2 token.

:information_source: **Note** This endpoint does not require authentication.

```csharp
RequestTokenOAuthCCGAsync(
    string authorization,
    string scope = null,
    Dictionary<string, object> fieldParameters = null)
```

## Parameters

| Parameter | Type | Tags | Description |
|  --- | --- | --- | --- |
| `authorization` | `string` | Header, Required | Authorization header in Basic auth format |
| `scope` | `string` | Form, Optional | Requested scopes as a space-delimited list. |
| `fieldParameters` | `Dictionary<string, object>` | Optional | Pass additional field parameters. |

## Response Type

[`Task<Models.OAuthToken>`](../../doc/models/o-auth-token.md)

## Example Usage

```csharp
string authorization = "Authorization8";
Dictionary<string, object> fieldParameters = new Dictionary<string, object>
{
    ["key0"] = ApiHelper.JsonDeserialize<object>("\"additionalFieldParams9\""),
};

try
{
    OAuthToken result = await oAuthAuthorizationController.RequestTokenOAuthCCGAsync(
        authorization,
        null,
        fieldParameters
    );
}
catch (ApiException e)
{
    // TODO: Handle exception here
    Console.WriteLine(e.Message);
}
```

## Errors

| HTTP Status Code | Error Description | Exception Class |
|  --- | --- | --- |
| 400 | OAuth 2 provider returned an error. | [`OAuthProviderException`](../../doc/models/o-auth-provider-exception.md) |
| 401 | OAuth 2 provider says client authentication failed. | [`OAuthProviderException`](../../doc/models/o-auth-provider-exception.md) |


# Request Token O Auth ACG

Create a new OAuth 2 token.

:information_source: **Note** This endpoint does not require authentication.

```csharp
RequestTokenOAuthACGAsync(
    string authorization,
    string code,
    string redirectUri,
    Dictionary<string, object> fieldParameters = null)
```

## Parameters

| Parameter | Type | Tags | Description |
|  --- | --- | --- | --- |
| `authorization` | `string` | Header, Required | Authorization header in Basic auth format |
| `code` | `string` | Form, Required | Authorization Code |
| `redirectUri` | `string` | Form, Required | Redirect Uri |
| `fieldParameters` | `Dictionary<string, object>` | Optional | Pass additional field parameters. |

## Response Type

[`Task<Models.OAuthToken>`](../../doc/models/o-auth-token.md)

## Example Usage

```csharp
string authorization = "Authorization8";
string code = "code8";
string redirectUri = "redirect_uri8";
Dictionary<string, object> fieldParameters = new Dictionary<string, object>
{
    ["key0"] = ApiHelper.JsonDeserialize<object>("\"additionalFieldParams9\""),
};

try
{
    OAuthToken result = await oAuthAuthorizationController.RequestTokenOAuthACGAsync(
        authorization,
        code,
        redirectUri,
        fieldParameters
    );
}
catch (ApiException e)
{
    // TODO: Handle exception here
    Console.WriteLine(e.Message);
}
```

## Errors

| HTTP Status Code | Error Description | Exception Class |
|  --- | --- | --- |
| 400 | OAuth 2 provider returned an error. | [`OAuthProviderException`](../../doc/models/o-auth-provider-exception.md) |
| 401 | OAuth 2 provider says client authentication failed. | [`OAuthProviderException`](../../doc/models/o-auth-provider-exception.md) |


# Refresh Token O Auth ACG

Obtain a new access token using a refresh token

:information_source: **Note** This endpoint does not require authentication.

```csharp
RefreshTokenOAuthACGAsync(
    string authorization,
    string refreshToken,
    string scope = null,
    Dictionary<string, object> fieldParameters = null)
```

## Parameters

| Parameter | Type | Tags | Description |
|  --- | --- | --- | --- |
| `authorization` | `string` | Header, Required | Authorization header in Basic auth format |
| `refreshToken` | `string` | Form, Required | Refresh token |
| `scope` | `string` | Form, Optional | Requested scopes as a space-delimited list. |
| `fieldParameters` | `Dictionary<string, object>` | Optional | Pass additional field parameters. |

## Response Type

[`Task<Models.OAuthToken>`](../../doc/models/o-auth-token.md)

## Example Usage

```csharp
string authorization = "Authorization8";
string refreshToken = "refresh_token0";
Dictionary<string, object> fieldParameters = new Dictionary<string, object>
{
    ["key0"] = ApiHelper.JsonDeserialize<object>("\"additionalFieldParams9\""),
};

try
{
    OAuthToken result = await oAuthAuthorizationController.RefreshTokenOAuthACGAsync(
        authorization,
        refreshToken,
        null,
        fieldParameters
    );
}
catch (ApiException e)
{
    // TODO: Handle exception here
    Console.WriteLine(e.Message);
}
```

## Errors

| HTTP Status Code | Error Description | Exception Class |
|  --- | --- | --- |
| 400 | OAuth 2 provider returned an error. | [`OAuthProviderException`](../../doc/models/o-auth-provider-exception.md) |
| 401 | OAuth 2 provider says client authentication failed. | [`OAuthProviderException`](../../doc/models/o-auth-provider-exception.md) |


# Request Token O Auth ROPCG

Create a new OAuth 2 token.

:information_source: **Note** This endpoint does not require authentication.

```csharp
RequestTokenOAuthROPCGAsync(
    string authorization,
    string username,
    string password,
    string scope = null,
    Dictionary<string, object> fieldParameters = null)
```

## Parameters

| Parameter | Type | Tags | Description |
|  --- | --- | --- | --- |
| `authorization` | `string` | Header, Required | Authorization header in Basic auth format |
| `username` | `string` | Form, Required | Resource owner username |
| `password` | `string` | Form, Required | Resource owner password |
| `scope` | `string` | Form, Optional | Requested scopes as a space-delimited list. |
| `fieldParameters` | `Dictionary<string, object>` | Optional | Pass additional field parameters. |

## Response Type

[`Task<Models.OAuthToken>`](../../doc/models/o-auth-token.md)

## Example Usage

```csharp
string authorization = "Authorization8";
string username = "username0";
string password = "password4";
Dictionary<string, object> fieldParameters = new Dictionary<string, object>
{
    ["key0"] = ApiHelper.JsonDeserialize<object>("\"additionalFieldParams9\""),
};

try
{
    OAuthToken result = await oAuthAuthorizationController.RequestTokenOAuthROPCGAsync(
        authorization,
        username,
        password,
        null,
        fieldParameters
    );
}
catch (ApiException e)
{
    // TODO: Handle exception here
    Console.WriteLine(e.Message);
}
```

## Errors

| HTTP Status Code | Error Description | Exception Class |
|  --- | --- | --- |
| 400 | OAuth 2 provider returned an error. | [`OAuthProviderException`](../../doc/models/o-auth-provider-exception.md) |
| 401 | OAuth 2 provider says client authentication failed. | [`OAuthProviderException`](../../doc/models/o-auth-provider-exception.md) |


# Refresh Token O Auth ROPCG

Obtain a new access token using a refresh token

:information_source: **Note** This endpoint does not require authentication.

```csharp
RefreshTokenOAuthROPCGAsync(
    string authorization,
    string refreshToken,
    string scope = null,
    Dictionary<string, object> fieldParameters = null)
```

## Parameters

| Parameter | Type | Tags | Description |
|  --- | --- | --- | --- |
| `authorization` | `string` | Header, Required | Authorization header in Basic auth format |
| `refreshToken` | `string` | Form, Required | Refresh token |
| `scope` | `string` | Form, Optional | Requested scopes as a space-delimited list. |
| `fieldParameters` | `Dictionary<string, object>` | Optional | Pass additional field parameters. |

## Response Type

[`Task<Models.OAuthToken>`](../../doc/models/o-auth-token.md)

## Example Usage

```csharp
string authorization = "Authorization8";
string refreshToken = "refresh_token0";
Dictionary<string, object> fieldParameters = new Dictionary<string, object>
{
    ["key0"] = ApiHelper.JsonDeserialize<object>("\"additionalFieldParams9\""),
};

try
{
    OAuthToken result = await oAuthAuthorizationController.RefreshTokenOAuthROPCGAsync(
        authorization,
        refreshToken,
        null,
        fieldParameters
    );
}
catch (ApiException e)
{
    // TODO: Handle exception here
    Console.WriteLine(e.Message);
}
```

## Errors

| HTTP Status Code | Error Description | Exception Class |
|  --- | --- | --- |
| 400 | OAuth 2 provider returned an error. | [`OAuthProviderException`](../../doc/models/o-auth-provider-exception.md) |
| 401 | OAuth 2 provider says client authentication failed. | [`OAuthProviderException`](../../doc/models/o-auth-provider-exception.md) |

