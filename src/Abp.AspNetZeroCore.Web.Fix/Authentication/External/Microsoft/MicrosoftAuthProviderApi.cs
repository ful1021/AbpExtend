// Decompiled with JetBrains decompiler
// Type: Abp.AspNetZeroCore.Web.Authentication.External.Microsoft.MicrosoftAuthProviderApi
// Assembly: Abp.AspNetZeroCore.Web, Version=1.2.2.0, Culture=neutral, PublicKeyToken=null
// MVID: E82D7A41-87A0-49FB-853C-F00596815594
// Assembly location: C:\Users\fuliang\.nuget\packages\abp.aspnetzerocore.web\1.2.2\lib\netcoreapp2.2\Abp.AspNetZeroCore.Web.dll

using Microsoft.AspNetCore.Authentication.MicrosoftAccount;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Abp.AspNetZeroCore.Web.Authentication.External.Microsoft
{
  public class MicrosoftAuthProviderApi : ExternalAuthProviderApiBase
  {
    public const string Name = "Microsoft";

    public override async Task<ExternalAuthUserInfo> GetUserInfo(
      string accessCode)
    {
      ExternalAuthUserInfo externalAuthUserInfo;
      using (HttpClient client = new HttpClient())
      {
        client.DefaultRequestHeaders.UserAgent.ParseAdd("Microsoft ASP.NET Core OAuth middleware");
        client.DefaultRequestHeaders.Accept.ParseAdd("application/json");
        client.Timeout = TimeSpan.FromSeconds(30.0);
        client.MaxResponseContentBufferSize = 10485760L;
        HttpResponseMessage httpResponseMessage = await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, (string) MicrosoftAccountDefaults.UserInformationEndpoint) { Headers = { Authorization = new AuthenticationHeaderValue("Bearer", accessCode) } });
        httpResponseMessage.EnsureSuccessStatusCode();
        JObject user = JObject.Parse(await httpResponseMessage.Content.ReadAsStringAsync());
        externalAuthUserInfo = new ExternalAuthUserInfo()
        {
          Name = MicrosoftAccountHelper.GetDisplayName(user),
          EmailAddress = MicrosoftAccountHelper.GetEmail(user),
          Surname = MicrosoftAccountHelper.GetSurname(user),
          Provider = "Microsoft",
          ProviderKey = MicrosoftAccountHelper.GetId(user)
        };
      }
      return externalAuthUserInfo;
    }
  }
}
