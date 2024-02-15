// <copyright file="AuthenticationControllerTest.cs" company="APIMatic">
// Copyright (c) APIMatic. All rights reserved.
// </copyright>
namespace MultiAuthSample.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Threading.Tasks;
    using APIMatic.Core.Utilities;
    using MultiAuthSample.Standard;
    using MultiAuthSample.Standard.Controllers;
    using MultiAuthSample.Standard.Exceptions;
    using MultiAuthSample.Standard.Http.Client;
    using MultiAuthSample.Standard.Http.Response;
    using MultiAuthSample.Standard.Utilities;
    using Newtonsoft.Json.Converters;
    using NUnit.Framework;

    /// <summary>
    /// AuthenticationControllerTest.
    /// </summary>
    [TestFixture]
    public class AuthenticationControllerTest : ControllerTestBase
    {
        /// <summary>
        /// Controller instance (for all tests).
        /// </summary>
        private AuthenticationController controller;

        /// <summary>
        /// Setup test class.
        /// </summary>
        [OneTimeSetUp]
        public void SetUpDerived()
        {
            this.controller = this.Client.AuthenticationController;
        }

        /// <summary>
        /// TestOAuth2Test.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Test]
        public async Task TestOAuth2Test()
        {
            // Perform API call
            string result = null;
            try
            {
                result = await this.controller.OAuthBearerTokenAsync();
            }
            catch (ApiException)
            {
            }

            // Test response code
            Assert.AreEqual(200, HttpCallBack.Response.StatusCode, "Status should be 200");

            // Test whether the captured response is as we expected
            Assert.IsNotNull(result, "Result should exist");
            Assert.AreEqual("You've passed the test!", TestHelper.ConvertStreamToString(HttpCallBack.Response.RawBody), "Response body should match exactly (string literal match)");
        }

        /// <summary>
        /// TestCustomAuthenticationTest.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Test]
        public async Task TestCustomAuthenticationTest()
        {
            // Perform API call
            string result = null;
            try
            {
                result = await this.controller.CustomAuthenticationAsync();
            }
            catch (ApiException)
            {
            }

            // Test response code
            Assert.AreEqual(200, HttpCallBack.Response.StatusCode, "Status should be 200");

            // Test whether the captured response is as we expected
            Assert.IsNotNull(result, "Result should exist");
            Assert.AreEqual("You've passed the test!", TestHelper.ConvertStreamToString(HttpCallBack.Response.RawBody), "Response body should match exactly (string literal match)");
        }

        /// <summary>
        /// TestCustomQueryParamTest.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Test]
        public async Task TestCustomQueryParamTest()
        {
            // Perform API call
            string result = null;
            try
            {
                result = await this.controller.CustomQueryOrHeaderAuthenticationAsync();
            }
            catch (ApiException)
            {
            }

            // Test response code
            Assert.AreEqual(200, HttpCallBack.Response.StatusCode, "Status should be 200");

            // Test whether the captured response is as we expected
            Assert.IsNotNull(result, "Result should exist");
            Assert.AreEqual("You've passed the test!", TestHelper.ConvertStreamToString(HttpCallBack.Response.RawBody), "Response body should match exactly (string literal match)");
        }

        /// <summary>
        /// TestOAuthOrCombinationTest.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Test]
        public async Task TestOAuthOrCombinationTest()
        {
            // Perform API call
            string result = null;
            try
            {
                result = await this.controller.OAuthGrantTypesORCombinationsAsync();
            }
            catch (ApiException)
            {
            }

            // Test response code
            Assert.AreEqual(200, HttpCallBack.Response.StatusCode, "Status should be 200");

            // Test whether the captured response is as we expected
            Assert.IsNotNull(result, "Result should exist");
            Assert.AreEqual("You've passed the test!", TestHelper.ConvertStreamToString(HttpCallBack.Response.RawBody), "Response body should match exactly (string literal match)");
        }

        /// <summary>
        /// TestCheckServiceStatus.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Test]
        public async Task TestCheckServiceStatus()
        {
            // Perform API call
            Standard.Models.ServiceStatus result = null;
            try
            {
                result = await this.controller.OAuthClientCredentialsGrantAsync();
            }
            catch (ApiException)
            {
            }

            // Test response code
            Assert.AreEqual(200, HttpCallBack.Response.StatusCode, "Status should be 200");
        }

        /// <summary>
        /// TestBasicAuthAndApiKeyAndApiHeader.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Test]
        public async Task TestBasicAuthAndApiKeyAndApiHeader()
        {
            // Perform API call
            string result = null;
            try
            {
                result = await this.controller.BasicAuthAndApiHeaderAuthAsync();
            }
            catch (ApiException)
            {
            }

            // Test response code
            Assert.AreEqual(200, HttpCallBack.Response.StatusCode, "Status should be 200");

            // Test whether the captured response is as we expected
            Assert.IsNotNull(result, "Result should exist");
            Assert.AreEqual("You've passed the test!", TestHelper.ConvertStreamToString(HttpCallBack.Response.RawBody), "Response body should match exactly (string literal match)");
        }

        /// <summary>
        /// TestMultipleAuthOrCombinationTest.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Test]
        public async Task TestMultipleAuthOrCombinationTest()
        {
            // Perform API call
            string result = null;
            try
            {
                result = await this.controller.MultipleAuthCombinationAsync();
            }
            catch (ApiException)
            {
            }

            // Test response code
            Assert.AreEqual(200, HttpCallBack.Response.StatusCode, "Status should be 200");

            // Test whether the captured response is as we expected
            Assert.IsNotNull(result, "Result should exist");
            Assert.AreEqual("You've passed the test!", TestHelper.ConvertStreamToString(HttpCallBack.Response.RawBody), "Response body should match exactly (string literal match)");
        }
    }
}