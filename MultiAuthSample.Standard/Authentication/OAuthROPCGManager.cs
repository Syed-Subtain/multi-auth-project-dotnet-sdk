// <copyright file="OAuthROPCGManager.cs" company="APIMatic">
// Copyright (c) APIMatic. All rights reserved.
// </copyright>
namespace MultiAuthSample.Standard.Authentication
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using MultiAuthSample.Standard.Controllers;
    using MultiAuthSample.Standard.Exceptions;
    using MultiAuthSample.Standard.Http.Request;
    using MultiAuthSample.Standard.Models;
    using MultiAuthSample.Standard.Utilities;
    using APIMatic.Core.Authentication;
    using APIMatic.Core;

    /// <summary>
    /// OAuthROPCGManager Class.
    /// </summary>
    public class OAuthROPCGManager : AuthManager, IOAuthROPCGCredentials
    {
        private Func<OAuthAuthorizationController> oAuthApi;

        /// <summary>
        /// Initializes a new instance of the <see cref="OAuthROPCGManager"/> class.
        /// </summary>
        /// <param name="oAuthROPCG"> OAuth 2 Resource Owner Password Client Cridentials Model.</param>
        internal OAuthROPCGManager(OAuthROPCGModel oAuthROPCG)
        {
            this.OAuthClientId = oAuthROPCG?.OAuthClientId;
            this.OAuthClientSecret = oAuthROPCG?.OAuthClientSecret;
            this.OAuthUsername = oAuthROPCG?.OAuthUsername;
            this.OAuthPassword = oAuthROPCG?.OAuthPassword;
            this.OAuthToken = oAuthROPCG?.OAuthToken;
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
        /// Gets string value for oAuthUsername.
        /// </summary>
        public string OAuthUsername { get; }

        /// <summary>
        /// Gets string value for oAuthPassword.
        /// </summary>
        public string OAuthPassword { get; }

        /// <summary>
        /// Gets Models.OAuthToken value for oAuthToken.
        /// </summary>
        public Models.OAuthToken OAuthToken { get; }

        /// <summary>
        /// Check if credentials match.
        /// </summary>
        /// <param name="oAuthClientId"> The string value for credentials.</param>
        /// <param name="oAuthClientSecret"> The string value for credentials.</param>
        /// <param name="oAuthUsername"> The string value for credentials.</param>
        /// <param name="oAuthPassword"> The string value for credentials.</param>
        /// <param name="oAuthToken"> The Models.OAuthToken value for credentials.</param>
        /// <returns> True if credentials matched.</returns>
        public bool Equals(string oAuthClientId, string oAuthClientSecret, string oAuthUsername, string oAuthPassword, Models.OAuthToken oAuthToken)
        {
            return oAuthClientId.Equals(this.OAuthClientId)
                    && oAuthClientSecret.Equals(this.OAuthClientSecret)
                    && oAuthUsername.Equals(this.OAuthUsername)
                    && oAuthPassword.Equals(this.OAuthPassword)
                    && ((oAuthToken == null && this.OAuthToken == null) || (oAuthToken != null && this.OAuthToken != null && oAuthToken.Equals(this.OAuthToken)));
        }

        /// <summary>
        /// Fetch the OAuth token.
        /// </summary>
        /// <param name="additionalParameters">Dictionary of additional parameters.</param>
        /// <returns>Models.OAuthToken.</returns>
        public Models.OAuthToken FetchToken(Dictionary<string, object> additionalParameters = null)
            => ApiHelper.RunTask(FetchTokenAsync(additionalParameters));
        

        /// <summary>
        /// Fetch the OAuth token asynchronously.
        /// </summary>
        /// <param name="additionalParameters"> Dictionary of additional parameters.</param>
        /// <returns> OAuthToken.</returns>
        public async Task<OAuthToken> FetchTokenAsync(Dictionary<string, object> additionalParameters = null)
        {
            string authorizationHeader = this.BuildBasicAuthheader();
            var token = await this.oAuthApi?.Invoke().RequestTokenOAuthROPCGAsync(authorizationHeader, this.OAuthUsername, this.OAuthPassword, fieldParameters: additionalParameters);

            if (token.ExpiresIn != null && token.ExpiresIn != 0)
            {
                token.Expiry = (long)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds + token.ExpiresIn;
            }

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
        /// <param name="additionalParameters"> Dictionary of additional parameters.</param>
        /// <returns> OAuthToken.</returns>
        public async Task<OAuthToken> RefreshTokenAsync(Dictionary<string, object> additionalParameters = null)
        {
            string authorizationHeader = this.BuildBasicAuthheader();
            var token = await this.oAuthApi?.Invoke().RefreshTokenOAuthROPCGAsync(authorizationHeader, this.OAuthToken.RefreshToken, fieldParameters: additionalParameters);

            if (token.ExpiresIn != null && token.ExpiresIn != 0)
            {
                token.Expiry = (long)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds + token.ExpiresIn;
            }

            return token;
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

    public sealed class OAuthROPCGModel
    {
        internal OAuthROPCGModel()
        {
        }

        internal string OAuthClientId { get; set; }

        internal string OAuthClientSecret { get; set; }

        internal string OAuthUsername { get; set; }

        internal string OAuthPassword { get; set; }

        internal Models.OAuthToken OAuthToken { get; set; }

        /// <summary>
        /// Creates an object of the OAuthROPCGModel using the values provided for the builder.
        /// </summary>
        /// <returns>Builder.</returns>
        public Builder ToBuilder()
        {
            return new Builder(OAuthClientId, OAuthClientSecret, OAuthUsername, OAuthPassword)
                .OAuthToken(OAuthToken);
        }

        /// <summary>
        /// Builder class for OAuthROPCGModel.
        /// </summary>
        public class Builder
        {
            private string oAuthClientId;
            private string oAuthClientSecret;
            private string oAuthUsername;
            private string oAuthPassword;
            private Models.OAuthToken oAuthToken;

            public Builder(string oAuthClientId, string oAuthClientSecret, string oAuthUsername, string oAuthPassword)
            {
                this.oAuthClientId = oAuthClientId ?? throw new ArgumentNullException(nameof(oAuthClientId));
                this.oAuthClientSecret = oAuthClientSecret ?? throw new ArgumentNullException(nameof(oAuthClientSecret));
                this.oAuthUsername = oAuthUsername ?? throw new ArgumentNullException(nameof(oAuthUsername));
                this.oAuthPassword = oAuthPassword ?? throw new ArgumentNullException(nameof(oAuthPassword));
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
            /// Sets OAuthUsername.
            /// </summary>
            /// <param name="oAuthUsername">OAuthUsername.</param>
            /// <returns>Builder.</returns>
            public Builder OAuthUsername(string oAuthUsername)
            {
                this.oAuthUsername = oAuthUsername ?? throw new ArgumentNullException(nameof(oAuthUsername));
                return this;
            }


            /// <summary>
            /// Sets OAuthPassword.
            /// </summary>
            /// <param name="oAuthPassword">OAuthPassword.</param>
            /// <returns>Builder.</returns>
            public Builder OAuthPassword(string oAuthPassword)
            {
                this.oAuthPassword = oAuthPassword ?? throw new ArgumentNullException(nameof(oAuthPassword));
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
            /// Creates an object of the OAuthROPCGModel using the values provided for the builder.
            /// </summary>
            /// <returns>OAuthROPCGModel.</returns>
            internal OAuthROPCGModel Build()
            {
                return new OAuthROPCGModel()
                {
                    OAuthClientId = this.oAuthClientId,
                    OAuthClientSecret = this.oAuthClientSecret,
                    OAuthUsername = this.oAuthUsername,
                    OAuthPassword = this.oAuthPassword,
                    OAuthToken = this.oAuthToken
                };
            }
        }
    }
}