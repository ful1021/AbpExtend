// Decompiled with JetBrains decompiler
// Type: Abp.AspNetZeroCore.Web.Authentication.External.RemoteAuthenticationContextExtensions
// Assembly: Abp.AspNetZeroCore.Web, Version=1.2.2.0, Culture=neutral, PublicKeyToken=null
// MVID: E82D7A41-87A0-49FB-853C-F00596815594
// Assembly location: C:\Users\fuliang\.nuget\packages\abp.aspnetzerocore.web\1.2.2\lib\netcoreapp2.2\Abp.AspNetZeroCore.Web.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace Abp.AspNetZeroCore.Web.Authentication.External
{
    public static class RemoteAuthenticationContextExtensions
    {
        public static void AddMappedClaims<TOptions>(
          this RemoteAuthenticationContext<TOptions> context,
          List<JsonClaimMap> mappings)
          where TOptions : RemoteAuthenticationOptions
        {
            if (!mappings.Any<JsonClaimMap>())
                return;
            foreach (JsonClaimMap mapping in mappings)
            {
                JsonClaimMap claimMapping = mapping;
                Claim claim = context.Principal.Claims.ToList<Claim>().FirstOrDefault<Claim>((Func<Claim, bool>)(c => c.Type == claimMapping.Key));
                if (claim != null)
                    context.Principal.AddIdentity(new ClaimsIdentity((IEnumerable<Claim>)new List<Claim>()
          {
            new Claim(claimMapping.Claim, claim.Value)
          }));
            }
        }
    }
}