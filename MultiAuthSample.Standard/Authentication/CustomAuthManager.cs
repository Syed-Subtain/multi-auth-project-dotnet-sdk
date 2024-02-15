// <copyright file="CustomAuthManager.cs" company="APIMatic">
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
/// CustomAuthManager Class.
/// </summary>
internal class CustomAuthManager : AuthManager, ICustomAuthCredentials
     {
        /// <summary>
        /// Constructor
        /// </summary>
        public CustomAuthManager()
        {
            // TODO: Add your custom authentication here
            // Parameters(parameters => parameters
            //     .Header(headerParameter => headerParameter.Setup("Key 1", "Value 1"))
            //     .Header(headerParameter => headerParameter.Setup("Key 2", "Value 2"))
            //     .Header(headerParameter => headerParameter.Setup("...", "...")));
        }


        /// <summary>
        /// Check if credentials match.
        /// </summary>
        /// <returns> True if credentials matched.</returns>
        public bool Equals()
        {
            return false;
        }
    }
}