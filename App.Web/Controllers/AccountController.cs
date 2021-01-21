using App.Core.Common;
using App.Web.App_Start;
using App.Web.Factories;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Ninject;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using App.Infrastructure.Factories.EntityFramework;
using App.Core.Models.Entities;
using App.Core.Models.Inputs;
using App.Core.Security;

namespace App.Web.Controllers
{
    public class AccountController : AppBaseController
    {
        private readonly IAuthFactory _authService;

        public AccountController()
        {
            _authService = new StandardKernel(new NinjectConfig()).Get<IAuthFactory>();
        }

        public ActionResult Login(string Tenant, string ReturnUrl)
        {
            if (string.IsNullOrEmpty(Tenant))
            {
                Tenant = TenantStore.ApplicationDefaultTenants.FirstOrDefault()?.Code ?? null;
            }
            TenantViewModel tenant = _tenantService.GetByCode(Tenant);
            if (tenant != null)
            {
                ViewBag.TenantName = tenant?.Name;
                ViewBag.TenantCode = tenant?.Code;
                ViewBag.logoBase64 = Common.ConvertByteToBase64String(tenant.Logo);
                ViewBag.TenantCopyright = tenant.Copyright;
                //ViewBag.faviconBase64 = Common.ConvertByteToBase64String(tenant.Favicon);
            }
            ReturnUrl = ReturnUrl ?? Url.Content(Url.Action("Index", "Account", new { area = "Admin" }));
            var model = new LoginViewModel() { ReturnUrl = ReturnUrl, Tenant = Tenant };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = _authService.SignIn(model, false); //This is just a login match not an authentication
            switch (result)
            {
                case SignInResult.Success:
                    #region Get User Informations
                    var roles = _authService.GetRoles(_authService.User.ID);
                    var permissions = _authService.GetPermissions(_authService.User.ID);
                    var claims = permissions.Distinct()
                        .Select(s => new Claim(s.Claim.Type, s.Claim.Value)).ToList();
                    #endregion

                    #region Add User Identification Claims
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, _authService.User.ID.ToString()));
                    claims.Add(new Claim(ClaimTypes.Name, $"{_authService.User?.LastName ?? string.Empty } {_authService.User?.FirstName ?? string.Empty } {_authService.User?.OtherName ?? string.Empty }"));
                    claims.Add(new Claim(ClaimTypes.GivenName, _authService.User.UserName));
                    claims.Add(new Claim(ClaimTypes.Email, _authService.User.Email));
                    claims.Add(new Claim(ApplicationClaimTypes.TenancyID, _authService.User.TenantID.ToString()));
                    claims.Add(new Claim(ApplicationClaimTypes.TenancyName,_authService.User.Tenant.Name.ToString()));
                    claims.Add(new Claim(ApplicationClaimTypes.TenancyCode, _authService.User.Tenant.Code.ToString()));
                    if (_authService.User.Tenant.FaviconUrl != null)
                    {
                        claims.Add(new Claim(ApplicationClaimTypes.TenancyFavicon, Path.GetFileName(_authService.User.Tenant.FaviconUrl)));
                    }
                    //claims.AddRange(roles.Select(s => new Claim(ClaimTypes.Role, s.Name)));
                    var persistentCookie = new HttpCookie("Tenant", _authService.User.Tenant.Code.ToString())
                    {
                        Expires = DateTime.MaxValue
                    };
                    Response.SetCookie(persistentCookie);
                    #endregion

                    #region User Cookie Authentication
                    var authProperties = new AuthenticationProperties
                    {
                        AllowRefresh = true,
                        // Refreshing the authentication session should be allowed.

                        ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1),
                        // The time at which the authentication ticket expires. A 
                        // value set here overrides the ExpireTimeSpan option of 
                        // CookieAuthenticationOptions set with AddCookie.

                        IsPersistent = true,
                        // Whether the authentication session is persisted across 
                        // multiple requests. When used with cookies, controls
                        // whether the cookie's lifetime is absolute (matching the
                        // lifetime of the authentication ticket) or session-based.

                        IssuedUtc = DateTimeOffset.Now,
                        // The time at which the authentication ticket was issued.

                        //RedirectUri = <string>
                        // The full path or absolute URI to be used as an http 
                        // redirect response value.
                    };
                    HttpContext.GetOwinContext().Authentication.SignIn(authProperties,
                        new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie));
                    #endregion

                    return RedirectToLocal(model.ReturnUrl);
                case SignInResult.LockedOut:
                    AddNotification(ExceptionType.error, $"Account locked. Contact support.");
                    break;
                case SignInResult.TwoFactorAuthRequired:
                    AddNotification(ExceptionType.error, $"Extra authentication required. Contact support.");
                    break;
                case SignInResult.Failed:
                    AddNotification(ExceptionType.error, $"Invalid login. Try again!");
                    break;
                case SignInResult.Inactive:
                    AddNotification(ExceptionType.error, $"Account Inactive. Contact support.");
                    break;
            }
            var tenant = _tenantService.GetByCode(model.Tenant);
            ViewBag.TenantName = tenant?.Name;
            ViewBag.logoBase64 = Common.ConvertByteToBase64String(tenant?.Logo ?? null);
            ViewBag.faviconBase64 = Common.ConvertByteToBase64String(tenant?.Favicon ?? null);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout(string T)
        {
            HttpContext.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Login", "Account", routeValues: new { Tenant = T });
        }

        [NonAction]
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Account", new { area = "Admin" });
        }
    }
}