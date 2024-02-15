// <copyright file="IOAuthBearerTokenCredentials.cs" company="APIMatic">
// Copyright (c) APIMatic. All rights reserved.
// </copyright>
namespace MultiAuthSample.Standard.Authentication
{
    using System;

    /// <summary>
    /// Authentication configuration interface for OAuthBearerToken.
    /// </summary>
    public interface IOAuthBearerTokenCredentials
    {
        /// <summary>
        /// Gets string value for accessToken.
        /// </summary>
        string AccessToken { get; }

        /// <summary>
        ///  Returns true if credentials matched.
        /// </summary>
        /// <param name="accessToken"> The string value for credentials.</param>
        /// <returns>True if credentials matched.</returns>
        bool Equals(string accessToken);
    }
}