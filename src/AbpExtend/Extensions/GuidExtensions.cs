using System;

namespace Abp.Extensions
{
    public static class GuidExtensions
    {
        public static bool IsNullOrEmpty(this Guid? id)
        {
            return id.GetValueOrDefault() == Guid.Empty;
        }

    }
}