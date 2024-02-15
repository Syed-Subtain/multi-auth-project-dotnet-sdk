// <copyright file="OAuthScopeOAuthACGEnum.cs" company="APIMatic">
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
    using System.Reflection;

    /// <summary>
    /// OAuthScopeOAuthACGEnum.
    /// </summary>

    [JsonConverter(typeof(StringEnumConverter))]
    public enum OAuthScopeOAuthACGEnum
    {
        /// <summary>
        ///Read request for files
        /// ReadScope.
        /// </summary>
        [EnumMember(Value = "file_requests.read")]
        ReadScope
    }

    static class OAuthScopeOAuthACGEnumExtention
    {
        internal static string GetValues(this IEnumerable<OAuthScopeOAuthACGEnum> values)
        {
            return values != null ? string.Join(" ", values.Select(s => s.GetValue()).Where(s => !string.IsNullOrEmpty(s)).ToArray()) : null;
        }

        private static string GetValue(this Enum value)
        {
            return value.GetType()
                .GetTypeInfo()
                .DeclaredMembers
                .SingleOrDefault(x => x.Name == value.ToString())
                ?.GetCustomAttribute<EnumMemberAttribute>(false)
                ?.Value;
        }
    }
}