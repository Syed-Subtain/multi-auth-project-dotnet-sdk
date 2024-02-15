// <copyright file="ICustomAuthCredentials.cs" company="APIMatic">
// Copyright (c) APIMatic. All rights reserved.
// </copyright>
namespace MultiAuthSample.Standard.Authentication
{
    using System;

    /// <summary>
    /// Authentication configuration interface for CustomAuth.
    /// </summary>
    public interface ICustomAuthCredentials
    {
        /// <summary>
        ///  Returns true if credentials matched.
        /// </summary>
        /// <returns>True if credentials matched.</returns>
        bool Equals();
    }
}