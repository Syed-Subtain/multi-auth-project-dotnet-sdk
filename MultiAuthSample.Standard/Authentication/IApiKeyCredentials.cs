// <copyright file="IApiKeyCredentials.cs" company="APIMatic">
// Copyright (c) APIMatic. All rights reserved.
// </copyright>
namespace MultiAuthSample.Standard.Authentication
{
    using System;

    /// <summary>
    /// Authentication configuration interface for ApiKey.
    /// </summary>
    public interface IApiKeyCredentials
    {
        /// <summary>
        /// Gets string value for token.
        /// </summary>
        string Token { get; }

        /// <summary>
        /// Gets string value for apiKey.
        /// </summary>
        string ApiKey { get; }

        /// <summary>
        ///  Returns true if credentials matched.
        /// </summary>
        /// <param name="token"> The string value for credentials.</param>
        /// <param name="apiKey"> The string value for credentials.</param>
        /// <returns>True if credentials matched.</returns>
        bool Equals(string token, string apiKey);
    }
}