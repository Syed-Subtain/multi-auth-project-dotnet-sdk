// <copyright file="ControllerTestBase.cs" company="APIMatic">
// Copyright (c) APIMatic. All rights reserved.
// </copyright>
namespace MultiAuthSample.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using APIMatic.Core.Types;
    using MultiAuthSample.Standard;
    using MultiAuthSample.Standard.Authentication;
    using MultiAuthSample.Standard.Exceptions;
    using MultiAuthSample.Standard.Http.Client;
    using MultiAuthSample.Standard.Models;
    using NUnit.Framework;

    /// <summary>
    /// ControllerTestBase Class.
    /// </summary>
    [TestFixture]
    public class ControllerTestBase
    {
        /// <summary>
        /// Assert precision.
        /// </summary>
        protected const double AssertPrecision = 0.1;

        /// <summary>
        /// Gets HttpCallBackHandler.
        /// </summary>
        internal HttpCallBack HttpCallBack { get; private set; } = new HttpCallBack();

        /// <summary>
        /// Gets MultiAuthSampleClient Client.
        /// </summary>
        protected MultiAuthSampleClient Client { get; private set; }

        /// <summary>
        /// Set up the client.
        /// </summary>
        [OneTimeSetUp]
        public void SetUp()
        {
            MultiAuthSampleClient config = MultiAuthSampleClient.CreateFromEnvironment();
            this.Client = config.ToBuilder()
                .HttpCallBack(HttpCallBack)
                .OAuthBearerTokenCredentials(new OAuthBearerTokenModel.Builder("azHmdOe09EdchxeWsdnplkQbv76sJH").Build())
                .AccessToken2("azHmdOe09EdchxeWsdnplkQbv76sJH")
                .Build();

            try
            {
                this.Client = this.Client.ToBuilder().OAuthCCGCredentials(Client.OAuthCCGModel.ToBuilder()
                    .OAuthToken(this.Client.OAuthCCGCredentials.FetchToken()).Build())
                    .Build();
            }
            catch (ApiException) 
            {
                // TODO Auto-generated catch block;
            }

            try
            {
                this.Client = this.Client.ToBuilder().OAuthACGCredentials(Client.OAuthACGModel.ToBuilder()
                    .OAuthToken(this.Client.OAuthACGCredentials.FetchToken("910b000d4f")).Build())
                    .Build();
            }
            catch (ApiException) 
            {
                // TODO Auto-generated catch block;
            }

            try
            {
                this.Client = this.Client.ToBuilder().OAuthROPCGCredentials(Client.OAuthROPCGModel.ToBuilder()
                    .OAuthToken(this.Client.OAuthROPCGCredentials.FetchToken()).Build())
                    .Build();
            }
            catch (ApiException) 
            {
                // TODO Auto-generated catch block;
            }
        }
    }
}