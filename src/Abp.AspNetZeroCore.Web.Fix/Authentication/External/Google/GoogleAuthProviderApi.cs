// Decompiled with JetBrains decompiler
// Type: Abp.AspNetZeroCore.Web.Authentication.External.Google.GoogleAuthProviderApi
// Assembly: Abp.AspNetZeroCore.Web, Version=1.1.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 80F05ACE-4DE3-4EA6-96CC-9307E608EE8F
// Assembly location: C:\Users\fuliang\.nuget\packages\abp.aspnetzerocore.web\1.1.8\lib\netcoreapp2.1\Abp.AspNetZeroCore.Web.dll

using Microsoft.AspNetCore.Authentication.Google;
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

    public override async Task<ExternalAuthUserInfo> GetUserInfo(string accessCode)
    {
      ExternalAuthUserInfo externalAuthUserInfo;
      using (HttpClient client = new HttpClient())
      {
        client.DefaultRequestHeaders.UserAgent.ParseAdd("Microsoft ASP.NET Core OAuth middleware");
        client.DefaultRequestHeaders.Accept.ParseAdd("application/json");
        client.Timeout = TimeSpan.FromSeconds(30.0);
        client.MaxResponseContentBufferSize = 10485760L;
        HttpResponseMessage httpResponseMessage = await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, GoogleDefaults.UserInformationEndpoint)
        {
          Headers = {
            Authorization = new AuthenticationHeaderValue("Bearer", accessCode)
          }
        });
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
