// <copyright file="OAuthACGManager.cs" company="APIMatic">
// Copyright (c) APIMatic. All rights reserved.
// </copyright>
namespace MultiAuthSample.Standard.Authentication
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using MultiAuthSample.Standard.Controllers;
    using MultiAuthSample.Standard.Utilities;
    using MultiAuthSample.Standard.Exceptions;
    using MultiAuthSample.Standard.Models;
    using APIMatic.Core.Authentication;
    using APIMatic.Core;
    using System.Net.Http;

    /// <summary>
    /// OAuthACGManager Class.
    /// </summary>
    public class OAuthACGManager : AuthManager, IOAuthACGCredentials
    {
        private Func<OAuthAuthorizationController> oAuthApi;
        private GlobalConfiguration globalConfiguration;

        /// <summary>
        /// Initializes a new instance of the <see cref="OAuthACGManager"/> class.
        /// </summary>
        /// <param name="oAuthACG"> OAuth 2 Authorization Code Model.</param>
        internal OAuthACGManager(OAuthACGModel oAuthACG)
        {
            OAuthClientId = oAuthACG?.OAuthClientId;
            OAuthClientSecret = oAuthACG?.OAuthClientSecret;
            OAuthRedirectUri = oAuthACG?.OAuthRedirectUri;
            OAuthToken = oAuthACG?.OAuthToken;
            OAuthScopes = oAuthACG?.OAuthScopes;
            Parameters(authParameter => authParameter
                .Header(headerParameter => headerParameter
                    .Setup("Authorization",
                        OAuthToken?.AccessToken == null ? null : $"Bearer {OAuthToken?.AccessToken}"
                    ).Required()));
        }

        /// <summary>
        /// Gets string value for oAuthClientId.
        /// </summary>
        public string OAuthClientId { get; }

        /// <summary>
        /// Gets string value for oAuthClientSecret.
        /// </summary>
        public string OAuthClientSecret { get; }

        /// <summary>
        /// Gets string value for oAuthRedirectUri.
        /// </summary>
        public string OAuthRedirectUri { get; }

        /// <summary>
        /// Gets Models.OAuthToken value for oAuthToken.
        /// </summary>
        public Models.OAuthToken OAuthToken { get; }

        /// <summary>
        /// Gets List of Models.OAuthScopeOAuthACGEnum value for oAuthScopes.
        /// </summary>
        public List<Models.OAuthScopeOAuthACGEnum> OAuthScopes { get; }

        /// <summary>
        /// Check if credentials match.
        /// </summary>
        /// <param name="oAuthClientId"> The string value for credentials.</param>
        /// <param name="oAuthClientSecret"> The string value for credentials.</param>
        /// <param name="oAuthRedirectUri"> The string value for credentials.</param>
        /// <param name="oAuthToken"> The Models.OAuthToken value for credentials.</param>
        /// <param name="oAuthScopes"> The List of Models.OAuthScopeOAuthACGEnum value for credentials.</param>
        /// <returns> True if credentials matched.</returns>
        public bool Equals(string oAuthClientId, string oAuthClientSecret, string oAuthRedirectUri, Models.OAuthToken oAuthToken, List<Models.OAuthScopeOAuthACGEnum> oAuthScopes)
        {
            return oAuthClientId.Equals(this.OAuthClientId)
                    && oAuthClientSecret.Equals(this.OAuthClientSecret)
                    && oAuthRedirectUri.Equals(this.OAuthRedirectUri)
                    && ((oAuthToken == null && this.OAuthToken == null) || (oAuthToken != null && this.OAuthToken != null && oAuthToken.Equals(this.OAuthToken)))
                    && ((oAuthScopes == null && this.OAuthScopes == null) || (oAuthScopes != null && this.OAuthScopes != null && oAuthScopes.Equals(this.OAuthScopes)));
        }

        /// <summary>
        /// Fetch the OAuth token.
        /// </summary>
        /// <param name="authorizationCode">Authorization code returned by the OAuth provider.</param>
        /// <param name="additionalParameters">Dictionary of additional parameters.</param>
        /// <returns>Models.OAuthToken.</returns>
        public Models.OAuthToken FetchToken(string authorizationCode, Dictionary<string, object> additionalParameters = null)
            => ApiHelper.RunTask(FetchTokenAsync(authorizationCode, additionalParameters));

        /// <summary>
        /// Fetch the OAuth token asynchronously.
        /// </summary>
        /// <param name="authorizationCode">Authorization code returned by the OAuth provider.</param>
        /// <param name="additionalParameters">Dictionary of additional parameters.</param>
        /// <returns>Models.OAuthToken.</returns>
        public async Task<Models.OAuthToken> FetchTokenAsync(string authorizationCode, Dictionary<string, object> additionalParameters = null)
        {
            var token = await oAuthApi?.Invoke().RequestTokenOAuthACGAsync(BuildBasicAuthheader(), authorizationCode, OAuthRedirectUri, additionalParameters);

            if (token.ExpiresIn != null && token.ExpiresIn != 0)
            {
                token.Expiry = (long)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds + token.ExpiresIn;
            }

            return token;
        }
        
        /// <summary>
        /// Checks if token is expired.
        /// </summary>
        /// <returns> Returns true if token is expired.</returns>
        public bool IsTokenExpired()
        {
           if (this.OAuthToken == null)
           {
               throw new InvalidOperationException("OAuth token is missing.");
           }
        
           return this.OAuthToken.Expiry != null
               && this.OAuthToken.Expiry < (long)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        }

        /// <summary>
        /// Refresh the OAuth token asynchronously.
        /// </summary>
        /// <param name="additionalParameters">Dictionary of additional parameters.</param>
        /// <returns>Models.OAuthToken.</returns>
        public async Task<Models.OAuthToken> RefreshTokenAsync(Dictionary<string, object> additionalParameters = null)
        {
            var token = await oAuthApi?.Invoke().RefreshTokenOAuthACGAsync(BuildBasicAuthheader(),
                OAuthToken.RefreshToken,
                scope: this.OAuthScopes.GetValues(),
                fieldParameters: additionalParameters);
            return token;
        }

        /// <summary>
        /// Refresh the OAuth token.
        /// </summary>
        /// <param name="additionalParameters">Dictionary of additional parameters.</param>
        /// <returns>Models.OAuthToken.</returns>
        public Models.OAuthToken RefreshToken(Dictionary<string, object> additionalParameters = null)
            => ApiHelper.RunTask(RefreshTokenAsync(additionalParameters));
        

        /// <summary>
        /// Builds the authorization url for making authorized calls.
        /// </summary>
        /// <param name="state">State</param>        
        /// <param name="additionalParameters">Additional parameters to be appended</param>        
        public string BuildAuthorizationUrl(string state = null, Dictionary<string, object> additionalParameters = null)
        {
            return globalConfiguration.GlobalRequestBuilder(Server.Default)
              .Setup(HttpMethod.Get, "/oauth/authorize")
              .Parameters(parameter => parameter
                .Query(queryParameter => queryParameter.Setup("response_type", "code"))
                .Query(queryParameter => queryParameter.Setup("client_id", OAuthClientId))
                .Query(queryParameter => queryParameter.Setup("redirect_uri", OAuthRedirectUri))
                .Query(queryParameter => queryParameter.Setup("scope", OAuthScopes.GetValues()))
                .Query(queryParameter => queryParameter.Setup("state", state))
                .AdditionalQueries(additionalQuery => additionalQuery.Setup(additionalParameters)))
              .Build().QueryUrl;
        }

        public void ApplyGlobalConfiguration(Func<OAuthAuthorizationController> controllerGetter)
        {
            oAuthApi = controllerGetter;
        }

        /// <summary>
        /// Validates the authentication parameters for the HTTP Request.
        /// </summary>
        public override void Validate()
        {
            base.Validate();
            if (OAuthToken == null)
            {
                throw new ApiException(
                        "Client is not authorized.An OAuth token is needed to make API calls.");
            }

            if (IsTokenExpired())
            {
                throw new ApiException(
                        "OAuth token is expired. A valid token is needed to make API calls.");
            }
        }


        /// <summary>
        /// Build basic auth header.
        /// </summary>
        /// <returns> string. </returns>
        private string BuildBasicAuthheader()
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(this.OAuthClientId + ':' + this.OAuthClientSecret);
            return "Basic " + Convert.ToBase64String(plainTextBytes);
        }
    }

    public sealed class OAuthACGModel
    {
        internal OAuthACGModel()
        {
        }

        internal string OAuthClientId { get; set; }

        internal string OAuthClientSecret { get; set; }

        internal string OAuthRedirectUri { get; set; }

        internal Models.OAuthToken OAuthToken { get; set; }

        internal List<Models.OAuthScopeOAuthACGEnum> OAuthScopes { get; set; }

        /// <summary>
        /// Creates an object of the OAuthACGModel using the values provided for the builder.
        /// </summary>
        /// <returns>Builder.</returns>
        public Builder ToBuilder()
        {
            return new Builder(OAuthClientId, OAuthClientSecret, OAuthRedirectUri)
                .OAuthToken(OAuthToken)
                .OAuthScopes(OAuthScopes);
        }

        /// <summary>
        /// Builder class for OAuthACGModel.
        /// </summary>
        public class Builder
        {
            private string oAuthClientId;
            private string oAuthClientSecret;
            private string oAuthRedirectUri;
            private Models.OAuthToken oAuthToken;
            private List<Models.OAuthScopeOAuthACGEnum> oAuthScopes;

            public Builder(string oAuthClientId, string oAuthClientSecret, string oAuthRedirectUri)
            {
                this.oAuthClientId = oAuthClientId ?? throw new ArgumentNullException(nameof(oAuthClientId));
                this.oAuthClientSecret = oAuthClientSecret ?? throw new ArgumentNullException(nameof(oAuthClientSecret));
                this.oAuthRedirectUri = oAuthRedirectUri ?? throw new ArgumentNullException(nameof(oAuthRedirectUri));
            }

            /// <summary>
            /// Sets OAuthClientId.
            /// </summary>
            /// <param name="oAuthClientId">OAuthClientId.</param>
            /// <returns>Builder.</returns>
            public Builder OAuthClientId(string oAuthClientId)
            {
                this.oAuthClientId = oAuthClientId ?? throw new ArgumentNullException(nameof(oAuthClientId));
                return this;
            }


            /// <summary>
            /// Sets OAuthClientSecret.
            /// </summary>
            /// <param name="oAuthClientSecret">OAuthClientSecret.</param>
            /// <returns>Builder.</returns>
            public Builder OAuthClientSecret(string oAuthClientSecret)
            {
                this.oAuthClientSecret = oAuthClientSecret ?? throw new ArgumentNullException(nameof(oAuthClientSecret));
                return this;
            }


            /// <summary>
            /// Sets OAuthRedirectUri.
            /// </summary>
            /// <param name="oAuthRedirectUri">OAuthRedirectUri.</param>
            /// <returns>Builder.</returns>
            public Builder OAuthRedirectUri(string oAuthRedirectUri)
            {
                this.oAuthRedirectUri = oAuthRedirectUri ?? throw new ArgumentNullException(nameof(oAuthRedirectUri));
                return this;
            }


            /// <summary>
            /// Sets OAuthToken.
            /// </summary>
            /// <param name="oAuthToken">OAuthToken.</param>
            /// <returns>Builder.</returns>
            public Builder OAuthToken(Models.OAuthToken oAuthToken)
            {
                this.oAuthToken = oAuthToken;
                return this;
            }


            /// <summary>
            /// Sets OAuthScopes.
            /// </summary>
            /// <param name="oAuthScopes">OAuthScopes.</param>
            /// <returns>Builder.</returns>
            public Builder OAuthScopes(List<Models.OAuthScopeOAuthACGEnum> oAuthScopes)
            {
                this.oAuthScopes = oAuthScopes;
                return this;
            }


            /// <summary>
            /// Creates an object of the OAuthACGModel using the values provided for the builder.
            /// </summary>
            /// <returns>OAuthACGModel.</returns>
            internal OAuthACGModel Build()
            {
                return new OAuthACGModel()
                {
                    OAuthClientId = this.oAuthClientId,
                    OAuthClientSecret = this.oAuthClientSecret,
                    OAuthRedirectUri = this.oAuthRedirectUri,
                    OAuthToken = this.oAuthToken,
                    OAuthScopes = this.oAuthScopes
                };
            }
        }
    }
}