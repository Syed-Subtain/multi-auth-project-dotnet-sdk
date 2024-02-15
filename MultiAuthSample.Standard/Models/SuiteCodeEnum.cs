// <copyright file="SuiteCodeEnum.cs" company="APIMatic">
// Copyright (c) APIMatic. All rights reserved.
// </copyright>
namespace MultiAuthSample.Standard.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using APIMatic.Core.Utilities.Converters;
    using MultiAuthSample.Standard;
    using MultiAuthSample.Standard.Utilities;
    using Newtonsoft.Json;

    /// <summary>
    /// SuiteCodeEnum.
    /// </summary>

    [JsonConverter(typeof(NumberEnumConverter))]
    public enum SuiteCodeEnum
    {
        /// <summary>
        /// Hearts.
        /// </summary>
        Hearts = 1,

        /// <summary>
        /// Spades.
        /// </summary>
        Spades = 2,

        /// <summary>
        /// Clubs.
        /// </summary>
        Clubs = 3,

        /// <summary>
        /// Diamonds.
        /// </summary>
        Diamonds = 4
    }
}