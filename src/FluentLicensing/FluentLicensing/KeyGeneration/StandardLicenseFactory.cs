using System;

using FluentLicensing.Jwt;

namespace FluentLicensing.KeyGeneration
{
	public class StandardLicenseFactory<T> : ILicenseFactory<T>
	{
		private readonly LicenseKey<T> _licenseKey;

		public StandardLicenseFactory(LicenseKey<T> licenseKey)
		{
			_licenseKey = licenseKey;
		}

		public ILicenseFactory<T> WithActivationDate(DateTime activationDate)
		{
			_licenseKey.ActivationDate = activationDate.ToUniversalTime();
			return this;
		}

		public ILicenseFactory<T> WithExpirationDate(DateTime expirationDate)
		{
			_licenseKey.ExpirationDate = expirationDate.ToUniversalTime();
			return this;
		}

		public ILicenseFactory<T> WithLicenseType(LicenseType licenseType)
		{
			_licenseKey.LicenseType = licenseType;
			return this;
		}

		public ILicenseFactory<T> WithLicenseName(string licenseName)
		{
			_licenseKey.LicenseName = licenseName;
			return this;
		}

		public SignedLicense CreateAndSignLicense(LicenseSigningParameters parameters)
			=> new SignedLicense(
				new JwtTokenManager(parameters).CreateSecurityToken(_licenseKey),
				parameters.PublicKey);
	}
}