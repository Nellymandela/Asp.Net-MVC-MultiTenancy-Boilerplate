using App.Core.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace App.Web.Authorizers
{
    public sealed class PermissionAuthorize : AuthorizeAttribute
    {
        private string claimType { get; set; }
        public PermissionAuthorize(string _claimType)
        {
            claimType = _claimType;
        }

        public PermissionAuthorize()
        {

        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                if (claimType == null || ((ClaimsIdentity)filterContext.HttpContext.User.Identity).HasClaim(x => x.Type.Equals(claimType)))
                {
                    base.OnAuthorization(filterContext);
                }
                else
                {
                    HandleUnauthorizedRequest(filterContext);
                }
            }
            else
            {
                string tenantCode = filterContext.HttpContext.Request.Cookies["Tenant"]?.Value;
                filterContext.Result = new RedirectResult($"/Account/Login?Tenant={ tenantCode }&ReturnUrl={ filterContext.HttpContext.Request.RawUrl }");
            }
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            string tenantCode = ((ClaimsIdentity)filterContext.HttpContext.User.Identity).FindFirst(x => x.Type == ApplicationClaimTypes.TenancyCode)?.Value;
            tenantCode = tenantCode ?? filterContext.HttpContext.Request.Cookies["Tenant"]?.Value;
            filterContext.Result = new RedirectResult($"/Admin/Error/UnAuthorized?ReturnUrl={ filterContext.HttpContext.Request.RawUrl }");
        }
    }
}
