// Decompiled with JetBrains decompiler
// Type: Abp.AspNetZeroCore.Web.Authentication.External.Facebook.FacebookHelper
// Assembly: Abp.AspNetZeroCore.Web, Version=1.1.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 80F05ACE-4DE3-4EA6-96CC-9307E608EE8F
// Assembly location: C:\Users\fuliang\.nuget\packages\abp.aspnetzerocore.web\1.1.8\lib\netcoreapp2.1\Abp.AspNetZeroCore.Web.dll

using System;
using Newtonsoft.Json.Linq;

namespace Abp.AspNetZeroCore.Web.Authentication.External.Facebook
{
    public static class FacebookHelper
    {
        public static string GetId(JObject user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            return (string)((JToken)user).Value<string>((object)"id");
        }

        public static string GetAgeRangeMin(JObject user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            return FacebookHelper.TryGetValue(user, "age_range", "min");
        }

        public static string GetAgeRangeMax(JObject user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            return FacebookHelper.TryGetValue(user, "age_range", "max");
        }

        public static string GetBirthday(JObject user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            return (string)((JToken)user).Value<string>((object)"birthday");
        }

        public static string GetEmail(JObject user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            return (string)((JToken)user).Value<string>((object)"email");
        }

        public static string GetFirstName(JObject user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            return (string)((JToken)user).Value<string>((object)"first_name");
        }

        public static string GetGender(JObject user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            return (string)((JToken)user).Value<string>((object)"gender");
        }

        public static string GetLastName(JObject user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            return (string)((JToken)user).Value<string>((object)"last_name");
        }

        public static string GetLink(JObject user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            return (string)((JToken)user).Value<string>((object)"link");
        }

        public static string GetLocation(JObject user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            return FacebookHelper.TryGetValue(user, "location", "name");
        }

        public static string GetLocale(JObject user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            return (string)((JToken)user).Value<string>((object)"locale");
        }

        public static string GetMiddleName(JObject user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            return (string)((JToken)user).Value<string>((object)"middle_name");
        }

        public static string GetName(JObject user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            return (string)((JToken)user).Value<string>((object)"name");
        }

        public static string GetTimeZone(JObject user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            return (string)((JToken)user).Value<string>((object)"timezone");
        }

        private static string TryGetValue(JObject user, string propertyName, string subProperty)
        {
            JToken jtoken;
            if (user.TryGetValue(propertyName, out jtoken))
            {
                JObject jobject = JObject.Parse(((object)jtoken).ToString());
                if (jobject != null && jobject.TryGetValue(subProperty, out jtoken))
                    return ((object)jtoken).ToString();
            }
            return (string)null;
        }
    }
}