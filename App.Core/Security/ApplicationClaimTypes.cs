using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;

namespace App.Core.Security
{
    /// <summary>
    /// This is the claim types for this application
    /// </summary>
    public static class ApplicationClaimTypes
    {
        public static string TenancyID { get; } = $" { typeof(ApplicationClaimTypes).FullName}.{nameof(TenancyID)}";
        public static string TenancyName { get; } = $" { typeof(ApplicationClaimTypes).FullName}.{nameof(TenancyName)}";
        public static string TenancyCode { get; } = $" { typeof(ApplicationClaimTypes).FullName}.{nameof(TenancyCode)}";
        public static string TenancyLogo { get; } = $" { typeof(ApplicationClaimTypes).FullName}.{nameof(TenancyLogo)}";
        public static string TenancyFavicon { get; } = $" { typeof(ApplicationClaimTypes).FullName}.{nameof(TenancyFavicon)}";

        public static string GetClaimValue(IIdentity identity, string type)
        {
            if (identity == null)
            {
                return string.Empty;
            }
            return ((ClaimsIdentity)identity)?.FindFirst(x => x.Type == type)?.Value ?? string.Empty;
        }
    }
}
