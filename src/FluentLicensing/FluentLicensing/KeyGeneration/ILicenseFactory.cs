using System;

namespace FluentLicensing.KeyGeneration
{
	public interface ILicenseFactory<T>
	{
		ILicenseFactory<T> WithActivationDate(DateTime activationDate);

		ILicenseFactory<T> WithExpirationDate(DateTime expirationDate);

		ILicenseFactory<T> WithLicenseType(LicenseType licenseType);

		ILicenseFactory<T> WithLicenseName(string licenseName);

		SignedLicense CreateAndSignLicense(LicenseSigningParameters parameters);

	}
}