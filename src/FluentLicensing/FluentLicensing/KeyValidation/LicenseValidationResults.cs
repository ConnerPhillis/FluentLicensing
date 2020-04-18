﻿using System.Collections.Generic;
using System.Linq;

namespace FluentLicensing.KeyValidation
{
	public class LicenseValidationResults<T>
	{
		public bool HasErrors => Errors.Any();

		public IReadOnlyCollection<string> Errors { get; internal set; }

		public LicenseKey<T> LicenseKey { get; internal set; }
	}
}