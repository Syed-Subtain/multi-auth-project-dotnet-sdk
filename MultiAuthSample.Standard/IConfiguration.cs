// <copyright file="IConfiguration.cs" company="APIMatic">
// Copyright (c) APIMatic. All rights reserved.
// </copyright>
namespace MultiAuthSample.Standard
{
    using System;
    using System.Net;
    using MultiAuthSample.Standard.Authentication;
    using MultiAuthSample.Standard.Models;

    /// <summary>
    /// IConfiguration.
    /// </summary>
    public interface IConfiguration
    {
        string AccessToken2 { get; }

        /// <summary>
        /// Gets Current API environment.
        /// </summary>
        Environment Environment { get; }

        /// <summary>
        /// Gets Port value.
        /// </summary>
        string Port { get; }

        /// <summary>
        /// Gets Suites value.
        /// </summary>
        Models.SuiteCodeEnum Suites { get; }

        /// <summary>
        /// Gets the credentials to use with BasicAuth.
        /// </summary>
        IBasicAuthCredentials BasicAuthCredentials { get; }

        /// <summary>
        /// Gets the credentials model to use with BasicAuth.
        /// </summary>
        BasicAuthModel BasicAuthModel { get; }

        /// <summary>
        /// Gets the credentials to use with ApiKey.
        /// </summary>
        IApiKeyCredentials ApiKeyCredentials { get; }

        /// <summary>
        /// Gets the credentials model to use with ApiKey.
        /// </summary>
        ApiKeyModel ApiKeyModel { get; }

        /// <summary>
        /// Gets the credentials to use with ApiHeader.
        /// </summary>
        IApiHeaderCredentials ApiHeaderCredentials { get; }

        /// <summary>
        /// Gets the credentials model to use with ApiHeader.
        /// </summary>
        ApiHeaderModel ApiHeaderModel { get; }

        /// <summary>
        /// Gets the credentials to use with OAuthCCG.
        /// </summary>
        IOAuthCCGCredentials OAuthCCGCredentials { get; }

        /// <summary>
        /// Gets the credentials model to use with OAuthCCG.
        /// </summary>
        OAuthCCGModel OAuthCCGModel { get; }

        /// <summary>
        /// Gets the credentials to use with OAuthACG.
        /// </summary>
        IOAuthACGCredentials OAuthACGCredentials { get; }

        /// <summary>
        /// Gets the credentials model to use with OAuthACG.
        /// </summary>
        OAuthACGModel OAuthACGModel { get; }

        /// <summary>
        /// Gets the credentials to use with OAuthROPCG.
        /// </summary>
        IOAuthROPCGCredentials OAuthROPCGCredentials { get; }

        /// <summary>
        /// Gets the credentials model to use with OAuthROPCG.
        /// </summary>
        OAuthROPCGModel OAuthROPCGModel { get; }

        /// <summary>
        /// Gets the OAuth 2.0 Access Token.
        /// </summary>
        string AccessToken { get; }

        /// <summary>
        /// Gets the credentials to use with CustomAuth.
        /// </summary>
        ICustomAuthCredentials CustomAuthCredentials { get; }

        /// <summary>
        /// Gets the URL for a particular alias in the current environment and appends it with template parameters.
        /// </summary>
        /// <param name="alias">Default value:DEFAULT.</param>
        /// <returns>Returns the baseurl.</returns>
        string GetBaseUri(Server alias = Server.Default);
    }
}