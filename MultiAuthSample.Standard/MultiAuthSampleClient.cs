// <copyright file="MultiAuthSampleClient.cs" company="APIMatic">
// Copyright (c) APIMatic. All rights reserved.
// </copyright>
namespace MultiAuthSample.Standard
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using APIMatic.Core;
    using APIMatic.Core.Authentication;
    using APIMatic.Core.Types;
    using MultiAuthSample.Standard.Authentication;
    using MultiAuthSample.Standard.Controllers;
    using MultiAuthSample.Standard.Http.Client;
    using MultiAuthSample.Standard.Utilities;

    /// <summary>
    /// The gateway for the SDK. This class acts as a factory for Controller and
    /// holds the configuration of the SDK.
    /// </summary>
    public sealed class MultiAuthSampleClient : IConfiguration
    {
        // A map of environments and their corresponding servers/baseurls
        private static readonly Dictionary<Environment, Dictionary<Enum, string>> EnvironmentsMap =
            new Dictionary<Environment, Dictionary<Enum, string>>
        {
            {
                Environment.Production, new Dictionary<Enum, string>
                {
                    { Server.Default, "http://apimatic.hopto.org:{suites}" },
                    { Server.Auth, "http://apimaticauth.hopto.org:3000" },
                }
            },
            {
                Environment.Testing, new Dictionary<Enum, string>
                {
                    { Server.Default, "http://localhost:3000" },
                    { Server.Auth, "http://localhost:3000/oauth2/auth-server" },
                }
            },
        };

        private readonly GlobalConfiguration globalConfiguration;
        private const string userAgent = "APIMATIC 3.0";
        private readonly HttpCallBack httpCallBack;
        private readonly Lazy<AuthenticationController> authentication;
        private readonly Lazy<OAuthAuthorizationController> oAuthAuthorization;

        private MultiAuthSampleClient(
            string accessToken2,
            Environment environment,
            string port,
            Models.SuiteCodeEnum suites,
            BasicAuthModel basicAuthModel,
            ApiKeyModel apiKeyModel,
            ApiHeaderModel apiHeaderModel,
            OAuthCCGModel oAuthCCGModel,
            OAuthACGModel oAuthACGModel,
            OAuthROPCGModel oAuthROPCGModel,
            OAuthBearerTokenModel oAuthBearerTokenModel,
            CustomAuthManager customAuthManager,
            HttpCallBack httpCallBack,
            IHttpClientConfiguration httpClientConfiguration)
        {
            this.AccessToken2 = accessToken2;
            this.Environment = environment;
            this.Port = port;
            this.Suites = suites;
            this.httpCallBack = httpCallBack;
            this.HttpClientConfiguration = httpClientConfiguration;
            BasicAuthModel = basicAuthModel;
            var basicAuthManager = new BasicAuthManager(basicAuthModel);
            ApiKeyModel = apiKeyModel;
            var apiKeyManager = new ApiKeyManager(apiKeyModel);
            ApiHeaderModel = apiHeaderModel;
            var apiHeaderManager = new ApiHeaderManager(apiHeaderModel);
            OAuthCCGModel = oAuthCCGModel;
            var oAuthCCGManager = new OAuthCCGManager(oAuthCCGModel);
            oAuthCCGManager.ApplyGlobalConfiguration(() => OAuthAuthorizationController);
            OAuthACGModel = oAuthACGModel;
            var oAuthACGManager = new OAuthACGManager(oAuthACGModel);
            oAuthACGManager.ApplyGlobalConfiguration(() => OAuthAuthorizationController);
            OAuthROPCGModel = oAuthROPCGModel;
            var oAuthROPCGManager = new OAuthROPCGManager(oAuthROPCGModel);
            oAuthROPCGManager.ApplyGlobalConfiguration(() => OAuthAuthorizationController);
            OAuthBearerTokenModel = oAuthBearerTokenModel;
            var oAuthBearerTokenManager = new OAuthBearerTokenManager(oAuthBearerTokenModel);
            globalConfiguration = new GlobalConfiguration.Builder()
                .AuthManagers(new Dictionary<string, AuthManager> {
                    {"basicAuth", basicAuthManager},
                    {"apiKey", apiKeyManager},
                    {"apiHeader", apiHeaderManager},
                    {"OAuthCCG", oAuthCCGManager},
                    {"OAuthACG", oAuthACGManager},
                    {"OAuthROPCG", oAuthROPCGManager},
                    {"OAuthBearerToken", oAuthBearerTokenManager},
                    {"CustomAuth", customAuthManager},
                })
                .ApiCallback(httpCallBack)
                .HttpConfiguration(httpClientConfiguration)
                .ServerUrls(EnvironmentsMap[environment], Server.Default)
                .Parameters(globalParameter => globalParameter
                    .Header(headerParameter => headerParameter.Setup("accessToken", this.AccessToken2))
                    .Template(templateParameter => templateParameter.Setup("port", this.Port))
                    .Template(templateParameter => templateParameter.Setup("suites", (int)this.Suites)))
                .UserAgent(userAgent)
                .Build();

            BasicAuthCredentials = basicAuthManager;
            ApiKeyCredentials = apiKeyManager;
            ApiHeaderCredentials = apiHeaderManager;
            OAuthCCGCredentials = oAuthCCGManager;
            OAuthACGCredentials = oAuthACGManager;
            OAuthROPCGCredentials = oAuthROPCGManager;
            OAuthBearerTokenCredentials = oAuthBearerTokenManager;
            CustomAuthCredentials = customAuthManager;

            this.authentication = new Lazy<AuthenticationController>(
                () => new AuthenticationController(globalConfiguration));
            this.oAuthAuthorization = new Lazy<OAuthAuthorizationController>(
                () => new OAuthAuthorizationController(globalConfiguration));
        }

        /// <summary>
        /// Gets AuthenticationController controller.
        /// </summary>
        public AuthenticationController AuthenticationController => this.authentication.Value;

        /// <summary>
        /// Gets OAuthAuthorizationController controller.
        /// </summary>
        public OAuthAuthorizationController OAuthAuthorizationController => this.oAuthAuthorization.Value;

        /// <summary>
        /// Gets the configuration of the Http Client associated with this client.
        /// </summary>
        public IHttpClientConfiguration HttpClientConfiguration { get; }

        /// <summary>
         /// Gets AccessToken2.
        /// </summary>
        public string AccessToken2 { get; }

        /// <summary>
        /// Gets Environment.
        /// Current API environment.
        /// </summary>
        public Environment Environment { get; }

        /// <summary>
        /// Gets Port.
        /// Port value.
        /// </summary>
        public string Port { get; }

        /// <summary>
        /// Gets Suites.
        /// Suites value.
        /// </summary>
        public Models.SuiteCodeEnum Suites { get; }

        /// <summary>
        /// Gets http callback.
        /// </summary>
        internal HttpCallBack HttpCallBack => this.httpCallBack;

        /// <summary>
        /// Gets the credentials to use with BasicAuth.
        /// </summary>
        public IBasicAuthCredentials BasicAuthCredentials { get; private set; }

        /// <summary>
        /// Gets the credentials model to use with BasicAuth.
        /// </summary>
        public BasicAuthModel BasicAuthModel { get; private set; }

        /// <summary>
        /// Gets the credentials to use with ApiKey.
        /// </summary>
        public IApiKeyCredentials ApiKeyCredentials { get; private set; }

        /// <summary>
        /// Gets the credentials model to use with ApiKey.
        /// </summary>
        public ApiKeyModel ApiKeyModel { get; private set; }

        /// <summary>
        /// Gets the credentials to use with ApiHeader.
        /// </summary>
        public IApiHeaderCredentials ApiHeaderCredentials { get; private set; }

        /// <summary>
        /// Gets the credentials model to use with ApiHeader.
        /// </summary>
        public ApiHeaderModel ApiHeaderModel { get; private set; }

        /// <summary>
        /// Gets the credentials to use with OAuthCCG.
        /// </summary>
        public IOAuthCCGCredentials OAuthCCGCredentials { get; private set; }

        /// <summary>
        /// Gets the credentials model to use with OAuthCCG.
        /// </summary>
        public OAuthCCGModel OAuthCCGModel { get; private set; }

        /// <summary>
        /// Gets the credentials to use with OAuthACG.
        /// </summary>
        public IOAuthACGCredentials OAuthACGCredentials { get; private set; }

        /// <summary>
        /// Gets the credentials model to use with OAuthACG.
        /// </summary>
        public OAuthACGModel OAuthACGModel { get; private set; }

        /// <summary>
        /// Gets the credentials to use with OAuthROPCG.
        /// </summary>
        public IOAuthROPCGCredentials OAuthROPCGCredentials { get; private set; }

        /// <summary>
        /// Gets the credentials model to use with OAuthROPCG.
        /// </summary>
        public OAuthROPCGModel OAuthROPCGModel { get; private set; }

        /// <summary>
        /// Gets the credentials to use with OAuthBearerToken.
        /// </summary>
        public IOAuthBearerTokenCredentials OAuthBearerTokenCredentials { get; private set; }

        /// <summary>
        /// Gets the credentials model to use with OAuthBearerToken.
        /// </summary>
        public OAuthBearerTokenModel OAuthBearerTokenModel { get; private set; }

        /// <summary>
        /// Gets the access token to use with OAuth 2 authentication.
        /// </summary>
        public string AccessToken => this.OAuthBearerTokenCredentials.AccessToken;

        /// <summary>
        /// Gets the credentials to use with CustomAuth.
        /// </summary>
        public ICustomAuthCredentials CustomAuthCredentials { get; private set; }


        /// <summary>
        /// Gets the URL for a particular alias in the current environment and appends
        /// it with template parameters.
        /// </summary>
        /// <param name="alias">Default value:DEFAULT.</param>
        /// <returns>Returns the baseurl.</returns>
        public string GetBaseUri(Server alias = Server.Default)
        {
            return globalConfiguration.ServerUrl(alias);
        }

        /// <summary>
        /// Creates an object of the MultiAuthSampleClient using the values provided for the builder.
        /// </summary>
        /// <returns>Builder.</returns>
        public Builder ToBuilder()
        {
            Builder builder = new Builder()
                .AccessToken2(this.AccessToken2)
                .Environment(this.Environment)
                .Port(this.Port)
                .Suites(this.Suites)
                .HttpCallBack(httpCallBack)
                .HttpClientConfig(config => config.Build());

            if (BasicAuthModel != null)
            {
                builder.BasicAuthCredentials(BasicAuthModel.ToBuilder().Build());
            }

            if (ApiKeyModel != null)
            {
                builder.ApiKeyCredentials(ApiKeyModel.ToBuilder().Build());
            }

            if (ApiHeaderModel != null)
            {
                builder.ApiHeaderCredentials(ApiHeaderModel.ToBuilder().Build());
            }

            if (OAuthCCGModel != null)
            {
                builder.OAuthCCGCredentials(OAuthCCGModel.ToBuilder().Build());
            }

            if (OAuthACGModel != null)
            {
                builder.OAuthACGCredentials(OAuthACGModel.ToBuilder().Build());
            }

            if (OAuthROPCGModel != null)
            {
                builder.OAuthROPCGCredentials(OAuthROPCGModel.ToBuilder().Build());
            }

            if (OAuthBearerTokenModel != null)
            {
                builder.OAuthBearerTokenCredentials(OAuthBearerTokenModel.ToBuilder().Build());
            }

            return builder;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return
                $"AccessToken2 = {this.AccessToken2}, " +
                $"Environment = {this.Environment}, " +
                $"Port = {this.Port}, " +
                $"Suites = {this.Suites}, " +
                $"HttpClientConfiguration = {this.HttpClientConfiguration}, ";
        }

        /// <summary>
        /// Creates the client using builder.
        /// </summary>
        /// <returns> MultiAuthSampleClient.</returns>
        internal static MultiAuthSampleClient CreateFromEnvironment()
        {
            var builder = new Builder();

            string accessToken2 = System.Environment.GetEnvironmentVariable("MULTI_AUTH_SAMPLE_STANDARD_ACCESS_TOKEN_2");
            string environment = System.Environment.GetEnvironmentVariable("MULTI_AUTH_SAMPLE_STANDARD_ENVIRONMENT");
            string port = System.Environment.GetEnvironmentVariable("MULTI_AUTH_SAMPLE_STANDARD_PORT");
            string suites = System.Environment.GetEnvironmentVariable("MULTI_AUTH_SAMPLE_STANDARD_SUITES");
            string username = System.Environment.GetEnvironmentVariable("MULTI_AUTH_SAMPLE_STANDARD_USERNAME");
            string password = System.Environment.GetEnvironmentVariable("MULTI_AUTH_SAMPLE_STANDARD_PASSWORD");
            string token = System.Environment.GetEnvironmentVariable("MULTI_AUTH_SAMPLE_STANDARD_TOKEN");
            string apiKey = System.Environment.GetEnvironmentVariable("MULTI_AUTH_SAMPLE_STANDARD_API_KEY");
            string token2 = System.Environment.GetEnvironmentVariable("MULTI_AUTH_SAMPLE_STANDARD_TOKEN_2");
            string apiKey2 = System.Environment.GetEnvironmentVariable("MULTI_AUTH_SAMPLE_STANDARD_API_KEY_2");
            string oAuthClientId = System.Environment.GetEnvironmentVariable("MULTI_AUTH_SAMPLE_STANDARD_O_AUTH_CLIENT_ID");
            string oAuthClientSecret = System.Environment.GetEnvironmentVariable("MULTI_AUTH_SAMPLE_STANDARD_O_AUTH_CLIENT_SECRET");
            string oAuthClientId2 = System.Environment.GetEnvironmentVariable("MULTI_AUTH_SAMPLE_STANDARD_O_AUTH_CLIENT_ID_2");
            string oAuthClientSecret2 = System.Environment.GetEnvironmentVariable("MULTI_AUTH_SAMPLE_STANDARD_O_AUTH_CLIENT_SECRET_2");
            string oAuthRedirectUri2 = System.Environment.GetEnvironmentVariable("MULTI_AUTH_SAMPLE_STANDARD_O_AUTH_REDIRECT_URI_2");
            string oAuthClientId3 = System.Environment.GetEnvironmentVariable("MULTI_AUTH_SAMPLE_STANDARD_O_AUTH_CLIENT_ID_3");
            string oAuthClientSecret3 = System.Environment.GetEnvironmentVariable("MULTI_AUTH_SAMPLE_STANDARD_O_AUTH_CLIENT_SECRET_3");
            string oAuthUsername3 = System.Environment.GetEnvironmentVariable("MULTI_AUTH_SAMPLE_STANDARD_O_AUTH_USERNAME_3");
            string oAuthPassword3 = System.Environment.GetEnvironmentVariable("MULTI_AUTH_SAMPLE_STANDARD_O_AUTH_PASSWORD_3");
            string accessToken = System.Environment.GetEnvironmentVariable("MULTI_AUTH_SAMPLE_STANDARD_ACCESS_TOKEN");

            if (accessToken2 != null)
            {
                builder.AccessToken2(accessToken2);
            }

            if (environment != null)
            {
                builder.Environment(ApiHelper.JsonDeserialize<Environment>($"\"{environment}\""));
            }

            if (port != null)
            {
                builder.Port(port);
            }

            if (suites != null)
            {
                builder.Suites(ApiHelper.JsonDeserialize<Models.SuiteCodeEnum>($"\"{suites}\""));
            }

            if (username != null && password != null)
            {
                builder.BasicAuthCredentials(new BasicAuthModel
                .Builder(username, password)
                .Build());
            }

            if (token != null && apiKey != null)
            {
                builder.ApiKeyCredentials(new ApiKeyModel
                .Builder(token, apiKey)
                .Build());
            }

            if (token2 != null && apiKey2 != null)
            {
                builder.ApiHeaderCredentials(new ApiHeaderModel
                .Builder(token2, apiKey2)
                .Build());
            }

            if (oAuthClientId != null && oAuthClientSecret != null)
            {
                builder.OAuthCCGCredentials(new OAuthCCGModel
                .Builder(oAuthClientId, oAuthClientSecret)
                .Build());
            }

            if (oAuthClientId2 != null && oAuthClientSecret2 != null && oAuthRedirectUri2 != null)
            {
                builder.OAuthACGCredentials(new OAuthACGModel
                .Builder(oAuthClientId2, oAuthClientSecret2, oAuthRedirectUri2)
                .Build());
            }

            if (oAuthClientId3 != null && oAuthClientSecret3 != null && oAuthUsername3 != null && oAuthPassword3 != null)
            {
                builder.OAuthROPCGCredentials(new OAuthROPCGModel
                .Builder(oAuthClientId3, oAuthClientSecret3, oAuthUsername3, oAuthPassword3)
                .Build());
            }

            if (accessToken != null)
            {
                builder.OAuthBearerTokenCredentials(new OAuthBearerTokenModel
                .Builder(accessToken)
                .Build());
            }

            return builder.Build();
        }

        /// <summary>
        /// Builder class.
        /// </summary>
        public class Builder
        {
            private string accessToken2 = String.Empty;
            private Environment environment = MultiAuthSample.Standard.Environment.Testing;
            private string port = "80";
            private Models.SuiteCodeEnum suites = Models.SuiteCodeEnum.Hearts;
            private BasicAuthModel basicAuthModel = new BasicAuthModel();
            private ApiKeyModel apiKeyModel = new ApiKeyModel();
            private ApiHeaderModel apiHeaderModel = new ApiHeaderModel();
            private OAuthCCGModel oAuthCCGModel = new OAuthCCGModel();
            private OAuthACGModel oAuthACGModel = new OAuthACGModel();
            private OAuthROPCGModel oAuthROPCGModel = new OAuthROPCGModel();
            private OAuthBearerTokenModel oAuthBearerTokenModel = new OAuthBearerTokenModel();
            private HttpClientConfiguration.Builder httpClientConfig = new HttpClientConfiguration.Builder();
            private HttpCallBack httpCallBack;

            /// <summary>
            /// Sets credentials for BasicAuth.
            /// </summary>
            /// <param name="basicAuthModel">BasicAuthModel.</param>
            /// <returns>Builder.</returns>
            public Builder BasicAuthCredentials(BasicAuthModel basicAuthModel)
            {
                if (basicAuthModel is null)
                {
                    throw new ArgumentNullException(nameof(basicAuthModel));
                }

                this.basicAuthModel = basicAuthModel;
                return this;
            }

            /// <summary>
            /// Sets credentials for ApiKey.
            /// </summary>
            /// <param name="apiKeyModel">ApiKeyModel.</param>
            /// <returns>Builder.</returns>
            public Builder ApiKeyCredentials(ApiKeyModel apiKeyModel)
            {
                if (apiKeyModel is null)
                {
                    throw new ArgumentNullException(nameof(apiKeyModel));
                }

                this.apiKeyModel = apiKeyModel;
                return this;
            }

            /// <summary>
            /// Sets credentials for ApiHeader.
            /// </summary>
            /// <param name="apiHeaderModel">ApiHeaderModel.</param>
            /// <returns>Builder.</returns>
            public Builder ApiHeaderCredentials(ApiHeaderModel apiHeaderModel)
            {
                if (apiHeaderModel is null)
                {
                    throw new ArgumentNullException(nameof(apiHeaderModel));
                }

                this.apiHeaderModel = apiHeaderModel;
                return this;
            }

            /// <summary>
            /// Sets credentials for OAuthCCG.
            /// </summary>
            /// <param name="oAuthCCGModel">OAuthCCGModel.</param>
            /// <returns>Builder.</returns>
            public Builder OAuthCCGCredentials(OAuthCCGModel oAuthCCGModel)
            {
                if (oAuthCCGModel is null)
                {
                    throw new ArgumentNullException(nameof(oAuthCCGModel));
                }

                this.oAuthCCGModel = oAuthCCGModel;
                return this;
            }

            /// <summary>
            /// Sets credentials for OAuthACG.
            /// </summary>
            /// <param name="oAuthACGModel">OAuthACGModel.</param>
            /// <returns>Builder.</returns>
            public Builder OAuthACGCredentials(OAuthACGModel oAuthACGModel)
            {
                if (oAuthACGModel is null)
                {
                    throw new ArgumentNullException(nameof(oAuthACGModel));
                }

                this.oAuthACGModel = oAuthACGModel;
                return this;
            }

            /// <summary>
            /// Sets credentials for OAuthROPCG.
            /// </summary>
            /// <param name="oAuthROPCGModel">OAuthROPCGModel.</param>
            /// <returns>Builder.</returns>
            public Builder OAuthROPCGCredentials(OAuthROPCGModel oAuthROPCGModel)
            {
                if (oAuthROPCGModel is null)
                {
                    throw new ArgumentNullException(nameof(oAuthROPCGModel));
                }

                this.oAuthROPCGModel = oAuthROPCGModel;
                return this;
            }

            /// <summary>
            /// Sets credentials for OAuthBearerToken.
            /// </summary>
            /// <param name="oAuthBearerTokenModel">OAuthBearerTokenModel.</param>
            /// <returns>Builder.</returns>
            public Builder OAuthBearerTokenCredentials(OAuthBearerTokenModel oAuthBearerTokenModel)
            {
                if (oAuthBearerTokenModel is null)
                {
                    throw new ArgumentNullException(nameof(oAuthBearerTokenModel));
                }

                this.oAuthBearerTokenModel = oAuthBearerTokenModel;
                return this;
            }

            /// <summary>
            /// Sets AccessToken2.
            /// </summary>
            /// <param name="accessToken2"> AccessToken2. </param>
            /// <returns> Builder. </returns>
            public Builder AccessToken2(string accessToken2)
            {
                this.accessToken2 = accessToken2 ?? throw new ArgumentNullException(nameof(accessToken2));
                return this;
            }

            /// <summary>
            /// Sets Environment.
            /// </summary>
            /// <param name="environment"> Environment. </param>
            /// <returns> Builder. </returns>
            public Builder Environment(Environment environment)
            {
                this.environment = environment;
                return this;
            }

            /// <summary>
            /// Sets Port.
            /// </summary>
            /// <param name="port"> Port. </param>
            /// <returns> Builder. </returns>
            public Builder Port(string port)
            {
                this.port = port ?? throw new ArgumentNullException(nameof(port));
                return this;
            }

            /// <summary>
            /// Sets Suites.
            /// </summary>
            /// <param name="suites"> Suites. </param>
            /// <returns> Builder. </returns>
            public Builder Suites(Models.SuiteCodeEnum suites)
            {
                this.suites = suites;
                return this;
            }

            /// <summary>
            /// Sets HttpClientConfig.
            /// </summary>
            /// <param name="action"> Action. </param>
            /// <returns>Builder.</returns>
            public Builder HttpClientConfig(Action<HttpClientConfiguration.Builder> action)
            {
                if (action is null)
                {
                    throw new ArgumentNullException(nameof(action));
                }

                action(this.httpClientConfig);
                return this;
            }

           

            /// <summary>
            /// Sets the HttpCallBack for the Builder.
            /// </summary>
            /// <param name="httpCallBack"> http callback. </param>
            /// <returns>Builder.</returns>
            internal Builder HttpCallBack(HttpCallBack httpCallBack)
            {
                this.httpCallBack = httpCallBack;
                return this;
            }

            /// <summary>
            /// Creates an object of the MultiAuthSampleClient using the values provided for the builder.
            /// </summary>
            /// <returns>MultiAuthSampleClient.</returns>
            public MultiAuthSampleClient Build()
            {

                if (basicAuthModel.Username == null || basicAuthModel.Password == null)
                {
                    basicAuthModel = null;
                }
                if (apiKeyModel.Token == null || apiKeyModel.ApiKey == null)
                {
                    apiKeyModel = null;
                }
                if (apiHeaderModel.Token == null || apiHeaderModel.ApiKey == null)
                {
                    apiHeaderModel = null;
                }
                if (oAuthCCGModel.OAuthClientId == null || oAuthCCGModel.OAuthClientSecret == null)
                {
                    oAuthCCGModel = null;
                }
                if (oAuthACGModel.OAuthClientId == null || oAuthACGModel.OAuthClientSecret == null || oAuthACGModel.OAuthRedirectUri == null)
                {
                    oAuthACGModel = null;
                }
                if (oAuthROPCGModel.OAuthClientId == null || oAuthROPCGModel.OAuthClientSecret == null || oAuthROPCGModel.OAuthUsername == null || oAuthROPCGModel.OAuthPassword == null)
                {
                    oAuthROPCGModel = null;
                }
                if (oAuthBearerTokenModel.AccessToken == null)
                {
                    oAuthBearerTokenModel = null;
                }
                return new MultiAuthSampleClient(
                    accessToken2,
                    environment,
                    port,
                    suites,
                    basicAuthModel,
                    apiKeyModel,
                    apiHeaderModel,
                    oAuthCCGModel,
                    oAuthACGModel,
                    oAuthROPCGModel,
                    oAuthBearerTokenModel,
                    new CustomAuthManager(),
                    httpCallBack,
                    httpClientConfig.Build());
            }
        }
    }
}
