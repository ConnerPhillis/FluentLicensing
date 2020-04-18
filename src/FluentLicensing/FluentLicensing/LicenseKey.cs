using System;

using FluentLicensing.Internals.Extensions;

using Newtonsoft.Json;

namespace FluentLicensing
{
	public class LicenseKey<T>
	{
		[JsonProperty("ad")]
		public DateTime ActivationDate { get; set; }

		[JsonProperty("ed")]
		public DateTime ExpirationDate { get; set; }

		[JsonProperty("lt")]
		public LicenseType LicenseType { get; set; } = LicenseType.Standard;

		[JsonProperty("ln")]
		public string LicenseName { get; set; }

		[JsonProperty("ekd")]
		internal string EncodedKeyData { get; set; }

		[JsonIgnore]
		public T KeyData
		{
			get => EncodedKeyData.FromBase64JsonString<T>();
			set => EncodedKeyData = value.ToBase64JsonString();
		}

		public LicenseKey()
		{
		}

		public LicenseKey(T keyData)
		{
			KeyData = keyData;
		}
	}
}