// <copyright file="OAuthAuthorizationController.cs" company="APIMatic">
// Copyright (c) APIMatic. All rights reserved.
// </copyright>
namespace MultiAuthSample.Standard.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using APIMatic.Core;
    using APIMatic.Core.Types;
    using APIMatic.Core.Utilities;
    using APIMatic.Core.Utilities.Date.Xml;
    using MultiAuthSample.Standard;
    using MultiAuthSample.Standard.Exceptions;
    using MultiAuthSample.Standard.Http.Client;
    using MultiAuthSample.Standard.Utilities;
    using Newtonsoft.Json.Converters;
    using System.Net.Http;

    /// <summary>
    /// OAuthAuthorizationController.
    /// </summary>
    public class OAuthAuthorizationController : BaseController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OAuthAuthorizationController"/> class.
        /// </summary>
        internal OAuthAuthorizationController(GlobalConfiguration globalConfiguration) : base(globalConfiguration) { }

        /// <summary>
        /// Create a new OAuth 2 token.
        /// </summary>
        /// <param name="authorization">Required parameter: Authorization header in Basic auth format.</param>
        /// <param name="scope">Optional parameter: Requested scopes as a space-delimited list..</param>
        /// <param name="fieldParameters">Additional optional form parameters are supported by this endpoint.</param>
        /// <returns>Returns the Models.OAuthToken response from the API call.</returns>
        public Models.OAuthToken RequestTokenOAuthCCG(
                string authorization,
                string scope = null,
                Dictionary<string, object> fieldParameters = null)
            => CoreHelper.RunTask(RequestTokenOAuthCCGAsync(authorization, scope, fieldParameters));

        /// <summary>
        /// Create a new OAuth 2 token.
        /// </summary>
        /// <param name="authorization">Required parameter: Authorization header in Basic auth format.</param>
        /// <param name="scope">Optional parameter: Requested scopes as a space-delimited list..</param>
        /// <param name="fieldParameters">Additional optional form parameters are supported by this endpoint.</param>
        /// <param name="cancellationToken"> cancellationToken. </param>
        /// <returns>Returns the Models.OAuthToken response from the API call.</returns>
        public async Task<Models.OAuthToken> RequestTokenOAuthCCGAsync(
                string authorization,
                string scope = null,
                Dictionary<string, object> fieldParameters = null,
                CancellationToken cancellationToken = default)
            => await CreateApiCall<Models.OAuthToken>()
              .Server(Server.Auth)
              .RequestBuilder(_requestBuilder => _requestBuilder
                  .Setup(HttpMethod.Post, "/request_token")
                  .Parameters(_parameters => _parameters
                      .AdditionalForms(_additionalForms => _additionalForms.Setup(fieldParameters))
                      .Form(_form => _form.Setup("grant_type", "client_credentials"))
                      .Header(_header => _header.Setup("Authorization", authorization))
                      .Form(_form => _form.Setup("scope", scope))))
              .ResponseHandler(_responseHandler => _responseHandler
                  .ErrorCase("400", CreateErrorCase("OAuth 2 provider returned an error.", (_reason, _context) => new OAuthProviderException(_reason, _context)))
                  .ErrorCase("401", CreateErrorCase("OAuth 2 provider says client authentication failed.", (_reason, _context) => new OAuthProviderException(_reason, _context))))
              .ExecuteAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Create a new OAuth 2 token.
        /// </summary>
        /// <param name="authorization">Required parameter: Authorization header in Basic auth format.</param>
        /// <param name="code">Required parameter: Authorization Code.</param>
        /// <param name="redirectUri">Required parameter: Redirect Uri.</param>
        /// <param name="fieldParameters">Additional optional form parameters are supported by this endpoint.</param>
        /// <returns>Returns the Models.OAuthToken response from the API call.</returns>
        public Models.OAuthToken RequestTokenOAuthACG(
                string authorization,
                string code,
                string redirectUri,
                Dictionary<string, object> fieldParameters = null)
            => CoreHelper.RunTask(RequestTokenOAuthACGAsync(authorization, code, redirectUri, fieldParameters));

        /// <summary>
        /// Create a new OAuth 2 token.
        /// </summary>
        /// <param name="authorization">Required parameter: Authorization header in Basic auth format.</param>
        /// <param name="code">Required parameter: Authorization Code.</param>
        /// <param name="redirectUri">Required parameter: Redirect Uri.</param>
        /// <param name="fieldParameters">Additional optional form parameters are supported by this endpoint.</param>
        /// <param name="cancellationToken"> cancellationToken. </param>
        /// <returns>Returns the Models.OAuthToken response from the API call.</returns>
        public async Task<Models.OAuthToken> RequestTokenOAuthACGAsync(
                string authorization,
                string code,
                string redirectUri,
                Dictionary<string, object> fieldParameters = null,
                CancellationToken cancellationToken = default)
            => await CreateApiCall<Models.OAuthToken>()
              .RequestBuilder(_requestBuilder => _requestBuilder
                  .Setup(HttpMethod.Post, "/oauth2/non-auth-server/token")
                  .Parameters(_parameters => _parameters
                      .AdditionalForms(_additionalForms => _additionalForms.Setup(fieldParameters))
                      .Form(_form => _form.Setup("grant_type", "authorization_code"))
                      .Header(_header => _header.Setup("Authorization", authorization))
                      .Form(_form => _form.Setup("code", code))
                      .Form(_form => _form.Setup("redirect_uri", redirectUri))))
              .ResponseHandler(_responseHandler => _responseHandler
                  .ErrorCase("400", CreateErrorCase("OAuth 2 provider returned an error.", (_reason, _context) => new OAuthProviderException(_reason, _context)))
                  .ErrorCase("401", CreateErrorCase("OAuth 2 provider says client authentication failed.", (_reason, _context) => new OAuthProviderException(_reason, _context))))
              .ExecuteAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Obtain a new access token using a refresh token.
        /// </summary>
        /// <param name="authorization">Required parameter: Authorization header in Basic auth format.</param>
        /// <param name="refreshToken">Required parameter: Refresh token.</param>
        /// <param name="scope">Optional parameter: Requested scopes as a space-delimited list..</param>
        /// <param name="fieldParameters">Additional optional form parameters are supported by this endpoint.</param>
        /// <returns>Returns the Models.OAuthToken response from the API call.</returns>
        public Models.OAuthToken RefreshTokenOAuthACG(
                string authorization,
                string refreshToken,
                string scope = null,
                Dictionary<string, object> fieldParameters = null)
            => CoreHelper.RunTask(RefreshTokenOAuthACGAsync(authorization, refreshToken, scope, fieldParameters));

        /// <summary>
        /// Obtain a new access token using a refresh token.
        /// </summary>
        /// <param name="authorization">Required parameter: Authorization header in Basic auth format.</param>
        /// <param name="refreshToken">Required parameter: Refresh token.</param>
        /// <param name="scope">Optional parameter: Requested scopes as a space-delimited list..</param>
        /// <param name="fieldParameters">Additional optional form parameters are supported by this endpoint.</param>
        /// <param name="cancellationToken"> cancellationToken. </param>
        /// <returns>Returns the Models.OAuthToken response from the API call.</returns>
        public async Task<Models.OAuthToken> RefreshTokenOAuthACGAsync(
                string authorization,
                string refreshToken,
                string scope = null,
                Dictionary<string, object> fieldParameters = null,
                CancellationToken cancellationToken = default)
            => await CreateApiCall<Models.OAuthToken>()
              .RequestBuilder(_requestBuilder => _requestBuilder
                  .Setup(HttpMethod.Post, "/oauth2/non-auth-server/token")
                  .Parameters(_parameters => _parameters
                      .AdditionalForms(_additionalForms => _additionalForms.Setup(fieldParameters))
                      .Form(_form => _form.Setup("grant_type", "refresh_token"))
                      .Header(_header => _header.Setup("Authorization", authorization))
                      .Form(_form => _form.Setup("refresh_token", refreshToken))
                      .Form(_form => _form.Setup("scope", scope))))
              .ResponseHandler(_responseHandler => _responseHandler
                  .ErrorCase("400", CreateErrorCase("OAuth 2 provider returned an error.", (_reason, _context) => new OAuthProviderException(_reason, _context)))
                  .ErrorCase("401", CreateErrorCase("OAuth 2 provider says client authentication failed.", (_reason, _context) => new OAuthProviderException(_reason, _context))))
              .ExecuteAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Create a new OAuth 2 token.
        /// </summary>
        /// <param name="authorization">Required parameter: Authorization header in Basic auth format.</param>
        /// <param name="username">Required parameter: Resource owner username.</param>
        /// <param name="password">Required parameter: Resource owner password.</param>
        /// <param name="scope">Optional parameter: Requested scopes as a space-delimited list..</param>
        /// <param name="fieldParameters">Additional optional form parameters are supported by this endpoint.</param>
        /// <returns>Returns the Models.OAuthToken response from the API call.</returns>
        public Models.OAuthToken RequestTokenOAuthROPCG(
                string authorization,
                string username,
                string password,
                string scope = null,
                Dictionary<string, object> fieldParameters = null)
            => CoreHelper.RunTask(RequestTokenOAuthROPCGAsync(authorization, username, password, scope, fieldParameters));

        /// <summary>
        /// Create a new OAuth 2 token.
        /// </summary>
        /// <param name="authorization">Required parameter: Authorization header in Basic auth format.</param>
        /// <param name="username">Required parameter: Resource owner username.</param>
        /// <param name="password">Required parameter: Resource owner password.</param>
        /// <param name="scope">Optional parameter: Requested scopes as a space-delimited list..</param>
        /// <param name="fieldParameters">Additional optional form parameters are supported by this endpoint.</param>
        /// <param name="cancellationToken"> cancellationToken. </param>
        /// <returns>Returns the Models.OAuthToken response from the API call.</returns>
        public async Task<Models.OAuthToken> RequestTokenOAuthROPCGAsync(
                string authorization,
                string username,
                string password,
                string scope = null,
                Dictionary<string, object> fieldParameters = null,
                CancellationToken cancellationToken = default)
            => await CreateApiCall<Models.OAuthToken>()
              .RequestBuilder(_requestBuilder => _requestBuilder
                  .Setup(HttpMethod.Post, "/oauth2/non-auth-server/token")
                  .Parameters(_parameters => _parameters
                      .AdditionalForms(_additionalForms => _additionalForms.Setup(fieldParameters))
                      .Form(_form => _form.Setup("grant_type", "password"))
                      .Header(_header => _header.Setup("Authorization", authorization))
                      .Form(_form => _form.Setup("username", username))
                      .Form(_form => _form.Setup("password", password))
                      .Form(_form => _form.Setup("scope", scope))))
              .ResponseHandler(_responseHandler => _responseHandler
                  .ErrorCase("400", CreateErrorCase("OAuth 2 provider returned an error.", (_reason, _context) => new OAuthProviderException(_reason, _context)))
                  .ErrorCase("401", CreateErrorCase("OAuth 2 provider says client authentication failed.", (_reason, _context) => new OAuthProviderException(_reason, _context))))
              .ExecuteAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Obtain a new access token using a refresh token.
        /// </summary>
        /// <param name="authorization">Required parameter: Authorization header in Basic auth format.</param>
        /// <param name="refreshToken">Required parameter: Refresh token.</param>
        /// <param name="scope">Optional parameter: Requested scopes as a space-delimited list..</param>
        /// <param name="fieldParameters">Additional optional form parameters are supported by this endpoint.</param>
        /// <returns>Returns the Models.OAuthToken response from the API call.</returns>
        public Models.OAuthToken RefreshTokenOAuthROPCG(
                string authorization,
                string refreshToken,
                string scope = null,
                Dictionary<string, object> fieldParameters = null)
            => CoreHelper.RunTask(RefreshTokenOAuthROPCGAsync(authorization, refreshToken, scope, fieldParameters));

        /// <summary>
        /// Obtain a new access token using a refresh token.
        /// </summary>
        /// <param name="authorization">Required parameter: Authorization header in Basic auth format.</param>
        /// <param name="refreshToken">Required parameter: Refresh token.</param>
        /// <param name="scope">Optional parameter: Requested scopes as a space-delimited list..</param>
        /// <param name="fieldParameters">Additional optional form parameters are supported by this endpoint.</param>
        /// <param name="cancellationToken"> cancellationToken. </param>
        /// <returns>Returns the Models.OAuthToken response from the API call.</returns>
        public async Task<Models.OAuthToken> RefreshTokenOAuthROPCGAsync(
                string authorization,
                string refreshToken,
                string scope = null,
                Dictionary<string, object> fieldParameters = null,
                CancellationToken cancellationToken = default)
            => await CreateApiCall<Models.OAuthToken>()
              .RequestBuilder(_requestBuilder => _requestBuilder
                  .Setup(HttpMethod.Post, "/oauth2/non-auth-server/token")
                  .Parameters(_parameters => _parameters
                      .AdditionalForms(_additionalForms => _additionalForms.Setup(fieldParameters))
                      .Form(_form => _form.Setup("grant_type", "refresh_token"))
                      .Header(_header => _header.Setup("Authorization", authorization))
                      .Form(_form => _form.Setup("refresh_token", refreshToken))
                      .Form(_form => _form.Setup("scope", scope))))
              .ResponseHandler(_responseHandler => _responseHandler
                  .ErrorCase("400", CreateErrorCase("OAuth 2 provider returned an error.", (_reason, _context) => new OAuthProviderException(_reason, _context)))
                  .ErrorCase("401", CreateErrorCase("OAuth 2 provider says client authentication failed.", (_reason, _context) => new OAuthProviderException(_reason, _context))))
              .ExecuteAsync(cancellationToken).ConfigureAwait(false);
    }
}