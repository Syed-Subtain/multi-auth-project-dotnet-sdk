
# Getting Started with MultiAuth-Sample

## Introduction

API for Markdown Notes app.

## Install the Package

If you are building with .NET CLI tools then you can also use the following command:

```bash
dotnet add package MultiauthProjectSDK --version 1.0.0
```

You can also view the package at:
https://www.nuget.org/packages/MultiauthProjectSDK/1.0.0

## Test the SDK

The generated SDK also contain one or more Tests, which are contained in the Tests project. In order to invoke these test cases, you will need `NUnit 3.0 Test Adapter Extension` for Visual Studio. Once the SDK is complied, the test cases should appear in the Test Explorer window. Here, you can click `Run All` to execute these test cases.

## Initialize the API Client

**_Note:_** Documentation for the client can be found [here.](https://www.github.com/Syed-Subtain/multi-auth-project-dotnet-sdk/tree/1.0.0/doc/client.md)

The following parameters are configurable for the API Client:

| Parameter | Type | Description |
|  --- | --- | --- |
| `AccessToken2` | `string` |  |
| `Port` | `string` | *Default*: `"80"` |
| `Suites` | `Models.SuiteCodeEnum` | *Default*: `SuiteCodeEnum.Hearts` |
| `Environment` | Environment | The API environment. <br> **Default: `Environment.Testing`** |
| `Timeout` | `TimeSpan` | Http client timeout.<br>*Default*: `TimeSpan.FromSeconds(100)` |
| `BasicAuthCredentials` | [`BasicAuthCredentials`](https://www.github.com/Syed-Subtain/multi-auth-project-dotnet-sdk/tree/1.0.0/doc/$a/https://www.github.com/Syed-Subtain/multi-auth-project-dotnet-sdk/tree/1.0.0/basic-authentication.md) | The Credentials Setter for Basic Authentication |
| `ApiKeyCredentials` | [`ApiKeyCredentials`](https://www.github.com/Syed-Subtain/multi-auth-project-dotnet-sdk/tree/1.0.0/doc/$a/https://www.github.com/Syed-Subtain/multi-auth-project-dotnet-sdk/tree/1.0.0/custom-query-parameter.md) | The Credentials Setter for Custom Query Parameter |
| `ApiHeaderCredentials` | [`ApiHeaderCredentials`](https://www.github.com/Syed-Subtain/multi-auth-project-dotnet-sdk/tree/1.0.0/doc/$a/https://www.github.com/Syed-Subtain/multi-auth-project-dotnet-sdk/tree/1.0.0/custom-header-signature.md) | The Credentials Setter for Custom Header Signature |
| `OAuthCCGCredentials` | [`OAuthCCGCredentials`](https://www.github.com/Syed-Subtain/multi-auth-project-dotnet-sdk/tree/1.0.0/doc/$a/https://www.github.com/Syed-Subtain/multi-auth-project-dotnet-sdk/tree/1.0.0/oauth-2-client-credentials-grant.md) | The Credentials Setter for OAuth 2 Client Credentials Grant |
| `OAuthACGCredentials` | [`OAuthACGCredentials`](https://www.github.com/Syed-Subtain/multi-auth-project-dotnet-sdk/tree/1.0.0/doc/$a/https://www.github.com/Syed-Subtain/multi-auth-project-dotnet-sdk/tree/1.0.0/oauth-2-authorization-code-grant.md) | The Credentials Setter for OAuth 2 Authorization Code Grant |
| `OAuthROPCGCredentials` | [`OAuthROPCGCredentials`](https://www.github.com/Syed-Subtain/multi-auth-project-dotnet-sdk/tree/1.0.0/doc/$a/https://www.github.com/Syed-Subtain/multi-auth-project-dotnet-sdk/tree/1.0.0/oauth-2-resource-owner-credentials-grant.md) | The Credentials Setter for OAuth 2 Resource Owner Credentials Grant |
| `OAuthBearerTokenCredentials` | [`OAuthBearerTokenCredentials`](https://www.github.com/Syed-Subtain/multi-auth-project-dotnet-sdk/tree/1.0.0/doc/$a/https://www.github.com/Syed-Subtain/multi-auth-project-dotnet-sdk/tree/1.0.0/oauth-2-bearer-token.md) | The Credentials Setter for OAuth 2 Bearer token |

The API client can be initialized as follows:

```csharp
MultiAuthSample.Standard.MultiAuthSampleClient client = new MultiAuthSample.Standard.MultiAuthSampleClient.Builder()
    .BasicAuthCredentials(
        new BasicAuthModel.Builder(
            "Username",
            "Password"
        )
        .Build())
    .ApiKeyCredentials(
        new ApiKeyModel.Builder(
            "token",
            "api-key"
        )
        .Build())
    .ApiHeaderCredentials(
        new ApiHeaderModel.Builder(
            "token",
            "api-key"
        )
        .Build())
    .OAuthCCGCredentials(
        new OAuthCCGModel.Builder(
            "OAuthClientId",
            "OAuthClientSecret"
        )
        .Build())
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
    .OAuthROPCGCredentials(
        new OAuthROPCGModel.Builder(
            "OAuthClientId",
            "OAuthClientSecret",
            "OAuthUsername",
            "OAuthPassword"
        )
        .Build())
    .OAuthBearerTokenCredentials(
        new OAuthBearerTokenModel.Builder(
            "AccessToken"
        )
        .Build())
    .AccessToken2("accessToken")
    .Environment(MultiAuthSample.Standard.Environment.Testing)
    .Port("80")
    .Suites(SuiteCodeEnum.Hearts)
    .Build();
```

## Environments

The SDK can be configured to use a different environment for making API calls. Available environments are:

### Fields

| Name | Description |
|  --- | --- |
| production | - |
| testing | **Default** |

## Authorization

This API uses the following authentication schemes.

* [`basicAuth (Basic Authentication)`](https://www.github.com/Syed-Subtain/multi-auth-project-dotnet-sdk/tree/1.0.0/doc/$a/https://www.github.com/Syed-Subtain/multi-auth-project-dotnet-sdk/tree/1.0.0/basic-authentication.md)
* [`apiKey (Custom Query Parameter)`](https://www.github.com/Syed-Subtain/multi-auth-project-dotnet-sdk/tree/1.0.0/doc/$a/https://www.github.com/Syed-Subtain/multi-auth-project-dotnet-sdk/tree/1.0.0/custom-query-parameter.md)
* [`apiHeader (Custom Header Signature)`](https://www.github.com/Syed-Subtain/multi-auth-project-dotnet-sdk/tree/1.0.0/doc/$a/https://www.github.com/Syed-Subtain/multi-auth-project-dotnet-sdk/tree/1.0.0/custom-header-signature.md)
* [`OAuthCCG (OAuth 2 Client Credentials Grant)`](https://www.github.com/Syed-Subtain/multi-auth-project-dotnet-sdk/tree/1.0.0/doc/$a/https://www.github.com/Syed-Subtain/multi-auth-project-dotnet-sdk/tree/1.0.0/oauth-2-client-credentials-grant.md)
* [`OAuthACG (OAuth 2 Authorization Code Grant)`](https://www.github.com/Syed-Subtain/multi-auth-project-dotnet-sdk/tree/1.0.0/doc/$a/https://www.github.com/Syed-Subtain/multi-auth-project-dotnet-sdk/tree/1.0.0/oauth-2-authorization-code-grant.md)
* [`OAuthROPCG (OAuth 2 Resource Owner Credentials Grant)`](https://www.github.com/Syed-Subtain/multi-auth-project-dotnet-sdk/tree/1.0.0/doc/$a/https://www.github.com/Syed-Subtain/multi-auth-project-dotnet-sdk/tree/1.0.0/oauth-2-resource-owner-credentials-grant.md)
* [`OAuthBearerToken (OAuth 2 Bearer token)`](https://www.github.com/Syed-Subtain/multi-auth-project-dotnet-sdk/tree/1.0.0/doc/$a/https://www.github.com/Syed-Subtain/multi-auth-project-dotnet-sdk/tree/1.0.0/oauth-2-bearer-token.md)
* `CustomAuth (Custom Authentication)`

## List of APIs

* [O Auth Authorization](https://www.github.com/Syed-Subtain/multi-auth-project-dotnet-sdk/tree/1.0.0/doc/controllers/o-auth-authorization.md)
* [Authentication](https://www.github.com/Syed-Subtain/multi-auth-project-dotnet-sdk/tree/1.0.0/doc/controllers/authentication.md)

## Classes Documentation

* [Utility Classes](https://www.github.com/Syed-Subtain/multi-auth-project-dotnet-sdk/tree/1.0.0/doc/utility-classes.md)
* [HttpRequest](https://www.github.com/Syed-Subtain/multi-auth-project-dotnet-sdk/tree/1.0.0/doc/http-request.md)
* [HttpResponse](https://www.github.com/Syed-Subtain/multi-auth-project-dotnet-sdk/tree/1.0.0/doc/http-response.md)
* [HttpStringResponse](https://www.github.com/Syed-Subtain/multi-auth-project-dotnet-sdk/tree/1.0.0/doc/http-string-response.md)
* [HttpContext](https://www.github.com/Syed-Subtain/multi-auth-project-dotnet-sdk/tree/1.0.0/doc/http-context.md)
* [HttpClientConfiguration](https://www.github.com/Syed-Subtain/multi-auth-project-dotnet-sdk/tree/1.0.0/doc/http-client-configuration.md)
* [HttpClientConfiguration Builder](https://www.github.com/Syed-Subtain/multi-auth-project-dotnet-sdk/tree/1.0.0/doc/http-client-configuration-builder.md)
* [IAuthManager](https://www.github.com/Syed-Subtain/multi-auth-project-dotnet-sdk/tree/1.0.0/doc/i-auth-manager.md)
* [ApiException](https://www.github.com/Syed-Subtain/multi-auth-project-dotnet-sdk/tree/1.0.0/doc/api-exception.md)

