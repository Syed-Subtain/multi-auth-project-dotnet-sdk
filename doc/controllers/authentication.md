# Authentication

```csharp
AuthenticationController authenticationController = client.AuthenticationController;
```

## Class Name

`AuthenticationController`

## Methods

* [O Auth Bearer Token](../../doc/controllers/authentication.md#o-auth-bearer-token)
* [Custom Authentication](../../doc/controllers/authentication.md#custom-authentication)
* [Custom Query or Header Authentication](../../doc/controllers/authentication.md#custom-query-or-header-authentication)
* [O Auth Grant Types or Combinations](../../doc/controllers/authentication.md#o-auth-grant-types-or-combinations)
* [No Auth](../../doc/controllers/authentication.md#no-auth)
* [O Auth Client Credentials Grant](../../doc/controllers/authentication.md#o-auth-client-credentials-grant)
* [Basic Auth and Api Header Auth](../../doc/controllers/authentication.md#basic-auth-and-api-header-auth)
* [O Auth Authorization Grant](../../doc/controllers/authentication.md#o-auth-authorization-grant)
* [Multiple Auth Combination](../../doc/controllers/authentication.md#multiple-auth-combination)


# O Auth Bearer Token

```csharp
OAuthBearerTokenAsync()
```

## Response Type

`Task<string>`

## Example Usage

```csharp
try
{
    string result = await authenticationController.OAuthBearerTokenAsync();
}
catch (ApiException e)
{
    // TODO: Handle exception here
    Console.WriteLine(e.Message);
}
```


# Custom Authentication

```csharp
CustomAuthenticationAsync()
```

## Response Type

`Task<string>`

## Example Usage

```csharp
try
{
    string result = await authenticationController.CustomAuthenticationAsync();
}
catch (ApiException e)
{
    // TODO: Handle exception here
    Console.WriteLine(e.Message);
}
```


# Custom Query or Header Authentication

```csharp
CustomQueryOrHeaderAuthenticationAsync()
```

## Response Type

`Task<string>`

## Example Usage

```csharp
try
{
    string result = await authenticationController.CustomQueryOrHeaderAuthenticationAsync();
}
catch (ApiException e)
{
    // TODO: Handle exception here
    Console.WriteLine(e.Message);
}
```


# O Auth Grant Types or Combinations

This endpoint tests or combinations of OAuth types

```csharp
OAuthGrantTypesORCombinationsAsync()
```

## Response Type

`Task<string>`

## Example Usage

```csharp
try
{
    string result = await authenticationController.OAuthGrantTypesORCombinationsAsync();
}
catch (ApiException e)
{
    // TODO: Handle exception here
    Console.WriteLine(e.Message);
}
```


# No Auth

**This endpoint is deprecated since version 0.0.1-alpha. You should not use this method as it requires no auth and can bring security issues to the server and api call itself!!**

This endpoint does not use auth.

Swagger URL Endpoint 1: [http://swagger.io/endpoint1](http://swagger.io/endpoint1)

:information_source: **Note** This endpoint does not require authentication.

```csharp
NoAuthAsync()
```

## Response Type

`Task<string>`

## Example Usage

```csharp
try
{
    string result = await authenticationController.NoAuthAsync();
}
catch (ApiException e)
{
    // TODO: Handle exception here
    Console.WriteLine(e.Message);
}
```


# O Auth Client Credentials Grant

```csharp
OAuthClientCredentialsGrantAsync()
```

## Response Type

[`Task<Models.ServiceStatus>`](../../doc/models/service-status.md)

## Example Usage

```csharp
try
{
    ServiceStatus result = await authenticationController.OAuthClientCredentialsGrantAsync();
}
catch (ApiException e)
{
    // TODO: Handle exception here
    Console.WriteLine(e.Message);
}
```


# Basic Auth and Api Header Auth

```csharp
BasicAuthAndApiHeaderAuthAsync()
```

## Response Type

`Task<string>`

## Example Usage

```csharp
try
{
    string result = await authenticationController.BasicAuthAndApiHeaderAuthAsync();
}
catch (ApiException e)
{
    // TODO: Handle exception here
    Console.WriteLine(e.Message);
}
```


# O Auth Authorization Grant

```csharp
OAuthAuthorizationGrantAsync()
```

## Response Type

[`Task<Models.User>`](../../doc/models/user.md)

## Example Usage

```csharp
try
{
    User result = await authenticationController.OAuthAuthorizationGrantAsync();
}
catch (ApiException e)
{
    // TODO: Handle exception here
    Console.WriteLine(e.Message);
}
```


# Multiple Auth Combination

**This endpoint is deprecated.**

This endpoint uses globally applied auth which is a hypothetical scneraio but covers the worst case.

Swagger URL Endpoint 1: [http://swagger.io/endpoint1](http://swagger.io/endpoint1)

```csharp
MultipleAuthCombinationAsync()
```

## Response Type

`Task<string>`

## Example Usage

```csharp
try
{
    string result = await authenticationController.MultipleAuthCombinationAsync();
}
catch (ApiException e)
{
    // TODO: Handle exception here
    Console.WriteLine(e.Message);
}
```

