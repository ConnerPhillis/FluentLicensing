using System;
using System.Collections.Generic;
using System.Text;

using FluentLicensing.Tests.TestLicenses;

using Xunit;

namespace FluentLicensing.Tests
{
	public class LicenseKeyTests
	{
		LicenseKey<SimpleTestLicense> _license = new LicenseKey<SimpleTestLicense>();

		[Fact]
		public void ActivationDateIsUtc()
		{
			var date = DateTime.Now;
			_license.ActivationDate = date;
			Assert.Equal(date.ToUniversalTime(), _license.ActivationDate);
		}

		[Fact]
		public void ExpirationDateIsUTc()
		{
			var date = DateTime.Now;
			_license.ActivationDate = date;
			Assert.Equal(date.ToUniversalTime(), _license.ActivationDate);
		}

		[Fact]
		public void TestCanReadKeyData()
		{
			_license.KeyData = new SimpleTestLicense();
			Assert.NotNull(_license.KeyData);
		}
	}
}
