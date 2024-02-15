// <copyright file="OAuthBearerTokenManager.cs" company="APIMatic">
// Copyright (c) APIMatic. All rights reserved.
// </copyright>
namespace MultiAuthSample.Standard.Authentication
{
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MultiAuthSample.Standard.Http.Request;
using APIMatic.Core.Authentication;

/// <summary>
/// OAuthBearerTokenManager.
/// </summary>
internal class OAuthBearerTokenManager : AuthManager, IOAuthBearerTokenCredentials
{
        /// <summary>
        /// Initializes a new instance of the <see cref="OAuthBearerTokenManager"/> class.
        /// </summary>
        /// <param name="accessToken">accessToken.</param>
        public OAuthBearerTokenManager(OAuthBearerTokenModel oAuthBearerTokenModel)
        {
            this.AccessToken = oAuthBearerTokenModel?.AccessToken;
            Parameters(paramBuilder => paramBuilder
                .Header(header => header.Setup("Authorization",
                    this.AccessToken == null ? null : $"Bearer {this.AccessToken}"
                ).Required()));
        }

        /// <summary>
        /// Gets string value for accessToken.
        /// </summary>
        public string AccessToken { get; }

        /// <summary>
        /// Check if credentials match.
        /// </summary>
        /// <param name="accessToken"> The string value for credentials.</param>
        /// <returns> True if credentials matched.</returns>
        public bool Equals(string accessToken)
        {
            return accessToken.Equals(this.AccessToken);
        }

    }

    public sealed class OAuthBearerTokenModel
    {
        internal OAuthBearerTokenModel()
        {
        }

        internal string AccessToken { get; set; }

        /// <summary>
        /// Creates an object of the OAuthBearerTokenModel using the values provided for the builder.
        /// </summary>
        /// <returns>Builder.</returns>
        public Builder ToBuilder()
        {
            return new Builder(AccessToken);
        }

        /// <summary>
        /// Builder class for OAuthBearerTokenModel.
        /// </summary>
        public class Builder
        {
            private string accessToken;

            public Builder(string accessToken)
            {
                this.accessToken = accessToken ?? throw new ArgumentNullException(nameof(accessToken));
            }

            /// <summary>
            /// Sets AccessToken.
            /// </summary>
            /// <param name="accessToken">AccessToken.</param>
            /// <returns>Builder.</returns>
            public Builder AccessToken(string accessToken)
            {
                this.accessToken = accessToken ?? throw new ArgumentNullException(nameof(accessToken));
                return this;
            }


            /// <summary>
            /// Creates an object of the OAuthBearerTokenModel using the values provided for the builder.
            /// </summary>
            /// <returns>OAuthBearerTokenModel.</returns>
            internal OAuthBearerTokenModel Build()
            {
                return new OAuthBearerTokenModel()
                {
                    AccessToken = this.accessToken
                };
            }
        }
    }
}