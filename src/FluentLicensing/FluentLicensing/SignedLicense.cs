namespace FluentLicensing
{
	public class SignedLicense
	{
		public string LicenseData { get; internal set; }
		public byte[] PublicKey { get; internal set; }

		public SignedLicense(string licenseData, byte[] publicKey)
		{
			LicenseData = licenseData;
			PublicKey = publicKey;
		}

	}
}
