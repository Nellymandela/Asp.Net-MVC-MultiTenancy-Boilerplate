using App.Core.Common;
using App.Web.App_Start;
using App.Web.Authorizers;
using Microsoft.AspNet.Identity;
using Ninject;
using System;
using System.Web.Mvc;
using App.Infrastructure.Factories.EntityFramework;
using App.Core.Models.Inputs;
using App.Web.Controllers;
using App.Infrastructure;

namespace App.Web.Areas.Admin.Controllers
{
    public class AccountController : AppBaseController
    {
        public readonly IUserFactory _userService;
        public readonly ApplicationDbContext _context;
        public AccountController()
        {
            _userService = new StandardKernel(new NinjectConfig()).Get<IUserFactory>();
            _context = new StandardKernel(new NinjectConfig()).Get<ApplicationDbContext>();
        }

        //[PermissionAuthorize]
        public ActionResult Index()
        {
            //var user = _userService.GetById(Convert.ToInt32(User.Identity.GetUserId()));
            //var tenant = _tenantService.GetById(user.TenantID);
            //ViewBag.dashboardLogo = Common.ConvertByteToBase64String(tenant.Logo);
            return View();
        }

        [PermissionAuthorize]
        public ActionResult ChangePassword()
        {
            return View(new ChangePasswordViewModel());
        }

        [PermissionAuthorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = _userService.GetById(Convert.ToInt32(User.Identity.GetUserId()));
            if (model.NewPassword.Equals(model.ConfirmNewPassword))
            {
                try
                {
                    //_userService.ChangePassword(user, model.CurrentPassword, model.NewPassword);
                    AddNotification(ExceptionType.success, "Password changed successfully");
                }
                catch (Exception x)
                {
                    TempData["error"] = x.Message;
                }
            }
            else
            {
                AddNotification(ExceptionType.error, "Both password do not match");
            }
            return View();
        }

        // GET: Admin/Account
        public ActionResult NotAuthorized(string ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }
    }
}