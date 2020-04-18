using System;

using FluentLicensing.Internals.Extensions;
using FluentLicensing.Jwt;

using Newtonsoft.Json;

namespace FluentLicensing
{
	public class LicenseKey<T>
	{
		[JsonProperty("ad")]
		private DateTime _activationDate = DateTime.UnixEpoch.ToUniversalTime();

		public DateTime ActivationDate
		{
			get => _activationDate.ToUniversalTime();
			set => _activationDate = value.ToUniversalTime();
		}

		[JsonProperty("ed")]
		private DateTime _expirationDate = DateTime.MaxValue.ToUniversalTime();

		public DateTime ExpirationDate
		{
			get => _expirationDate.ToUniversalTime();
			set => _expirationDate = value.ToUniversalTime();
		}

		[JsonProperty("lt")]
		public LicenseType LicenseType { get; set; } = LicenseType.Standard;

		[JsonProperty("ln")]
		public string LicenseName { get; set; } = string.Empty;

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

		public SignedLicense Sign(LicenseSigningParameters parameters)
			=> new SignedLicense(
				new JwtTokenManager(parameters).CreateSecurityToken(this),
				parameters.PublicKey);
	}
}