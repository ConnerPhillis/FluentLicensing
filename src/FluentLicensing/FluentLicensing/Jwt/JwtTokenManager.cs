﻿using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

using FluentLicensing.Internals.Extensions;

using Microsoft.IdentityModel.Tokens;

namespace FluentLicensing.Jwt
{
	internal sealed class JwtTokenManager
	{
		internal const string LicenseDataClaim = "ld";

		private readonly LicenseSigningParameters _signingParameters;

		public JwtTokenManager(LicenseSigningParameters signingParameters)
		{
			_signingParameters = signingParameters;
		}


		internal string CreateSecurityToken<T>(LicenseKey<T> licenseKey)
			=> CreateSecurityTokenInternal(licenseKey.ToBase64JsonString(), licenseKey.LicenseName);

		private string CreateSecurityTokenInternal(string licenseData, string licenseName)
		{
			if (_signingParameters.PrivateKey.Length == 0)
				throw new InvalidOperationException("Cannot sign license without private key");

			var handler = new JwtSecurityTokenHandler();

			var claimsIdentity = new ClaimsIdentity();
			claimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, licenseName));
			claimsIdentity.AddClaim(new Claim(LicenseDataClaim, licenseData));

			var tokenParams = new SecurityTokenDescriptor
			{
				Subject = claimsIdentity, SigningCredentials = GenerateSigningCredentials()
			};

			var token = handler.CreateJwtSecurityToken(tokenParams);
			return handler.WriteToken(token);
		}

		internal LicenseKey<T> ParseSecurityToken<T>(string token)
			=> GetLicenseData(token)
			   .FromBase64JsonString<LicenseKey<T>>();

		private string GetLicenseData(string token)
		{
			var handler = new JwtSecurityTokenHandler();

			var validationParameters = new TokenValidationParameters
			{
				ValidateIssuer = false,
				ValidateAudience = false,
				ValidateLifetime = false,
				IssuerSigningKey =
					new RsaSecurityKey(_signingParameters.rsa.ExportParameters(false))
			};

			var claimsPrincipal = handler.ValidateToken(token, validationParameters, out _);

			if (!claimsPrincipal.Claims.Any())
				throw new InvalidOperationException("token has no claims");

			return claimsPrincipal.Claims.First()
			   .Subject.Claims.First(value => value.Type == LicenseDataClaim)
			   .Value;
		}

		private SigningCredentials GenerateSigningCredentials()
			=> new SigningCredentials(
				new RsaSecurityKey(_signingParameters.rsa),
				SecurityAlgorithms.RsaSsaPssSha256);
	}
}