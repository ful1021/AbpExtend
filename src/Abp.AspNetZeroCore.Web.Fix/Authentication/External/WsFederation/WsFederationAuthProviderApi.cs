// Decompiled with JetBrains decompiler
// Type: Abp.AspNetZeroCore.Web.Authentication.External.WsFederation.WsFederationAuthProviderApi
// Assembly: Abp.AspNetZeroCore.Web, Version=1.2.2.0, Culture=neutral, PublicKeyToken=null
// MVID: E82D7A41-87A0-49FB-853C-F00596815594
// Assembly location: C:\Users\fuliang\.nuget\packages\abp.aspnetzerocore.web\1.2.2\lib\netcoreapp2.2\Abp.AspNetZeroCore.Web.dll

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.WsFederation;
using Microsoft.IdentityModel.Tokens;

namespace Abp.AspNetZeroCore.Web.Authentication.External.WsFederation
{
    public class WsFederationAuthProviderApi : ExternalAuthProviderApiBase
    {
        public const string Name = "WsFederation";

        public override async Task<ExternalAuthUserInfo> GetUserInfo(
          string token)
        {
            WsFederationAuthProviderApi federationAuthProviderApi = this;
            // ISSUE: explicit non-virtual call
            string additionalParam1 = federationAuthProviderApi != null && federationAuthProviderApi.ProviderInfo != null ? (federationAuthProviderApi.ProviderInfo).AdditionalParams["Authority"] : string.Empty;
            if (string.IsNullOrEmpty(additionalParam1))
                throw new ApplicationException("Authentication:WsFederation:Issuer configuration is required.");
            // ISSUE: explicit non-virtual call

            string additionalParam2 = federationAuthProviderApi != null && federationAuthProviderApi.ProviderInfo != null ? (federationAuthProviderApi.ProviderInfo).AdditionalParams["MetaDataAddress"] : string.Empty;
            if (string.IsNullOrEmpty(additionalParam1))
                throw new ApplicationException("Authentication:WsFederation:MetaDataAddress configuration is required.");

            WsFederationConfigurationRetriever configurationRetriever = new WsFederationConfigurationRetriever();
            HttpDocumentRetriever documentRetriever = new HttpDocumentRetriever();
            ConfigurationManager<WsFederationConfiguration> configurationManager = new ConfigurationManager<WsFederationConfiguration>(additionalParam2, (IConfigurationRetriever<WsFederationConfiguration>)configurationRetriever, (IDocumentRetriever)documentRetriever);
            JwtSecurityToken jwtSecurityToken = await federationAuthProviderApi.ValidateToken(token, additionalParam1, (IConfigurationManager<WsFederationConfiguration>)configurationManager, new CancellationToken());
            string str1 = jwtSecurityToken.Claims.First<Claim>((Func<Claim, bool>)(c => c.Type == "name")).Value;
            string str2 = jwtSecurityToken.Claims.First<Claim>((Func<Claim, bool>)(c => c.Type == "email")).Value;
            int num1 = 32;
            string[] strArray = str1.Split(' ', (char)num1);
            return new ExternalAuthUserInfo()
            {
                Provider = "WsFederation",
                ProviderKey = jwtSecurityToken.Subject,
                Name = strArray[0],
                Surname = strArray.Length > 1 ? strArray[1] : strArray[0],
                EmailAddress = str2
            };
        }

        private async Task<JwtSecurityToken> ValidateToken(
          string token,
          string issuer,
          IConfigurationManager<WsFederationConfiguration> configurationManager,
          CancellationToken ct = default(CancellationToken))
        {
            WsFederationAuthProviderApi federationAuthProviderApi = this;
            if (string.IsNullOrEmpty(token))
                throw new ArgumentNullException(nameof(token));
            if (string.IsNullOrEmpty(issuer))
                throw new ArgumentNullException(nameof(issuer));
            ICollection<SecurityKey> signingKeys = (await configurationManager.GetConfigurationAsync(ct)).SigningKeys;
            TokenValidationParameters validationParameters = new TokenValidationParameters();
            validationParameters.ValidateIssuer = true;
            validationParameters.ValidIssuer = issuer;
            validationParameters.ValidateIssuerSigningKey = true;
            validationParameters.IssuerSigningKeys = (IEnumerable<SecurityKey>)signingKeys;
            validationParameters.ValidateLifetime = true;
            validationParameters.ClockSkew = TimeSpan.FromMinutes(5.0);
            validationParameters.ValidateAudience = false;
            SecurityToken securityToken;
            ClaimsPrincipal claimsPrincipal = ((SecurityTokenHandler)new JwtSecurityTokenHandler()).ValidateToken(token, validationParameters, out securityToken);
            // ISSUE: explicit non-virtual call
            if (federationAuthProviderApi != null && federationAuthProviderApi.ProviderInfo != null && (federationAuthProviderApi.ProviderInfo).ClientId != claimsPrincipal.Claims.First<Claim>((Func<Claim, bool>)(c => c.Type == "aud")).Value)
                throw new ApplicationException("ClientId couldn't verified.");
            return (JwtSecurityToken)securityToken;
        }
    }
}