using System;

namespace App.Web.Core.Security.Auth
{
    public class ApplicationClaimTypes
    {
        public static string TenancyID { get; } = $" { typeof(ApplicationClaimTypes).FullName}.{nameof(TenancyID)}";
        public static string TenancyName { get; } = $" { typeof(ApplicationClaimTypes).FullName}.{nameof(TenancyName)}";
        public static string TenancyCode { get; } = $" { typeof(ApplicationClaimTypes).FullName}.{nameof(TenancyCode)}";
        public static string TenancyLogo { get; } = $" { typeof(ApplicationClaimTypes).FullName}.{nameof(TenancyLogo)}";
        public static string TenancyFavicon { get; } = $" { typeof(ApplicationClaimTypes).FullName}.{nameof(TenancyFavicon)}";
    }
}