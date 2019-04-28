// Decompiled with JetBrains decompiler
// Type: Abp.AspNetZeroCore.Web.Authentication.JwtBearer.JwtTokenMiddleware
// Assembly: Abp.AspNetZeroCore.Web, Version=1.2.2.0, Culture=neutral, PublicKeyToken=null
// MVID: E82D7A41-87A0-49FB-853C-F00596815594
// Assembly location: C:\Users\fuliang\.nuget\packages\abp.aspnetzerocore.web\1.2.2\lib\netcoreapp2.2\Abp.AspNetZeroCore.Web.dll

using System;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Abp.AspNetZeroCore.Web.Authentication.JwtBearer
{
    public static class JwtTokenMiddleware
    {
        public static IApplicationBuilder UseJwtTokenMiddleware(
          this IApplicationBuilder app,
          string schema = "Bearer")
        {
            return UseExtensions.Use(app, (Func<HttpContext, Func<Task>, Task>)(async (ctx, next) =>
           {
               IIdentity identity = ctx.User.Identity;
               if ((identity != null ? (!identity.IsAuthenticated ? 1 : 0) : 1) != 0)
               {
                   AuthenticateResult authenticateResult = await AuthenticationHttpContextExtensions.AuthenticateAsync(ctx, schema);
                   if (authenticateResult.Succeeded && authenticateResult.Principal != null)
                       ctx.User = authenticateResult.Principal;
               }
               await next();
           }));
        }
    }
}