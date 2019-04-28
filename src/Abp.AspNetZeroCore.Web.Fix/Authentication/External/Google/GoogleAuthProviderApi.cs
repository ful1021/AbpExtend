// Decompiled with JetBrains decompiler
// Type: Abp.AspNetZeroCore.Web.Authentication.External.Google.GoogleAuthProviderApi
// Assembly: Abp.AspNetZeroCore.Web, Version=1.2.2.0, Culture=neutral, PublicKeyToken=null
// MVID: E82D7A41-87A0-49FB-853C-F00596815594
// Assembly location: C:\Users\fuliang\.nuget\packages\abp.aspnetzerocore.web\1.2.2\lib\netcoreapp2.2\Abp.AspNetZeroCore.Web.dll

using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Abp.AspNetZeroCore.Web.Authentication.External.Google
{
  public class GoogleAuthProviderApi : ExternalAuthProviderApiBase
  {
    public const string Name = "Google";

    public override async Task<ExternalAuthUserInfo> GetUserInfo(
      string accessCode)
    {
      string additionalParam = this.ProviderInfo.AdditionalParams["UserInfoEndpoint"];
      if (string.IsNullOrEmpty(additionalParam))
        throw new ApplicationException("Authentication:Google:UserInfoEndpoint configuration is required.");
      ExternalAuthUserInfo externalAuthUserInfo;
      using (HttpClient client = new HttpClient())
      {
        client.DefaultRequestHeaders.UserAgent.ParseAdd("Microsoft ASP.NET Core OAuth middleware");
        client.DefaultRequestHeaders.Accept.ParseAdd("application/json");
        client.Timeout = TimeSpan.FromSeconds(30.0);
        client.MaxResponseContentBufferSize = 10485760L;
        HttpResponseMessage httpResponseMessage = await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, additionalParam) { Headers = { Authorization = new AuthenticationHeaderValue("Bearer", accessCode) } });
        httpResponseMessage.EnsureSuccessStatusCode();
        JObject user = JObject.Parse(await httpResponseMessage.Content.ReadAsStringAsync());
        externalAuthUserInfo = new ExternalAuthUserInfo()
        {
          Name = GoogleHelper.GetName(user),
          EmailAddress = GoogleHelper.GetEmail(user),
          Surname = GoogleHelper.GetFamilyName(user),
          ProviderKey = GoogleHelper.GetId(user),
          Provider = "Google"
        };
      }
      return externalAuthUserInfo;
    }
  }
}
