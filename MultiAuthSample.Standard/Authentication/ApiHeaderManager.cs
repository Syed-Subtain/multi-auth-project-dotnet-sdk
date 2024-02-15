// <copyright file="ApiHeaderManager.cs" company="APIMatic">
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
    /// ApiHeaderManager Class.
    /// </summary>
    internal class ApiHeaderManager : AuthManager, IApiHeaderCredentials
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiHeaderManager"/> class.
        /// </summary>
        /// <param name="apiHeader">ApiHeader.</param>
        public ApiHeaderManager(ApiHeaderModel apiHeaderModel)
        {
            Token = apiHeaderModel?.Token;
            ApiKey = apiHeaderModel?.ApiKey;
            Parameters(paramBuilder => paramBuilder
                .Header(header => header.Setup("token", Token).Required())
                .Header(header => header.Setup("api-key", ApiKey).Required())
            );
        }

        /// <summary>
        /// Gets string value for token.
        /// </summary>
        public string Token { get; }

        /// <summary>
        /// Gets string value for apiKey.
        /// </summary>
        public string ApiKey { get; }

        /// <summary>
        /// Check if credentials match.
        /// </summary>
        /// <param name="token"> The string value for credentials.</param>
        /// <param name="apiKey"> The string value for credentials.</param>
        /// <returns> True if credentials matched.</returns>
        public bool Equals(string token, string apiKey)
        {
            return token.Equals(this.Token)
                    && apiKey.Equals(this.ApiKey);
        }
    }

    public sealed class ApiHeaderModel
    {
        internal ApiHeaderModel()
        {
        }

        internal string Token { get; set; }

        internal string ApiKey { get; set; }

        /// <summary>
        /// Creates an object of the ApiHeaderModel using the values provided for the builder.
        /// </summary>
        /// <returns>Builder.</returns>
        public Builder ToBuilder()
        {
            return new Builder(Token, ApiKey);
        }

        /// <summary>
        /// Builder class for ApiHeaderModel.
        /// </summary>
        public class Builder
        {
            private string token;
            private string apiKey;

            public Builder(string token, string apiKey)
            {
                this.token = token ?? throw new ArgumentNullException(nameof(token));
                this.apiKey = apiKey ?? throw new ArgumentNullException(nameof(apiKey));
            }

            /// <summary>
            /// Sets Token.
            /// </summary>
            /// <param name="token">Token.</param>
            /// <returns>Builder.</returns>
            public Builder Token(string token)
            {
                this.token = token ?? throw new ArgumentNullException(nameof(token));
                return this;
            }


            /// <summary>
            /// Sets ApiKey.
            /// </summary>
            /// <param name="apiKey">ApiKey.</param>
            /// <returns>Builder.</returns>
            public Builder ApiKey(string apiKey)
            {
                this.apiKey = apiKey ?? throw new ArgumentNullException(nameof(apiKey));
                return this;
            }


            /// <summary>
            /// Creates an object of the ApiHeaderModel using the values provided for the builder.
            /// </summary>
            /// <returns>ApiHeaderModel.</returns>
            internal ApiHeaderModel Build()
            {
                return new ApiHeaderModel()
                {
                    Token = this.token,
                    ApiKey = this.apiKey
                };
            }
        }
    }
    
}