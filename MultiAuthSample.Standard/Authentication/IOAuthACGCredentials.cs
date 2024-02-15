// <copyright file="IOAuthACGCredentials.cs" company="APIMatic">
// Copyright (c) APIMatic. All rights reserved.
// </copyright>
namespace MultiAuthSample.Standard.Authentication
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    public interface IOAuthACGCredentials
    {
        /// <summary>
        /// Gets string value for oAuthClientId.
        /// </summary>
        string OAuthClientId { get; }

        /// <summary>
        /// Gets string value for oAuthClientSecret.
        /// </summary>
        string OAuthClientSecret { get; }

        /// <summary>
        /// Gets string value for oAuthRedirectUri.
        /// </summary>
        string OAuthRedirectUri { get; }

        /// <summary>
        /// Gets Models.OAuthToken value for oAuthToken.
        /// </summary>
        Models.OAuthToken OAuthToken { get; }

        /// <summary>
        /// Gets List of Models.OAuthScopeOAuthACGEnum value for oAuthScopes.
        /// </summary>
        List<Models.OAuthScopeOAuthACGEnum> OAuthScopes { get; }

        /// <summary>
        ///  Returns true if credentials matched.
        /// </summary>
        /// <param name="oAuthClientId"> The string value for credentials.</param>
        /// <param name="oAuthClientSecret"> The string value for credentials.</param>
        /// <param name="oAuthRedirectUri"> The string value for credentials.</param>
        /// <param name="oAuthToken"> The Models.OAuthToken value for credentials.</param>
        /// <param name="oAuthScopes"> The List of Models.OAuthScopeOAuthACGEnum value for credentials.</param>
        /// <returns>True if credentials matched.</returns>
        bool Equals(string oAuthClientId, string oAuthClientSecret, string oAuthRedirectUri, Models.OAuthToken oAuthToken, List<Models.OAuthScopeOAuthACGEnum> oAuthScopes);

        /// <summary>
        /// Fetch the OAuth token asynchronously.
        /// </summary>
        /// <param name="authorizationCode">Authorization code returned by the OAuth provider.</param>
        /// <param name="additionalParameters">Dictionary of additional parameters.</param>
        /// <returns>OAuthToken.</returns>
        Task<Models.OAuthToken> FetchTokenAsync(string authorizationCode, Dictionary<string, object> additionalParameters = null);

        /// <summary>
        /// Fetch the OAuth token.
        /// </summary>
        /// <param name="authorizationCode">Authorization code returned by the OAuth provider.</param>
        /// <param name="additionalParameters">Dictionary of additional parameters.</param>
        /// <returns>OAuthToken.</returns>
        Models.OAuthToken FetchToken(string authorizationCode, Dictionary<string, object> additionalParameters = null);

        /// <summary>
        /// Checks if token is expired.
        /// </summary>
        /// <returns> Returns true if token is expired.</returns>
        bool IsTokenExpired();

        /// <summary>
        /// Refresh the OAuth token.
        /// </summary>
        /// <param name="additionalParameters">Dictionary of additional parameters.</param>
        /// <returns>OAuthToken.</returns>
        Models.OAuthToken RefreshToken(Dictionary<string, object> additionalParameters = null);

        /// <summary>
        /// Refresh the OAuth token asynchronously.
        /// </summary>
        /// <param name="additionalParameters">Dictionary of additional parameters.</param>
        /// <returns>OAuthToken.</returns>
        Task<Models.OAuthToken> RefreshTokenAsync(Dictionary<string, object> additionalParameters = null);

        /// <summary>
        /// Build an authorization URL for taking the user's consent to access data.
        /// </summary>
        /// <param name="state">An opaque state string.</param>
        /// <param name="additionalParameters">Additional parameters to add the the authorization URL.</param>
        /// <returns>Authorization URL.</returns>
        string BuildAuthorizationUrl(string state = null, Dictionary<string, object> additionalParameters = null);
    }
}