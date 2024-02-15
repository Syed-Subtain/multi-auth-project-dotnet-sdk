// <copyright file="AuthenticationController.cs" company="APIMatic">
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
    using MultiAuthSample.Standard.Http.Client;
    using MultiAuthSample.Standard.Utilities;
    using Newtonsoft.Json.Converters;
    using System.Net.Http;

    /// <summary>
    /// AuthenticationController.
    /// </summary>
    public class AuthenticationController : BaseController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationController"/> class.
        /// </summary>
        internal AuthenticationController(GlobalConfiguration globalConfiguration) : base(globalConfiguration) { }

        /// <summary>
        /// OAuth Bearer Token EndPoint.
        /// </summary>
        /// <returns>Returns the string response from the API call.</returns>
        public string OAuthBearerToken()
            => CoreHelper.RunTask(OAuthBearerTokenAsync());

        /// <summary>
        /// OAuth Bearer Token EndPoint.
        /// </summary>
        /// <param name="cancellationToken"> cancellationToken. </param>
        /// <returns>Returns the string response from the API call.</returns>
        public async Task<string> OAuthBearerTokenAsync(CancellationToken cancellationToken = default)
            => await CreateApiCall<string>()
              .RequestBuilder(_requestBuilder => _requestBuilder
                  .Setup(HttpMethod.Get, "/auth/oauth2")
                  .WithAuth("OAuthBearerToken"))
              .ExecuteAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Custom Authentication EndPoint.
        /// </summary>
        /// <returns>Returns the string response from the API call.</returns>
        public string CustomAuthentication()
            => CoreHelper.RunTask(CustomAuthenticationAsync());

        /// <summary>
        /// Custom Authentication EndPoint.
        /// </summary>
        /// <param name="cancellationToken"> cancellationToken. </param>
        /// <returns>Returns the string response from the API call.</returns>
        public async Task<string> CustomAuthenticationAsync(CancellationToken cancellationToken = default)
            => await CreateApiCall<string>()
              .RequestBuilder(_requestBuilder => _requestBuilder
                  .Setup(HttpMethod.Get, "/auth/customAuthentication")
                  .WithAuth("CustomAuth"))
              .ExecuteAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Custom Query Or Header Authentication EndPoint.
        /// </summary>
        /// <returns>Returns the string response from the API call.</returns>
        public string CustomQueryOrHeaderAuthentication()
            => CoreHelper.RunTask(CustomQueryOrHeaderAuthenticationAsync());

        /// <summary>
        /// Custom Query Or Header Authentication EndPoint.
        /// </summary>
        /// <param name="cancellationToken"> cancellationToken. </param>
        /// <returns>Returns the string response from the API call.</returns>
        public async Task<string> CustomQueryOrHeaderAuthenticationAsync(CancellationToken cancellationToken = default)
            => await CreateApiCall<string>()
              .RequestBuilder(_requestBuilder => _requestBuilder
                  .Setup(HttpMethod.Get, "/auth/customQueryOrHeaderParam")
                  .WithOrAuth(_orAuth => _orAuth
                      .Add("apiKey")
                      .Add("apiHeader")
                  ))
              .ExecuteAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// This endpoint tests or combinations of OAuth types.
        /// </summary>
        /// <returns>Returns the string response from the API call.</returns>
        public string OAuthGrantTypesORCombinations()
            => CoreHelper.RunTask(OAuthGrantTypesORCombinationsAsync());

        /// <summary>
        /// This endpoint tests or combinations of OAuth types.
        /// </summary>
        /// <param name="cancellationToken"> cancellationToken. </param>
        /// <returns>Returns the string response from the API call.</returns>
        public async Task<string> OAuthGrantTypesORCombinationsAsync(CancellationToken cancellationToken = default)
            => await CreateApiCall<string>()
              .RequestBuilder(_requestBuilder => _requestBuilder
                  .Setup(HttpMethod.Get, "/oauth2/oauthOrCombination")
                  .WithOrAuth(_orAuth => _orAuth
                      .Add("OAuthCCG")
                      .Add("OAuthBearerToken")
                  ))
              .ExecuteAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// This endpoint does not use auth.
        /// </summary>
        /// <returns>Returns the string response from the API call.</returns>
        [Obsolete("   You should not use this method as it requires no auth and can bring security issues to the server and api call itself!!. This method was deprecated in version 0.0.1-alpha.")]
        public string NoAuth()
            => CoreHelper.RunTask(NoAuthAsync());

        /// <summary>
        /// This endpoint does not use auth.
        /// </summary>
        /// <param name="cancellationToken"> cancellationToken. </param>
        /// <returns>Returns the string response from the API call.</returns>
        [Obsolete("   You should not use this method as it requires no auth and can bring security issues to the server and api call itself!!. This method was deprecated in version 0.0.1-alpha.")]
        public async Task<string> NoAuthAsync(CancellationToken cancellationToken = default)
            => await CreateApiCall<string>()
              .RequestBuilder(_requestBuilder => _requestBuilder
                  .Setup(HttpMethod.Get, "/auth/noAuth")
                  .Parameters(_parameters => _parameters
                      .Query(_query => _query.Setup("array", "true"))))
              .ExecuteAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// OAuth Client Credentials Grant EndPoint.
        /// </summary>
        /// <returns>Returns the Models.ServiceStatus response from the API call.</returns>
        public Models.ServiceStatus OAuthClientCredentialsGrant()
            => CoreHelper.RunTask(OAuthClientCredentialsGrantAsync());

        /// <summary>
        /// OAuth Client Credentials Grant EndPoint.
        /// </summary>
        /// <param name="cancellationToken"> cancellationToken. </param>
        /// <returns>Returns the Models.ServiceStatus response from the API call.</returns>
        public async Task<Models.ServiceStatus> OAuthClientCredentialsGrantAsync(CancellationToken cancellationToken = default)
            => await CreateApiCall<Models.ServiceStatus>()
              .RequestBuilder(_requestBuilder => _requestBuilder
                  .Setup(HttpMethod.Get, "/oauth2/non-auth-server/status")
                  .WithAuth("OAuthCCG"))
              .ExecuteAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Basic Auth And Api/Header Auth EndPoint.
        /// </summary>
        /// <returns>Returns the string response from the API call.</returns>
        public string BasicAuthAndApiHeaderAuth()
            => CoreHelper.RunTask(BasicAuthAndApiHeaderAuthAsync());

        /// <summary>
        /// Basic Auth And Api/Header Auth EndPoint.
        /// </summary>
        /// <param name="cancellationToken"> cancellationToken. </param>
        /// <returns>Returns the string response from the API call.</returns>
        public async Task<string> BasicAuthAndApiHeaderAuthAsync(CancellationToken cancellationToken = default)
            => await CreateApiCall<string>()
              .RequestBuilder(_requestBuilder => _requestBuilder
                  .Setup(HttpMethod.Get, "/auth/basicAndApiKeyAndApiHeader")
                  .WithAndAuth(_andAuth => _andAuth
                      .Add("basicAuth")
                      .Add("apiKey")
                      .Add("apiHeader")
                  ))
              .ExecuteAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// OAuth Authorization Grant EndPoint.
        /// </summary>
        /// <returns>Returns the Models.User response from the API call.</returns>
        public Models.User OAuthAuthorizationGrant()
            => CoreHelper.RunTask(OAuthAuthorizationGrantAsync());

        /// <summary>
        /// OAuth Authorization Grant EndPoint.
        /// </summary>
        /// <param name="cancellationToken"> cancellationToken. </param>
        /// <returns>Returns the Models.User response from the API call.</returns>
        public async Task<Models.User> OAuthAuthorizationGrantAsync(CancellationToken cancellationToken = default)
            => await CreateApiCall<Models.User>()
              .RequestBuilder(_requestBuilder => _requestBuilder
                  .Setup(HttpMethod.Get, "/oauth2/non-auth-server/user")
                  .WithAndAuth(_andAuth => _andAuth
                      .Add("OAuthACG")
                      .Add("OAuthROPCG")
                  ))
              .ExecuteAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// This endpoint uses globally applied auth which is a hypothetical scneraio but covers the worst case.
        /// </summary>
        /// <returns>Returns the string response from the API call.</returns>
        [Obsolete]
        public string MultipleAuthCombination()
            => CoreHelper.RunTask(MultipleAuthCombinationAsync());

        /// <summary>
        /// This endpoint uses globally applied auth which is a hypothetical scneraio but covers the worst case.
        /// </summary>
        /// <param name="cancellationToken"> cancellationToken. </param>
        /// <returns>Returns the string response from the API call.</returns>
        [Obsolete]
        public async Task<string> MultipleAuthCombinationAsync(CancellationToken cancellationToken = default)
            => await CreateApiCall<string>()
              .RequestBuilder(_requestBuilder => _requestBuilder
                  .Setup(HttpMethod.Get, "/auth/multipleAuthCombination")
                  .WithOrAuth(_orAuth => _orAuth
                      .Add("CustomAuth")
                      .Add("OAuthBearerToken")
                      .AddAndGroup(_andAuth1 => _andAuth1
                          .Add("basicAuth")
                          .Add("apiKey")
                          .Add("apiHeader")
                      )
                  ))
              .ExecuteAsync(cancellationToken).ConfigureAwait(false);
    }
}