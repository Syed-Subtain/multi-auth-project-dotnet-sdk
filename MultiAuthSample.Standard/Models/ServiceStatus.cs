// <copyright file="ServiceStatus.cs" company="APIMatic">
// Copyright (c) APIMatic. All rights reserved.
// </copyright>
namespace MultiAuthSample.Standard.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using APIMatic.Core.Utilities.Converters;
    using MultiAuthSample.Standard;
    using MultiAuthSample.Standard.Utilities;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// ServiceStatus.
    /// </summary>
    public class ServiceStatus : BaseModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceStatus"/> class.
        /// </summary>
        public ServiceStatus()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceStatus"/> class.
        /// </summary>
        /// <param name="status">status.</param>
        /// <param name="app">app.</param>
        /// <param name="moto">moto.</param>
        /// <param name="notes">notes.</param>
        /// <param name="users">users.</param>
        /// <param name="time">time.</param>
        /// <param name="os">os.</param>
        /// <param name="phpVersion">php_version.</param>
        public ServiceStatus(
            string status,
            string app = null,
            string moto = null,
            int? notes = null,
            int? users = null,
            string time = null,
            string os = null,
            string phpVersion = null)
        {
            this.App = app;
            this.Moto = moto;
            this.Notes = notes;
            this.Users = users;
            this.Time = time;
            this.Os = os;
            this.PhpVersion = phpVersion;
            this.Status = status;
        }

        /// <summary>
        /// Gets or sets App.
        /// </summary>
        [JsonProperty("app", NullValueHandling = NullValueHandling.Ignore)]
        public string App { get; set; }

        /// <summary>
        /// Gets or sets Moto.
        /// </summary>
        [JsonProperty("moto", NullValueHandling = NullValueHandling.Ignore)]
        public string Moto { get; set; }

        /// <summary>
        /// Gets or sets Notes.
        /// </summary>
        [JsonProperty("notes", NullValueHandling = NullValueHandling.Ignore)]
        public int? Notes { get; set; }

        /// <summary>
        /// Gets or sets Users.
        /// </summary>
        [JsonProperty("users", NullValueHandling = NullValueHandling.Ignore)]
        public int? Users { get; set; }

        /// <summary>
        /// Gets or sets Time.
        /// </summary>
        [JsonProperty("time", NullValueHandling = NullValueHandling.Ignore)]
        public string Time { get; set; }

        /// <summary>
        /// Gets or sets Os.
        /// </summary>
        [JsonProperty("os", NullValueHandling = NullValueHandling.Ignore)]
        public string Os { get; set; }

        /// <summary>
        /// Gets or sets PhpVersion.
        /// </summary>
        [JsonProperty("php_version", NullValueHandling = NullValueHandling.Ignore)]
        public string PhpVersion { get; set; }

        /// <summary>
        /// Gets or sets Status.
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <inheritdoc/>
        public override string ToString()
        {
            var toStringOutput = new List<string>();

            this.ToString(toStringOutput);

            return $"ServiceStatus : ({string.Join(", ", toStringOutput)})";
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (obj == this)
            {
                return true;
            }
            return obj is ServiceStatus other &&                ((this.App == null && other.App == null) || (this.App?.Equals(other.App) == true)) &&
                ((this.Moto == null && other.Moto == null) || (this.Moto?.Equals(other.Moto) == true)) &&
                ((this.Notes == null && other.Notes == null) || (this.Notes?.Equals(other.Notes) == true)) &&
                ((this.Users == null && other.Users == null) || (this.Users?.Equals(other.Users) == true)) &&
                ((this.Time == null && other.Time == null) || (this.Time?.Equals(other.Time) == true)) &&
                ((this.Os == null && other.Os == null) || (this.Os?.Equals(other.Os) == true)) &&
                ((this.PhpVersion == null && other.PhpVersion == null) || (this.PhpVersion?.Equals(other.PhpVersion) == true)) &&
                ((this.Status == null && other.Status == null) || (this.Status?.Equals(other.Status) == true));
        }
        
        /// <summary>
        /// ToString overload.
        /// </summary>
        /// <param name="toStringOutput">List of strings.</param>
        protected new void ToString(List<string> toStringOutput)
        {
            toStringOutput.Add($"this.App = {(this.App == null ? "null" : this.App)}");
            toStringOutput.Add($"this.Moto = {(this.Moto == null ? "null" : this.Moto)}");
            toStringOutput.Add($"this.Notes = {(this.Notes == null ? "null" : this.Notes.ToString())}");
            toStringOutput.Add($"this.Users = {(this.Users == null ? "null" : this.Users.ToString())}");
            toStringOutput.Add($"this.Time = {(this.Time == null ? "null" : this.Time)}");
            toStringOutput.Add($"this.Os = {(this.Os == null ? "null" : this.Os)}");
            toStringOutput.Add($"this.PhpVersion = {(this.PhpVersion == null ? "null" : this.PhpVersion)}");
            toStringOutput.Add($"this.Status = {(this.Status == null ? "null" : this.Status)}");

            base.ToString(toStringOutput);
        }
    }
}